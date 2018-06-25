using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class ArticleRepository : IRepository<ArticleEntity>
    {
        private readonly string dbConnection;
        
        public ArticleRepository(string connectionString)
        {
            dbConnection = connectionString;
        }

        public void Create(ArticleEntity[] items)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "INSERT INTO ArticleEntities (Name, Description, Price) VALUES(@Name, @Description, @Price)";
                connection.Execute(sql, items);
            }
        }

        public void Delete(ArticleEntity[] items)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "DELETE FROM ArticleEntities WHERE ID = @Id";
                connection.Execute(sql, items);
            }            
        }

        public ArticleEntity Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "SELECT * FROM ArticleEntities WHERE Id = @articleId";
                return connection.QuerySingleOrDefault<ArticleEntity>(sql, new { articleId = id });
            }
        }

        public IEnumerable<ArticleEntity> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "SELECT * FROM ArticleEntities";
                return connection.Query<ArticleEntity>(sql);
            }           
        }

        public void Update(ArticleEntity[] items)
        {
            using (IDbConnection connection = new SqlConnection(dbConnection))
            {
                connection.Open();
                string sql = "UPDATE ArticleEntities SET Name = @Name, Description = @Description, Price = @Price WHERE ID = @Id";
                connection.Execute(sql, items);
            }
        }

        //not in use, just interface impl
        public IEnumerable<ArticleEntity> Find(string str, int? id = null)
        {
            return null;
        }
    }
}
