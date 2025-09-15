using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DTO
{
    public class AddToOrderDTO
    {
        public Guid SelectedProductId { get; set; }
        public string? SelectedProductName { get; set; }
        public int Quantity { get; set; }
    }
}
