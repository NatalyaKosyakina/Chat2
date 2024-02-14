using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat2
{
    internal class Client
    {
        public void Run() {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 5555);
            var stream = client.GetStream();
            while (true)
            {
                Console.WriteLine("Введите сообщение: ");
                string info = Console.ReadLine();
                if (string.IsNullOrEmpty(info))
                {
                    break;
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(info);
                    }
                }
            }
        }

    }
}
