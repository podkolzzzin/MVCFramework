using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCFramework;

namespace MatchApplication.Controllers
{
  public class Index : ControllerBase
  {
    public string GetIndex() {

      return "Hello World";
    }

    public string GetPhotos() {

      return View("HTMLPage1.html", new Dictionary<string, object>() {
        ["PhotosCount"] = 10
      });
    }
  }
}
