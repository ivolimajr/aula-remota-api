using AulaRemota.Core.Configuration;
using AulaRemota.Core.Interfaces.Repository;
using AulaRemota.Core.Interfaces.Repository.Auth;
using AulaRemota.Core.Interfaces.Services;
using AulaRemota.Core.Interfaces.Services.Auth;
using AulaRemota.Core.Services;
using AulaRemota.Core.Services.Auth;
using AulaRemota.Infra.Data;
using AulaRemota.Infra.Repository;
using AulaRemota.Infra.Repository.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

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
            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfiguration")
                )
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
            });

/*            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));*/

            services.AddControllers();

            var serverVersion = new MySqlServerVersion(new Version(5, 6, 23));
            services.AddDbContext<MySqlContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(Configuration.GetConnectionString("MySQLConnLocal"), serverVersion) // <- COMENTA ESSA LINHA E DESCOMENTA A DE BAIXO PARA USAR O SANDBOX
                    //.UseMySql(Configuration.GetConnectionString("MySQLConnSandbox"), serverVersion) // <--DESCOMENTE PARA USAR O SANDBOX REMOTO
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());



            services.AddScoped(typeof(Core.Interfaces.Repository.IRepository<>), typeof(Infra.Repository.Repository<>));

            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IEdrivingCargoServices, EdrivingCargoServices>();
            services.AddScoped<IEdrivingCargoRepository, EdrivingCargoRepository>();

            services.AddScoped<IAuthUserRepository, AuthUserRepository>();

            services.AddScoped<ITokenServices, TokenServices>();

            services.AddScoped<IParceiroCargoServices, ParceiroCargoServices>();
            services.AddScoped<IParceiroCargoRepository, ParceiroCargoRepository>();

            services.AddScoped<IParceiroServices, ParceiroServices>();
            services.AddScoped<IParceiroRepository, ParceiroRepository>();

            services.AddScoped<IEnderecoServices, EnderecoServices>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            var assembly = AppDomain.CurrentDomain.Load("AulaRemota.Core");
            services.AddMediatR(assembly);

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
