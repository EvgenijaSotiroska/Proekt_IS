using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Repository.Interface;
using CoffeeEShop.Service.Interface;
using CoffeeEShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IOrderService _orderService;

        public ProductService(IRepository<Product> productRepository, IOrderService orderService, IRepository<ProductInOrder> productInOrderRepository)
        {
            _productRepository = productRepository;
            _orderService = orderService;
            _productInOrderRepository = productInOrderRepository;
        }

        public void AddProductToOrder(Guid id, Guid userId, int quantity)
        {
            var order = _orderService.GetByUserId(userId);

            if (order == null)
            {
                throw new Exception("Order doesn't exist.");
            }

            var product = GetById(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            UpdateCartItem(product, order, quantity);

        }
        private void UpdateCartItem(Product product, Order order, int quantity)
        {
            var existingProduct = GetProductInOrder(product.Id, order.Id);

            if (existingProduct == null)
            {
                var productInShoppingCart = new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    OrderId = order.Id,
                    Product = product,
                    Order = order,
                    Quantity = quantity
                };

                _productInOrderRepository.Insert(productInShoppingCart);
            }
            else
            {
                existingProduct.Quantity += quantity;
                _productInOrderRepository.Update(existingProduct);
            }
        }
        private ProductInOrder? GetProductInOrder(Guid productId, Guid orderId)
        {
            return _productInOrderRepository.Get(selector: x => x,
                predicate: x => x.OrderId.ToString() == orderId.ToString()
                                                && x.ProductId.ToString() == productId.ToString());
        }

        public AddToOrderDTO GetSelectedProduct(Guid id)
        {
            var selectedProduct = GetById(id);

            var addProductToOrderModel = new AddToOrderDTO
            {
                SelectedProductId = selectedProduct.Id,
                SelectedProductName = selectedProduct.Name,
                Quantity = 1
            };

            return addProductToOrderModel;
        }
        public Product DeleteById(Guid id)
        {
            var product = GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _productRepository.Delete(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll(selector: x => x).ToList();
        }

        public Product? GetById(Guid id)
        {
            return _productRepository.Get(selector: x => x,
                                          predicate: x => x.Id.Equals(id));
        }

        public Product Insert(Product product)
        {
            product.Id = Guid.NewGuid();
            return _productRepository.Insert(product);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
