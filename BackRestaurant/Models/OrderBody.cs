namespace BackRestaurant.Models
{
    public class OrderBody
    {
        public Order order { get; set; }
        public OrderItems[] items { get; set; }
    }
}
