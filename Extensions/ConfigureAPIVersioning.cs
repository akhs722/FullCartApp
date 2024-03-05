using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace FullCartAPI.Extensions
{
    public static class ConfigureAPIVersioning
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
            options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
                });
        }
    }
}
