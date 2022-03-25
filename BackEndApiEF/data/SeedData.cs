using System.Linq;
using Domain.entities;
namespace BackEndApiEF.data
{
    public static class SeedData
    {
        public static void seed(ProjectDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            if (dbContext.Accounts.Any())
            {
                return; 
            }
            if(CVSFileReaderLibray.CsvDataReader.GetAccountsDataFromFile() != null)
            {
                foreach(Account ac in CVSFileReaderLibray.CsvDataReader.GetAccountsDataFromFile())
                {
                    dbContext.Accounts.Add(ac);
                    Account account = new Account()
                    {
                       AccountId = ac.AccountId,
                        FirstName = ac.FirstName,
                        LastName = ac.LastName
                    };
                    dbContext.SaveChanges();
                }
                
            }
            dbContext.SaveChanges();
        }
    }
}
