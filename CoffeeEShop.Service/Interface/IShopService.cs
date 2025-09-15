using CoffeeEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Interface
{
    public interface IShopService
    {
        List<Shop> GetAll();
        Shop? GetById(Guid id);
        Shop Insert(Shop shop);
        Shop Update(Shop shop);
        Shop DeleteById(Guid id);
    }
}
