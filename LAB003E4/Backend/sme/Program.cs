using Serilog;
using System.Reflection;
using Microsoft.OpenApi.Models;
using sme.src.Middlewares;
using sme.src.Data;
using sme.src.Services;
using DotNetEnv;

Env.Load();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();


// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Lab003 API", 
        Version = "v1",
        Description = "O endpoint da requisição não precisa estar em maisculo."
    });
});

// Adicionando a conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmailSender, MailerService>();
var app = builder.Build();



// Pipeline de requisição
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var swaggerUrl = "http://localhost:3030/swagger/index.html";
    Log.Information("Swagger UI available at {SwaggerUrl}", swaggerUrl);
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

Log.Information("Server starting and listening on {Url}", "http://127.0.0.1:3030");
app.Run("http://127.0.0.1:3030");