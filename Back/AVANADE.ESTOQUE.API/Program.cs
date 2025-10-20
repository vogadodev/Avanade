using AVANADE.ESTOQUE.API.Data; 
using AVANADE.ESTOQUE.API.InjecaoDependencias;
using AVANADE.INFRASTRUCTURE;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
const string DbConnectionName = "EstoqueDbConnection";

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando servi�os comum
builder.Services.AddInfraServicosComum(configuration).           
             AddDbContextConfiguration<EstoqueDbContext>(configuration, DbConnectionName);

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.AddCustomMiddlewares();

app.MapControllers();

app.Run();