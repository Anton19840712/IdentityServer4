using Authorization.IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Authorization.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
                {
                    config.UseInMemoryDatabase("MEMORY");
                })
                .AddIdentity<IdentityUser, IdentityRole>(config => //this should be before .AddAspNetIdentity<IdentityUser>(), because it could be mistake something like add 1 user
                    //IUserClaimsPrincipalFactory`1 not registered error in IdentityServer implementation
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //add package identityServer4, then add app.UseIdentity, then services.AddIdentityServer();
            //could work in in-memory or database style
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Auth/Login"; //this url will be used as path to go on this identity server.
                })
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryClients(Configuration.GetClients())//Xamarin, Console, Angular, etc.
                .AddInMemoryApiResources(Configuration.GetApiResources()) //
                .AddInMemoryIdentityResources(Configuration.GetIdentityResources()) //
                .AddDeveloperSigningCredential();//not certificates, but something like plug

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();//adds link on the package, that allows when page has reloaded view changes on it from code. Press f5 on the page.
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
