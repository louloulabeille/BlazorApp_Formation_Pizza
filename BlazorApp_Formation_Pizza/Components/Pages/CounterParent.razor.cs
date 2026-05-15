using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class CounterParentBase : ComponentBase
    {
        #region protected properties view
        protected int InitialCount { get; set; }
        #endregion

        #region inject services
        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;
        #endregion


        #region protected methods
        protected void DefaultKey(KeyboardEventArgs e)
        {
            if (e.Key.Equals("+") && InitialCount < 20)
            {
                InitialCount++;
            }
            if (InitialCount>= 17) 
                JSRuntime.InvokeVoidAsync("displayAlert", InitialCount.ToString());

        }
        #endregion

        #region protected override methods
        protected override void OnInitialized()
        {
            InitialCount = 0;
            base.OnInitialized();
        }

        /// <summary>
        /// methode qui attend que tous les élements de la fenêtre soit chargée pour faire des appels au javascript, 
        /// c'est à dire que si on veut faire des appels au javascript pour récupérer des données ou faire des actions sur la page,
        /// c'est dans cette méthode qu'on doit les faire
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender) // - premier rendu
                {
                    // Call JavaScript function to ask for initial value
                    var result = await JSRuntime.InvokeAsync<string>("askInitial");
                    if (int.TryParse(result, out int parsedValue))
                    {
                        InitialCount = parsedValue;
                        // - rafraichie la page pour afficher la nouvelle valeur comme le rendu est déjà fait,
                        // on doit forcer le rafraichissement de la page pour afficher la nouvelle valeur
                        await this.InvokeAsync(StateHasChanged);                     
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling JavaScript: {ex.Message}");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        /*protected async override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                // Call JavaScript function to ask for initial value
                InitialCount = await JSRuntime.InvokeAsync<int>("askinitial");
            }
            base.OnAfterRender(firstRender);
        }*/

        #endregion
    }
}
