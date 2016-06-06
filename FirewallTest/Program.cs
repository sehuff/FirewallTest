using System;
using System.Linq;

namespace FirewallTest
{
    class Program
    {
        private static string server { get; set; } = string.Empty;
        private static string port { get; set; } = string.Empty;

        static void Main(string[] args)
        {
            GetArguments(args);

            if(server == string.Empty || port == string.Empty)
            {
                Console.WriteLine("Returns Firewall Open or Firewall Closed.");
                Console.WriteLine("Usage: FirewallTest /S sever_name /P port_number");
                return;           
            }
            
            var bolResponse =  TestAddress(server, int.Parse(port));            
            if(bolResponse)
            {
                Console.WriteLine("Firewall Open.");
            }
            else
            {
                Console.WriteLine("Firewall Closed.");
            }
        }

        static private void GetArguments(string[] args)
        {
            for (var x = 0; x < args.Count(); x++)
            {
                switch (args[x].Trim().ToUpper())
                {
                    case "/S":
                        server = args[x + 1];
                        break;
                    case "/P":
                        port = args[x + 1];
                        break;
                }
            }
        }
        static private bool TestAddress(string address, int port)
        {
            try
            {
                var tcpClient = new System.Net.Sockets.TcpClient();
                tcpClient.Connect(address, port);
                var networkstream = tcpClient.GetStream();
                if (networkstream.CanRead)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {                
                return false;
            }            
        }
        
    }
}
