namespace BackRestaurant.Models
{
    public class Business
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int user_id { get; set; }
        public User? user { get; set; }
    }
}
