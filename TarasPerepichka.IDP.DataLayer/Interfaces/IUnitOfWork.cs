using System;
using TarasPerepichka.IDP.DataLayer.Entitties;

namespace TarasPerepichka.IDP.DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ArticleEntity> ArticleEntities { get; }
        void Save();
    }
}