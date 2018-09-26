using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using E_Guest.API.Models;
using Microsoft.AspNet.SignalR;

namespace E_Guest.API.Hub
{
    public class ChatHub : Microsoft.AspNet.SignalR.Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);

        //Specific User Call
        [Authorize]
        public void SendNotification(UserHubModels userHubModels)
        {
            try
            {
             
                //Send To
                UserHubModels receiver;
                if (Users.TryGetValue(userHubModels.UserId.ToString(), out receiver))
                {
                    var cid = receiver.ConnectionIds.FirstOrDefault();
                    var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                    context.Clients.Client(cid).broadcastMessage(userHubModels.Message);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }

            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserHubModels user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        UserHubModels removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }

    }
}