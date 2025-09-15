using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DomainModels
{
    public class Shop : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public double? Rating { get; set; }
        public virtual ICollection<ProductInCoffeeShop>? AllProducts { get; set; }
    }
}
