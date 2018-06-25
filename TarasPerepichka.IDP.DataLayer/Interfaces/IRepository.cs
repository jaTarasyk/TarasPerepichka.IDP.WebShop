using System;
using System.Collections.Generic;

namespace TarasPerepichka.IDP.DataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(string str, int? id = null);
        void Create(T[] item);
        void Update(T[] item);
        void Delete(T[] item);
    }
}