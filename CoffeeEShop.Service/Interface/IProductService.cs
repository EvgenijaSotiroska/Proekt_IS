using CoffeeEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Interface
{
    public interface  IProductService
    {
        List<Product> GetAll();
        Product? GetById(Guid id);
        Product Insert(Product product);
        Product Update(Product product);
        Product DeleteById(Guid id);
    }
}
