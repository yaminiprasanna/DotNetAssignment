using EFlib.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Registration.Helpers;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Types;

namespace Registration
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
            //services.AddRazorPages();
            services.AddControllers();
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = " ME API",
                    Version = "v1"
                });
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //setup.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<EmployeeContext>();
            services.AddTransient<IUser, UserService>();
            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<ILogin, LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json",
                "ME API V1");
                options.RoutePrefix = string.Empty;
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
