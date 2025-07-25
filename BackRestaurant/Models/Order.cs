namespace BackRestaurant.Models
{
    public class Order
    {
        public int? id { get; set; }
        public string status { get; set; }
        public double total { get; set; }
        public int waiter_id { get; set; }
        public int table_id { get; set; }
        public Waiter? waiter { get; set; }
        public Table? table { get; set; }

    }
}
