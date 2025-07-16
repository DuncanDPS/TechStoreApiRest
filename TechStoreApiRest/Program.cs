using Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// habilitar el registro de controladores
builder.Services.AddControllers();

// habilitar el registro de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar la cadena de conexión a la base de datos
builder.Services.AddDbContext<AppContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el servicio de productos
builder.Services.AddScoped<Servicios.IServicios.IProductoService, Servicios.ProductoService>();



var app = builder.Build();

// Habilita Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita la autorización
app.UseAuthorization();

// Mapea los controladores
app.MapControllers();

app.Run();
