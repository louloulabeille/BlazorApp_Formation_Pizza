using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class InMemoryPizzaManager : IPizzaManager
    {
        #region private properties
        // - ConcurrentBag est utilisé pour stocker les pizzas en mémoire de manière thread-safe en cas de connexions simultanées.
        // Il permet d'ajouter et de récupérer des éléments de manière sécurisée dans un environnement multi-thread.
        private ConcurrentBag<PizzaDTO> _pizzas;
        #endregion


        #region constructeur
        public InMemoryPizzaManager()
        {
            _pizzas = 
            [
                new() { Id = 1, NomPizza = "Bacon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon", Ingredients = [], PrixPizza = 14.50, ImagePizza = "lib/Images/ImgPizza/bacon.jpg" },
                new() { Id = 2, NomPizza = "Cheese", DescriptionPizza = "Tomate, mozzarella, gorgonzola", Ingredients = [], PrixPizza = 13.80, ImagePizza = "lib/Images/ImgPizza/cheese.jpg" },
                new() { Id = 3, NomPizza = "Margherita", DescriptionPizza = "Tomate, mozzarella, basilic", Ingredients = [], PrixPizza = 12, ImagePizza = "lib/Images/ImgPizza/margherita.jpg" },
                new() { Id = 4, NomPizza = "Meaty", DescriptionPizza = "Tomate, mozzarella, basilic, viande hachée, ricotta", Ingredients = [], PrixPizza = 17, ImagePizza = "lib/Images/ImgPizza/meaty.jpg" },
                new() { Id = 5, NomPizza = "Champignon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon, Champignon de Paris", Ingredients = [], PrixPizza = 13.20, ImagePizza = "lib/Images/ImgPizza/mushroom.jpg" },
                new() { Id = 6, NomPizza = "Pepperoni", DescriptionPizza = "Tomate, mozzarella, basilic, pepperoni", Ingredients = [], PrixPizza = 13.20, ImagePizza = "lib/Images/ImgPizza/pepperoni.jpg" },
                new() { Id = 7, NomPizza = "Veggie", DescriptionPizza = "Tomate, mozzarella, basilic, aubergine, roquette", Ingredients = [], PrixPizza = 12.50, ImagePizza = "lib/Images/ImgPizza/veggie.jpg" },
            ];
        }
        #endregion


        /// <summary>
        /// method qui ajouter ou modifie une pizza en memoire
        /// </summary>
        /// <param name="pizza"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddOrUpdate(PizzaDTO pizza)
        {
            var existingPizza = _pizzas.FirstOrDefault(x => x.Id == pizza.Id);
            if (existingPizza is not null)
            {
                // -- cette méthode on retranscrit les propriétés de l'objet pizza existant avec les nouvelles valeurs de l'objet pizza passé en paramètre.
                /*existingPizza.NomPizza = pizza.NomPizza;
                existingPizza.DescriptionPizza = pizza.DescriptionPizza;
                existingPizza.PrixPizza = pizza.PrixPizza;
                existingPizza.Ingredients = pizza.Ingredients;*/
                // - on supprime toute la commection car ConcurrentBag n'a pas de méthod remove
                _pizzas = new ConcurrentBag<PizzaDTO>(_pizzas.Where(x => x.Id != pizza.Id));

            }
            /*else
            {
                _pizzas.Add(pizza);
            }*/

            _pizzas.Add(pizza);
            return Task.CompletedTask;
        }

        /// <summary>
        /// retourne une liste de recette de pizza en mémoire
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PizzaDTO>> GetPizzas()
        {
            return Task.FromResult(_pizzas.OrderBy(x => x.Id).AsEnumerable());
        }
    }
}
