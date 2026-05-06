using BlazorApp_Formation_Pizza_Infrastructure.Services;
using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO.Infrastructure;
using Microsoft.Extensions.Options;
using Polly;

namespace BlazorApp_Formation_Pizza.Infrastructure.Extends
{
    public static class HttpClientExtends
    {
        extension (IServiceCollection services)
        {
            /// <summary>
            /// method pour ajouter l'injection de dépendance pour la liaison de donnée par HttpClient
            /// </summary>
            /// <returns></returns>
            public  IServiceCollection AddHttpClientExtend()
            {
                services.AddHttpClient<IPizzaManager, HttpPizzaManager>((serviceProvider, client) => {
                    var options = serviceProvider.GetRequiredService<IOptions<UrlApi>>();
                    client.BaseAddress = new Uri(options.Value.Adresse);

                })
                    .AddTransientHttpErrorPolicy(policyBuilder =>policyBuilder.WaitAndRetryAsync(
                        retryCount: 5,
                        retryNumber => TimeSpan.FromMilliseconds(50 + retryNumber*150))); ;
                return services;
            }
        }
    }
}
