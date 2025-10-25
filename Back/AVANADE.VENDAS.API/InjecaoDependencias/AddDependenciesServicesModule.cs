using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories;
using AVANADE.VENDAS.API.Data;
using AVANADE.VENDAS.API.Services.RabbitMQServices;
using AVANADE.VENDAS.API.Services.VendaServices;

namespace AVANADE.VENDAS.API.InjecaoDependencias
{
    public static class AddDependenciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Usuario
            services.AddScoped(typeof(ValidarPedidoService));
            services.AddScoped(typeof(GravarPedidoService));
            services.AddScoped(typeof(StatusPedidoConsumer));            

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped(typeof(VendaRepository<VendaDbContext>));
            return repositories;
        }
    }
}
