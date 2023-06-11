using Library.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Library.API
{
    public static class ApiServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            return services;
        }
    }
}