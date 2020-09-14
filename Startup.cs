using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VikLearnsToCook.Data;
using static VikLearnsToCook.Data.IRestaurantData;

namespace VikLearnsToCook
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

            services.AddDbContextPool<VikLearnsToCookDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("VikLearnsToCookDB"));
            });

            //commented below line as we achieved the DB connection
            //Changed the AddSingleton to AddScoped
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.AddRazorPages();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseNodeModules();           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

           
        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate next)
        {
            return async ctx =>
            {
                if(ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello There!");
                }
                else
                {
                    await next(ctx);                    
                }
            };
        }
    }
}
