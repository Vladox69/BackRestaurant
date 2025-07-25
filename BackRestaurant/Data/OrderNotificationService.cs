using BackRestaurant.Hub;
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
        public async Task NotifyNewOrderToKitchen(string orderId, string waiterId,string businessId)
        {
            await _hubContext.Clients.Group($"cook-{businessId}").SendAsync("NewOrder", new
            {
                OrderId = orderId,
                WaiterId = waiterId
            });
        }
        
        public async Task NotifyOrderReadyToWaiter(string itemId, string waiterId)
        {
            await _hubContext.Clients.Group($"waiter-{waiterId}").SendAsync("OrderReady", new
            {
                OrderId = itemId,
                Status = "Ready"
            });
        }
    }
}
