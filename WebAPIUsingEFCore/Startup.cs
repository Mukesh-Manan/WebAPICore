using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog;
using WebAPI.DB.Core;
using WebAPI.DB.Repository;
using WebAPI.Logging.NLogLogging;

namespace WebAPIUsingEFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(configs =>
            {
                configs.RespectBrowserAcceptHeader = true;
                configs.ReturnHttpNotAcceptable = true;
                configs.InputFormatters.Add(new XmlSerializerInputFormatter(new MvcOptions()));
                configs.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
            services.AddDbContext<InventoryContext>(options =>
                        options.UseSqlServer(Configuration["Data:InventoryConnection:ConnectionString"]));
            services.AddDbContext<ApplicationDBContext>(options =>
                        options.UseSqlServer(Configuration["Data:InventoryConnection:ConnectionString"]));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<ILog, NLogLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
