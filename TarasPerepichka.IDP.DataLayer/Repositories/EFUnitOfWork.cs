using System;
using TarasPerepichka.IDP.DataLayer.DataContext;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ArticlesContext db;
        private ArticleRepository articleRepository;
        private OrderRepository orderRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ArticlesContext(connectionString);
        }

        public IRepository<ArticleEntity> Articles
        {
            get { return articleRepository ?? (articleRepository = new ArticleRepository(db)); }
        }

        public IRepository<OrderEntity> Orders
        {
            get { return orderRepository ?? (orderRepository = new OrderRepository(db)); }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
