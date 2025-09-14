using CoffeeEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Interface
{
    public interface ICoffeeShopService
    {
        List<CoffeeShop> GetAll();
        CoffeeShop? GetById(Guid id);
        CoffeeShop Insert(CoffeeShop shop);
        CoffeeShop Update(CoffeeShop shop);
        CoffeeShop DeleteById(Guid id);
    }
}
