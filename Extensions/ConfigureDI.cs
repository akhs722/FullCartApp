using Application.Contratcs.Repos;
using Application.Contratcs.Services;
using Application.Services;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

            service.AddApplication(configuration);
            service.AddInfraStructure(configuration);
        }


        public static void ConfigureIdentity(this IServiceCollection services)
        {

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<FullCartDbContext>()
              .AddDefaultTokenProviders();
        }

        private static void AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthService>();
           
        }
        private static void AddInfraStructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthRepo, AuthRepo>();
           
        }

    }
}
