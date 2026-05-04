using BlazorApp_Formation_Pizza_Model_DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Formation_Pizza.Components.Layout
{
    /// <summary>
    /// mise en place du bind two-way Parent vers enfant et vice versa
    /// </summary>
    public class TableWeatherBase : ComponentBase
    {
        #region parameter properties public
        [Parameter]
        public WeatherForecast[]? Forecasts { get; set; } = null;
        #endregion

        #region parameter properties 
        [Parameter]
        public EventCallback<WeatherForecast[]?> ForecastsChanged { get; set; }
        #endregion


        #region protected view method
        public void OnClickClear () { 
            Forecasts = null;
            ForecastsChanged.InvokeAsync(Forecasts);
        }

        #endregion
    }
}
