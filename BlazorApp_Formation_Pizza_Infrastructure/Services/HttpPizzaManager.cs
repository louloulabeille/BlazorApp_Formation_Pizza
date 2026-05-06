using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class HttpPizzaManager : IPizzaManager
    {
        public Task AddOrUpdate(PizzaDTO pizza)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PizzaDTO>> GetPizzas()
        {
            throw new NotImplementedException();
        }
    }
}
