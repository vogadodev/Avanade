using AVANADE.MODULOS.Modulos.AVANADE_USUARIO.Repositories;
using AVANADE.USUARIO.API.Data;
using AVANADE.USUARIO.API.Services.UsuarioServices;

namespace AVANADE.USUARIO.API.InjecaoDependencias
{
    public static class AddDependeciesServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Usuario
            services.AddScoped(typeof(ValidarUsuarioService));
            services.AddScoped(typeof(GravarUsuarioClienteService));
            services.AddScoped(typeof(UsuarioLoginService));

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {           
            repositories.AddScoped(typeof(UsuarioRepository<UsuarioDbContext>));
            return repositories;
        }
    }
}
