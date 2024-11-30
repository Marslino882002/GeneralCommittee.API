﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace GeneralCommittee.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {


        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }



    }
}
