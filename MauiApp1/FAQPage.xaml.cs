using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class FAQPage : ContentPage
    {
        public FAQPage()
        {
            InitializeComponent();
        }

        private async void OnBackToOverviewClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route1");
        }
    }
}
