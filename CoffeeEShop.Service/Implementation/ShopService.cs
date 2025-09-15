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
    public class ShopService : IShopService
    {
        private readonly IRepository<Shop> _coffeeShopRepository;

        public ShopService(IRepository<Shop> coffeeShopRepository)
        {
            _coffeeShopRepository = coffeeShopRepository;
        }

        public Shop DeleteById(Guid id)
        {
            var coffeeShop = GetById(id);
            if (coffeeShop == null)
            {
                throw new Exception("Coffee shop not found");
            }
            _coffeeShopRepository.Delete(coffeeShop);
            return coffeeShop;
        }

        public List<Shop> GetAll()
        {
            return _coffeeShopRepository.GetAll(selector: x => x).ToList();
        }

        public Shop? GetById(Guid id)
        {
            return _coffeeShopRepository.Get(selector: x => x,
                                         predicate: x => x.Id.Equals(id));
        }

        public Shop Insert(Shop shop)
        {
            shop.Id = Guid.NewGuid();
            return _coffeeShopRepository.Insert(shop);
        }

        public Shop Update(Shop shop)
        {
            return _coffeeShopRepository.Update(shop);
        }
    }
}
