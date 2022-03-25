using System;

using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;

using System.Text;
using System.Data;
using LumenWorks.Framework.IO.Csv;
using Domain.entities;
using Domain.Models;

namespace CVSFileReaderLibray
{
    public static class CsvDataReader
    {
        public static List<Account> GetAccountsDataFromFile()
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
            return accountList;
          
        }
        public static List<MeterReadModel> GetMeterData(string path)
        {
            var csvTable = new DataTable();
            List<MeterReadModel> meterReadModels = new List<MeterReadModel>();

            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
                for (int i = 0; i < csvTable.Rows.Count; i++)
                {
                    meterReadModels.Add(new MeterReadModel { AccountId = int.Parse(csvTable.Rows[i][0].ToString()), MeterReadingDateTime = csvTable.Rows[i][1].ToString(), MeterReadValue = csvTable.Rows[i][2].ToString() });
                }

            }
            return meterReadModels;

        }
        public static DataTable GetMeterDataFromFile()
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead("C:/Ensek/api/BillingEnsekTest/Domain/data/Meter_Reading.csv")), true))
            {
                csvTable.Load(csvReader);
                return csvTable;



            }
        }
        public static DataTable GetMeterDataUpLoadFile(string filename)
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filename)), true))
            {
                csvTable.Load(csvReader);
                return csvTable;



            }
        }
    }
}
