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
    public class RealtorEquipmentViewModel : ViewModelBase
    {
        private IHouseService _houseService;

        public ObservableRangeCollection<House> House { get; set; }
        public ObservableRangeCollection<Grouping<string, House>> HouseGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<House> FavoruteCommand { get; }


        public AsyncCommand<House> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }
        public Command ToolbarItem_ClickedCommand { get; }

        public RealtorEquipmentViewModel()
        {
            Title = "HouseEquipment";

            _houseService = DependencyService.Get<IHouseService>();

            House = new ObservableRangeCollection<House>();
            HouseGroups = new ObservableRangeCollection<Grouping<string, House>>();


            RefreshCommand = new AsyncCommand(Refresh);
            FavoruteCommand = new AsyncCommand<House>(Favorite);
            SelectedCommand = new AsyncCommand<House>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ToolbarItem_ClickedCommand = new Command(ToolbarItem_Clicked);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        private async void ToolbarItem_Clicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        async Task Favorite(House house)
        {
            if (house == null)
                return;
            await _houseService.RemoveHouse(house.Id);
            await _houseService.AddHouse(house.Name, house.Locate, house.Price, "Booking");
            Refresh();
            await Application.Current.MainPage.DisplayAlert("favorite", house.Name, "OK");
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
            if (house == null)
                return;


            await Application.Current.MainPage.Navigation.PushAsync(new AddMyHouseDetailsPage(_houseService, house.Id));
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(500);
            House.Clear();

            var houses = await _houseService.GetHouses("Simple");
            House.AddRange(houses);
            IsBusy = false;
        }

        private static string BaseUrl =
            "https://www.booking.com/searchresults.en-gb.html?ss=Lviv+Region%2C+Ukraine&aid=304142&lang=en-gb&sb=1&src_elem=sb&src=searchresults&dest_id=4823&dest_type=region&ac_position=2&ac_click_type=b&ac_langcode=en&ac_suggestion_list_length=5&search_selected=true&search_pageview_id=b39268308dc600d4&checkin=2022-06-15&checkout=2022-06-16&group_adults=2&no_rooms=1&group_children=0&sb_travel_purpose=leisure";

        void LoadMore()
        {
            Random random = new Random();
            HtmlWeb web = new HtmlWeb();
            var document = web.Load(BaseUrl);
            List<string> strings = new List<string>();
            var ClassToGet = "bea018f16c";

            var w = document.DocumentNode.Descendants("div");
            var previusName = "";

            foreach (var VARIABLE in w)
            {
                var Names = getBetween(VARIABLE.InnerHtml,
                    "<div data-testid=\"title\" class=\"fcab3ed991 a23c043802\">", "</div>");
                var locate = getBetween(VARIABLE.InnerHtml,
                    "<span data-testid=\"address\" class=\"f4bd0794db b4273d69aa\">", "</span>");
                var image = getBetween(VARIABLE.InnerHtml, "<img src=\"", "\" alt=");
                if (Names != "" && previusName != Names)
                {
                    previusName = Names;
                    var r = document.DocumentNode.Descendants("span");


                    /*var price = getBetween(VARIABLE1.InnerHtml, "fcab3ed991 bd73d13072",
                        "</span>");*/


                    _houseService.AddHouse(Names, locate, Convert.ToString(random.Next(1000, 5000)), "Simple", image);
                }
            }
        }


        private static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        void DelayLoadMore()
        {
            if (House.Count <= 10)
            {
                return;
            }
        }
    }
}