using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat2
{
    public class ClientEx
    {
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;
        //CancellationToken ct;


        public ClientEx(TcpClient client)
        {
            this.client = client;
            var stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }


        public async Task SimpleWork(string message, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                string msg = await reader.ReadLineAsync();
                await Console.Out.WriteLineAsync(msg);
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();
            }
        }
        public void SendMessage(string message)
        {
            try
            {
                writer.WriteLine(message);
                writer.Flush();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message); 
            }
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                await writer.WriteLineAsync(message);
                writer.Flush();
            }
            catch (Exception e)
            {
                throw new System.IO.IOException(e.Message);
            }
        }

        public void Listen()
        {
            try
            {
                Console.WriteLine(reader.ReadLine());
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
