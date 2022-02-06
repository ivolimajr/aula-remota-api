using AulaRemota.Core.Services;
using AulaRemota.Infra.Repository;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AulaRemota.Api.Code
{
    public static class DependecyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IUnitOfWorkFactory<>), typeof(AulaRemotaUnitOfWorkFactory<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IValidatorServices), typeof(ValidatorServices));

            return services;
        }
    }
}