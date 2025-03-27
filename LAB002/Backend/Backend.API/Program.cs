using Microsoft.EntityFrameworkCore;
using Backend.API.Data; // o namespace do AppDbContext

var builder = WebApplication.CreateBuilder(args);

// Ler a string de conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar o contexto do Entity Framework usando Postgres:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Adicionar serviços de controllers
builder.Services.AddControllers();

// Adicionar Swagger, se quiser
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construir a aplicação
var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
