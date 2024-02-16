using Application.Contracts.Logger;
using Application.Contracts.UnitOfWork;
using Application.CoreInformation.Context;
using Application.Infrastructure.Logger;
using Application.Infrastructure.UnitOfWorkRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(option => 
            {
                option.AddPolicy("CorsPolicy", 
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegeration(this IServiceCollection services) 
        {
            services.Configure<IISOptions>(options => 
            {
            
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services) 
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqLiteContext(this IServiceCollection services, IConfiguration config) 
        {
            var connectionString = config["ConnectionStrings:Con"];
            services.AddDbContext<ProjectDbContext>(o => o.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("Application.API")));
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
        }
    }
}
