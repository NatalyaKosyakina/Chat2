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
            try
            {
                client.Connect("127.0.0.1", 5555);
                var stream = client.GetStream();
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
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
                                writer.WriteLine(info);
                                Task.Run(async () =>
                                {
                                    Console.WriteLine(await reader.ReadLineAsync());
                                }).Wait();
                            }
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void Run2()
        {
            TcpClient client = new TcpClient();
            try
            {
                client.Connect("127.0.0.1", 5555);
                var stream = client.GetStream();
                //Stream second = new MemoryStream();
                //stream.CopyTo(second);
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
                        //using (StreamWriter writer = new StreamWriter(stream))
                        //{
                        //    writer.WriteLine(info);
                        //}
                        Task.Run(async () =>
                        {
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                Console.WriteLine(await sr.ReadLineAsync());
                            }
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
