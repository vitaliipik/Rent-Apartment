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
    public class LoginPageViewModel:ViewModelBase
    {
       
        public Command Button_ClickedCommand { get; }
        public Command  TapGestureRecognizer_TappedCommand { get; }
        
        public LoginPageViewModel()
        {
            Title = "House Equipment";
           
            Button_ClickedCommand = new Command(Button_Clicked);
            TapGestureRecognizer_TappedCommand = new Command(TapGestureRecognizer_Tapped);


        }

        private async void TapGestureRecognizer_Tapped(object sender)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");

        }
        private  async void  Button_Clicked(object obj)
        {
          
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            
        }
    }
}