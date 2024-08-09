using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private DataManager _dataManager;

        public MainPage()
        {
            InitializeComponent();
            _dataManager = new DataManager();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            using (var database = new Database())
            {
                int loggedInUserId = await App.DataManager.GetLoggedInUserId(username, password);

                if (loggedInUserId != -1)
                {
                    App.DataManager.SetLoggedInUserId(loggedInUserId);
                    await Shell.Current.GoToAsync("route1");
                }
                else
                {
                    await DisplayAlert("Error", "Invalid login credentials. Please check username and password.", "OK");
                }
            }
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route3");
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route3");
        }
    }
}
