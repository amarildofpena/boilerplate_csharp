using Business.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureInjection(this IServiceCollection services)
        {

            services.AddDbContext<CentralDbContext>(options =>
               options.UseMySql("Server=localhost;Database=pkmn_sinnoh;User=root;Password=;;ConvertZeroDateTime=True;DefaultCommandTimeout=30;AllowZeroDateTime=True;AllowLoadLocalInfile=true;",
                    new MySqlServerVersion(new Version(8, 0, 26)))
            );

            services.AddScoped<CentralDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
