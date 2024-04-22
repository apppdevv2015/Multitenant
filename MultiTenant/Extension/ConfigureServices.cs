using Microsoft.OpenApi.Models;
using MultiTenant.Services.IService;
using MultiTenant.Services.Services;

namespace MultiTenant.Extension
{
    public static class ConfigureServices
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenantService, TenantService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
              builder =>
              {
                  builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
              });
            });
            services.AddHttpClient();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MultiTenant", Version = "v1" });
            });
        }
    }
}
