using CoffeeEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Interface
{
    public interface IOrderService
    {
        Order? GetByUserId(Guid userId);
        void CompleteOrder(Order order);
        Order Update(Order order);
        string? GetStatus(Guid orderId);
        bool UpdateStatus(Guid orderId, string status);
        IEnumerable<Order> GetAllNonDeliveredOrders();
        Order? GetById(Guid id);
    }
}
