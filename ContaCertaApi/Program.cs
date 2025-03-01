using ContaCerta.Api.Configs;
using ContaCerta.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterContexts(builder.Configuration.GetConnectionString("conexaoPadrao"));
builder.Services.RegisterServices();
builder.Services.ResolveInterfaces();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); // Ignora logs de consulta
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
