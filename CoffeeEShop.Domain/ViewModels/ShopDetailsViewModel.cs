using CoffeeEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.ViewModels
{
    public class ShopDetailsViewModel
    {
        public Shop Shop { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
