using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(TA.Classified.Web.App_Start.Startupauth))]

namespace TA.Classified.Web.App_Start
{
    public class Startupauth
    {
        public void Configuration(IAppBuilder app)
        { // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login")
                
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // app.UseIncludeContextKeys();

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            

            app.UseGoogleAuthentication(clientId: "980354972421-11jkjchb98vc20jl7psppmnnt72tik09.apps.googleusercontent.com",
                clientSecret: "AU0IReCerrps3gbF8_kQCwOT");
        }
    }
}
