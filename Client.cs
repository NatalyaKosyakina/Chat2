﻿using System;
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
        CancellationTokenSource cts = new CancellationTokenSource();
        public async Task Run() {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect("127.0.0.1", 5555);
                    reader = new StreamReader(client.GetStream());
                    writer = new StreamWriter(client.GetStream());
                    string infoTo = "-1";
                    bool flag = true;
                    if (reader != null && writer != null)
                    {
                        while (!cts.IsCancellationRequested && flag)
                        {
                            Thread.Sleep(100);
                            Console.WriteLine("Введите сообщение: ");
                             infoTo = Console.ReadLine();
                            Task.Run(() => 
                            {
                                if (string.IsNullOrEmpty(infoTo) || infoTo.Equals("Exit"))
                                {
                                    flag = false;
                                    cts.Cancel();
                                }
                                else
                                {
                                    writer.WriteLine(infoTo);
                                    writer.Flush();
                                    string msgFromServer = reader.ReadLine();
                                    if (msgFromServer != null)
                                    {
                                        Console.WriteLine(msgFromServer);
                                    }
                                    else 
                                    {
                                        Console.WriteLine("Работа сервера была завершена");
                                        cts.Cancel();
                                    }
                                }
                            });
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
