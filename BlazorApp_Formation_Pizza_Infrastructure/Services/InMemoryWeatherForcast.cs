using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class InMemoryWeatherForcast : IWeatherForecast
    {
        public async Task<WeatherForecast[]> GetForecasts(DateTime date)
        {
            await Task.Delay(5000);

            var startDate = DateOnly.FromDateTime(date);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            WeatherForecast[] forecasts = [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })];

            return forecasts;
        }
    }
}
