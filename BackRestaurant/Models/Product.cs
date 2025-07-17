namespace BackRestaurant.Models
{
    public class Product
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int business_id { get; set; }
        public int category_id { get; set; }
        public Business? business { get; set; }
        public Category? category { get; set; }

    }
}
