using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using TarasPerepichka.IDP.DataLayer.DataContext;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class OrderRepository : IRepository<OrderEntity>
    {
        private readonly string dbConnection;

        public OrderRepository(string connectionstring)
        {
            dbConnection = connectionstring;
        }

        public void Create(OrderEntity[] items)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "INSERT INTO OrderEntities(UserRef, Quantity, ArticleId) VALUES(@UserRef, @Quantity, @ArticleId)";
                connection.Execute(sql, items);
            }
        }

        public void Delete(OrderEntity[] orders)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "DELETE FROM OrderEntities WHERE ID = @Id";
                connection.Execute(sql, orders);
            }
        }

        public IEnumerable<OrderEntity> Find(string userRef, int? articleId = null)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                var queryArgs = new DynamicParameters();
                queryArgs.Add("@userRef", userRef, DbType.String, ParameterDirection.Input);
                if (articleId.HasValue)
                {
                    queryArgs.Add("@articleId", articleId.Value, DbType.Int32, ParameterDirection.Input);
                }
                return connection.Query<OrderEntity>("SP_FindUserOrders", param: queryArgs, commandType: CommandType.StoredProcedure);
            }
        }

        public OrderEntity Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "SELECT * FROM OrderEntities WHERE Id = @orderId";
                return connection.QuerySingleOrDefault<OrderEntity>(sql, new { orderId = id });
            }
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "SELECT * FROM OrderEntities";
                return connection.Query<OrderEntity>(sql);
            }
        }

        public void Update(OrderEntity[] items)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "UPDATE OrderEntities SET Quantity = @Quantity WHERE ID = @Id";
                connection.Execute(sql, items);
            }
        }
    }
}
