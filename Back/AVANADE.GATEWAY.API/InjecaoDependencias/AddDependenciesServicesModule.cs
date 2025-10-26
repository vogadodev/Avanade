using AVANADE.GATEWAY.API.Services.RabbitMQServices;
using AVANADE.MODULOS.Modulos.AVANADE_VENDAS.Repositories;

namespace AVANADE.GATEWAY.API.InjecaoDependencias
{
    public static class AddDependenciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {            
            //RabbitMQ
            services.AddHostedService<NotificacaoEmailConsumer>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
           
            return repositories;
        }
    }
}
