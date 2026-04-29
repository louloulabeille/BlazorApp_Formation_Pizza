using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Interface.Services
{
    public interface I_pizzaManager
    {
        public Task<IEnumerable<PizzaDTO>> GetPizzas();
        public Task AddOrUpdate(PizzaDTO pizza);
    }
}
