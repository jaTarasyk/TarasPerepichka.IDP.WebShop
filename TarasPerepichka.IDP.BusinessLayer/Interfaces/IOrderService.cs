using System;
using System.Collections.Generic;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.BusinessLayer.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderVM> GetOrders(string userRef);
        bool OrderItem(int articleId, string userRef);
        bool SaveOrders(List<OrderVM> orders);
        void Dispose();
    }
}
