using System;
using System.Collections.Generic;
using System.Text;
using Domain.entities;

namespace Domain.Models
{
    public class MeterReadingResponseMode
    {
        public List<MeterRead> sucessfulMeterReads { get; set; }
        public List<MeterReadModel> failedMeterReads { get; set; }
    }
}
