using System.Collections.Generic;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.BusinessLayer.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<ArticleVM> GetArticles();
        void Dispose();
    }
}
