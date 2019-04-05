using System;
using System.Linq;

using System.IO;
using System.Data;
using System.Text;

namespace processCSV
{


    public class Program
    {
        static void Main(string[] args)
        {

            DataTable csvInput = new DataTable();
            var csvOutput = new StringBuilder();
            string strSymble = "";

            try
            {
                csvInput = ReadCSVFile();

                //get the distinct sysmble into an arrey
                var symbleList = (from r in csvInput.AsEnumerable()
                                  select r["Symbol"]).Distinct().ToList();



                for (int i = 0; i < symbleList.Count; i++)
                {

                    strSymble = symbleList[i].ToString();
                    var newOutPutLine = ProcessCSVFile(strSymble, csvInput);

                    csvOutput.AppendLine(newOutPutLine);

                }

            }
            catch (Exception ex)
            {

                csvOutput.AppendLine(ex.Message);

            }

            File.WriteAllText("C:\\Users\\u181432\\Desktop\\CSV\\Output.csv", csvOutput.ToString());

        }


        public static string ProcessCSVFile(string symbol, DataTable dt)
        {
            long maxTimeDiff = 0;

            long totalVol = 0;
            long maxTradePrice = 0;
            string weightedAvePrice = "";
            long totalPrice = 0;
            string newOutPutLine;

            try
            {

                maxTradePrice = FindMaxValue(dt, "Symbol", symbol, "Price");

                totalVol = FindSumValue(dt, "Symbol", symbol, "Quantity");

                totalPrice = FindSumValue(dt, "Symbol", symbol, "TransactionAmt");

                weightedAvePrice = (Convert.ToDecimal(totalPrice) / totalVol).ToString("F");

                maxTimeDiff = Program.CalculateMaxDiff(dt.AsEnumerable()
                    .Where(row => row["Symbol"].ToString() == symbol).CopyToDataTable(), "TimeStamp");

                newOutPutLine = string.Format("{0},{1},{2},{3},{4}", symbol, maxTimeDiff.ToString(), totalVol.ToString(), maxTradePrice.ToString(), weightedAvePrice.ToString());
            }
            catch (Exception ex)
            {
                newOutPutLine = ex.Message;
            }


            return newOutPutLine;

        }


        public static long FindMaxValue(DataTable dt, string key1, string key2, string key3)
        {
            long findValue = Convert.ToInt64(dt.AsEnumerable()
                  .Where(row => row[key1].ToString() == key2)
                  .Max(row => row[key3]));

            return findValue;

        }

        public static long FindSumValue(DataTable dt, string key1, string key2, string key3)
        {
            long findSumValue = Convert.ToInt64(dt.AsEnumerable()
                    .Where(row => row[key1].ToString() == key2)
                    .Sum(row => row.Field<long>(key3)));

            return findSumValue;

        }


        public static long CalculateMaxDiff(DataTable dt, string fieldName)
        {
            long longestDiff = 0;

            //0
            if (dt.Rows.Count == 1) return 0;

            for (int k = 0; k < dt.Rows.Count - 1; k++)
                longestDiff = Math.Max(longestDiff, Convert.ToInt64(dt.Rows[k + 1][fieldName]) - Convert.ToInt64(dt.Rows[k][fieldName]));

            return longestDiff;
        }

        public static DataTable ReadCSVFile()
        {
            DataTable csvData = new DataTable();
            csvData.Columns.Add("TimeStamp", typeof(long));
            csvData.Columns.Add("Symbol", typeof(string));
            csvData.Columns.Add("Quantity", typeof(long));
            csvData.Columns.Add("Price", typeof(int));
            csvData.Columns.Add("TransactionAmt", typeof(long));

            try
            {
                //read input csv file and stored into table
                using (StreamReader sr = new StreamReader("C:\\Users\\u181432\\Desktop\\CSV\\input.csv"))
                {

                    string line = string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] strRow = line.Split(',');
                        DataRow dr = csvData.NewRow();
                        dr["TimeStamp"] = strRow[0];
                        dr["Symbol"] = strRow[1];
                        dr["Quantity"] = strRow[2];
                        dr["Price"] = strRow[3];
                        dr["TransactionAmt"] = (Convert.ToInt64(strRow[2]) * Convert.ToInt16(strRow[3])).ToString();
                        csvData.Rows.Add(dr);
                    }
                }


                //sorted table
                var csvDataSorted = csvData.AsEnumerable()
                       .OrderBy(r => r.Field<string>("Symbol"))
                       .ThenBy(r => r.Field<long>("TimeStamp"))
                       .CopyToDataTable();

                return csvDataSorted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }

}
