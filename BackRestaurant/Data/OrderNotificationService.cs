using BackRestaurant.Hub;
using BackRestaurant.Models;
using Microsoft.AspNetCore.SignalR;

namespace BackRestaurant.Data
{
    public class OrderNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public OrderNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyNewOrderToKitchen(Order order,string businessId)
        {
            await _hubContext.Clients.Group($"cook-{businessId}").SendAsync("NewOrder", new
            {
                order
            });
        }
        
        public async Task NotifyOrderReadyToWaiter(OrderItem orderItem, string waiterId)
        {
            await _hubContext.Clients.Group($"waiter-{waiterId}").SendAsync("OrderReady", new
            {
                orderItem
            });
        }
    }
}
