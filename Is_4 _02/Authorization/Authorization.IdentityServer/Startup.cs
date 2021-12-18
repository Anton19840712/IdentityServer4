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
            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/Auth/Login";
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

            app.UseIdentityServer();//instead of authN, authZ 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
