using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Client.Mvc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
                {
                    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = "oidc";

                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect("oidc", config =>
                {
                    config.Authority = "https://localhost:10001";
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true;

                    config.ResponseType = "code";

                    config.GetClaimsFromUserInfoEndpoint = true;

                    config.Scope.Add("OrdersAPI"); //this is variant how to manage scopes for current client
                    //we need to 

                    config.GetClaimsFromUserInfoEndpoint = true; //enables to check claims in endpoint need to add claims in access
                    //config.ClaimActions.MapAll();this copy all claims from access token to user claims
                    config.ClaimActions.MapJsonKey(ClaimTypes.DateOfBirth, ClaimTypes.DateOfBirth);
                });


            services.AddHttpClient();
            services.AddControllersWithViews();

            services.AddAuthorization(config =>
            {
                config.AddPolicy("HasDateOfBirth", builder =>
                {
                    builder.RequireClaim(ClaimTypes.DateOfBirth);//determine claim, that will be required from method in controller: [Authorize(Policy = "HasDateOfBirth")]
                });
                //config.AddPolicy("OlderThan", builder =>
                //{
                //    builder.AddRequirements(new OlderThanRequirement(10));
                //});


            });

            services.AddSingleton<IAuthorizationHandler, OlderThanRequirementHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
