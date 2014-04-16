using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyleWeb.Models;
using Newtonsoft.Json;

namespace MyleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(Config.DisplayTimeZoneId);

            var dir = this.HttpContext.Server.MapPath("~/Uploads");
            var infos = Config.Devices
                .Select(d => 
                    {
                        var files = Directory
                            .GetFiles(dir, "*" + d.Id + ".json", SearchOption.AllDirectories)
                            .Select(f => new FileInfo(f))
                            .ToArray();
                        return new DeviceInfo
                        {
                            Device = d,
                            TimesExported = files.Length,
                            LastExported = files
                                .Select(f => TimeZoneInfo.ConvertTime(f.CreationTimeUtc, timeZone))
                                .OrderByDescending(t => t)
                                .FirstOrDefault()
                        };
                    })
                .ToArray();

            return View(infos);
        }

        public ActionResult Records(string id)
        {
            var device = Config.Devices.FirstOrDefault(d => d.Id == id);
            if(device == null) { return HttpNotFound(); }

            this.ViewBag.Title = device.Name + " Records";

            var dir = this.HttpContext.Server.MapPath("~/Uploads");
            var file = Directory
                .GetFiles(dir, "*" + id + ".json", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.CreationTimeUtc)
                .FirstOrDefault()
                .OpenText()
                .ReadToEnd();

            var records = JsonConvert.DeserializeObject<Record[]>(file)
                .Distinct(new Record.Comparer())
                .OrderByDescending(r => r.date)
                .ToArray();

            return View(records);
        }

        public ContentResult Download(string id)
        {
            //new ContentResult().Content = 
            return null;
        }
    }
}
