namespace BackRestaurant.Models
{
    public class Waiter
    {
        public int? id { get; set; }
        public string shift { get; set; }
        public int user_id { get; set; }
        public int business_id { get; set; }
        public User? user { get; set; }
        public Business? business { get; set; }
    }
}
