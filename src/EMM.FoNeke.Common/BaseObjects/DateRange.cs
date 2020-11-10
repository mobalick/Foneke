using System;

namespace EMM.FoNeke.Common.BaseObjects
{
    public class DateRange
    {
        private DateTime? _from;
        private DateTime? _to;

        public DateTime? From
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FromToString))
                {
                    _from = DateTime.Parse(FromToString.Split('-')[0]);
                }
                return _from;
            }
            set => _from = value;
        }

        public DateTime? To
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FromToString))
                {
                    _from = DateTime.Parse(FromToString.Split('-')[1]);
                }
                return _to;
            }
            set => _to = value;
        }

        public string FromToString { get; set; }
    }
}