namespace HarmonyHotles.Models
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<Hotel> Hotels { get; set; }
        public List<Event> Events { get; set; }
        public List<Image> Images { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Hotelservice> Services { get; set; }  
        public List<Hotelamenity> Amenities { get; set; }  
        public List<Roomtype> RoomTypes { get; set; }
    }
}
