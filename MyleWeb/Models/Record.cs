using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyleWeb.Models
{
    public class Record
    {
        public string text;
        public string date;
        public double lat;
        public double lng;

        public DateTime LocalDate
        {
            get
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(Config.DisplayTimeZoneId);
                return TimeZoneInfo.ConvertTime(DateTime.Parse(this.date + "Z"), timeZone);
            }
        }

        public class Comparer : IEqualityComparer<Record>
        {
            public bool Equals(Record x, Record y)
            {
                if (x == null || y == null) { return false; }

                return x.text == y.text
                    && x.date == y.date
                    && x.lat == y.lat
                    && x.lng == y.lng;
            }

            public int GetHashCode(Record o)
            {
                return ((o.text == null) ? 0 : o.text.GetHashCode())
                    ^ ((o.date == null) ? 0 : o.date.GetHashCode())
                    ^ o.lat.GetHashCode()
                    ^ o.lng.GetHashCode();
            }
        }
    }
}