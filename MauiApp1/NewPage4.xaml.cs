using System;

namespace MauiApp1
{
    public partial class NewPage4 : ContentPage
    {
        private DataManager _dataManager;
        public NewPage4()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            datePicker.MinimumDate = DateTime.Now.Date;
            SetInitialTime();
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

            Aufgabe neueAufgabe = new Aufgabe
            {
                BenutzerId = loggedInUserId,
                Titel = titleEntry.Text,
                Faelligkeitsdatum = dueDateTime.ToUniversalTime(), 
                Beschreibung = description,
                ZeigeAufStartseite = showOnStartPage,
                BenachrichtigungenAktiviert = notificationsEnabled,
                Prioritaet = priority,
                Status = status
            };

            await App.Database.SaveAufgabeAsync(neueAufgabe);

            await Shell.Current.GoToAsync("route5");
        }


    }
}
