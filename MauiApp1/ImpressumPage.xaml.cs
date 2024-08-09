using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class ImpressumPage : ContentPage
    {
        public ImpressumPage()
        {
            InitializeComponent();
        }

        private async void OnBackToOverviewClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route1");
        }
    }
}
