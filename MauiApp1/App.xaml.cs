namespace MauiApp1
{
    public partial class App : Application
    {
        public static Database Database { get; private set; }
        public static int LoggedInUserId { get; set; }
        public static DataManager DataManager { get; private set; }
        public static string LoggedInUsername { get; set; }
        public static int CurrentTaskId { get; set; }

        public App()
        {
            InitializeComponent();

            Database = new Database();
            DataManager = new DataManager();
            MainPage = new AppShell();

            new NotificationService();
        }

        public static int GetLoggedInUserId()
        {
            return LoggedInUserId;
        }

        public static void SetCurrentTaskId(int taskId)
        {
            CurrentTaskId = taskId;
        }

        public static int GetCurrentTaskId()
        {
            return CurrentTaskId;
        }
    }
}
