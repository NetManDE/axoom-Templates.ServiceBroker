using System.Security.Principal;
using System.Threading.Tasks;
using idunno.Authentication.Basic;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MyVendor.ServiceBroker.Infrastructure
{
    public static class Security
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.Get<AuthenticationOptions>();

            services
               .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
               .AddBasic(options =>
                {
                    options.AllowInsecureProtocol = true;
                    options.Realm = authOptions.Realm;
                    options.Events = new BasicAuthenticationEvents
                    {
                        OnValidateCredentials = context =>
                        {
                            if (context.Username == authOptions.Username && context.Password == authOptions.Password)
                            {
                                context.Principal = new GenericPrincipal(new GenericIdentity(context.Username), new string[0]);
                                context.Success();
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(BasicAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

//            services.ConfigureSwaggerGen(options =>
//            {
//                options.AddSecurityDefinition("basic", new BasicAuthScheme());
//            });

            return services;
        }
    }

    [UsedImplicitly]
    public class AuthenticationOptions
    {
        public string Realm { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
