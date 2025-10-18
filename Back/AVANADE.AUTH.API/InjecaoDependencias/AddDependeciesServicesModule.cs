using AVANADE.AUTH.API.Data;
using AVANADE.AUTH.API.Services.LoginServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Repositories;

namespace AVANADE.AUTH.API.InjecaoDependencias
{
    public static class AddDependeciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(ValidarLoginService));
            services.AddScoped(typeof(LoginServices));
            services.AddScoped(typeof(TokenService));
            services.AddScoped(typeof(RefreshTokenService));

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {           
            repositories.AddScoped(typeof(RefreshTokenRepository<AuthDbContext>));
            return repositories;
        }
    }
}
