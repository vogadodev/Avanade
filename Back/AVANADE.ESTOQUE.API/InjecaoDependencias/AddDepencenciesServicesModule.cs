using AVANADE.ESTOQUE.API.Data;
using AVANADE.ESTOQUE.API.Services.ProdutoServices;
using AVANADE.MODULOS.Modulos.AVANADE_ESTOQUE.Repositories;

namespace AVANADE.ESTOQUE.API.InjecaoDependencias
{
    public  static class AddDepencenciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Produto
            services.AddScoped(typeof(GravarProdutoService));
            services.AddScoped(typeof(ValidarProdutoService));
            services.AddScoped(typeof(ObterProdutoService));  
            services.AddScoped(typeof(ExcluirProdutoService));              
           
            //Fornecedor

            //Marca

            //Categoria

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
