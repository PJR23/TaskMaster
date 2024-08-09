using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiApp1
{
    public partial class NewPage1 : ContentPage
    {
        private StackLayout tasksStackLayout;

        public NewPage1()
        {
            InitializeComponent();
            tasksStackLayout = new StackLayout
            {
                Padding = new Thickness(20, 20, 20, 20),
                Spacing = 10
            };
            Content = tasksStackLayout;
            ShowTasksAsync();
        }

        private async void ShowTasksAsync()
        {
            int loggedInUserId = App.DataManager.GetLoggedInUserId();

            if (loggedInUserId > 0)
            {
                List<Aufgabe> tasks = await App.Database.GetAufgabenAsync(loggedInUserId);

                tasks = tasks.OrderByDescending(task => task.Prioritaet)
                             .ThenBy(task => task.Status)
                             .Take(3)
                             .ToList();

                /*Label welcomeToTaskManagerLabel = new Label
                {
                    Text = "Welcome to Task Manager!",
                    FontSize = 30,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                tasksStackLayout.Children.Add(welcomeToTaskManagerLabel)*/

                Image symbolImage = new Image
                {
                    Source = "logo_nur_symbol.png",
                    HorizontalOptions = LayoutOptions.Center,
                    MaximumWidthRequest = 60
                };

                Image textImage = new Image
                {
                    Source = "logo_nur_text.png",
                    HorizontalOptions = LayoutOptions.Center,
                    MaximumWidthRequest = 150
                };

                tasksStackLayout.Children.Add(symbolImage);
                tasksStackLayout.Children.Add(textImage);

                Label welcomeMessageLabel = new Label
                {
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    MaximumWidthRequest = 500
                };

                if (!string.IsNullOrEmpty(App.LoggedInUsername))
                {
                    welcomeMessageLabel.Text = $"Welcome to Task Manager, {App.LoggedInUsername}! Here, you can plan your day and stay organized with an overview of your upcoming tasks and projects on your personal dashboard. If you need assistance, click on the orange button to make your task management experience even smoother. Stay one step ahead of your schedule!";
                }
                else
                {
                    welcomeMessageLabel.Text = "Welcome to Task Manager!";
                }

                tasksStackLayout.Children.Add(welcomeMessageLabel);

                foreach (Aufgabe task in tasks)
                {
                    StackLayout taskLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 5
                    };

                    Label titleLabel = new Label
                    {
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        Text = task.Titel,
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        MaximumWidthRequest = 400
                    };

                    Label dueDateLabel = new Label
                    {
                        FontSize = 16,
                        Text = $"Due Date: {task.Faelligkeitsdatum.ToString()}",
                    };

                    Label descriptionLabel = new Label
                    {
                        FontSize = 16,
                        Text = $"Description: {task.Beschreibung}",
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalOptions = LayoutOptions.Center,
                        MaximumWidthRequest = 400
                    };

                    taskLayout.Children.Add(titleLabel);
                    taskLayout.Children.Add(dueDateLabel);
                    taskLayout.Children.Add(descriptionLabel);

                    tasksStackLayout.Children.Add(taskLayout);
                }
            }
            else
            {
                // funktion falls die benutzer id nicht gefunden werden kann, ist nicht vorhanden da dieser fehler bis jetzt nich vergekommen ist
            }

            Button seefaqButton = new Button
            {
                Text = "!",
                BackgroundColor = Color.FromRgb(230, 126, 34),
                Command = new Command(() => OnInfoButtonClicked(this, EventArgs.Empty)),
                HorizontalOptions = LayoutOptions.Center,
                MinimumWidthRequest = 50,
                FontFamily = "FontAwesome",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                Margin = new Thickness(0, 50, 0, 0)
            };

            Button seeAllTasksButton = new Button
            {
                Text = "See all Tasks",
                Command = new Command(() => Clicked1(this, EventArgs.Empty)),
                HorizontalOptions = LayoutOptions.Center,
                MinimumWidthRequest = 50
            };

            tasksStackLayout.Children.Add(seeAllTasksButton);
            tasksStackLayout.Children.Add(seefaqButton);
        }

        private async void Clicked1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route5");
        }

        private async void OnInfoButtonClicked(object sender, EventArgs e)
        {
            bool goToFAQ = await DisplayAlert("Info", "Möchten Sie zur FAQ oder zum Impressum gehen?", "FAQ", "Impressum");

            if (goToFAQ)
            {
                await Shell.Current.GoToAsync("FAQPage");
            }
            else
            {
                await Shell.Current.GoToAsync("ImpressumPage");
            }
        }
    }
}
