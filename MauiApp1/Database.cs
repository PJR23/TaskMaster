using Plugin.LocalNotification;
using SQLite;
using MauiApp1;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
// test for changes

public class Benutzer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Benutzername { get; set; }
    public string Email { get; set; }
    public string Passwort { get; set; }
}

public class Aufgabe
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int BenutzerId { get; set; }
    public string Titel { get; set; }
    public DateTime Faelligkeitsdatum { get; set; }
    public string Beschreibung { get; set; }
    public bool ZeigeAufStartseite { get; set; }
    public bool BenachrichtigungenAktiviert { get; set; }
    public int Prioritaet { get; set; }
    public int Status { get; set; }
}

public class Database : IDisposable
{
    private readonly SQLiteAsyncConnection _database;

    public Database()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string dbPath = Path.Combine(folderPath, "AufgabenDB.db3");

        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Benutzer>().Wait();
        _database.CreateTableAsync<Aufgabe>().Wait();
    }

    public void Dispose()
    {
        _database.CloseAsync().Wait();
    }

    public async Task<Aufgabe> GetAufgabeAsync(int taskId)
    {
        return await Task.Run(() => _database.Table<Aufgabe>().FirstOrDefaultAsync(a => a.Id == taskId));
    }

    public async Task<int> SaveBenutzerAsync(Benutzer benutzer)
    {
        try
        {
            if (benutzer.Id != 0)
            {
                return await Task.Run(() => _database.UpdateAsync(benutzer));
            }
            else
            {
                return await Task.Run(() => _database.InsertAsync(benutzer));
            }
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public async Task<List<Aufgabe>> GetAufgabenAsync(int userId)
    {
        return await Task.Run(() => _database.Table<Aufgabe>().Where(a => a.BenutzerId == userId).ToListAsync());
    }

    public async Task<int> DeleteTaskAsync(int taskId)
    {
        try
        {
            return await Task.Run(() => _database.DeleteAsync<Aufgabe>(taskId));
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public async Task<int> SaveAufgabeAsync(Aufgabe aufgabe)
    {
        try
        {
            int result;
            if (aufgabe.Id != 0)
            {
                result = await Task.Run(() => _database.UpdateAsync(aufgabe));
            }
            else
            {
                result = await Task.Run(() => _database.InsertAsync(aufgabe));
            }

            if (result != -1)
            {
                var notificationService = new NotificationService();
                notificationService.ScheduleTaskNotification(aufgabe);
            }

            return result;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }


    public async Task<Benutzer> GetBenutzerAsync(string username, string password)
    {
        return await _database.Table<Benutzer>().FirstOrDefaultAsync(u => u.Benutzername == username && u.Passwort == password);
    }

    public async Task<Aufgabe> GetTaskByIdAsync(int taskId)
    {
        return await _database.Table<Aufgabe>().Where(task => task.Id == taskId).FirstOrDefaultAsync();
    }

    public async Task<bool> IsUsernameRegisteredAsync(string username)
    {
        return await Task.Run(() => _database.Table<Benutzer>().ToListAsync().ContinueWith(list => list.Result.Any(u => u.Benutzername == username)));
    }
}
