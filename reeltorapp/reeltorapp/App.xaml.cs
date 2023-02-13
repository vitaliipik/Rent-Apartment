
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using reeltorapp.Helpers;
using reeltorapp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;



namespace reeltorapp
{
    public partial class App : Application
    {
        public App()
        {
            Initialize();
            InitializeComponent();
            DependencyService.Register<IHouseService,HouseService>();

            MainPage = new AppShell();
            
        }

        protected void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ContainerConfig());

            var lacator = new AutofacServiceLocator(builder.Build());
            ServiceLocator.SetLocatorProvider(()=>lacator);
        }
        


        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}