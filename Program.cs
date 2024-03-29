﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Thread listen = new Thread(() =>
                {
                    Server server = new Server();
                    server.Run();
                });
                listen.Start();
            }
            else
            {
                Thread send = new Thread(async() =>
                {
                    Client client = new Client();
                    await client.Run();
                });
                send.Start();
            }
        }
    }
}
