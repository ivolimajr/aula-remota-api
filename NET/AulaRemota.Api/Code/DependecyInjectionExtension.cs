using AulaRemota.Core.Services;
using AulaRemota.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AulaRemota.Api.Code
{
    public static class DependecyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticatedUserServices>();
            services.AddScoped(typeof(IValidatorServices), typeof(ValidatorServices));

            return services;
        }
    }
}