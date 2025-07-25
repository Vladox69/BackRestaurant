namespace BackRestaurant.Models
{
    public class Table
    {
        public int? id { get; set; }
        public int table_number { get; set; }
        public int capacity { get; set; }
        public string location { get; set; }
        public int business_id { get; set; }
        public Business? business { get; set; }

    }
}
