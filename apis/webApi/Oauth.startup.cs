using Microsoft.AspNetCore.Builder;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Web.Http;

[assembly: OwinStartup(typeof(webApi.OauthStartup))]
namespace webApi
{
    public class OauthStartup
    {
        private string[] orgins = new List<string>() { "http://localhost:3000" }.ToArray();

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new OAuthPropvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
            });

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world");
            });

        }

    }
}

