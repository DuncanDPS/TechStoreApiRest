using Datos;
using Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Servicios.Servicios;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// configuracion de serilog
Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning).MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning).MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning).WriteTo.Console().WriteTo.File("log/log-.txt", rollingInterval: RollingInterval.Day).Filter.ByExcluding(Serilog.Filters.Matching.FromSource("Microsoft.EntityFrameworkCore.Database.Command")).CreateLogger();

try
{
    Log.Information("Starting Web App");
    builder.Services.AddSerilog(); //establece Serilog como el loggin provider


    builder.Services.AddControllers();

    // habilitar el registro de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Configurar la cadena de conexi�n a la base de datos
    builder.Services.AddDbContext<AppContextDb>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Registrar el servicio de productos
    builder.Services.AddScoped<Servicios.IServicios.IProductoService, ProductoService>();
    // Registra el servicio de categorias
    builder.Services.AddScoped<Servicios.IServicios.ICategoriaService, CategoriaService>();
    // Registra el servicio de TokenGenerator
    builder.Services.AddScoped<Servicios.IServicios.ITokenGeneratorService, TokenGeneratorService>();
    // Registrar el servicio de Usuario
    builder.Services.AddScoped<Servicios.IServicios.IUsuarioService, UsuarioService>();
    // Registrar el servicio de Review
    builder.Services.AddScoped<Servicios.IServicios.IReviewService, ReviewService>();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
        });


    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });


    // configuracion JWT
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
        options.AddPolicy("ClientePolicy", policy => policy.RequireRole("Cliente"));

    });

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();

    // creacion del ADMIN por defecto
    app.Lifetime.ApplicationStarted.Register(async () =>
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppContextDb>();

        if (!await db.Usuarios.AnyAsync(u => u.Email == builder.Configuration["AdminCredentials:UserAdmin"]))
        {
            var admin = new Usuario
            {
                Nombre = "Admin",
                Apellidos = "Admin",
                Rol = "Admin",
                Email = builder.Configuration["AdminCredentials:UserAdmin"] ?? throw new Exception("Las credenciales del admin no estan definidas correctamente"),
                ContraseniaHash = builder.Configuration["AdminCredentials:AdminPass"] ?? throw new Exception("Las credenciales del admin no estan definidas correctamente")
            };
            admin.ContraseniaHash = BCrypt.Net.BCrypt.HashPassword(admin.ContraseniaHash);

            db.Usuarios.Add(admin);
            db.SaveChanges();
            Log.Information("Admin Creado");
        }
    });

    // Habilita Swagger solo en desarrollo
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Redirige HTTP a HTTPS
    app.UseHttpsRedirection();

    // Mapea los controladores
    app.MapControllers();

    app.Run();
}
catch(Exception exception)
{
    Log.Fatal(exception, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}




