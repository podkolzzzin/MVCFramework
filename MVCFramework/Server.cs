using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVCFramework
{
  class Server
  {
    private TcpListener listener;

    public Server() {

      listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80));
    }

    public void Start() {

      listener.Start();

      TcpClient client = listener.AcceptTcpClient();
      using (var stream = client.GetStream()) {
        HttpServer server = new HttpServer(new StreamReader(stream), new StreamWriter(stream));
        HttpRequest request = server.Parse();
        Router r = new Router();
        string result = r.Route(request);
        server.Write(result);
      }
      client.Close();
    }
  }
}
