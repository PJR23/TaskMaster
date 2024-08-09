using Plugin.LocalNotification;
using System;

namespace MauiApp1
{
    public class NotificationService
    {
        public void ScheduleTaskNotification(Aufgabe aufgabe)
        {
            if (!aufgabe.BenachrichtigungenAktiviert)
                return;

            var notificationTime = aufgabe.Faelligkeitsdatum.AddHours(-2);

            var notification = new NotificationRequest
            {
                NotificationId = aufgabe.Id,
                Title = "Aufgaben Erinnerung",
                Description = $"Ihre Aufgabe '{aufgabe.Titel}' ist in 2 Stunden fällig.",
                ReturningData = aufgabe.Id.ToString(),
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notificationTime
                }
            };


        }
    }
}
