using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Domain.DTO
{
    public class CoffeeApiDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Ratio { get; set; }
        public Dictionary<string, IngredientRange> Ingredients { get; set; }
        public bool? Cold { get; set; }
        public CaffeineRange Caffeine { get; set; }
        public string Pressure { get; set; }
        public string Temperature { get; set; }
        public string TemperatureF { get; set; }
        public string BrewTime { get; set; }
        public string Preparation { get; set; }
        public string Image { get; set; }
    }

    public class IngredientRange
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class CaffeineRange
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }
}
