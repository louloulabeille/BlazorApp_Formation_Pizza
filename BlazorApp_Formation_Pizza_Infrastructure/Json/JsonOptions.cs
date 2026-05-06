using System.Text.Json;

namespace BlazorApp_Formation_Pizza_Infrastructure.Json
{
    public class JsonOptions
    {
        public static JsonSerializerOptions GetOptions()
        {
            return new()
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    }
}
