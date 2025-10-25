using AVANADE.INFRASTRUCTURE;
using AVANADE.INFRASTRUCTURE.MiddlewaresGlobais;
using AVANADE.INFRASTRUCTURE.RabbitMQServices.Interfaces;
using AVANADE.INFRASTRUCTURE.RabbitMQServices.Services;
using AVANADE.INFRASTRUCTURE.ServicesComum.AuthServices;
using AVANADE.INFRASTRUCTURE.ServicesComum.IntegracaoApiService;
using AVANADE.MODULOS.Modulos.AVANADE_AUTH.Entidades;
using AVANADE.MODULOS.Modulos.AVANADE_MENSSAGERIA.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace AVANADE.INFRASTRUCTURE
{
    public static class InfrastructureComum
    {
        public static IServiceCollection AddInfraServicosComum(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesComum(configuration);
            services.AddCorsComum(configuration);
            services.AddJsonConfigComum(configuration);
            services.AddHttpClient();
            services.AddJwtAuthentication(configuration);
            services.AddAuthorizationPolices();           

            return services;
        }
        #region INJEÇÃO DE METODOS AUTOMÁTICOS

        private static IServiceCollection AddServicesComum(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));

            services.AddHttpContextAccessor();
            services.AddScoped(typeof(ConsumirApiExternaService));
            services.AddTransient(typeof(CookiePropagationHandler));
            services.AddHttpClient<ConsumirApiExternaService>()
                        .AddHttpMessageHandler<CookiePropagationHandler>();
            services.AddSingleton<IMessageBusService, RabbitMQService>();            
            return services;

        }

        private static IServiceCollection AddJsonConfigComum(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtOptions);

            // Adiciona a configuração como um singleton para que possa ser injetada em outros lugares, se necessário
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ClockSkew = TimeSpan.Zero
                };

                // Lógica para ler o token do cookie HttpOnly
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[jwtOptions.AccessTokenCookieName];
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
       

        private static IServiceCollection AddAuthorizationPolices(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Política para usuários autenticados (qualquer um que tenha um token válido)
                options.AddPolicy(PoliciesTipoUsuario.Todos, policy =>
                    policy.RequireAuthenticatedUser());

                // Política que exige que o usuário seja do tipo "Adm"
                options.AddPolicy(PoliciesTipoUsuario.ApenasAdm, policy =>
                    policy.RequireAuthenticatedUser()
                          .RequireClaim("UserType", "Adm"));             


            });
            return services;

        }

        private static IServiceCollection AddCorsComum(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    string[] origins = { "http://avanade.ecommerce.com.br", "https://avanade.apigateway.com.br" };
                    builder.
                     WithOrigins(origins)
                     .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); ;
                });
            });

            return services;
        }
        #endregion

        #region INJECTGLOBALSERVICESMANUALLY

        public static void AddDbContextConfiguration<TDbContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
                where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(connectionStringName)
                );
            });
        }
        public static IApplicationBuilder AddCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        #endregion
    }
}
