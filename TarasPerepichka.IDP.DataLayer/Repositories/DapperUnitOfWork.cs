using System;
using TarasPerepichka.IDP.DataLayer.DataContext;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private ArticleRepository articleRepository;
        private OrderRepository orderRepository;
        public string dbConnection;

        public DapperUnitOfWork(string connectionString)
        {
            dbConnection = connectionString;
        }

        public IRepository<ArticleEntity> Articles
        {
            get { return articleRepository ?? (articleRepository = new ArticleRepository(dbConnection)); }
        }

        public IRepository<OrderEntity> Orders
        {
            get { return orderRepository ?? (orderRepository = new OrderRepository(dbConnection)); }
        }
    }
}
