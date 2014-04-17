using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyleWeb.Models
{
    public class Account
    {
        public string Name;
        public Guid Id;
        public List<Device> Devices;
    }

    public class AccountInfo
    {
        public Account Account;
        public int TimesExported;
        public DateTime LastExported;
    }
}