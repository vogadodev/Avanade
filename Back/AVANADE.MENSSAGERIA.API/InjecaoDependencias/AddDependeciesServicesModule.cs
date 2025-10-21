using AVANADE.Menssageria.API.Data;
// Adicione aqui outros usings de services e repositories

namespace AVANADE.Menssageria.API.InjecaoDependencias
{
    public static class AddDependeciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Adicione os serviços específicos desta API aqui
            // Ex: services.AddScoped<ILoginService, LoginService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            // Adicione os repositórios específicos desta API aqui
            // Ex: repositories.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return repositories;
        }
    }
}