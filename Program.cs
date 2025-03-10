using ApiPostgress.Modelo;
using Microsoft.EntityFrameworkCore;
//using ApiPostgress.Models; // Cambia "ApiPostgress" por el nombre de tu proyecto

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar el DbContext con PostgreSQL
builder.Services.AddDbContext<ApiplatosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar el servicio HttpClient para hacer solicitudes HTTP externas
builder.Services.AddHttpClient("WeatherService", client =>
{
    client.BaseAddress = new Uri("https://api.weatherapi.com/v1/"); // URL base del servicio externo
    client.DefaultRequestHeaders.Add("Accept", "application/json"); // Configurar headers
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();