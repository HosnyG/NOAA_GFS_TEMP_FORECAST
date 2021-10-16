using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast_API.Services;
using Wgrib2_Operator_NS;

namespace WeatherForecast_API
{
    public class Startup
    {
        private readonly IConfiguration _configs;

        public Startup(IConfiguration configuration)
        {
            this._configs = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //Swagger service registration
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                { Title = "ICE WeatherForecast", Description = "ICE WeatherForecast WEB API" });
            });

            string wgrib2AppPath = _configs["wgrib2:appPath"];
            string grb2filesDirectoryPath = _configs["wgrib2:grb2filesDirectory"];
            string bucketname = _configs["NOAA_GFS_S3:bucketname"];
            string grb2filesDirectory = _configs["NOAA_GFS_S3:grb2filesDirectory"];
            string bucketRegion = _configs["NOAA_GFS_S3:region"];


            services.AddTransient<IWgrib2_Operator>(w => new Wgrib2_Operator(wgrib2AppPath));
            services.AddScoped<IForecastService,GFS_ForecastService>(f=> new GFS_ForecastService(f.GetService<IWgrib2_Operator>(),bucketname, grb2filesDirectory, bucketRegion));


            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            if (env.IsDevelopment()) //swagger UI
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ICE WeatherForecast WEB API V1");
                });
            }
        }
    }
}
