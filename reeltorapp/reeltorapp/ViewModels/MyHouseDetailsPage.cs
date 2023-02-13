using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using HtmlAgilityPack;
using MvvmHelpers;
using MvvmHelpers.Commands;
using reeltorapp.Models;
using reeltorapp.Services;
using reeltorapp.Views;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

namespace reeltorapp.ViewModels
{
    public class MyHouseDetailsPage:ViewModelBase
    {
        public ICommand ButtonComand;
        
        
        public MyHouseDetailsPage()
        {
            ButtonComand = new Command(Button_OnClicked);
            

        }

        public async void Button_OnClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}