using BlazorApp_Formation_Pizza_Model_DTO.Infrastructure;

namespace BlazorApp_Formation_Pizza.Infrastructure.Extends
{
    public static class OptionsExtends
    {
        extension(IServiceCollection services)
        {
            /// <summary>
            /// récupération et injection des adresses de l'API
            /// </summary>
            /// <param name="configuration"></param>
            /// <returns></returns>
            public IServiceCollection AddUrlApiExtend(IConfiguration configuration)
            {
                services.AddOptions();
                services.Configure<UrlApi>(configuration.GetSection("API"));
                return services;
            }
        }
    }
}
