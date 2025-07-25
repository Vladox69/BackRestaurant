namespace BackRestaurant.Models
{
    public class OrderItems
    {
        public int? id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
        public Order? order { get; set; }
        public Product? product { get; set; }
    }
}
