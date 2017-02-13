using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCFramework
{
  class Router
  {
    public Router() {
    }

    public string Route(HttpRequest request) {

      var assembly = ConfigurationManager.AppSettings["WebApplicationAssembly"];
      var controlersNamespace = ConfigurationManager.AppSettings["ControllersNamespace"];

      Assembly asm;
      if (Path.IsPathRooted(assembly))
        asm = Assembly.LoadFile(assembly);
      else
        asm = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, assembly));

      string searchType = $"{controlersNamespace}.{request.Location.Substring(1).ToLowerInvariant()}";

      var type = asm.GetTypes().FirstOrDefault(f => f.Name.ToLowerInvariant() ==  searchType);
      if (type == null) {
        type = asm.GetType($"{controlersNamespace}.Index");
        if (type == null) {
          throw new InvalidOperationException("No IndexController Found!");
        }
      }

      string methodName = GetMethodName(request);
      var method = type.GetMethod(methodName);
      
      return (string)method.Invoke(Activator.CreateInstance(type), new object[0]);
    }

    private string GetMethodName(HttpRequest request) {

      var inLower = request.Method.ToLowerInvariant();
      inLower = char.ToUpperInvariant(inLower[0]) + inLower.Substring(1);

      var m = request.Location.Split('/').ToArray();
      string method;
      if (m.Length < 2)
        method = "Index";
      else
        method = m[1];

      return inLower + method;
    }
  }
}
