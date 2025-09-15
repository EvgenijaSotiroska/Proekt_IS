using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DTO
{
    public class OrderDTO
    {
        public Guid SelectedProductId { get; set; }
        public string? SelectedProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total => Price * Quantity;
    }
}
