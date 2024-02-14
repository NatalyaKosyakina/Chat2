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
        public async Task Run()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("127.0.0.1", 5555);
                    while (true)
                    {
                        Console.Out.WriteLineAsync("Введите сообщение");
                        string info = Console.ReadLine();
                        if (!string.IsNullOrEmpty(info))
                        {
                            using (StreamWriter sw = new StreamWriter(tcpClient.GetStream()))
                            {
                                sw.WriteAsync(info);
                            }
                        }
                        Task.Run(() =>
                        {
                            using (StreamReader reader = new StreamReader(tcpClient.GetStream())) {
                                reader.ReadToEndAsync();
                            }
                        });
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Console.WriteLine("Сервер выключен");
                }
            }
            
        }

        public void Run2()
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 5556);
                Thread send = new Thread(() =>
                {
                    using (StreamWriter writer = new StreamWriter(tcpClient.GetStream()))
                    {
                        while (true)
                        {
                            Console.WriteLine("Введите сообщение: ");
                            string info = Console.ReadLine();
                            if (!string.IsNullOrEmpty(info))
                            {
                                writer.WriteAsync(info);
                            }
                            Thread.Sleep(1000);
                        }
                    }

                });
                send.Start();
                Thread receive = new Thread(async () =>
                {
                    using (StreamReader reader = new StreamReader(tcpClient.GetStream()))
                    {
                        while (true)
                        {
                            string msg = await reader.ReadToEndAsync();
                            await Console.Out.WriteLineAsync(msg);
                            Thread.Sleep(100);
                        }
                    }
                });
                receive.Start();
            }
            catch (System.Net.Sockets.SocketException) {
                Console.WriteLine("Сервер не запущен");
            }


        }

        public void Run3()
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect("127.0.0.1", 5556);

                StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                Console.WriteLine("Введите сообщение: ");
                string info = Console.ReadLine();
                if (!string.IsNullOrEmpty(info))
                {
                    writer.WriteAsync(info);
                }
                Thread.Sleep(1000);

                StreamReader reader = new StreamReader(tcpClient.GetStream());
                string msg = reader.ReadToEnd();
                Console.WriteLine(msg);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
