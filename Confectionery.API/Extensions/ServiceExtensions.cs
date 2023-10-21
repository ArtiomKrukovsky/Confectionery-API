using Confectionery.API.Application.Services;
using Confectionery.API.Application.Services.Interfaces;
using Confectionery.API.Options;
using Confectionery.Domain.IRepositories;
using Confectionery.Infrastructure;
using Confectionery.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Confectionery.API.Extensions
{
    public static class ServiceExtensions
    {
        private const string DefaultConnectionString = "DefaultConnection";

        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<СonfectioneryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(DefaultConnectionString),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("Confectionery.Infrastructure");
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                }, ServiceLifetime.Scoped
            );
        }

        public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedCorsOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200/")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(origin => true)
                            .AllowCredentials();
                    });
            });
        }

        public static void AddMapster(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig { RequireExplicitMapping = true };
            config.Scan(Assembly.GetExecutingAssembly());
            config.Compile(); // validate mappings
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConfectionRepository, ConfectionRepository>();
            services.AddScoped<IConfectionPictureRepository, ConfectionPictureRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderOptions.EmailSender));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));
        }
    }
}
