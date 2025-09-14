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
        List<Order> GetAllOrders();
        Order GetOrder(Guid Id);
    }
}
