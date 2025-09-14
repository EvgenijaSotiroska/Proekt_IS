using CoffeeEShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public SystemUser? Owner { get; set; }
        public virtual ICollection<ProductInOrder>? AllProducts { get; set; }
        public Order? UserOrder { get; set; }
    }
}
