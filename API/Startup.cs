using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


//for ui
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connectionString = Configuration["ConnectionStrings:sqlString"];
            services.AddDbContext<ApiDBContext>(c => c.UseSqlServer(connectionString));
            services.AddScoped<ITempRepository, TempRepository>();
            services.AddScoped<IPpgRepository, PpgRepository>();


            //for ui
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Info
               {
                   Title = "Brak-Trak API",
                   Version = "v1",
                   Description = "This is the GUI for Brak-trak API"
               });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});



            //used for insert test in development envirment
            //context.SeedTempDataContext();
            //context.SeedPPGDataContext();


            app.UseMvc();


            //enable ui
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
