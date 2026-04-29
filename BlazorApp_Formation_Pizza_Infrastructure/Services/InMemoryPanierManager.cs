using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class InMemory_panierManager : I_panierManager
    {
        #region private properties
        private ConcurrentBag<PizzaDTO> _panier = [];
        #endregion

        public Task AddOrUpdate(PizzaDTO pizza)
        {
            var p = _panier.FirstOrDefault(p => p.Id == pizza.Id);
            if (p is not null) { _panier =  new ConcurrentBag<PizzaDTO>(_panier.Where(x=> x.Id != pizza.Id)); }

            _panier.Add(pizza);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<PizzaDTO>> GetPanier()
        {
            return Task.FromResult(_panier.AsEnumerable());
        }

        public Task Remove(PizzaDTO pizza)
        {
            _panier = new ConcurrentBag<PizzaDTO>(_panier.Where(x => x.Id != pizza.Id));
            return Task.CompletedTask;
        }
    }
}
