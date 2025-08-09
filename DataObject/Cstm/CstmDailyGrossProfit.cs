using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{
    public class SumGrossProfit : IDataMapper<SumGrossProfit>
    {
        public DateTime Date
        {
            get
            {
                return new DateTime(this.Year, this.Sequence, 1);
            }
        }
        public string Month { get; set; }
        public int Year { get; set; }
        public int Sequence { get; set; }
        public decimal TotalGrossProfit { get; set; }
        public decimal TotalSales { get; set; }


        public SumGrossProfit Map(System.Data.IDataReader reader)
        {
            SumGrossProfit obj = new SumGrossProfit();
            obj.Month = string.Format("{0}", reader["Month"]);
            obj.Year = reader["Year"] is DBNull ? 0 : Convert.ToInt32(reader["Year"]);
            obj.Sequence = reader["Sequence"] is DBNull ? 0 : Convert.ToInt32(reader["Sequence"]);
            obj.TotalSales = reader["TotalSales"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSales"]);
            obj.TotalGrossProfit = reader["TotalGrossProfit"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalGrossProfit"]);
            return obj;
        }
    }

    public class CstmTotalSalePerMonth : IDataMapper<CstmTotalSalePerMonth>
    {
        public string MonthYear { get; set; }
        public DateTime TransDate { get; set; }
        public string CatalogName { get; set; }
        public int CatalogID { get; set; }
        public decimal Quantity { get; set; }
        //public decimal HPP { get; set; }
        //public decimal Purchase { get; set; }
        //public decimal GrossProfit { get; set; }
        public decimal TotalSale { get; set; }
        public string Unit { get; set; }

        public CstmTotalSalePerMonth Map(System.Data.IDataReader reader)
        {
            CstmTotalSalePerMonth obj = new CstmTotalSalePerMonth();
            try
            {
                obj.MonthYear = string.Format("{0}", reader["MONTH_YEAR"]);
                obj.TransDate = Convert.ToDateTime(reader["TransDate"]);
                obj.CatalogName = string.Format("{0}", reader["Item"]);
                obj.CatalogID = reader["ItemID"] is DBNull ? 0 : Convert.ToInt32(reader["ItemID"]);
                obj.Unit = string.Format("{0}", reader["Unit"]);
                obj.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToDecimal(reader["Quantity"]);
                obj.TotalSale = reader["Sale"] is DBNull ? 0 : Convert.ToDecimal(reader["Sale"]);
                //obj.HPP = reader["HPP"] is DBNull ? 0 : Convert.ToDecimal(reader["HPP"]);
                //obj.Purchase = reader["Purchase"] is DBNull ? 0 : Convert.ToDecimal(reader["Purchase"]);
                //obj.GrossProfit = reader["GrossProfit"] is DBNull ? 0 : Convert.ToDecimal(reader["GrossProfit"]);

            }
            catch (Exception)
            {

            }



            return obj;
        }

    }

    public class TotalSale : IDataMapper<TotalSale>
    { 
    //TotalSale, t.TransDate ,t.CatalogID
        public DateTime TransDate { get; set; }
        //public int CatalogID { get; set; }
        public decimal Amount { get; set; }

        public TotalSale Map(System.Data.IDataReader reader)
        {
            TotalSale obj = new TotalSale();
            obj.TransDate = Convert.ToDateTime(reader["TransDate"]);
            //obj.CatalogID = reader["CatalogID"] is DBNull ? 0 : Convert.ToInt32(reader["CatalogID"]);         
            obj.Amount = reader["TotalSale"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSale"]);
            return obj;
        }
    }
    public class DailyGrossProfit : IDataMapper<DailyGrossProfit>
    {
        public DateTime TransDate { get; set; }
        public string Item { get; set; }
        public int ItemID { get; set; }
        public decimal Quantity { get; set; }
        public decimal HPP { get; set; }
        public decimal Purchase { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal Sale { get; set; }
        public string Unit { get; set; }

        public DailyGrossProfit Map(System.Data.IDataReader reader)
        {
            DailyGrossProfit obj = new DailyGrossProfit();
            try
            {
                obj.TransDate = Convert.ToDateTime(reader["TransDate"]);
                obj.Item = string.Format("{0}", reader["Item"]);
                obj.ItemID = reader["ItemID"] is DBNull ? 0 : Convert.ToInt32(reader["ItemID"]);
                obj.Unit = string.Format("{0}", reader["Unit"]);
                obj.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToDecimal(reader["Quantity"]);
                obj.Sale = reader["Sale"] is DBNull ? 0 : Convert.ToDecimal(reader["Sale"]);
                obj.HPP = reader["HPP"] is DBNull ? 0 : Convert.ToDecimal(reader["HPP"]);
                obj.Purchase = reader["Purchase"] is DBNull ? 0 : Convert.ToDecimal(reader["Purchase"]);
                obj.GrossProfit = reader["GrossProfit"] is DBNull ? 0 : Convert.ToDecimal(reader["GrossProfit"]);
           
            }
            catch (Exception)
            {
                
            }
            
       

            return obj;
        }

    }
    public class CstmTotalSalePerCustomer : IDataMapper<CstmTotalSalePerCustomer>
    {
        public string CustomerName { get; set; }

        public decimal TotalSale { get; set; }
        public string MonthYear { get; set; }

        public DateTime TransDate { get; set; }

        public CstmTotalSalePerCustomer Map(System.Data.IDataReader reader)
        {
            CstmTotalSalePerCustomer obj = new CstmTotalSalePerCustomer();
            obj.CustomerName = string.Format("{0}", reader["CustomerName"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }

    public class CstmTotalSalePerDay : IDataMapper<CstmTotalSalePerDay>
    {
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public int CatalogID { get; set; }
        public int Qty { get; set; }
        public decimal TotalSales { get; set; }

        public DateTime TransDate { get; set; }

        public CstmTotalSalePerDay Map(System.Data.IDataReader reader)
        {
            CstmTotalSalePerDay obj = new CstmTotalSalePerDay();
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.TotalSales = reader["TotalSales"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSales"]);
            obj.CatalogID = reader["CatalogID"] is DBNull ? 0 : Convert.ToInt32(reader["CatalogID"]);
            obj.Qty = reader["Qty"] is DBNull ? 0 : Convert.ToInt32(reader["Qty"]);
            obj.TransDate = Convert.ToDateTime(reader["TransactionDate"]);

            return obj;
        }

    }


    public class CstmPerformancePerMonth : IDataMapper<CstmPerformancePerMonth>
    {
        //public string CatalogName { get; set; }
        //public int CatalogID { get; set; }
        public decimal TotalSale { get; set; }
        public string MonthYear { get; set; }

        public DateTime TransDate { get; set; }

        public CstmPerformancePerMonth Map(System.Data.IDataReader reader)
        {
            CstmPerformancePerMonth obj = new CstmPerformancePerMonth();
            //obj.CatalogName = string.Format("{0}", reader["CATALOG_NAME"]);
            //obj.CatalogID = Convert.ToInt32(reader["CATALOG_ID"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }



    public class CstmDailySalesPerCatalog : IDataMapper<CstmDailySalesPerCatalog>
    {
        public string CatalogName { get; set; }
        public int CatalogID { get; set; }
        public decimal TotalSale { get; set; }
        public string MonthYear { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TransDate { get; set; }
        public string Unit { get; set; }

        public CstmDailySalesPerCatalog Map(System.Data.IDataReader reader)
        {
            CstmDailySalesPerCatalog obj = new CstmDailySalesPerCatalog();
            obj.CatalogName = string.Format("{0}", reader["CATALOG_NAME"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.CatalogID = Convert.ToInt32(reader["CATALOG_ID"]);
            obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
            obj.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToDecimal(reader["Quantity"]);
            obj.MonthYear = reader["MONTH_YEAR"].ToString();
            obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

            return obj;
        }

    }

   

    //public class DailyGrossProfit : IDataMapper<DailyGrossProfit>
    //{
    //    public string CatalogName { get; set; }
    //    public int CatalogID { get; set; }
    //    public decimal TotalSale { get; set; }
    //    public string MonthYear { get; set; }
    //    public decimal Quantity { get; set; }
    //    public DateTime TransDate { get; set; }

    //    public DailyGrossProfit Map(System.Data.IDataReader reader)
    //    {
    //        DailyGrossProfit obj = new DailyGrossProfit();
    //        obj.CatalogName = string.Format("{0}", reader["CATALOG_NAME"]);
    //        obj.CatalogID = Convert.ToInt32(reader["CATALOG_ID"]);
    //        obj.TotalSale = reader["TOTAL_SALE"] is DBNull ? 0 : Convert.ToDecimal(reader["TOTAL_SALE"]);
    //        obj.Quantity = reader["Quantity"] is DBNull ? 0 : Convert.ToDecimal(reader["Quantity"]);
    //        obj.MonthYear = reader["MONTH_YEAR"].ToString();
    //        obj.TransDate = Convert.ToDateTime(reader["YEAR_MONTH_DAY"]);

    //        return obj;
    //    }

    //}

    public class CstmMonthlyGrossProfit : IDataMapper<CstmMonthlyGrossProfit>
    {

        //SELECT c.ID, c.Name, c.MONTH_YEAR ,  tt.TotalSale, yy.TotalPurchase ,
        //(tt.TotalSale - yy.TotalPurchase) AS MonthlyGrossProfit
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalPurchase { get; set; }
        public string MONTH_YEAR { get; set; }
        public DateTime TempMonth { get; set; }

        public decimal MonthlyGrossProfit { get; set; }

        public CstmMonthlyGrossProfit Map(System.Data.IDataReader reader)
        {
            CstmMonthlyGrossProfit obj = new CstmMonthlyGrossProfit();
            obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
            obj.Unit = string.Format("{0}", reader["Unit"]);
            obj.TotalSale = reader["TotalSale"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalSale"]);
            obj.TotalPurchase = reader["TotalPurchase"] is DBNull ? 0 : Convert.ToDecimal(reader["TotalPurchase"]);
            obj.MonthlyGrossProfit = reader["MonthlyGrossProfit"] is DBNull ? 0 : Convert.ToDecimal(reader["MonthlyGrossProfit"]);
            obj.MONTH_YEAR = string.Format("{0}", reader["MONTH_YEAR"]);
            obj.TempMonth = reader["TempMonth"] is DBNull ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["TempMonth"]);
            return obj;
        }

    }
    //public class CstmDailyGrossProfit : IDataMapper<CstmDailyGrossProfit>
    //{
    //    public int CatalogID { get; set; }
    //    public string CatalogName { get; set; }
    //    public string Unit { get; set; }
    //    public decimal SellPrice { get; set; }
    //    public decimal BuyPrice { get; set; }
    //    public DateTime CatalogDate { get; set; }

    //    public decimal Stock { get; set; }
    //    public decimal DailyGrossProfit { get; set; }

    //    public CstmDailyGrossProfit Map(System.Data.IDataReader reader)
    //    {
    //        CstmDailyGrossProfit obj = new CstmDailyGrossProfit();
    //        obj.CatalogID = Convert.ToInt32(reader["CatalogID"]);
    //        obj.CatalogName = string.Format("{0}", reader["CatalogName"]);
    //        obj.Unit = string.Format("{0}", reader["Unit"]);
    //        obj.SellPrice = reader["SellPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["SellPrice"]);
    //        obj.BuyPrice = reader["BuyPrice"] is DBNull ? 0 : Convert.ToDecimal(reader["BuyPrice"]);
    //        obj.CatalogDate = (reader["CatalogDate"] is System.DBNull) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(reader["CatalogDate"]);
    //        obj.Stock = reader["Stock"] is DBNull ? 0 : Convert.ToDecimal(reader["Stock"]);



    //        return obj;
    //    }

    //}

    public class CstmDailyGrossProfit //: IDataMapper<CstmDailyGrossProfit>
    {
        public int CatalogID { get; set; }
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public decimal TotalSales { get; set; }
        private decimal? _HPP = 0;

        public decimal? HPP
        {
            get { return _HPP; }
            set { _HPP = value; }
        }

        //public decimal YesterdayBuyPrice { get; set; }
        public decimal Qty { get; set; }
        public DateTime Transdate { get; set; }

        public decimal DailyGrossProfit { get; set; }


    }

}