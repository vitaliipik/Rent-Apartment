using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using reeltorapp.Models;
using reeltorapp.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace reeltorapp.Services
{
    public interface IHouseService
    {
        Task Init();
        Task<int> RemoveHouse<House>();
        Task AddHouse(string name, string locate,string price,string type,string image="https://smilyhomes.com/wp-content/uploads/2022/05/house.jpg");
        Task RemoveHouse(int id);
        Task<IEnumerable<House>> GetHouses(string type);
        Task<IEnumerable<House>> GetHouses();
        Task<House> GetHouse(int id);

    }
}