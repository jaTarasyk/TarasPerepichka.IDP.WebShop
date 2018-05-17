using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarasPerepichka.IDP.DataLayer.Entitties;

namespace TarasPerepichka.IDP.DataLayer.DataContext
{
    public class ArticlesContext : DbContext
    {
        public DbSet<ArticleEntity> Articles { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        static ArticlesContext()
        {
            Database.SetInitializer(new ArticlesDbInintializer());
        }

        public ArticlesContext(string connectionString) : base(connectionString)
        {
        }
    }

    public class ArticlesDbInintializer : DropCreateDatabaseIfModelChanges<ArticlesContext>
    {
        protected override void Seed(ArticlesContext context)
        {
            context.Articles.Add(new ArticleEntity { Name = "Super Ball", Description = "This ball belonged to Pele", Price = 5000.00M });
            context.Articles.Add(new ArticleEntity { Name = "A very valuable thing", Description = "description description description description description description", Price = 1.00M});
            context.Articles.Add(new ArticleEntity { Name = "Samsung Phone", Description = "Works like a horse, make all kinds of job", Price = 800.00M });
            context.Articles.Add(new ArticleEntity { Name = "Ferrari", Description = "0 to 100km/h for 3 seconds", Price = 1000000.00M });
            context.Articles.Add(new ArticleEntity { Name = "Mustang", Description = "A lazy horse, eats lots of ...", Price = 50.00M });
            context.Articles.Add(new ArticleEntity { Name = "Junior .net developer", Description = "Is looking for a job", Price = 300.00M });
            context.Articles.Add(new ArticleEntity { Name = "Jenie from a bottle (with bottle)", Description = "Fulfills all your wishes, just take it for free ))", Price = 0.00M });
            context.Articles.Add(new ArticleEntity { Name = "Paper handkerchiefs", Description = "By a rheum it is necessary thing", Price = 1.00M });
            context.SaveChanges();
        }
    }
}
