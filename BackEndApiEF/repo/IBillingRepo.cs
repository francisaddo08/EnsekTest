using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndApiEF.repo
{
    public interface IBillingRepo : IGenericDataStore<Domain.entities.MeterRead>
    {
        Task<List<Domain.entities.MeterRead>> currentMeterReading(DateTime dataTime);
        Domain.entities.Account FindAccount(int d);
        int Save();
      
    }
}
