using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MauiApp1
{
    // Um zu überprüfen ob ein Text feld leer ist
    public class MultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return false;

            // First value is the input length, second is the validation state
            bool hasInput = !string.IsNullOrEmpty(values[0]?.ToString());
            bool isValid = (bool)values[1];

            // Return true if there is input and the validation state is valid
            return hasInput && isValid;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class NewPage2 : ContentPage
    {
        int count = 0;

        public NewPage2()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("route4");
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }

        private bool ValidatePasswordCriteria(string password)
        {
            if (password == null)
            {
                return false;
            }

            if (!password.Any(char.IsDigit) ||
                !password.Any(ch => !char.IsLetterOrDigit(ch)) ||
                !password.Any(char.IsUpper) ||
                !password.Any(char.IsLower) ||
                password.Length < 8)
            {
                return false;
            }

            return true;
        }

        private bool ValidateNameCriteria(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length <= 10;
        }

        private bool ValidateAgreement(bool isAgreed)
        {
            return isAgreed;
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            bool agreedToTerms = agreeCheckBox.IsChecked;

            if (await App.Database.IsUsernameRegisteredAsync(username))
            {
                await DisplayAlert("Fehler", "Dieser Benutzername ist bereits registriert. Bitte wählen Sie einen anderen.", "OK");
                return;
            }

            bool isEmailValid = IsEmailValid(email);
            bool isPasswordValid = ValidatePasswordCriteria(password);
            bool isNameValid = ValidateNameCriteria(username);
            bool isAgreementValid = ValidateAgreement(agreedToTerms);

            if (isEmailValid && isPasswordValid && isNameValid && isAgreementValid)
            {
                Benutzer newUser = new Benutzer
                {
                    Benutzername = username,
                    Email = email,
                    Passwort = password
                };

                using (Database db = new Database())
                {
                    int result = await db.SaveBenutzerAsync(newUser);
                    if (result > 0)
                    {
                        await DisplayAlert("Success", "User successfully created.", "OK");

                        int loggedInUserId = await App.DataManager.GetLoggedInUserId(username, password);
                        App.DataManager.SetLoggedInUserId(loggedInUserId);

                        await Shell.Current.GoToAsync("route4");
                    }
                    else
                    {
                        await DisplayAlert("Error", "User could not be created. Please try again.", "OK");
                    }
                }
            }
            else
            {
                // hinzufügen wenn validierungen fehlschlagen
            }
        }
    }
}
