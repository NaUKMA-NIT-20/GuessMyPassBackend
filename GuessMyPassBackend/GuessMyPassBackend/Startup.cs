using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GuessMyPassBackend.Middlewares;
using GuessMyPassBackend.Models;
using GuessMyPassBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace GuessMyPassBackend
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
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddCors();
            
            services.AddScoped<IUserRepository, Controllers.UserRepository>();
            services.AddScoped<IDataRepository, Controllers.DataRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });


            services.Configure<Settings>(options =>
            {

                options.ConnectionString = "mongodb+srv://guessMyPass:SosiPomojka@cluster0.25l2f.azure.mongodb.net/guess-my-pass?retryWrites=true&w=majority";
                options.Database = "guess-my-pass";
                options.JWT_SECRET = "Suda idi pomojka";

                /*options.ConnectionString = System.Environment.GetEnvironmentVariable("MONGODB_URL");
                options.Database = System.Environment.GetEnvironmentVariable("DB_NAME");
                options.JWT_SECRET = System.Environment.GetEnvironmentVariable("JWT_SECRET");*/
            });

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
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseMiddleware(typeof(AuthMiddleware));

            app.UseHttpsRedirection();

            app.UseMvc();


        }

    }
}
