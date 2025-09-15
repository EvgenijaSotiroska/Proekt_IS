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
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        public OrderService (IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrder)
        {
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrder;

        }

        public Order? GetByUserId(Guid userId)
        {
            return _orderRepository.Get(selector: x => x,
                                          predicate: x => x.OwnerId.Equals(userId.ToString()));
        
        }

        public void CompleteOrder(Order order)
        {
            var items = _productInOrderRepository.GetAll(
                                                    selector: x => x,
                                                    predicate: pio => pio.OrderId == order.Id
                                                     );

            foreach (var item in items)
            {
                _productInOrderRepository.Delete(item);
            }
        }

       
    }
}
