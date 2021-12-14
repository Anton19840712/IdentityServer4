using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorization.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //add package identityServer4, then add app.UseIdentity, then services.AddIdentityServer();
            //could work in in-memory or database style
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Auth/Login"; //this url will be used as path to go on this identity server.
                })
                .AddInMemoryClients(Configuration.GetClients())//Xamarin, Console, Angular, etc.
                .AddInMemoryApiResources(Configuration.GetApiResources()) //
                .AddInMemoryIdentityResources(Configuration.GetIdentityResources()) //
                .AddDeveloperSigningCredential();//not certificates, but something like plug

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
