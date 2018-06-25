using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TarasPerepichka.IDP.BusinessLayer.Interfaces;
using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.DataLayer.Interfaces;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.BusinessLayer.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork dataBase;

        public ArticleService(IUnitOfWork unit)
        {
            dataBase = unit;
        }

        public IEnumerable<ArticleVM> GetArticles()
        {
            List<ArticleEntity> entities = dataBase.Articles.GetAll().ToList();
            return Mapper.Map<List<ArticleEntity>, List<ArticleVM>>(entities, opts => opts.ConfigureMap(MemberList.Destination));
        }
    }
}
