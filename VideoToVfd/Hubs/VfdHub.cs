using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using System.Runtime.Intrinsics.Arm;
using System;

namespace VideoToVfd.Hubs
{
    public class VfdHub : Hub
    {
        public void SendMessage(string message)
        {
            for (var i = 0; i < message.Length; i += 2)
            {
                var b = Convert.ToByte(message.Substring(i, 2), 16);

                VfdPlayer_Gpio.bytes[i / 2 + 8] = b;
            }
        }
    }
}
