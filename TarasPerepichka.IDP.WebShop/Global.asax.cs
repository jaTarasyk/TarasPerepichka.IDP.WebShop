using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TarasPerepichka.IDP.Bootstrap.Mapper;
using TarasPerepichka.IDP.BusinessLayer.Infrastructure;
using TarasPerepichka.IDP.WebShop.Util;

namespace TarasPerepichka.IDP.WebShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationMapper.Init();
            NinjectModule entityModule = new ArticleModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection2");
            var kernel = new StandardKernel(entityModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

        }
    }
}
