using System;

namespace BackEndApiEF.entities
{
    public class MeterRead
    {
        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
        public string X { get; set; }
    }
}
