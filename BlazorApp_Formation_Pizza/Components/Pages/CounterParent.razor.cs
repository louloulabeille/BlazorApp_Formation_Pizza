using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorApp_Formation_Pizza.Components.Pages
{
    public class CounterParentBase : ComponentBase
    {
        #region protected properties view
        protected int InitialCount { get; set; }
        #endregion


        #region protected methods
        protected void DefaultKey(KeyboardEventArgs e)
        {
            if (e.Key.Equals("+") && InitialCount < 20)
            {
                InitialCount++;
            }           

        }
        #endregion

        protected override void OnInitialized()
        {
            InitialCount = 0;
            base.OnInitialized();
        }
    }
}
