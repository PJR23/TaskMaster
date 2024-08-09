using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class DataManager
    {
        private readonly Database _database;
        private Benutzer _loggedInUser;
        private int _loggedInUserId;
        private Aufgabe _editedTask;

        public DataManager()
        {
            _database = new Database();
        }

        public async Task<List<Aufgabe>> GetAufgabenAsync()
        {
            return await _database.GetAufgabenAsync(_loggedInUserId);
        }

        public async Task<int> SaveAufgabeAsync(Aufgabe aufgabe)
        {
            aufgabe.BenutzerId = _loggedInUserId;
            return await _database.SaveAufgabeAsync(aufgabe);
        }

        public async Task<Aufgabe> GetTaskByIdAsync(int taskId)
        {
            return await _database.GetAufgabeAsync(taskId);
        }

        public void SetEditedTask(Aufgabe editedTask)
        {
            _editedTask = editedTask;
        }

        public Aufgabe GetEditedTask()
        {
            return _editedTask;
        }



        public void SetLoggedInUser(Benutzer loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }

        public void SetLoggedInUserId(int userId)
        {
            _loggedInUserId = userId;
        }

        public int GetLoggedInUserId()
        {
            return _loggedInUserId;
        }

        // DataManager.cs
        public async Task<int> GetLoggedInUserId(string username, string password)
        {
            var user = await App.Database.GetBenutzerAsync(username, password);

            if (user != null)
            {
                App.LoggedInUsername = user.Benutzername; // benutzername beim einloggen in app festlegen
                return user.Id;
            }
            else
            {
                return -1; // wenn benutzer nicht gefunden wird
            }
        }


    }
}
