using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiApp1
{
    public partial class NewPage3 : ContentPage
    {
        private DataManager _dataManager;

        public NewPage3()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            ShowTasksAsync();
            int loggedInUserId = App.DataManager.GetLoggedInUserId();
            userIdLabel.Text = $"User ID: {loggedInUserId}";
        }

        private async void ShowTasksAsync()
        {
            int loggedInUserId = App.DataManager.GetLoggedInUserId();

            if (loggedInUserId > 0)
            {
                List<Aufgabe> tasks = await App.Database.GetAufgabenAsync(loggedInUserId);

                tasks = tasks.OrderByDescending(task => task.Prioritaet)
                             .ThenBy(task => task.Status)
                             .ToList();

                StackLayout tasksStackLayout = new StackLayout
                {
                    Padding = new Thickness(20, 20, 20, 20)
                };

                foreach (Aufgabe task in tasks)
                {
                    string statusText = TranslateStatus(task.Status);
                    string priorityText = TranslatePriority(task.Prioritaet);

                    Frame taskFrame = new Frame
                    {
                        BorderColor = new Microsoft.Maui.Graphics.Color(0, 0, 0),
                        Padding = 10,
                        Margin = new Thickness(0, 0, 0, 10),
                        CornerRadius = 10,
                        HasShadow = true
                    };

                    StackLayout taskContentLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 10
                    };

                    Label titleLabel = new Label
                    {
                        FontSize = 18,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    titleLabel.FormattedText = new FormattedString();
                    titleLabel.FormattedText.Spans.Add(new Span { Text = "Title: ", FontAttributes = FontAttributes.Bold });
                    titleLabel.FormattedText.Spans.Add(new Span { Text = task.Titel });

                    Label dueDateLabel = new Label
                    {
                        FontSize = 16,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    dueDateLabel.FormattedText = new FormattedString();
                    dueDateLabel.FormattedText.Spans.Add(new Span { Text = "Due Date: ", FontAttributes = FontAttributes.Bold });
                    dueDateLabel.FormattedText.Spans.Add(new Span { Text = task.Faelligkeitsdatum.ToString() });

                    Label priorityLabel = new Label
                    {
                        FontSize = 16,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    priorityLabel.FormattedText = new FormattedString();
                    priorityLabel.FormattedText.Spans.Add(new Span { Text = "Priority: ", FontAttributes = FontAttributes.Bold });
                    priorityLabel.FormattedText.Spans.Add(new Span { Text = priorityText });

                    Label descriptionLinkLabel = new Label
                    {
                        Text = "Click here to see description",
                        TextColor = new Microsoft.Maui.Graphics.Color(0, 0, 255),
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 0, 0, 5),
                    };

                    descriptionLinkLabel.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => ShowDescriptionAlert(task.Beschreibung)),
                        NumberOfTapsRequired = 1
                    });

                    Label notificationLabel = new Label
                    {
                        FontSize = 16,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    notificationLabel.FormattedText = new FormattedString();
                    notificationLabel.FormattedText.Spans.Add(new Span { Text = "Notifications Enabled: ", FontAttributes = FontAttributes.Bold });
                    notificationLabel.FormattedText.Spans.Add(new Span { Text = task.BenachrichtigungenAktiviert.ToString() });

                    Label statusLabel = new Label
                    {
                        FontSize = 16,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    statusLabel.FormattedText = new FormattedString();
                    statusLabel.FormattedText.Spans.Add(new Span { Text = "Status: ", FontAttributes = FontAttributes.Bold });
                    statusLabel.FormattedText.Spans.Add(new Span { Text = statusText });

                    Button deleteButton = new Button
                    {
                        Text = "Delete",
                        CommandParameter = task,
                        BackgroundColor = Color.FromRgb(231, 76, 71),
                        Command = new Command(DeleteTask)
                    };

                    Button editButton = new Button
                    {
                        Text = "Edit",
                        CommandParameter = task,
                        Command = new Command(EditTask)
                    };

                    taskContentLayout.Children.Add(titleLabel);
                    taskContentLayout.Children.Add(dueDateLabel);
                    taskContentLayout.Children.Add(descriptionLinkLabel);
                    taskContentLayout.Children.Add(notificationLabel);
                    taskContentLayout.Children.Add(priorityLabel);
                    taskContentLayout.Children.Add(statusLabel);
                    taskContentLayout.Children.Add(deleteButton);
                    taskContentLayout.Children.Add(editButton);

                    taskFrame.Content = taskContentLayout;
                    tasksStackLayout.Children.Add(taskFrame);
                }

                Button newTaskButton = new Button
                {
                    Text = "New Task",
                    Command = new Command(() => Clicked1(this, EventArgs.Empty)),
                    HorizontalOptions = LayoutOptions.Center,
                    MinimumWidthRequest = 50
                };

                tasksStackLayout.Children.Add(newTaskButton);

                Button backButton = new Button
                {
                    Text = "Back to overview",
                    Command = new Command(() => Clicked2(this, EventArgs.Empty)),
                    HorizontalOptions = LayoutOptions.Center,
                    MinimumWidthRequest = 50
                };

                tasksStackLayout.Children.Add(backButton);

                ScrollView scrollView = new ScrollView
                {
                    Content = tasksStackLayout,
                    Orientation = ScrollOrientation.Horizontal,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Always
                };

                Content = scrollView;
            }
            else
            {
                // funktion falls die benutzer id nicht gefunden werden kann, ist nicht vorhanden da dieser fehler bis jetzt nich vergekommen ist
            }
        }

        private async void EditTask(object taskObject)
        {
            if (taskObject is Aufgabe task)
            {
                App.SetCurrentTaskId(task.Id);
                await Shell.Current.GoToAsync("route8");
            }
            else
            {
                // fehler noch nicht vorgekommen daher keine code
            }
        }

        private async void DeleteTask(object taskObject)
        {
            if (taskObject is Aufgabe task)
            {
                bool result = await DisplayAlert("Delete Task", $"Do you want to delete the task '{task.Titel}'?", "Yes", "No");

                if (result)
                {
                    await App.Database.DeleteTaskAsync(task.Id);
                    ShowTasksAsync();
                }
            }
        }

        private async void ShowDescriptionAlert(string description)
        {
            await DisplayAlert("Task Description", description, "OK");
        }

        private async void Clicked1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route6");
        }

        private async void Clicked2(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route1");
        }

        private string TranslateStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "Not started yet";
                case 1:
                    return "Paused";
                case 2:
                    return "In progress";
                case 3:
                    return "Finished";
                default:
                    return "Unknown";
            }
        }

        private string TranslatePriority(double sliderValue)
        {
            if (sliderValue <= 1)
            {
                return "Very Low";
            }
            else if (sliderValue > 1 && sliderValue <= 2)
            {
                return "Low";
            }
            else if (sliderValue > 2 && sliderValue <= 3)
            {
                return "Normal";
            }
            else if (sliderValue > 3 && sliderValue <= 4)
            {
                return "High";
            }
            else
            {
                return "Very High";
            }
        }
    }
}
