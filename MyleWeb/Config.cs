using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyleWeb.Models;

namespace MyleWeb
{
    public class Config
    {
        public static List<Account> Accounts = new List<Account>
        {
            new Account
            {
                Name = "Женя",
                Id = new Guid("07C7D0F8-3B5A-4D64-AF47-9562339A6035"),
                Devices = new List<Device>
                {
                    new Device { Name = "iPhone", Id = new Guid("684E1953-63C6-4F0A-B9A2-46B4CEAC8486") }
                }
            },
            new Account
            { 
                Name = "Коля",
                Id = new Guid("E97F0C92-DF71-4ADE-8AD5-4E0EA1A06308"),
                Devices = new List<Device>
                {
                    new Device { Name = "iPhone", Id = new Guid("22656BDD-D958-4BB1-9C84-673820E94E35") }
                }
            },
            new Account
            {
                Name = "Паша", 
                Id = new Guid("61847F90-20A5-49B2-B8F8-F545A6B4BAA9"),
                Devices = new List<Device>
                {
                    new Device { Name = "iPad", Id = new Guid("434472B6-301A-40D7-8080-E6F4CE39CE02") }
                }
            },
            new Account
            {
                Name = "Саша",
                Id = new Guid("289C1CC7-1AAA-4D08-9A48-BBA7BAE2F984"),
                Devices = new List<Device>
                {
                    new Device { Name = "iPhone", Id = new Guid("5A6F9FE9-236C-45D0-B8AC-AB6282811C74") },
                    new Device { Name = "iPad", Id = new Guid("3549A6E1-8B20-4282-902D-A30667C64F27") }
                }
            },
            new Account
            {
                Name = "Юля",
                Id = new Guid("E78C652C-9417-4610-BEFE-8522728F90C5"),
                Devices = new List<Device>
                {
                    new Device { Name = "iPhone", Id = new Guid("F14FD581-92EA-4066-9D89-83EEC48E666F") }
                }
            }
        };

        public static string DisplayTimeZoneId = "Mountain Standard Time";

        public static Guid AdminKey = new Guid("8221991C-9234-4071-80F2-D954A346CE5F");
    }
}