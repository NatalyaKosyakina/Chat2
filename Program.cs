using System;
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
                Thread send = new Thread(() =>
                {
                    Client client = new Client();
                    client.Run();
                });
                send.Start();
                
            }
            else
            {
                Thread listen = new Thread(() =>
                {
                    Server server = new Server();
                    server.Run();
                });
                listen.Start();
            }
        }
    }
}
