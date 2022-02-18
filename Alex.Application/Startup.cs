using System.Web.Http;
using Alex.BusinessLogic;
using Unity;
using Owin;
using Unity.WebApi;
using Unity.Injection;
using Unity.Lifetime;
using Alex.Model;
using System.Net.Http;
using System;
using System.Configuration;
using System.Runtime.Caching;

namespace Alex.Application
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //Configure Web API for self host
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();
            config.Routes.MapHttpRoute(
              "DefaultApi",
              "api/{controller}/{action}/{id}",
              new { id = RouteParameter.Optional }
            );
            appBuilder.UseWebApi(config);

            IUnityContainer container = new UnityContainer();

            container.RegisterType<HttpClient>(
                                                new InjectionFactory(x =>
                                                    new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["endpoint.giphy"]) }
                                                )
                                            );

            //container.RegisterType<ObjectCache>(
            //                                    new InjectionFactory(x =>
            //                                        MemoryCache.Default
            //                                    )
            //                                );

            container.RegisterType<IInMemoryCache>(
                                              new InjectionFactory(x =>
                                                    new InMemoryCache(container.Resolve<IService>(), MemoryCache.Default)
                                                )

                                            );

            //register services
            container.RegisterType<IService, Service>();         

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}