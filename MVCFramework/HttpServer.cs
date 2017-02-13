using System;
using System.IO;
using System.Linq;

namespace MVCFramework
{
  internal class HttpServer
  {
    private StreamReader reader;
    private StreamWriter writer;

    public HttpServer(StreamReader streamReader, StreamWriter streamWriter) {
      this.reader = streamReader;
      this.writer = streamWriter;
    }

    internal HttpRequest Parse() {

      HttpRequest request = new HttpRequest();

      string line = reader.ReadLine();
      var parts = line.Split(' ');
      request.Method = parts[0];
      request.Location = parts[1];


      while ((line = reader.ReadLine()) != null) {
        if (line == String.Empty)
          break;

        parts = line.Split(':').Select(f => f.Trim()).ToArray();
        request.Headers.Add(parts[0], parts[1]);
      }

      return request;
    }

    internal void Write(string result) {

      writer.Write(result);
      writer.Flush();
    }
  }
}