using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TarasPerepichka.IDP.BusinessLayer.Interfaces;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.WebShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService service)
        {
            orderService = service;
        }

        // GET: Order
        public ActionResult Index()
        {
            return PartialView();
        }

        // Get: _OrdersModal
        public PartialViewResult ViewModal()
        {
            return PartialView("_OrdersModal");
        }

        public JsonResult GetOrders()
        {
            var userOrders = orderService.GetOrders(User.Identity.GetUserId());
            return Json(userOrders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Order(int articleId)
        {
            var added = orderService.OrderItem(articleId, User.Identity.GetUserId());
            return Json(new { success = added, message = "Order has been successfully added." });
        }

        [HttpPost]
        public JsonResult SaveOrders(List<OrderVM> orders)
        {
            orderService.SaveOrders(orders);
            return Json(new { success = true, message = "Orders have been successfully modified." });
        }
    }
}