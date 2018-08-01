using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());
            services.AddAuthentication()
               .AddGoogle("Google", options =>
               {
                   options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                   options.ClientId = "809770630330-ckho9om9v0lbsd2a4nvl968lcntmv1hv.apps.googleusercontent.com";
                   options.ClientSecret = "fuZUH_X36zZ0bNy-_i1DIlXO";
                   options.CallbackPath = new PathString("/signin-google");
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes => 
            {
                //routes.MapRoute(
                //    name: "Google Api",
                //    template: "signin-google",
                //    defaults: new { controller = "Account", action = "Index" }
                //    );
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
