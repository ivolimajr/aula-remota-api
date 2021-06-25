using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Services;
using AulaRemota.Core.Services;
using AulaRemota.Infra.Data;
using AulaRemota.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AulaRemota.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<SqlContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IEdrivingServices, EdrivingServices>();
            services.AddScoped<IEdrivingRepository, EdrivingRepository>();

            services.AddScoped<IEdrivingCargoServices, EdrivingCargoServices>();
            services.AddScoped<IEdrivingCargoRepository, EdrivingCargoRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AulaRemota.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AulaRemota.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();

            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
