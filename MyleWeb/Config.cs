using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyleWeb.Models;

namespace MyleWeb
{
    public class Config
    {
        public static List<Device> Devices = new List<Device>()
        {
            new Device { Name = "Женя iPhone", Id = "684E1953-63C6-4F0A-B9A2-46B4CEAC8486" },
            new Device { Name = "Коля iPhone", Id = "22656BDD-D958-4BB1-9C84-673820E94E35" },
            new Device { Name = "Паша iPad", Id = "434472B6-301A-40D7-8080-E6F4CE39CE02" },
            new Device { Name = "Саша iPhone", Id = "5A6F9FE9-236C-45D0-B8AC-AB6282811C74" },
            new Device { Name = "Саша iPad", Id = "3549A6E1-8B20-4282-902D-A30667C64F27" },
            new Device { Name = "Юля iPhone", Id = "F14FD581-92EA-4066-9D89-83EEC48E666F" }
        };

        public static string DisplayTimeZoneId = "Mountain Standard Time";
    }
}