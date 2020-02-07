using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace tictactoe
{
    class TcpServer
    {
        public TcpListener Server = null;
        public TcpClient Connection = null;
        public IPAddress IP { get; private set; }
        public int Port { get; private set; }
        public void StartServer(int port)
        {
            this.Port = Port;
            this.IP = GetLocalIPAddress();
            this.Server = new TcpListener(this.IP, port);

            this.Server.Start();
            Console.WriteLine();
        }

        public void WaitForConnections()
        {
            this.Connection = this.Server.AcceptTcpClient();
            Console.WriteLine($"Accepted connection from {this.Connection.Client.RemoteEndPoint.AddressFamily}");
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
