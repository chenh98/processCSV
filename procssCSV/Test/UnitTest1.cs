using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using processCSV;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculateMaxDiff()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("name", typeof(String));
            dt.Columns.Add(dc);

            dc = new DataColumn("value", typeof(long));
            dt.Columns.Add(dc);

            DataRow dr = dt.NewRow();

            dr[0] = "aaa";
            dr[1] = 1000;

            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();

            dr1[0] = "aaa";
            dr1[1] = 2000;

            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();

            dr2[0] = "aaa";
            dr2[1] = 4000;

            dt.Rows.Add(dr2);

            long diffValue = Program.CalculateMaxDiff(dt, "value");
            Assert.AreEqual(diffValue, 2000);
        }


        [TestMethod]
        public void TestFindMaxValue()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Symbol", typeof(string));
            dt.Columns.Add("Quantity", typeof(long));
            dt.Columns.Add("Price", typeof(int));


            DataRow dr = dt.NewRow();

            dr[0] = "aaa";
            dr[1] = 20;
            dr[2] = 130;
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();

            dr1[0] = "aaa";
            dr1[1] = 10;
            dr1[2] = 140;

            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();

            dr2[0] = "aaa";
            dr2[1] = 30;
            dr2[2] = 110;

            dt.Rows.Add(dr2);

            long maxValue = Program.FindMaxValue(dt, "Symbol", "aaa", "Price");
            Assert.AreEqual(maxValue, 140);
        }

        [TestMethod]
        public void TestFindSumValue()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Symbol", typeof(string));
            dt.Columns.Add("Quantity", typeof(long));
            dt.Columns.Add("Price", typeof(int));


            DataRow dr = dt.NewRow();

            dr[0] = "aaa";
            dr[1] = 20;
            dr[2] = 130;
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();

            dr1[0] = "aaa";
            dr1[1] = 10;
            dr1[2] = 140;

            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();

            dr2[0] = "aaa";
            dr2[1] = 30;
            dr2[2] = 110;

            dt.Rows.Add(dr2);

            long sumValue = Program.FindSumValue(dt, "Symbol", "aaa", "Quantity");
            Assert.AreEqual(sumValue, 60);
        }

    }
}
