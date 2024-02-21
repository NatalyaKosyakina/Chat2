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
            //this.ct = ct;
        }

        //public void SimpleWork()
        //{
        //    while ()
        //    {
        //        try
        //        {
        //            writer.WriteLine();
        //            writer.Flush();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}


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

        public void Listen()
        {
            try
            {
                Console.WriteLine(reader.ReadLine());
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
