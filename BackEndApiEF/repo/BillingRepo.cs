using BackEndApiEF.data;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndApiEF.repo
{
    public class BillingRepo : GenericRepo<Domain.entities.MeterRead>, IBillingRepo
    {
        public BillingRepo(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<MeterRead>> currentMeterReading(DateTime dateTime)
        {
            return await _context.MeterReads.Where(m => m.CreatedOnDate == dateTime).ToListAsync();
        }

        public  Account FindAccount(int d)
        {
          return _context.Accounts.Find(d);
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
