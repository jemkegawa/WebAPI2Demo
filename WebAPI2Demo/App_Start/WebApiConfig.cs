using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebAPI2Demo.App_Start;

namespace WebAPI2Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services
			
			// There can only be one exception handler, so we have to replace the out-of-the-box
			config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
			
			// There can be multiple exception loggers, so we can just add a new one.
			config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "TwilioApi",
            //    routeTemplate: "api/twilio/{messageType}/{version}/reply",
            //    defaults: new { ext = "xml" }
            //    );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Routes.MapHttpRoute(
				name: "DemoApi",
				routeTemplate: "api/demo/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			//config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
			//config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
		}
    }
}
