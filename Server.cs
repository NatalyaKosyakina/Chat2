using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat2
{
    internal class Server
    {
        public HashSet<TcpClient> clients = new HashSet<TcpClient>();
        public TcpListener listener = new TcpListener(IPAddress.Any, 5555);
        public void Run()
        {
            listener.Start();
            Console.WriteLine("Сервер запущен");
            int count = 0;
            while (true)
            {
                var client = listener.AcceptTcpClient();
                clients.Add(client);
                try
                {
                    var stream = client.GetStream();
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            try
                            {
                                string msg = reader.ReadLine(); // Ошибка в этой строке...
                                Console.WriteLine(msg);
                                writer.WriteLine("Сообщение получено");
                            }
                            finally { Console.Write("_"); }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.WriteLine(++count);
            }
        }
    }
}
