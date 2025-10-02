using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Credimap.Hubs
{
	public class LocationHub : Hub
	{
		public async Task SendLocation(string userId, double lat, double lng)
		{
			await Clients.Others.SendAsync("ReceiveLocationUpdate", new
			{
				userId,
				lat,
				lng
			});
		}
	}
} 