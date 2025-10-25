using AVANADE.INFRASTRUCTURE;
using AVANADE.VENDAS.API.Data;
using AVANADE.VENDAS.API.InjecaoDependencias;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
const string DbConnectionName = "VendasDbConnection";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Adicionando servi�os comum
builder.Services.AddInfraServicosComum(configuration).
                 AddDbContextConfiguration<VendaDbContext>(configuration, DbConnectionName);

//Adicionando inje��o de depend�ncia dos reposit�rios e servi�os
builder.Services
    .AddRepositories()
    .AddServices();

//Dica Scott Sauber, para n�o expor o server header.
builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);

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
