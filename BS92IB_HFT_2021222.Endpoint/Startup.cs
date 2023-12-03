using BS92IB_HFT_2021222.Data;
using BS92IB_HFT_2021222.Logic;
using BS92IB_HFT_2021222.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IFleetLogic, FleetLogic>();
            services.AddTransient<IFleetRepository, FleetRepository>();
            services.AddTransient<IShipLogic, ShipLogic>();
            services.AddTransient<IShipRepository, ShipRepository>();
            services.AddTransient<IArmamentLogic, ArmamentLogic>();
            services.AddTransient<IArmamentRepository, ArmamentRepository>();
            services.AddTransient<IWeaponLogic, WeaponLogic>();
            services.AddTransient<IWeaponRepository, WeaponRepository>();
            services.AddTransient<NavyDbContext, NavyDbContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BS92IB_HFT_2021222.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BS92IB_HFT_2021222.Endpoint v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
