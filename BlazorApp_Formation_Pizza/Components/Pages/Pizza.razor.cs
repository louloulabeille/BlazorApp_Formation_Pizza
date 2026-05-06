using BlazorApp_Formation_Pizza_Interface.Services;
using BlazorApp_Formation_Pizza_Model_DTO;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class PizzaBase : ComponentBase
    {
        #region private properties
        private bool _admin = false;
        #endregion


        #region protected Properties view
        protected List<PizzaDTO> PizzaList = [];
        protected List<PizzaDTO> Panier = [];
        protected bool Loading = true;
        protected bool Admin { get { return _admin; } set { if(!_admin) Pizza = null; _admin = value; } }
        protected PizzaDTO? Pizza;  // - pizza à modifier

        // - test avec un tableau d'ingredients on prend une propertie en intermédiaire pour l'affichage dans le formulaire et l'enregistrement
        /*protected string Ingredients
        { 
            get {
                return Pizza is not null ? string.Join(", ", Pizza.Ingredients) : string.Empty;
            } 
            set {
                if (Pizza is not null)
                    Pizza.Ingredients = value.Split(", ").Select(x => x.Trim()).ToArray(); ; 
            } 
        }*/
        #endregion

        #region private inject properties
        [Inject]
        private IPizzaManager _pizzaManager { get; set; } = default!;
        [Inject]
        private IPanierManager _panierManager { get; set; } = default!;

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
            Panier.AddRange(await _panierManager.GetPanier());
            await base.OnInitializedAsync();
        }
        #endregion

        #region protected methods view
        /// <summary>
        /// sauvegarde la pizza dans la liste pour le moment après modification
        /// </summary>
        protected void SavePizza()
        {
            var piz= PizzaList.Find(x=> x.Id == Pizza!.Id);
            if (piz is not null && Pizza is not null)
            {
                _pizzaManager.AddOrUpdate(Pizza);
                piz.NomPizza            = Pizza.NomPizza;
                piz.DescriptionPizza    = Pizza.DescriptionPizza;
                piz.PrixPizza           = Pizza.PrixPizza;
                MiseAJourPanier();
            }
            Pizza = null;
        }

        /// <summary>
        /// méthod qui permet d'ajouter une pizza au panier et de mettre à jour le prix total
        /// </summary>
        /// <param name="pizza"></param>
        protected async Task AddToCart(PizzaDTO pizza)
        {
            await _panierManager.AddOrUpdate(pizza);
            Panier.Add(pizza);
        }

        /// <summary>
        /// ajoute une pizza en mode modification dans la variable Pizza pour l'afficher dans le formulaire
        /// </summary>
        /// <param name="pizza"></param>
        protected void EditPizza(PizzaDTO pizza)
        {
            Pizza = new() { 
                Id          = pizza.Id,
                NomPizza    = pizza.NomPizza,
                DescriptionPizza = pizza.DescriptionPizza,
                PrixPizza   = pizza.PrixPizza,
                Ingredients = pizza.Ingredients,
                ImagePizza  = pizza.ImagePizza
            };
        }

        /// <summary>
        /// supprimer la pizza du panier et mettre à jour le prix total
        /// </summary>
        /// <param name="pizza"></param>
        protected async Task RemoveFromCart(PizzaDTO pizza)
        {
            await _panierManager.Remove(pizza);
            Panier.Remove(pizza);

            Panier.Clear();
            Panier.AddRange(await _panierManager.GetPanier());
        }

        /// <summary>
        /// copie la description de la pizza dans la variable Ingredients pour l'afficher dans le formulaire
        /// </summary>
        /// <param name="description"></param>
        /*protected void Copie(string description)
        {
            Ingredients = description;
        }*/

        #endregion

        #region private methods
        /// <summary>
        /// method qui créer les pizzas il faudrat que je mettes en place une Api
        /// et faire appel a cette api pour récupérer les pizzas+
        /// </summary>
        /// <returns></returns>
        private async Task<List<PizzaDTO>> GetPizzas()
        {
            var pizzas = await _pizzaManager.GetPizzas();
            Loading = false;
            /* _ = Task.Delay(10000).ContinueWith(_ => 
             { 
                 Loading = false; 
                 InvokeAsync(StateHasChanged);
             });*/

            return [.. pizzas];
        }

        /// <summary>
        /// Méthod qui met à jour les pizzas du panier en fonction des modifications apportées à la liste des pizzas
        /// </summary>
        private void MiseAJourPanier()
        {
            for(int i = 0; i < Panier.Count; i++)
            {
                Panier[i] = PizzaList.Find(x => x.Id == Panier[i].Id)!;
            }
            
        }
        #endregion
    }
}
