using AulaRemota.Core.Configuration;
using AulaRemota.Infra.Repository;
using AulaRemota.Infra.Context;
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
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using AulaRemota.Api.Code;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

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

            UnitOfWork.ServiceCollection = services;
            UnitOfWork.Configuration = Configuration;

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                    Configuration.GetSection("TokenConfiguration")
                )
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            //Configura instancia do UOW
            //var provider = services.BuildServiceProvider();
            AulaRemotaUnitOfWorkFactory<MySqlContext>.SetObjectContext(() => new MySqlContext(true));

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

            services.AddControllers();

            var serverVersion = new MySqlServerVersion(new Version(5, 6, 23));
            services.AddDbContext<MySqlContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(Configuration.GetConnectionString("MySQLConnLocal"), serverVersion) // <- COMENTA ESSA LINHA E DESCOMENTA A DE BAIXO PARA USAR O SANDBOX
                                                                                                  //.UseMySql(Configuration.GetConnectionString("MySQLConnSandbox"), serverVersion) // <--DESCOMENTE PARA USAR O SANDBOX REMOTO
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            services.AddDependencyInjection();

            var assembly = AppDomain.CurrentDomain.Load("AulaRemota.Core");
            services.AddMediatR(assembly);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AulaRemota.Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Essa API usa autenticação JWT Bearer Token. Solicite acesso para fazer uso.",
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });


            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.RoutePrefix = string.Empty;
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "AulaRemota.Api v1");
                });
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
