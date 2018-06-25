using System;
using TarasPerepichka.IDP.DataLayer.Entitties;

namespace TarasPerepichka.IDP.DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<ArticleEntity> Articles { get; }
        IRepository<OrderEntity> Orders { get; }
        //void Save();
    }
}