using Application.Contratcs.Repos;
using Application.Contratcs.Services;
using Application.Services;
using AutoMapper;
using Domain.Entities.User;
using Infrastructure.Context;
using Infrastructure.MappingProfiles;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
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
            services.AddSingleton(GetMapper());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IItemService, ItemService>();


        }
        private static void AddInfraStructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IItemRepo, ItemRepo>();

        }


        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secret = jwtSettings.GetSection("Secret").Value;
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(secret))
                };
            });
        }

        private static IMapper GetMapper()
        {

            var mappingConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfiles(ProfileMaster.DomainAndEntityMappings);
                    cfg.AllowNullCollections = true;
                    cfg.AllowNullDestinationValues = true;
                }
                );
            return mappingConfig.CreateMapper();
        }

    }
}
