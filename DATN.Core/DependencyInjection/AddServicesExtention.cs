using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DATN.Core.DependencyInjection
{
    public static class AddServicesExtention
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IimageRepository, ImageReponsiroty>();
            services.AddScoped<IAuthenRepository, AuthenRepository>();

            return services;
        }
    }
}
