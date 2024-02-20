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

        public void SendMessage(string message)
        {
            try
            {
                writer.WriteLine();
                writer.Flush();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message); 
            }
        }

        public async Task Listen()
        {
            try
            {
                string info = await reader.ReadLineAsync();
                Console.WriteLine(info);
            }
            catch (Exception) { throw; }
        }
        public void Close()
        {
            writer.Close();
            reader.Close();
            client.Close();
        }
    }
}
