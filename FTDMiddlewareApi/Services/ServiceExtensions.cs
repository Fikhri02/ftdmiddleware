using FTDMiddlewareApi.Service;  // Replace with your actual namespace
using FTDMiddlewareApi.Service.Interface;  // Replace with your actual namespace
using Microsoft.Extensions.DependencyInjection;


namespace FTDMiddlewareApi.Service.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISubmitTrxMessageService, SubmitTrxMessageService>();
        }
    }
}
