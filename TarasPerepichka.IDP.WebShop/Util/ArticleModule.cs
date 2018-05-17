using Ninject.Modules;
using TarasPerepichka.IDP.BusinessLayer.Interfaces;
using TarasPerepichka.IDP.BusinessLayer.Services;

namespace TarasPerepichka.IDP.WebShop.Util
{
    public class ArticleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleService>().To<ArticleService>();
            Bind<IOrderService>().To<OrderService>();
        }
    }
}