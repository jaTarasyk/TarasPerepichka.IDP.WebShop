using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TarasPerepichka.IDP.DataLayer.DataContext;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {
        private readonly ArticlesContext db;

        public OrderRepository(ArticlesContext context)
        {
            db = context;
        }

        public void Create(OrderEntity item)
        {
            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            OrderEntity order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
            }
        }

        public IEnumerable<OrderEntity> Find(Func<OrderEntity, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public OrderEntity Get(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            return db.Orders;
        }

        public void Update(OrderEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
