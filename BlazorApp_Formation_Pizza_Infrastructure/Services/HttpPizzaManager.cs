using BlazorApp_Formation_Pizza_Infrastructure.Json;
using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using BlazorApp_Formation_Pizza_Model_DTO.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class HttpPizzaManager (IOptions<UrlApi> options, HttpClient client) : IPizzaManager
    {
        #region private readonly properties
        private readonly IOptions<UrlApi> _options = options;
        private readonly HttpClient _httpClient = client;
        #endregion

        public Task AddOrUpdate(PizzaDTO pizza)
        {
            try
            {
                JsonContent content = JsonContent.Create(pizza);
                using var result = _httpClient.PostAsync(_options.Value.AddOrUpdate, content);
            }
            catch ( Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - HttpPizzaManager - AddOrUpdate - {ex.Message}");
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<PizzaDTO>> GetPizzas()
        {
            try
            {
                using var result = await _httpClient.GetAsync(_options.Value.GetPizzas);

                if (result.IsSuccessStatusCode)
                {
                    string retour = await result.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<IEnumerable<PizzaDTO>>(retour, JsonOptions.GetOptions()) ??[];
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"HttpPizzaManager - GetPizzas - {ex.Message}");
            }

            return [];
        }
    }
}
