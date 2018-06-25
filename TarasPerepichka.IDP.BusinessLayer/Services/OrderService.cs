using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TarasPerepichka.IDP.BusinessLayer.Interfaces;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork dataBase;

        public OrderService(IUnitOfWork unit)
        {
            dataBase = unit;
        }

        public IEnumerable<OrderVM> GetOrders(string userRef)
        {
            var orders = dataBase.Orders.Find(userRef).ToList();
            foreach(var order in orders)
            {
                order.Article = dataBase.Articles.Get(order.ArticleId);
            }

            return Mapper.Map<List<OrderEntity>, List<OrderVM>>(orders);
        }

        public bool OrderItem(int articleId, string userRef)
        {
            var existingOrders = dataBase.Orders.Find(userRef, articleId).ToArray();
            if(existingOrders.Length == 0)
            {
                dataBase.Orders.Create(new[] { new OrderEntity { UserRef = userRef, ArticleId = articleId, Quantity = 1 } });
            }
            else
            {
                existingOrders[0].Quantity++;
                dataBase.Orders.Update(existingOrders);                
            }
            return true;
        }

        public bool SaveOrders(List<OrderVM> orders)
        {
            List<OrderEntity> ordersEM = Mapper.Map<List<OrderVM>, List<OrderEntity>>(orders);
            dataBase.Orders.Delete(ordersEM.Where(o=>o.Quantity == 0).ToArray());
            dataBase.Orders.Update(ordersEM.Where(o => o.Quantity > 0).ToArray());
            return true;
        }
    }
}
