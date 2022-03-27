using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BackEndApiEF.repo;
using Domain.entities;
using Domain.Models;

namespace Billing.Test
{
    public class BillingRepoFake : IBillingRepo
    {
       

        public BillingRepoFake()
        {}



        public void AddRange(IEnumerable<MeterRead> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<MeterRead>> allMeterReading()
        {
            throw new NotImplementedException();
        }

        public Task<List<MeterRead>> currentMeterReading(DateTime dataTime)
        {
            throw new NotImplementedException();
        }

        public Account FindAccount(int d)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeterRead> GetAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<MeterRead> entities)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}
