using AVANADE.INFRASTRUCTURE;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Adiciona o arquivo ocelot.json � configura��o
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//Adiciona os servi�os do Ocelot ao cont�iner de DI
builder.Services.AddOcelot(builder.Configuration);

// definidos no ocelot.json (MMLib.Ocelot.Swagger)
builder.Services.AddSwaggerForOcelot(builder.Configuration);

//Adicionando servi�os comum
builder.Services.AddInfraServicosComum(configuration);
           

var app = builder.Build();

app.UseRouting();

app.UseSwaggerForOcelotUI();

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.MapControllers();

await app.UseOcelot();

app.Run();