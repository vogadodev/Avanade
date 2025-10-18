using AVANADE.GATEWAY.API.Data;
using AVANADE.INFRASTRUCTURE;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona o arquivo ocelot.json à configuração
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//Adiciona os serviços do Ocelot ao contêiner de DI
builder.Services.AddOcelot(builder.Configuration);
//Adicionando serviços comum
builder.Services.AddInfraServicosComum(configuration).
                 AddDbContextConfiguration<GaterwayDbContext>(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.MapControllers();

app.Run();
