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
    }

    public class RecordInfo
    {
        public Record Record;
        public DateTime Date;
        public string Device;
        public string MapUrl;

        public RecordInfo(Record record)
        {
            this.Record = record;
            this.Date = TimeZoneInfo.ConvertTime(DateTime.Parse(record.date + "Z"), TimeZoneInfo.FindSystemTimeZoneById(Config.DisplayTimeZoneId));
            this.MapUrl = string.Format("https://maps.google.com/maps?q={0},{1}", record.lat, record.lng);
        }

        public class Comparer : IEqualityComparer<RecordInfo>
        {
            public bool Equals(RecordInfo x, RecordInfo y)
            {
                if (x == null || y == null) { return false; }

                return x.Record.text == y.Record.text
                    && x.Record.date == y.Record.date
                    && x.Record.lat == y.Record.lat
                    && x.Record.lng == y.Record.lng;
            }

            public int GetHashCode(RecordInfo o)
            {
                return ((o.Record.text == null) ? 0 : o.Record.text.GetHashCode())
                    ^ ((o.Record.date == null) ? 0 : o.Record.date.GetHashCode())
                    ^ o.Record.lat.GetHashCode()
                    ^ o.Record.lng.GetHashCode();
            }
        }
    }
}