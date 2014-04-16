using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyleWeb.Models
{
    public class Device
    {
        public string Name;
        public string Id;
    }

    public class DeviceInfo
    {
        public Device Device;
        public int TimesExported;
        public DateTime LastExported;
    }
}