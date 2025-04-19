using ComedorInfantil.Gestion.Application.DataBase;
using ComedorInfantil.Gestion.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComedorInfantil.Gestion.Persistence
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseService>(options =>
            {
                options.UseSqlServer(configuration["SqlConnectionString"]);
            });

            services.AddScoped<IDataBaseService, DataBaseService>();

            return services;
        }
    }
}
