namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("route1", typeof(NewPage1));
            Routing.RegisterRoute("route2", typeof(NewPage1));
            Routing.RegisterRoute("route3", typeof(NewPage2));
            Routing.RegisterRoute("route4", typeof(MainPage));
            Routing.RegisterRoute("route5", typeof(NewPage3));
            Routing.RegisterRoute("route6", typeof(NewPage4));
            Routing.RegisterRoute("route7", typeof(NewPage1));
            Routing.RegisterRoute("route8", typeof(NewPage5));
            Routing.RegisterRoute(nameof(FAQPage), typeof(FAQPage));
            Routing.RegisterRoute(nameof(ImpressumPage), typeof(ImpressumPage));
        }
    }
}
