using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace Domain.entities
{
    public class MeterRead
    {
        public int ID { get; set; }
        public int AccountId { get; set; }
        public string MeterReadingDateTime { get; set; }
        public String MeterReadValue { get; set; }
       
    }
}
