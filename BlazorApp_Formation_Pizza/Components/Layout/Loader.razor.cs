using Microsoft.AspNetCore.Components;

namespace BlazorApp_Formation_Pizza.Components.Layout
{
    public class LoaderBase : ComponentBase, IDisposable
    {
        #region public properties Parameters
        // passage du parent d'un composant html pour afficher dans le loader en cas de forte attente
        // obligation de faire un RenderFragment pour pouvoir passer du html dans le composant avec pour nom ChildContent
        // , c'est une convention pour les composants qui ont du html à afficher
        [Parameter] 
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public int TimeOut { get; set; } = 1000; // - temps d'attente avant d'affichier le message d'attente prolongé

        #endregion

        #region protected properties view
        protected bool ShowLoaderMessage { get; set; } = false; // - bool pour afficher ou non le loader
        protected CancellationTokenSource Cts { get; set; } = new(); // - token pour annuler le timer si le composant est détruit avant la fin du timer

        public void Dispose()
        {
            Cts.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion


        #region protected override methods
        /// <summary>
        /// Après le rendu des composants on lance le timer pour afficher le message d'attente prolongé, si le composant est détruit avant la fin du timer,
        /// on annule le timer pour éviter d'afficher le message d'attente prolongé
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _ = await Task.Delay(TimeOut, Cts.Token).ContinueWith(async t =>
                {
                    if(t.IsCompletedSuccessfully)
                    {
                        ShowLoaderMessage = true;
                        await InvokeAsync(StateHasChanged);
                    }
                }); // - après le temps d'attente, on affiche le message d'attente prolongé
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion
    }
}
