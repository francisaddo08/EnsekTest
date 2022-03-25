using System;


using System.IO;

using System.Globalization;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using CVSFileReaderLibray;
namespace ConsoleAppTest
{
    public class Program
    {
        static void Main(string[] args)
        {
           
         foreach(var data in CsvDataReader.GetAccountsDataFromFile())
            {
                Console.WriteLine(data.AccountId);
            }
            Console.WriteLine("------------------------------------------------------------------");
            DataTable table = CsvDataReader.GetMeterDataFromFile();
            for(int i = 0; i < table.Rows.Count; i++)
            {
               
                Console.WriteLine(table.Rows[i][0]);
            }

        }
        public void readcsvdata()
        {
            var csvTable = new DataTable();
            List<Account> accountList = new List<Account>();

            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead("C:/Ensek/api/BillingEnsekTest/Domain/data/Test_Accounts.csv")), true))
            {
                csvTable.Load(csvReader);
                for (int i = 0; i < csvTable.Rows.Count; i++)
                {
                    accountList.Add(new Account { AccountId = int.Parse(csvTable.Rows[i][0].ToString()), FirstName = csvTable.Rows[i][1].ToString(), LastName = csvTable.Rows[i][2].ToString() });
                }

            }
        }
    }
}
