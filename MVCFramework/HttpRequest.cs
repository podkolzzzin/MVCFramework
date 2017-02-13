using System.Collections.Generic;

namespace MVCFramework
{
  internal class HttpRequest
  {
    public string Method { get; set; }

    public string Location { get; set; }

    public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

    public HttpRequest() {
    }
  }
}