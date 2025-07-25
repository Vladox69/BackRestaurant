using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace BackRestaurant.Hub
{
    [Authorize]
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private static readonly ConcurrentDictionary<string, string> _activeConnections = new();

        public override async Task OnConnectedAsync()
        {
            var id = Context.User?.FindFirst("sub")?.Value; 
            var role = Context.User?.FindFirst("role")?.Value;
            var boss_id = Context.User?.FindFirst("boss_id")?.Value;

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                Context.Abort(); 
                return;
            }

            _activeConnections.TryAdd(Context.ConnectionId, id);

            if (role == "cook" && !string.IsNullOrEmpty(boss_id))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"cook-{boss_id}");
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _activeConnections.TryRemove(Context.ConnectionId, out _);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinCookGroup()
        {
            var id = Context.User?.FindFirst("boss_id")?.Value;
            if (!Context.User?.IsInRole("cook") ?? true)
            {
                throw new HubException("No tienes permiso para unirte a este grupo");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"cook-{id}");
            await Clients.Caller.SendAsync("JoinedGroup", $"cook-{id}");
        }

        public async Task JoinWaiterGroup()
        {
            var id = Context.User?.FindFirst("sub")?.Value;

            if (!Context.User?.IsInRole("waiter") ?? true || string.IsNullOrEmpty(id))
            {
                throw new HubException("No tienes permiso para unirte a este grupo");
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, $"waiter-{id}");
            await Clients.Caller.SendAsync("JoinedGroup", $"waiter-{id}");
        }


    }
}
