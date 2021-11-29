using _01.Filters;
using _01.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace _01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "My api" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "01.xml");
                c.IncludeXmlComments(filePath);

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "API-KEY",
                    Description = "Api key auth"
                });

                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header,
                };

                var requirement = new OpenApiSecurityRequirement
                {
                    { key, new List<string>() }
                };

                c.AddSecurityRequirement(requirement);

                c.SchemaFilter<EnumSchemaFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseReDoc(c => { c.RoutePrefix = "docs"; });

            app.Use(async (context, func) =>
            {
                if (!context.Request.Headers.TryGetValue("API-KEY", out var key) || !key.Equals("secret-api-key"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new ApiError() { Code = 4, Message = "Invalid api key" });
                    return;
                }

                await func();
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
