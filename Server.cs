using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat2
{
    internal class Server
    {
        //public Dictionary<string, IPEndPoint> clients = new Dictionary<string, IPEndPoint>();
        public HashSet<TcpClient> clients = new HashSet<TcpClient>();
        public TcpListener listener = new TcpListener(IPAddress.Any, 5555);
        public void Run()
        {
            listener.Start();
            Console.WriteLine("Сервер запущен");
            while (true)
            {
                var client = listener.AcceptTcpClient();
                clients.Add(client);
                var stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                Console.WriteLine(reader.ReadToEnd());
                //StreamWriter writer = new StreamWriter(stream);

            }
        }
    }
}
