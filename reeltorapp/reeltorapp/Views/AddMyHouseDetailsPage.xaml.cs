using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using MvvmHelpers;
using reeltorapp.Services;
using reeltorapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reeltorapp.Views
{
    
[QueryProperty(nameof(HouseId),nameof(HouseId))]
    public partial class AddMyHouseDetailsPage : ContentPage
    {
        private IHouseService _houseService;
        public int HouseId { get; set; }
        public AddMyHouseDetailsPage(IHouseService houseService,int r)
        {
            _houseService = houseService;
            HouseId = r;
            InitializeComponent();
            BindingContext = ServiceLocator.Current.GetInstance<MyHouseDetailsPage>();
           
           
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
           
            BindingContext = await _houseService.GetHouse(HouseId);
            
        }
        
        
        public async void Button_OnClicked(object sender, EventArgs eventArgs)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}