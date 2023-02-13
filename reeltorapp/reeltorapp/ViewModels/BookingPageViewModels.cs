using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using reeltorapp.Models;
using reeltorapp.Services;
using reeltorapp.Views;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;


namespace reeltorapp.ViewModels
{
    public class BookingPageViewModels:ViewModelBase
    {
      
        private IHouseService _houseService;
        public ObservableRangeCollection<House> House { get; set; }
        public ObservableRangeCollection<Grouping<string,House>> HouseGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
       
        public AsyncCommand <House> RemoveCommand { get; }
        public AsyncCommand  AddCommand { get; }
        public AsyncCommand <House> SelectedCommand { get; }
     
        public Command  ToolbarItem_ClickedCommand { get; }
        public BookingPageViewModels()
        {
            Title = "Booking Page";
            _houseService = DependencyService.Get<IHouseService>();
            House = new ObservableRangeCollection<House>();
            HouseGroups = new ObservableRangeCollection<Grouping<string,House>>();
            
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<House>(Remove);
            RefreshCommand = new AsyncCommand(Refresh);
          
            SelectedCommand = new AsyncCommand<House>(Selected);
          
            ToolbarItem_ClickedCommand = new Command(ToolbarItem_Clicked);
           
            
        }
        private async void ToolbarItem_Clicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async Task Remove(House house)
        {
            await _houseService.AddHouse(house.Name, house.Locate, house.Price, "Simple");
            await _houseService.RemoveHouse(house.Id);
            await Refresh();
        }
        

         async Task Add()
        {
            /*var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Lgs gsg");
            var locate = await App.Current.MainPage.DisplayPromptAsync("Locate", "Lgfjne");
            await HouseService.AddHouse(name, locate);
            await Refresh();*/
             var route = $"{nameof(AddMyHousePage)}";
             await Shell.Current.GoToAsync(route);
        }

       
        private House previoslySelected;
        private House selectedHouse;

       public House SelectedHouse
        {
            get => selectedHouse;
            set => SetProperty(ref selectedHouse, value);
        }

      
       

       async Task Selected(House house)
       {
           
           if(house==null)
               return;

        
           await Application.Current.MainPage.Navigation.PushAsync(new AddMyHouseDetailsPage(_houseService,house.Id));
       }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            House.Clear();
            var houses = await _houseService.GetHouses("Booking");
            House.AddRange(houses);
            IsBusy = false;
        }

      

    }
}
    