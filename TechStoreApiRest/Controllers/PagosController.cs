using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechStoreApiRest.Paypal;

namespace TechStoreApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PaypalSettings _paypal;

        public PagosController(IOptions<PaypalSettings> paypal)
        {
            _paypal = paypal.Value;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearPago([FromBody] decimal monto)
        {
            using var client = new HttpClient();
            var authToken = Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes($"{_paypal.ClientID}:{_paypal.Secret}")
            );

            // 1. Obtener token de acceso
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
            var tokenResponse = await client.PostAsync(
                $"https://api.{_paypal.Mode}.paypal.com/v1/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "client_credentials" } })
            );
            var tokenData = await tokenResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            var accessToken = tokenData["access_token"].ToString();

            // 2. Crear orden de pago
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var order = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                new { amount = new { currency_code = "USD", value = monto.ToString("F2") } }
            },
                application_context = new
                {
                    return_url = "http://localhost:5000/api/pagos/confirmar",
                    cancel_url = "http://localhost:5000/api/pagos/cancelar"
                }
            };

            var createResponse = await client.PostAsJsonAsync(
                $"https://api.{_paypal.Mode}.paypal.com/v2/checkout/orders", order
            );

            var orderData = await createResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            return Ok(orderData);
        }

        [HttpPost("capturar")]
        public async Task<IActionResult> CapturarPago([FromBody] string orderId)
        {
            using var client = new HttpClient();
            var authToken = Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes($"{_paypal.ClientID}:{_paypal.Secret}")
            );

            // Obtener token de acceso
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
            var tokenResponse = await client.PostAsync(
                $"https://api.{_paypal.Mode}.paypal.com/v1/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string> { { "grant_type", "client_credentials" } })
            );
            var tokenData = await tokenResponse.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            var accessToken = tokenData["access_token"].ToString();

            // Capturar pago
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var captureResponse = await client.PostAsync(
                $"https://api.{_paypal.Mode}.paypal.com/v2/checkout/orders/{orderId}/capture", null
            );

            var captureData = await captureResponse.Content.ReadFromJsonAsync<object>();
            return Ok(captureData);
        }




        [HttpGet("confirmar")]
        public IActionResult ConfirmarPago()
        {
            return Ok("Pago confirmado");
        }

        [HttpGet("cancelar")]
        public IActionResult CancelarPago()
        {
            return Ok("Pago Cancelado");
        }
    }

}
