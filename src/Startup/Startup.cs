using System.Collections.Generic;
using System.Globalization;
using DistanceCalculator.Application;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Infrastructure;
using DistanceCalculator.Infrastructure.Persistence;
using DistanceCalculator.Web;
using DistanceCalculator.Web.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace DistanceCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddWebComponents();

            services
                .AddHealthChecks()
                .AddDbContextCheck<DistanceCalculatorDbContext>();

            services
                .AddControllers()
                .AddFluentValidation(options => options
                    .RegisterValidatorsFromAssemblyContaining<IDistanceCalculatorData>())
                .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-IE");
                options.SupportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Distance calculator API",
                    Description = "An ASP.NET Core Web API for calculating distance",
                    Contact = new OpenApiContact()
                    {
                        Name = "Georgi Nikolov",
                    },
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseRequestLocalization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors();
        }
    }
}
