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
            var orders = dataBase.Orders.Find(o => o.UserRef == userRef).ToList();
            foreach(var order in orders)
            {
                order.Article = dataBase.Articles.Get(order.ArticleId);
            }

            return Mapper.Map<List<OrderEntity>, List<OrderVM>>(orders);
        }

        public bool OrderItem(int articleId, string userRef)
        {
            var existingOrders = dataBase.Orders.Find(o => o.UserRef == userRef && o.ArticleId == articleId).ToList();
            if(existingOrders.Count == 0)
            {
                dataBase.Orders.Create(new OrderEntity { UserRef = userRef, ArticleId = articleId, Quantity = 1 });
            }
            else
            {
                existingOrders[0].Quantity++;
                dataBase.Orders.Update(existingOrders[0]);                
            }
            dataBase.Save();
            return true;
        }

        public bool SaveOrders(List<OrderVM> orders)
        {
            foreach(var order in orders)
            {
                if (order.Quantity == 0)
                {
                    dataBase.Orders.Delete(order.Id);
                }
                else
                {
                    var orderEntity = dataBase.Orders.Get(order.Id);
                    if (order.Quantity != orderEntity.Quantity)
                    {
                        orderEntity.Quantity = order.Quantity;
                        dataBase.Orders.Update(orderEntity);
                    }
                }
            }
            dataBase.Save();
            return true;
        }

        public void Dispose()
        {
            dataBase.Dispose();
        }
    }
}
