using AVANADE.GATEWAY.API.InjecaoDependencias;
using AVANADE.INFRASTRUCTURE;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddInfraServicosComum(configuration);

//Adiciona o arquivo ocelot.json à configuração
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile(
    $"ocelot.{builder.Environment.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true
);
//Adiciona os serviços do Ocelot ao contêiner de DI
builder.Services.AddOcelot(builder.Configuration);

          

//Adicionando injeção de dependência dos repositórios e serviços
builder.Services
    .AddRepositories()
    .AddServices();

// definidos no ocelot.json (MMLib.Ocelot.Swagger)

builder.Services.AddSwaggerForOcelot(builder.Configuration);

//Dica Scott Sauber, para não expor o server header.
builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerForOcelotUI();
}
app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.MapControllers();

await app.UseOcelot();

app.Run();