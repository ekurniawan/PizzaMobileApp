using BlazingPizzaApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace BlazingPizzaApp.Data
{
    public class PizzaSpecialsServices : IPizzaSpecials
    {
        private readonly HttpClient _httpClient;

        public PizzaSpecialsServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PizzaSpecial>> GetPizzaSpecialsAsync()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Network Error", "No network connectivity. Skipping pizza specials request.", "OK");
                return new List<PizzaSpecial>();
            }

            try
            {
                var specials = await _httpClient.GetFromJsonAsync<List<PizzaSpecial>>("api/Specials");                return specials ?? new List<PizzaSpecial>();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error retrieving pizza specials: {ex.Message}");
                throw;
            }
        }

        public async Task<int> PlaceOrderAsync(Order order)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Network Error", "No network connectivity. Cannot place order.", "OK");
                throw new InvalidOperationException("No network connectivity.");
            }

            var response = await _httpClient.PostAsJsonAsync("orders", order);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
