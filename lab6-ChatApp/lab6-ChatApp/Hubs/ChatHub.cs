using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lab6_ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace lab6_ChatApp.Hubs
{
    public class ChatHub:Hub
    {
        public void SendMessage(ChatMessage message)
        {
            //todo: do anything with message
            Clients.All.SendAsync("ReceivedMessage", message);
        }

        public void SignInUser(string username)
        {
            Clients.All.SendAsync("UserSignedIn", username);
        }

    }
}
