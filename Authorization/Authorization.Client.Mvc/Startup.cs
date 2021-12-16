using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)//need to save from token
                .AddOpenIdConnect("oidc", config => //this allows to get two types of token: identity and access kinds of tokens.
                {
                    //look at documentation: there you can see 3.1.2.1 Authentication Request: what is REQUIRED 
                    //go to the identity server and write RedirectUrls in your mvc client.
                    config.Authority = "https://localhost:10001"; //here this client goes to authorize after clicking on secret link in mvc
                    // and by this address "https://localhost:10001" we have our authorization identity server
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true; //means, that the data I get from the server I need to save.

                    config.ResponseType = "code";

                }); //install Microsoft.AspNetCore.Authentication.OpenIdConnect// outside authenticator 
            //I authenticate myself, then I save auth results in cookies.
            services.AddControllersWithViews();

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
