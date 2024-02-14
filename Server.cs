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
                try
                {
                    var stream = client.GetStream();
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //string message = reader.ReadToEnd();  Ошибка где-то здесь...
                        //Console.WriteLine(message);
                        Task.Run(async() =>
                        {
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                Console.WriteLine("Код дошел сюда");
                                await writer.WriteLineAsync("Сообщение получено");
                                Console.WriteLine("И сюда");
                            }
                        }).Wait();
                    }                       
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
