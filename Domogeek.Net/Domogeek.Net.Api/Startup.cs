﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Domogeek.Net.Api
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
            services.AddMvc().AddJsonOptions(s =>
            {
                s.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddMemoryCache();
            services.AddSingleton<EdfHelper>();
            services.AddSingleton<SchoolHolidayHelper>();
            services.AddSingleton<VigilanceHelper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Domogeek.Net", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllParametersInCamelCase();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Domogeek.Net v1");

            });
        }
    }
}