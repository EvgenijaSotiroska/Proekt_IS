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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
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
