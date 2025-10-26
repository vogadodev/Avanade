using AVANADE.ESTOQUE.API.Data;
using AVANADE.ESTOQUE.API.Services.CategoriaServices;
using AVANADE.ESTOQUE.API.Services.FronecedorServices;
using AVANADE.ESTOQUE.API.Services.MarcaServices;
using AVANADE.ESTOQUE.API.Services.ProdutoServices;
using AVANADE.ESTOQUE.API.Services.RabbitMQServices;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.InjecaoDependencias
{
    public static class AddDependenciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Produto
            services.AddScoped(typeof(GravarProdutoService));
            services.AddScoped(typeof(ValidarProdutoService));
            services.AddScoped(typeof(ObterProdutoService));
            services.AddScoped(typeof(ExcluirProdutoService));

            //Fornecedor
            services.AddScoped(typeof(GravarFornecedorService));
            services.AddScoped(typeof(ValidarFornecedorService));
            services.AddScoped(typeof(ObterFornecedorService));
            services.AddScoped(typeof(ExcluirFornecedorService));

            //Marca
            services.AddScoped(typeof(GravarMarcaService));
            services.AddScoped(typeof(ValidarMarcaService));
            services.AddScoped(typeof(ObterMarcaService));
            services.AddScoped(typeof(ExcluirMarcaService));

            //Categoria
            services.AddScoped(typeof(GravarCategoriaService));
            services.AddScoped(typeof(ValidarCategoriaService));
            services.AddScoped(typeof(ObterCategoriaService));
            services.AddScoped(typeof(ExcluirCategoriaService));

            //Mensageria RabbitMQ
            services.AddHostedService<ProcessarPedidoConsumer>();

            return services;

        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ProdutoRepository<EstoqueDbContext>));
            services.AddScoped(typeof(FornecedorRepository<EstoqueDbContext>));
            services.AddScoped(typeof(MarcaRepository<EstoqueDbContext>));
            services.AddScoped(typeof(CategoriaRepository<EstoqueDbContext>));
            services.AddScoped(typeof(AvaliacaoRepository<EstoqueDbContext>));
            return services;
        }
    }
}
