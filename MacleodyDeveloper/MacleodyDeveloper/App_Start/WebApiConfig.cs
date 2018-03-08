using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MacleodyDeveloper
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Habilitando acesso API somente para usuários autenticados
            config.Filters.Add(new AuthorizeAttribute());

            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
