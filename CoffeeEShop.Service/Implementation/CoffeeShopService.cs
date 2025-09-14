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
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly IRepository<CoffeeShop> _coffeeShopRepository;

        public CoffeeShopService(IRepository<CoffeeShop> coffeeShopRepository)
        {
            _coffeeShopRepository = coffeeShopRepository;
        }

        public CoffeeShop DeleteById(Guid id)
        {
            var coffeeShop = GetById(id);
            if (coffeeShop == null)
            {
                throw new Exception("Coffee shop not found");
            }
            _coffeeShopRepository.Delete(coffeeShop);
            return coffeeShop;
        }

        public List<CoffeeShop> GetAll()
        {
            return _coffeeShopRepository.GetAll(selector: x => x).ToList();
        }

        public CoffeeShop? GetById(Guid id)
        {
            return _coffeeShopRepository.Get(selector: x => x,
                                         predicate: x => x.Id.Equals(id));
        }

        public CoffeeShop Insert(CoffeeShop shop)
        {
            shop.Id = Guid.NewGuid();
            return _coffeeShopRepository.Insert(shop);
        }

        public CoffeeShop Update(CoffeeShop shop)
        {
            return _coffeeShopRepository.Update(shop);
        }
    }
}
