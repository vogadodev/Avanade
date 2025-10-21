using AVANADE.Menssageria.API.Data;
using AVANADE.Menssageria.API.InjecaoDependencias;
using AVANADE.INFRASTRUCTURE;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
const string DbConnectionName = "MenssageriaDbConnection";

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adicionando serviços comum
builder.Services.AddInfraServicosComum(configuration)
                .AddDbContextConfiguration<MenssageriaDbContext>(configuration, DbConnectionName);

//Adicionando injeção de dependência dos repositórios e serviços
builder.Services
    .AddRepositories()
    .AddServices();

//Dica Scott Sauber, para não expor o server header.
builder.WebHost.UseKestrel(opt => opt.AddServerHeader = false);

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.MapControllers();

app.Run();