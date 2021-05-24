using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting client.");
            System.Threading.Thread.Sleep(2000);

            try
            {
                IPEndPoint srvAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
                TcpClient client = new TcpClient();
                client.Connect(srvAddress);
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                Console.WriteLine("\tClient ready and conected.\n");

                string s = string.Empty;
                while(!s.Equals("Exit"))
                {
                    Console.Write("\tMessage>\t");
                    s = Console.ReadLine();
                    writer.WriteLine(s);
                    writer.Flush();

                    if (!s.Equals("Exit"))
                    {
                        string response = reader.ReadLine();
                        Console.WriteLine("\tResponse:\t" + response);
                    }
                }
                reader.Close();
                writer.Close();
                client.Close();
                Console.WriteLine("\nConection ended.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
