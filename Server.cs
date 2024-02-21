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
    internal class Server
    {
        public HashSet<ClientEx> clients = new HashSet<ClientEx>();
        public TcpListener listener = new TcpListener(IPAddress.Any, 5555);
        bool flag = true;
        CancellationTokenSource cts = new CancellationTokenSource();

        public async void Run()
        {
            try
            {
                listener.Start();
                Console.WriteLine("Сервер запущен");
                ClientEx client = null;

                new Thread(() =>
                {
                    Console.ReadKey();
                    flag = false;
                    client = null;
                    cts.Cancel();
                    Stop();
                    Console.WriteLine("Работа сервера завершена");
                }).Start();
                while (!cts.Token.IsCancellationRequested)
                {
                    client = new ClientEx(listener.AcceptTcpClient());
                    if (client == null || cts.Token.IsCancellationRequested) { break; }
                    clients.Add(client);
                    await Task.Run(() =>
                    {
                        try
                        {
                            while (true)
                            {
                                client.Listen();
                                client.SendMessage("Сообщение получено");
                            }
                        }
                        catch (System.IO.IOException) 
                        {
                            Console.WriteLine("Клиент вышел из чата"); 
                            clients.Remove(client);
                        }                        
                    });
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Stop();
            }
        }
        private void Stop()
        {
            foreach (var client in clients)
            {
                SendToAll("Работа сервера завершена");
                client.Close();
            }
            listener.Stop();
        }

        private void SendToAll(string message, List<ClientEx> except = null)
        {
            foreach (var item in clients)
            {
                try
                {
                    if (except == null || except.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        item.SendMessage(message);
                    }
                }
                catch { }
            }
        }
       
    }
}
