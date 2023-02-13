using System.Threading.Tasks;
using MvvmHelpers.Commands;
using reeltorapp.Models;
using reeltorapp.Services;
using Xamarin.Forms;

namespace reeltorapp.ViewModels{  

    [QueryProperty(nameof(Name),nameof(Name))]
    public class AddMyHouseViewModel:ViewModelBase
    {
        private IHouseService _houseService;
        private string name, locate,price,type;
        public string Name
        {
            
            get => name;
            set => SetProperty(ref name,value);
        }

        public string Locate
        {
            get => locate;
            set => SetProperty(ref locate,value);
        }
        public string Price
        {
            get => price;
            set => SetProperty(ref price,value);
        }
 public string Type
        {
            get => type;
            set => SetProperty(ref type,value);
        }

        public AsyncCommand SaveCommand { get; }

        public AddMyHouseViewModel()
        {
            _houseService = DependencyService.Get<IHouseService>();
            Title = "Add House";
            SaveCommand = new AsyncCommand(Save);
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(locate) || string.IsNullOrWhiteSpace(price) || string.IsNullOrWhiteSpace(type))
            {
                return;
            }

            await _houseService.AddHouse(name, locate,price,type);
            await Shell.Current.GoToAsync("..");
        }
    }
}