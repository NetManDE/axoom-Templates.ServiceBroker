using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace MyVendor.ServiceBroker.Infrastructure
{
    public static class RestApi
    {
        public static IServiceCollection AddRestApi(this IServiceCollection services)
        {
            services.AddMvcCore(options =>
                     {
                         options.EnableEndpointRouting = false;
                     })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddApiExplorer()
                    .AddFormatterMappings()
                    .AddDataAnnotations()
                    .AddNewtonsoftJson(options =>
                     {
                         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                         options.SerializerSettings.Converters.Add(new StringEnumConverter {NamingStrategy = new CamelCaseNamingStrategy()});
                     });

//            services.AddSwaggerGen(options =>
//            {
//                options.SwaggerDoc("v1",
//                    new Info
//                    {
//                        Title = "My Service Broker",
//                        Version = "v1"
//                    });
//                options.IncludeXmlComments(Path.Combine(ApplicationEnvironment.ApplicationBasePath, "MyVendor.ServiceBroker.xml"));
//                options.DescribeAllEnumsAsStrings();
//            });

            return services;
        }

        public static IApplicationBuilder UseRestApi(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(TrustExternalProxy())
               .UseStatusCodePages();

            if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseExceptionDemystifier();
            }

//            app.UseSwagger()
//               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Service Broker API v1"));

            return app.UseMvc();
        }

        private static ForwardedHeadersOptions TrustExternalProxy()
        {
            var options = new ForwardedHeadersOptions {ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto};
            options.KnownProxies.Clear();
            options.KnownNetworks.Clear();
            return options;
        }
    }
}
