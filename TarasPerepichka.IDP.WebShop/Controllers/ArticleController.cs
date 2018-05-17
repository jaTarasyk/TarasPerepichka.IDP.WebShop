using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TarasPerepichka.IDP.BusinessLayer.Interfaces;

namespace TarasPerepichka.IDP.WebShop.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService service)
        {
            articleService = service;
        }

        // GET: Products
        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult GetAllArticles()
        {
            var articlesVM = articleService.GetArticles();
            return Json(new { data = articlesVM }, JsonRequestBehavior.AllowGet);
        }
    }
}