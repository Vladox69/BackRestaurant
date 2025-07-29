namespace BackRestaurant.Models
{
    public class OrderBody
    {
        public Order order { get; set; }
        public OrderItem[] items { get; set; }
    }
}
