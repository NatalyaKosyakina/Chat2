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
        StreamReader reader = null;
        StreamWriter writer = null;

        public async void Run() {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect("127.0.0.1", 5555);
                    reader = new StreamReader(client.GetStream());
                    writer = new StreamWriter(client.GetStream());
                    if (reader != null && writer != null)
                    {
                        while (true)
                        {
                            Thread.Sleep(100);
                            Console.WriteLine("Введите сообщение: ");
                            string info = Console.ReadLine();
                            if (string.IsNullOrEmpty(info) || info.Equals("Exit"))
                            {
                                break;
                            }
                            else
                            {
                                writer.WriteLine(info);
                                writer.Flush();
                                Console.WriteLine(reader.ReadLine());
                            }
                        }
                    }
                }
                catch (System.IO.IOException) {
                    Console.WriteLine("Работа сервера была завершена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally { reader?.Dispose(); }
            }
        }
    }
}
