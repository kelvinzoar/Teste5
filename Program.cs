using MediatR;
using Questao5.Infrastructure.Sqlite;
using Questao5.Services;
using Questao5.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configuração do SQLite
builder.Services.AddSingleton(new DatabaseConfig
{
    Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite")
});
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Registrando os serviços e repositórios
builder.Services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Contas Correntes",
        Version = "v1",
        Description = "API para movimentação e consulta de contas correntes"
    });
});

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Contas Correntes v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Inicializar o banco de dados SQLite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>()?.Setup();
#pragma warning restore CS8602

app.Run();
