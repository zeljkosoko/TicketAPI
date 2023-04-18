﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TicketAPI.UnitOfWork;
using TicketAPI.DIservices;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;

namespace TicketAPI
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
            //Application service
            //services.AddDbContext<TicketDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IEntitiesServices, EntitiesServices>();

            services.AddControllers();
            services.AddMvc();

            #region Register the Swagger generator, defining 1 or more Swagger documents

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Ticket API",
                    Contact = new OpenApiContact
                    {
                        Name = "Željko Sokolović",
                        Email = "zex.sokolovic@gmail.com"
                    },
                    Version = "v1",
                    Description = "ASP.NET CORE Web API receives data from the client, based on which a ticket is created in the central db.",
                    
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwagger()
                .UseSwaggerUI(sw =>
                {
                    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket API");
                });
            #endregion

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

        }
    }
}
