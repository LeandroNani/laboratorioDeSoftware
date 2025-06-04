using Serilog;
using System.Reflection;
using Microsoft.OpenApi.Models;
using sme.src.Middlewares;
using sme.src.Data;
using sme.src.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sme.src.Middlewares.Exceptions;
using System.Text;

Env.Load();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new ArgumentNullException("JWT_SECRET_KEY", "Chave JWT não está configurada.");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            throw new UnauthorizedException("Falha na autenticação do token JWT");
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            throw new UnauthorizedException("Token de autorização ausente ou inválido.");
        }
    };
});

// Configuração da Autorização para cada entidade
builder.Services.AddAuthorizationBuilder().AddPolicy("EmpresaPolicy", policy =>
{
    policy.RequireRole("Empresa");
});

builder.Services.AddAuthorizationBuilder().AddPolicy("ProfessorPolicy", policy =>
{
    policy.RequireRole("Professor");
});

builder.Services.AddAuthorizationBuilder().AddPolicy("AlunoPolicy", policy =>
{
    policy.RequireRole("Aluno");
});

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