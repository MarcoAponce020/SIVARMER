using InterfazRiesgosSimefin_API;
using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Repository;
using InterfazRiesgosSimefin_API.Repository.IRepository;
using InterfazRiesgosSimefin_API.Services.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false, //No interesa validar quién solicita, por usar credenciales
            ValidateAudience = false, //No se necesita saber de donde está solicitando el usuario
            ValidateLifetime = true, //Tiempo de vida del token
            ClockSkew = TimeSpan.Zero //No debe existir expiración del tiempo de vida del token
        };
    });


builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<UsuarioAplicacion, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IPortafolioRepository, PortafolioRepository>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

var app = builder.Build();

/* Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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
                Mensaje = "Se ha denegado la autorización para esta solicitud, , Favor de utilizar un Token Valido"
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
                Mensaje = "No cuentas con los permisos suficientes para realizar esta solicitud"
            }
        ));

    }
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
