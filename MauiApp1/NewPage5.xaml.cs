using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Collections.Generic;


namespace MauiApp1
{
    public partial class NewPage5 : ContentPage
    {
        private DataManager _dataManager;
        private Aufgabe _editedTask;
        private int _taskId;

        public NewPage5()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            datePicker.MinimumDate = DateTime.Now.Date;
            SetInitialTime();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            int currentTaskId = App.GetCurrentTaskId();
            if (currentTaskId != 0)
            {
                _taskId = currentTaskId;

                _editedTask = await App.Database.GetAufgabeAsync(_taskId);

                titleEntry.Text = _editedTask.Titel;
                datePicker.Date = _editedTask.Faelligkeitsdatum.Date;
                timePicker.Time = _editedTask.Faelligkeitsdatum.TimeOfDay;
                entry.Text = _editedTask.Beschreibung;
                switcher.IsToggled = _editedTask.BenachrichtigungenAktiviert;
                prioritySlider.Value = _editedTask.Prioritaet;
                picker.SelectedIndex = _editedTask.Status;
                radio1.IsChecked = _editedTask.ZeigeAufStartseite;
                radio2.IsChecked = !_editedTask.ZeigeAufStartseite;

            }
        }



        private void SetInitialTime()
        {
            timePicker.Time = DateTime.Now.TimeOfDay;
        }

        private void OnTitleEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string enteredText = e.NewTextValue;

            if (enteredText.Length > 50)
            {
                titleEntry.Text = enteredText.Substring(0, 50);
            }
        }

        private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string enteredText = e.NewTextValue;

            if (enteredText.Length > 600)
            {
                entry.Text = enteredText.Substring(0, 600);
            }
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            bool isSwitchedOn = e.Value;
        }

        private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isChecked = e.Value;
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double sliderValue = e.NewValue;
            string priority;

            if (sliderValue <= 1)
            {
                priority = "Very Low";
            }
            else if (sliderValue > 1 && sliderValue <= 2)
            {
                priority = "Low";
            }
            else if (sliderValue > 2 && sliderValue <= 3)
            {
                priority = "Normal";
            }
            else if (sliderValue > 3 && sliderValue <= 4)
            {
                priority = "High";
            }
            else
            {
                priority = "Very High";
            }

            priorityLabel.Text = $"Priority: {priority}";
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var statusList = new List<string>
            {
                "Not started yet",
                "Paused",
                "In progress",
                "Finished"
            };

            Picker picker = new Picker { Title = "Select the status" };
            picker.ItemsSource = statusList;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string title = titleEntry.Text;

            DateTime dueDate = datePicker.Date;
            TimeSpan dueTime = timePicker.Time;
            DateTime dueDateTime = dueDate.Add(dueTime);

            dueDateTime = dueDateTime.AddHours(2);

            string description = entry.Text;
            bool showOnStartPage = radio1.IsChecked;
            bool notificationsEnabled = switcher.IsToggled;

            int loggedInUserId = App.DataManager.GetLoggedInUserId();
            int priority = Convert.ToInt32(prioritySlider.Value);
            int statusIndex = picker.SelectedIndex;
            int status = statusIndex;

            int taskId = App.GetCurrentTaskId();

            Aufgabe aufgabeToUpdate = new Aufgabe
            {
                Id = taskId,
                BenutzerId = loggedInUserId,
                Titel = titleEntry.Text,
                Faelligkeitsdatum = dueDateTime.ToUniversalTime(),
                Beschreibung = description,
                ZeigeAufStartseite = showOnStartPage,
                BenachrichtigungenAktiviert = notificationsEnabled,
                Prioritaet = priority,
                Status = status
            };

            await App.Database.SaveAufgabeAsync(aufgabeToUpdate);

            await Shell.Current.GoToAsync("route5");
        }


        private async void Clicked1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route7");
        }

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            if (e.NewDate < DateTime.Now.Date)
            {
                datePicker.Date = DateTime.Now.Date;
                SetInitialTime();
            }
        }
    }
}
