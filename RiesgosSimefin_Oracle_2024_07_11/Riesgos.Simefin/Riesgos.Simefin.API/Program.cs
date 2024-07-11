using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Application.Interfaces.Portfolio;
using Riesgos.Simefin.Application.Interfaces.User;
using Riesgos.Simefin.Application.UseCases;
using Riesgos.Simefin.Domain.Entities;
using Riesgos.Simefin.Infrastructure.Oracle.Repositories;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

////SQL Server
//builder.Services.AddDbContext<SimefinDBContext>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
//});

//Oracle
//builder.Services.AddDbContext<SimefinDBContext>(option =>
//{
//    option.UseOracle(builder.Configuration.GetConnectionString("OracleConexion"));
//});

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresar Bearer [space] tuToken \r\n\r\n " +
                      "Ejemplo: Bearer 123456abcder",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                },
                Scheme = "oauth2",
                Name="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Configuración de clase estática para obtener la conexión a BD
Riesgos.Simefin.Infrastructure.Oracle.Helpers.OracleConnectionHelper.Initialize(configuration);

//Token
string key = string.Empty;
string environmentVariable = Environment.GetEnvironmentVariable("SIVARMER_API_SIMEFIN_DEV")!;
if (!string.IsNullOrEmpty(environmentVariable))
{
    var data = JsonConvert.DeserializeObject<EnvironmentVariables>(environmentVariable!);
    key = data!.Secret;
}

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
            ValidateIssuer = false, //No interesa validar quién solicita, por usar credenciales
            ValidateAudience = false, //No se necesita saber de donde está solicitando el usuario
            ValidateLifetime = true, //Tiempo de vida del token
            ClockSkew = TimeSpan.Zero //No debe existir expiración del tiempo de vida del token
        };
    });

// Add services to the container.
builder.Services.AddScoped<IAuthorizationUseCase, AuthorizationUseCase>();
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddScoped<IUserUseCase, UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPortfolioUseCase, PortfolioUseCase>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

//builder.Services.AddAutoMapper(builder => builder.AddProfile(MappingProfile));
//builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
//builder.Services.AddAutoMapper(System.Reflection.Assembly.GetEntryAssembly());
//builder.Services.AddAutoMapper(System.Reflection.Assembly.GetAssembly(typeof(AutoMappingProfile)));
//builder.Services.AddAutoMapper(GetType().Assembly, typeof(AutoMappingProfile).Assembly);
//builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
//builder.Services.AddAutoMapper(new[] { typeof(AutoMappingProfile).Assembly });
//builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<IMapper, Mapper>();
//builder.Services.AddSingleton(AutoMappingProfile.Initialize());
//builder.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
//builder.Services.AddSingleton<IMapper>(AutoMappingProfile);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Middleware de manejo de código de estado 401
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.HttpContext.Response.Headers["Content-Type"] = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        await context.HttpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(
            new
            {
                IsExitoso = false,
                statusCode = 401,
                Mensaje = "Se ha denegado la autorización para esta solicitud. Favor de utilizar un token válido."
            }
        ));

    }

    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        context.HttpContext.Response.Headers["Content-Type"] = "application/json";
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        await context.HttpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(
            new
            {
                IsExitoso = false,
                statusCode = 403,
                Mensaje = "No cuentas con los permisos suficientes para realizar esta solicitud."
            }
        ));
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
