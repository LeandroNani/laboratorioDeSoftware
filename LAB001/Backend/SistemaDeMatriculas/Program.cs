using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();

// Adiciona suporte ao Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger(); // Gera o JSON do Swagger
    //app.UseSwaggerUI(); // Interface interativa do Swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Mapeia os endpoints dos controllers

app.Run();
