using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DomainModels
{
    public class ProductInCoffeeShop
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid CoffeeShopId { get; set; }
        public CoffeeShop? CoffeeShop { get; set; }
    }
}
