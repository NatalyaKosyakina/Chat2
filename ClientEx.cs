using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat2
{
    public class ClientEx
    {
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;

        public ClientEx(TcpClient client)
        {
            this.client = client;
            var stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void Listen()
        {
            string info = "-1";
            try
            {
                while (true)
                {
                    try
                    {
                        info = reader.ReadLine();
                        Console.WriteLine(info);
                        writer.WriteLine("Сообщение получено");
                        writer.Flush();
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception e){ Console.WriteLine(e.Message); }
        }
        public void sendMessage(string message)
        {
            try
            {
                writer.WriteLine(message);
                writer.Flush();
            }
            catch { }
        }

        public void Close()
        {
            writer.Close();
            reader.Close();
            client.Close();
        }
    }
}
