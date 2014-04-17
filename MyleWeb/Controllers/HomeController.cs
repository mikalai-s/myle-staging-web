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
        public ActionResult Index(Guid? key)
        {   
            if(key != Config.AdminKey) { return HttpNotFound(); }

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(Config.DisplayTimeZoneId);

            var dir = this.HttpContext.Server.MapPath("~/Uploads");
            var infos = Config.Accounts
                .Select(a =>
                    {
                        var files = a.Devices
                            .SelectMany(d => Directory.GetFiles(dir, "*" + d.Id + ".json", SearchOption.AllDirectories))
                            .Select(f => new FileInfo(f))
                            .ToArray();
                        return new AccountInfo
                        {
                            Account = a,
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

        public ActionResult Records(Guid? id)
        {
            var account = Config.Accounts.FirstOrDefault(d => d.Id == id);
            if(account == null) { return HttpNotFound(); }

            this.ViewBag.Title = account.Name + " Records";

            var records = EnumerateRecords(account)
                .Distinct(new RecordInfo.Comparer())
                .OrderByDescending(r => r.Record.date)
                .ToArray();

            return View(records);
        }

        private IEnumerable<RecordInfo> EnumerateRecords(Account account)
        {
            var dir = this.HttpContext.Server.MapPath("~/Uploads");

            foreach (var device in account.Devices)
            {
                var content = Directory
                    .GetFiles(dir, "*" + device.Id + ".json", SearchOption.AllDirectories)
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTimeUtc)
                    .FirstOrDefault()
                    .OpenText()
                    .ReadToEnd();

                foreach (var record in JsonConvert.DeserializeObject<Record[]>(content))
                {
                    yield return new RecordInfo(record)
                    {
                        Device = device.Name
                    };
                }
            }
        }

        public ContentResult Download(string id)
        {
            //new ContentResult().Content = 
            return null;
        }
    }
}
