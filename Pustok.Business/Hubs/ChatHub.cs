using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Pustok.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(Context.User.Identity.Name);
                if (user != null)
                {
                    user.ConnectionId = Context.ConnectionId;
                    await userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("OnConnect", user.Id);
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(Context.User.Identity.Name);
                if (user != null)
                {
                    user.ConnectionId = null;
                    await userManager.UpdateAsync(user);
                    await Clients.All.SendAsync("DisConnect", user.Id);
                }
            }
        }
    }
}