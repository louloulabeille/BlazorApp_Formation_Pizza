using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Interface.Services
{
    public interface IPizzaManager
    {
        public Task<IEnumerable<PizzaDTO>> GetPizzas();
    }
}
