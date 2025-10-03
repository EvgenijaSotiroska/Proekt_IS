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
            var response = await _httpClient.GetStringAsync("iced");

            var apiCoffees = JsonSerializer.Deserialize<List<CoffeeApiDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CoffeeApiDTO>();

            var detailedProducts = apiCoffees.Select(c => new DetailedProduct
            {
                Title = c.Title,
                Description = c.Description,
                Ingredients = c.Ingredients
                .Select(i => i.Replace("*", "")) // remove asterisks first
                .Select(i =>
                i switch
                {
                    "Coffee" => "Coffee ☕",
                    "Espresso" => "Espresso ☕",
                    "Long steeped coffee" => "Long steeped coffee ☕",
                    "Ice" => "Ice 🧊",
                    "Blended ice" => "Blended ice 🧊",
                    "Sugar" => "Sugar 🍬",
                    "Cream" => "Cream 🥛",
                    "Whip" => "Whip 🍦",
                    "Rum" => "Rum 🥃",
                    "Lemon" => "Lemon 🍋",
                    "Nitrogen bubbles" => "Nitrogen bubbles 💨", // fallback for bubbles
                    _ => i
                }).ToList(),
                Image = c.Image
            }).ToList();

            return detailedProducts;
        }

    }
}
