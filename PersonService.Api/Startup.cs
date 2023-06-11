using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonService.Api.DataAccess;
using PersonService.Api.DataAccess.Commands;
using PersonService.Api.DataAccess.Queries;
using PersonService.Api.Models;

namespace PersonService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAddressValidator, AddressValidator>();
            services.AddScoped<IPersonQueryService, PersonQueryService>();
            services.AddScoped<IAddressQueryService, AddressQueryService>();
            services.AddScoped<IPersonCommandService, PersonCommandService>();
            services.AddTransient<IPersonValidator, PersonValidator>();
            services.AddScoped<IAddressCommandService, AddressCommandService>();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Person Service";
                    document.Info.Description = "A simple API for managing contacts";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Megan Chipps",
                        Email = "mdayconsulting@gmail.com",
                        Url = "https://github.com/mldst58"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
