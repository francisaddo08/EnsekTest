using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MeterReadModel
    {
        public int AccountId { get; set; }

        public string MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
       
    }
}
