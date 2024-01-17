using System.Threading;
using Xamarin.Forms;
using XamarinApp.Services;

namespace XamarinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<DataStoreTasks>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var cultureVal = "pl-PL";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureVal);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureVal);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
