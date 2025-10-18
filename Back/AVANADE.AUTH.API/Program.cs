using AVANADE.AUTH.API.Data;
using AVANADE.AUTH.API.InjecaoDependencias;
using AVANADE.INFRASTRUCTURE;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando servi�os comum
builder.Services.AddInfraServicosComum(configuration).
                 AddDbContextConfiguration<AuthDbContext>(configuration);

//Adicionando inje��o de depend�ncia dos reposit�rios e servi�os
builder.Services
    .AddRepositories()
    .AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.AddCustomMiddlewares();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
