using SQLite;

namespace reeltorapp.Models
{
    public class House
    {
        [PrimaryKey,AutoIncrement]
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public string Locate{ get; set; }
        public string Type{ get; set; }

    }
}