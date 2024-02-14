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
        public async Task Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5556);
            listener.Start();
            Console.Out.WriteLineAsync("Запущен");
            while (true)
            {
              await Task.Run(async () =>
                {
                    using (var client = await listener.AcceptTcpClientAsync())
                    {
                //        if (!clients.Contains(client.Client))
                //        {
                //            clients.Add(client.Client);
                //        }
                //      using (StreamReader reader = new StreamReader(client.GetStream()))
                //        {
                //            string msg = reader.ReadToEnd();

                //            Console.WriteLine(msg);
                //            sw.WriteLine("Сообщение получено");
                //}
                //        using ())
                //        {
                            
                //        }
                    }
                });
            }
        }

        public void Run2()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5556);
            listener.Start();
            Console.Out.WriteLineAsync("Запущен");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                var stream = client.GetStream();
                try
                {
                    StreamReader reader = new StreamReader(stream);
                    string msg = reader.ReadToEnd();
                    Console.WriteLine(msg);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("Сообщение получено");
                }
                catch (System.IO.IOException) {
                    Console.WriteLine("Клиент разорвал соединение");
                }


            }


        }
    }
}
