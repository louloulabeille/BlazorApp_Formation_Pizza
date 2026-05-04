using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class FetchDataBase : ComponentBase
    {
        #region properties protected view 
        protected DateTime DateSaisie { get; set; } = DateTime.Today;
        protected WeatherForecast[]? Forecasts = null;
        #endregion

        #region properties private injection de dépendance
        [Inject]
        private IWeatherForecast? _forecastService { get; set; }
        #endregion

        #region protected method
        protected async Task DateClicked()
        {
            Forecasts = await _forecastService!.GetForecasts(DateSaisie);
        }

        protected override async Task OnInitializedAsync()
        {
            await DateClicked();
            await base.OnInitializedAsync();
        }

        #endregion
    }
}
