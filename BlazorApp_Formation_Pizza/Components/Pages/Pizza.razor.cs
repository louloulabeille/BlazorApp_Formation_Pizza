using BlazorApp_Formation_Pizza_Model_DTO;
using Microsoft.AspNetCore.Components;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class PizzaBase : ComponentBase
    {
        #region protected Properties view
        protected List<PizzaDTO> PizzaList = [];
        protected List<PizzaDTO> Panier = [];
        protected bool Loading = true;
        protected bool Admin = false;
        protected PizzaDTO? Pizza;  // - pizza à modifier
        protected double PrixTotal = 0.0;
        #endregion

        #region override methods
        /// <summary>
        /// méthod d'initialisation de la fenêtre
        /// charge les datas de la pizza
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            PizzaList.AddRange(await GetPizzas());
            
            await base.OnInitializedAsync();
        }
        #endregion

        #region protected methods view
        /// <summary>
        /// sauvegarde la pizza dans la liste pour le moment après modification
        /// </summary>
        protected void SavePizza()
        {
            int index = PizzaList.FindIndex(x=> x.Id == Pizza!.Id);
            PizzaList[index] = Pizza!;

            Pizza = null;
        }

        /// <summary>
        /// méthod qui permet d'ajouter une pizza au panier et de mettre à jour le prix total
        /// </summary>
        /// <param name="pizza"></param>
        protected void AddToCart(PizzaDTO pizza)
        {
            Panier.Add(pizza);
            PrixTotal += pizza.PrixPizza;
        }

        /// <summary>
        /// ajoute une pizza en mode modification dans la variable Pizza pour l'afficher dans le formulaire
        /// </summary>
        /// <param name="pizza"></param>
        protected void EditPizza(PizzaDTO pizza)
        {
            Pizza = pizza;
        }

        /// <summary>
        /// supprimer la pizza du panier et mettre à jour le prix total
        /// </summary>
        /// <param name="pizza"></param>
        protected void RemoveFromCart(PizzaDTO pizza)
        {
            Panier.Remove(pizza);
            PrixTotal -= pizza.PrixPizza;
        }
        #endregion

        #region private methods
        /// <summary>
        /// method qui créer les pizzas il faudrat que je mettes en place une Api
        /// et faire appel a cette api pour récupérer les pizzas+
        /// </summary>
        /// <returns></returns>
        private async Task<List<PizzaDTO>> GetPizzas()
        {
            var pizzas = new List<PizzaDTO>
            {
                new (){ Id = 1, NomPizza = "Bacon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon", PrixPizza = 14.50, ImagePizza = "bacon.jpg" },
                new (){ Id = 2, NomPizza = "Cheese", DescriptionPizza = "Tomate, mozzarella, gorgonzola", PrixPizza = 13.80, ImagePizza = "cheese.jpg" },
                new (){ Id = 3, NomPizza = "Margherita", DescriptionPizza = "Tomate, mozzarella, basilic", PrixPizza = 12, ImagePizza = "margherita.jpg" },
                new (){ Id = 4, NomPizza = "Meaty", DescriptionPizza = "Tomate, mozzarella, basilic, viande hachée, ricotta", PrixPizza = 17, ImagePizza = "meaty.jpg" },
                new (){ Id = 5, NomPizza = "Champignon", DescriptionPizza = "Tomate, mozzarella, basilic, Bacon, Champignon de Paris", PrixPizza = 13.20, ImagePizza = "mushroom.jpg" },
                new (){ Id = 6, NomPizza = "Pepperoni", DescriptionPizza = "Tomate, mozzarella, basilic, pepperoni", PrixPizza = 13.20, ImagePizza = "pepperoni.jpg" },
                new (){ Id = 7, NomPizza = "Veggie", DescriptionPizza = "Tomate, mozzarella, basilic, aubergine, roquette", PrixPizza = 12.50, ImagePizza = "veggie.jpg" },
            };

            _ = Task.Delay(2000).ContinueWith(_ => 
            { 
                Loading = false; 
                InvokeAsync(StateHasChanged);
            });

            return pizzas;
        }
        #endregion
    }
}
