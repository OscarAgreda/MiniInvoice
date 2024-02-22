using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace DDDCleanArchStarter.Api.Hubs
{
    public class DDDCleanArchStarterHub : Hub
    {
        public Task UpdateScheduleAsync(string message)
        {
            return Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}