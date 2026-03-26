using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SistemaPatrimonio.Applications.Services;
using SistemaPatrimonio.Contexts;
using SistemaPatrimonio.Interfaces;
using SistemaPatrimonio.Repositories;

var builder = WebApplication.CreateBuilder(args);

// carregando . env
Env.Load();

// pegando a connection string
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

//conexao com banco
builder.Services.AddDbContext<SistemaPatrimonioContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Areas
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

//Locais
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<LocalService>();

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
