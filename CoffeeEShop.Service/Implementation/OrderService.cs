using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Repository.Interface;
using CoffeeEShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderService (IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order? GetByUserId(Guid userId)
        {
            return _orderRepository.Get(selector: x => x,
                                          predicate: x => x.OwnerId.Equals(userId.ToString()));
        
        }

        public Order GetOrder(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
