using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Model_DTO.Infrastructure
{
    public class UrlApi
    {
        public required string Adresse { get; set; }
        public required string GetPizzas { get; set; }
        public required string AddOrUpdate { get; set; }
    }
}
