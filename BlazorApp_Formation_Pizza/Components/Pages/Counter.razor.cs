using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
            Name = Name is null ? "Hanna" : Name;
            base.OnParametersSet();
        }

        #endregion

        #region public Methods view
        /// <summary>
        /// method qui incrémente de 3 le compteur, et qui affiche une pop up lorsque le compteur arrive à 18 ou plus
        /// method qui n'est appelé qu'en javascript
        /// Attention bien mettre la methode en public et ajouter l'attribut [JSInvokable] 
        /// pour pouvoir l'appeler depuis le javascript, sinon elle ne sera pas accessible depuis le javascript
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task JSIncrementBy3()
        {
            try
            {
                CurrentCount += 3;
                // - pour forcer le rafraichissement de la page après l'incrémentation,
                // car cette méthode est appelée depuis le javascript et le rendu ne se fait pas automatiquement
                await InvokeAsync(StateHasChanged);
                if (CurrentCount >= 18)
                    await JSRuntime.InvokeVoidAsync("displayAlert", CurrentCount.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region protected Methods view
        /// <summary>
        /// method d'incrementation
        /// </summary>
        /// <param name="args">paramètre qui prend les informations de l'événement de la souris</param>
        /// <returns></returns>
        protected async Task IncrementCount(MouseEventArgs args) 
        {
            try
            {
                if (args.AltKey) // - si la touche alt est enfoncée, on incrémente de 2
                    CurrentCount += 2;
                else if (CurrentCount < 20)
                    CurrentCount++;

                if (CurrentCount >= 18)
                    await JSRuntime.InvokeVoidAsync("displayAlert", CurrentCount.ToString());
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region protected override methods
        /// <summary>
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if ( firstRender )
            {
                // - appel a une classe qui va créer un composant pouvant être passé à un dom javascript
                var thisRef = DotNetObjectReference.Create(this);
                // appel de la method javascript qui va stocker cet object dans une variable javascript
                await JSRuntime.InvokeVoidAsync("storeCounterReference", thisRef);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        #endregion
    }
}
