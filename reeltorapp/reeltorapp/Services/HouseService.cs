using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using reeltorapp.Models;
using SQLite;
using Xamarin.Essentials;

namespace reeltorapp.Services
{
    public class HouseService:IHouseService
    {
        protected static SQLiteAsyncConnection db;
        
        public async Task Init()
        {
            if (db != null)
                return;
            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory,"MyData.db");
             db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<House>();
        }

        public  async Task AddHouse(string name, string locate,string price,string type,string image="https://smilyhomes.com/wp-content/uploads/2022/05/house.jpg")
        {
            await Init();
            
            var house = new House()
            {
                Name = name,
                Locate = locate,
                Image = image,
                Price=price,
                Type = type
            };
           var id= await db.InsertAsync(house);

        }

        public async Task RemoveHouse(int id)
        {
            await Init();
            db.DeleteAsync<House>(id);
        }
        public async Task<int> RemoveHouse<House>()
        {
            await Init();
            return await db.DeleteAllAsync<House>();

        }

        public  async Task<IEnumerable<House>> GetHouses(string type)
        {
            await Init();
            var house = await db.Table<House>().Where(x=>x.Type==type).ToListAsync();
            return house;
        }
       public  async Task<IEnumerable<House>> GetHouses()
        {
            await Init();
            var house = await db.Table<House>().ToListAsync();
            return house;
        }
        public async Task<House> GetHouse(int id)
        {
            await Init();
            var house = await db.Table<House>().FirstOrDefaultAsync(c=>c.Id==id);
            
            return house;
        }

    }
}