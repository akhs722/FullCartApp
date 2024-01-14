using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using static FullCartAPI.Constants.APIConstants;

namespace FullCartAPI.Extensions
{
    public static class ConfigureDI
    {

        public static void AddDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<FullCartDbContext>(options => {
                options.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString(Config.DbConnectionName), b => b.MigrationsAssembly("Infrastructure"));

        });
        }
    }
}
