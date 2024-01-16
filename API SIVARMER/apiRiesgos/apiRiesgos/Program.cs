
using apiRiesgos.Servicios;
using DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServicio_API, Servicio_API>();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


builder.Configuration.AddUserSecrets<Program>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

if (app.Environment.IsProduction() || app.Environment.IsStaging())
{
    app.UseExceptionHandler("/Error/index.html");
}
// Configure the HTTP request pipeline.

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");

    //c.inj
});

app.Use(async (ctx, next) =>
    {
        await next();
        if (ctx.Response.StatusCode == 204)
        {
            ctx.Response.ContentLength = 0;
        }
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
