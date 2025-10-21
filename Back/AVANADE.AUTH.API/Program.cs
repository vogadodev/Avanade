using AVANADE.AUTH.API.Data;
using AVANADE.AUTH.API.InjecaoDependencias;
using AVANADE.INFRASTRUCTURE;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
const string DbConnectionName = "AuthDbConnection";
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando serviços comum
builder.Services.AddInfraServicosComum(configuration).
                 AddDbContextConfiguration<AuthDbContext>(configuration, DbConnectionName);

//Adicionando injeção de dependência dos repositórios e serviços
builder.Services
    .AddRepositories()
    .AddServices();

//Dica Scott Sauber, para não expor o server header.
builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.MapControllers();

app.Run();

