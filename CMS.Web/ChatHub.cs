using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Web
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public void send(string Name, string Message)
        {
            Clients.All.addMessageToPage(Name, Message, DateTime.Now.ToString("mm:dd:ss"));
        }
    }
}