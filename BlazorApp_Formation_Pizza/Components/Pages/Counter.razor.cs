using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class CounterBase : ComponentBase
    {
        #region protected Properties view
        protected int CurrentCount;
        #endregion

        #region properties parameters
        /// <summary>
        /// passage en get de type https://localhost:port/counter/5/loulou
        /// </summary>
        [Parameter]
        //[SupplyParameterFromQuery(Name="InitCount")] // -> pour avoir cet url https://localhost:port/counter?InitCount=5&Name=loulou 
        public int? InitCount { get; set; }
        [Parameter]
        //[SupplyParameterFromQuery(Name="Name")] // -> mais le probleme il faut enlever les autres route dans la page razor /Counter/{InitCount:int}/{Name}
        public string? Name { get; set; }
        #endregion

        #region inject services
        // - injection pour utliser le js runtime pour faire des appels au javascript
        // - mise en place de 2 choses une pop up lorsque incrémentation arrive vers la limite
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;
        #endregion

        #region override methods

        /// <summary>
        /// method d'initialisation de la fenêtre appelée avant même que les éléments s'affichent
        /// </summary>
        protected override void OnInitialized()
        {
            CurrentCount = InitCount??0;
            Name = Name;
            base.OnInitialized();
        }

        /// <summary>
        /// méthod override qui est appelée à chaque fois que les paramètres sont mis à jour,
        /// c'est à dire à chaque fois que l'on change la valeur de InitCount ou Name
        /// si no veut faire des actions à chaque fois que les paramètres sont mis à jour, on peut les faire ici
        /// </summary>
        protected override void OnParametersSet()
        {
            CurrentCount = InitCount ?? 0;
            Name = Name ?? "Hanna Oberg";
            base.OnParametersSet();
        }

        #endregion


        #region protected Methods view
        protected void IncrementCount()
        {
            CurrentCount++;
        }
        #endregion
    }
}
