using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp_Formation_Pizza_Infrastructure.Services
{
    public class InMemoryPizzaManager : IPizzaManager
    {
        /// <summary>
        /// retourne une liste de recette de pizza en mémoire
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<PizzaDTO>> GetPizzas()
        {
            return Task.FromResult( new List<PizzaDTO>
            {
                new (){ Id = 1, NomPizza = "Bacon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon", Ingredients = [] ,PrixPizza = 14.50, ImagePizza = "lib/Images/ImgPizza/bacon.jpg" },
                new (){ Id = 2, NomPizza = "Cheese", DescriptionPizza = "Tomate, mozzarella, gorgonzola", Ingredients = [] ,PrixPizza = 13.80, ImagePizza = "lib/Images/ImgPizza/cheese.jpg" },
                new (){ Id = 3, NomPizza = "Margherita", DescriptionPizza = "Tomate, mozzarella, basilic", Ingredients = [] ,PrixPizza = 12, ImagePizza = "lib/Images/ImgPizza/margherita.jpg" },
                new (){ Id = 4, NomPizza = "Meaty", DescriptionPizza = "Tomate, mozzarella, basilic, viande hachée, ricotta",Ingredients = [] , PrixPizza = 17, ImagePizza = "lib/Images/ImgPizza/meaty.jpg" },
                new (){ Id = 5, NomPizza = "Champignon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon, Champignon de Paris", Ingredients = [] ,PrixPizza = 13.20, ImagePizza = "lib/Images/ImgPizza/mushroom.jpg" },
                new (){ Id = 6, NomPizza = "Pepperoni", DescriptionPizza = "Tomate, mozzarella, basilic, pepperoni", Ingredients = [] ,PrixPizza = 13.20, ImagePizza = "lib/Images/ImgPizza/pepperoni.jpg" },
                new (){ Id = 7, NomPizza = "Veggie", DescriptionPizza = "Tomate, mozzarella, basilic, aubergine, roquette", Ingredients = [] ,PrixPizza = 12.50, ImagePizza = "lib/Images/ImgPizza/veggie.jpg" },
            }.AsEnumerable());
        }
    }
}
