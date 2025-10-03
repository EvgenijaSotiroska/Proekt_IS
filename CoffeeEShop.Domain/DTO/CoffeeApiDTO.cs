using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DTO
{
    public class CoffeeApiDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public List<string> Ingredients { get; set; } 
        public string Image { get; set; } 
    }

}
