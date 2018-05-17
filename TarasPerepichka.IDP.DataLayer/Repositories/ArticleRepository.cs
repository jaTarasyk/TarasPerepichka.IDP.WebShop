using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TarasPerepichka.IDP.DataLayer.DataContext;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;

namespace TarasPerepichka.IDP.DataLayer.Repositories
{
    public class ArticleRepository : IRepository<ArticleEntity>
    {
        private readonly ArticlesContext db;
        
        public ArticleRepository(ArticlesContext context)
        {
            db = context;
        }

        public void Create(ArticleEntity item)
        {
            db.Articles.Add(item);
        }

        public void Delete(int id)
        {
            ArticleEntity article = db.Articles.Find(id);
            if (article != null)
            {
                db.Articles.Remove(article);
            }
        }

        public IEnumerable<ArticleEntity> Find(Func<ArticleEntity, bool> predicate)
        {
            return db.Articles.Where(predicate).ToList();
        }

        public ArticleEntity Get(int id)
        {
            return db.Articles.Find(id);
        }

        public IEnumerable<ArticleEntity> GetAll()
        {
            return db.Articles;
        }

        public void Update(ArticleEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
