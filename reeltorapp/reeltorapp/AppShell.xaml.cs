using reeltorapp.ViewModels;
using reeltorapp.Views;
using Xamarin.Forms;

namespace reeltorapp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddMyHousePage), typeof(AddMyHousePage));
            Routing.RegisterRoute(nameof(AddMyHouseDetailsPage), typeof(AddMyHouseDetailsPage));
            Routing.RegisterRoute(nameof(RegistrationPage),typeof(RegistrationPage));
            
            
           
           

           
        }
    }
}
