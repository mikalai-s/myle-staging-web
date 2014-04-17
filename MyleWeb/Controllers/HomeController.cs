using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MyleWeb.Models;
using Newtonsoft.Json;
using OpenLibrary.Document;

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
            this.ViewBag.AccountId = id;

            return View(GetAccountRecords(account).ToArray());
        }

        private IEnumerable<RecordInfo> GetAccountRecords(Account account)
        {
            return EnumerateRecords(account)
                .Distinct(new RecordInfo.Comparer())
                .OrderByDescending(r => r.Record.date);
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

        public FileResult Download(Guid? id)
        {
            var account = Config.Accounts.FirstOrDefault(d => d.Id == id);
            if (account == null) { throw new InvalidOperationException(); }

            var records = GetAccountRecords(account)
                .Select(r => new
                {
                    Text = r.Record.text,
                    Date = r.Date.ToString(),
                    Device = r.Device,
                    Longitude = r.Record.lng,
                    Latitude = r.Record.lat
                });
            var path = Path.Combine(Path.GetTempPath(), "__export.tmp");
            
            Excel.ToExcel(records.ToArray(), path, account.Name + " Records");

            return File(path, "application/vnd.ms-excel", account.Name + " Records.xls");

        }
    }
}
