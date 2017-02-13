using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFramework
{
  public abstract class ControllerBase
  {
    public string View(string name, Dictionary<string, object> viewData) {

      var asm = GetType().Assembly;
      string format = new StreamReader(asm.GetManifestResourceStream($"{asm.GetName().Name}.Views.{name}"))
        .ReadToEnd();

      int prev = 0;

      while (true) {
        int start = format.IndexOf("<%", prev) + 2;
        if (start < 0) {
          break;
        }
        int end = format.IndexOf("%>", start);

        string varName = format.Substring(start, end - start);


        if (varName.StartsWith("foreach") && varName.Contains(" ")) {
          ForeachProcessor(format, varName);
        }
        else if (varName.IndexOf(".") < 0) {
          format = format.Substring(0, start - 2) + viewData[varName] + format.Substring(end + 2);
        }



        prev = end;
      }

      return format;
    }

    private void ForeachProcessor(string format, string varName) {

      var data = varName.Substring("foreach".Length).Split(new string[] { "in" }, StringSplitOptions.None).Select(f => f.Trim()).ToArray();
    }
  }
}
