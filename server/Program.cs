using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace server
{
    class Server
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting server.");
            TcpListener listener = null;
            try
            {
                IPEndPoint srvAddress = new IPEndPoint(IPAddress.Loopback, 9000);
                listener = new TcpListener(srvAddress);
                listener.Start();
                Console.WriteLine("\tServer started.");

                while(true)
                {
                    Console.WriteLine();
                    Console.WriteLine("\tWaiting for connections...\n");
                    TcpClient client = listener.AcceptTcpClient();
                    StreamReader reader = new StreamReader(client.GetStream());
                    StreamWriter writer = new StreamWriter(client.GetStream());

                    string s = string.Empty;
                    while( !(s = reader.ReadLine()).Equals("Exit") || (s==null))
                    {
                        Console.WriteLine("\tMessage:\t" + s);
                        Console.Write("\tResponse>\t");
                        String r = Console.ReadLine();
                        Console.WriteLine("\t--Waiting--");
                        writer.WriteLine(r + " (Original message: " + s + ")");
                        writer.Flush();
                    }
                    reader.Close();
                    writer.Close();
                    client.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if(listener !=null)
                {
                    listener.Stop();
                }
            }

        }
    }
}
