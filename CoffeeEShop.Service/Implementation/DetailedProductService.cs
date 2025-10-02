using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Implementation
{
    public class DetailedProductService
    {
        private readonly HttpClient _httpClient;

        public DetailedProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DetailedProduct>> GetAllCoffeesAsync()
        {
            var response = await _httpClient.GetAsync("coffee");
            if (!response.IsSuccessStatusCode)
                return new List<DetailedProduct>();

            var json = await response.Content.ReadAsStringAsync();

            var apiList = JsonSerializer.Deserialize<List<CoffeeApiDTO>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiList == null) return new List<DetailedProduct>();

            var coffeeDetails = apiList.Select(api =>
            {
                static string ToTitle(string s)
                {
                    if (string.IsNullOrEmpty(s)) return s;
                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.Replace("_", " "));
                }

                // Filter out ingredients with 0 g
                var ingredientsDesc = "—";
                if (api.Ingredients != null && api.Ingredients.Any())
                {
                    var nonZero = api.Ingredients
                        .Where(kvp => kvp.Value.Min > 0 || kvp.Value.Max > 0)
                        .ToList();

                    if (nonZero.Any())
                    {
                        ingredientsDesc = string.Join(" + ", nonZero.Select(kvp =>
                        {
                            var ingName = ToTitle(kvp.Key);
                            var r = kvp.Value;
                            return Math.Abs(r.Min - r.Max) < 0.0001
                                ? $"{r.Min} g {ingName}"
                                : $"{r.Min}–{r.Max} g {ingName}";
                        }));
                    }
                }

                var caffeineInfo = api.Caffeine != null
                    ? $"{api.Caffeine.Min}–{api.Caffeine.Max} mg"
                    : "—";

                var served = api.Cold == true ? "Cold" : "Hot";

                return new DetailedProduct
                {
                    Id = Guid.NewGuid(),
                    DisplayName = api.Name ?? ToTitle(api.Id),
                    Origin = string.IsNullOrWhiteSpace(api.Origin) ? "—" : ToTitle(api.Origin),
                    IngredientsDescription = ingredientsDesc,
                    CaffeineInfo = caffeineInfo,
                    Served = served
                };
            }).ToList();

            return coffeeDetails;
        }

    }
}
