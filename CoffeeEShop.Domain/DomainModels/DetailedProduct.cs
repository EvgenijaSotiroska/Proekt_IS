using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DomainModels
{
    public class DetailedProduct : BaseEntity
    {
        public string DisplayName { get; set; }      
        public string Origin { get; set; }                     
        public string IngredientsDescription { get; set; } 
        public string CaffeineInfo { get; set; }        
        public string Served { get; set; }          
    }
}
