using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class GrossProfitItem
    {
        public static List<SumGrossProfit> GetSumGrossProfitYearly(DateTime transDate)
        {
            string query = @"
SELECT DATE_FORMAT(gp.TransDate, '%M') Month ,DATE_FORMAT(gp.TransDate, '%m') Sequence , 
DATE_FORMAT(gp.TransDate, '%Y') YEAR, 
SUM(gp.Sale) AS TotalSales, SUM(gp.GrossProfit) TotalGrossProfit FROM dailygrossprofit gp

WHERE DATE_FORMAT(gp.TransDate, '%Y') = DATE_FORMAT(@TransDate, '%Y')

GROUP BY DATE_FORMAT(gp.TransDate, '%M')

";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransDate", transDate.Date);

            //DataSet ds = DBUtil.ExecuteDataSet(context);
            return DBUtil.ExecuteMapper<SumGrossProfit>(context, new SumGrossProfit());

        }
        public static List<DailyGrossProfit> GetGrossProfitYearly(DateTime transDate)
        {
            string query = @"

SELECT 	gp.TransDate as TransDate , 
	gp.CatalogID AS ItemID, 
	c.Name AS Item,
	gp.Quantity, 
	gp.Purchase, 
	gp.Sale, 
	gp.GrossProfit,
	hpp.TotalHPP AS HPP,
	c.Unit FROM dailygrossprofit gp
	LEFT JOIN hpp ON hpp.TransDate = gp.TransDate AND hpp.CatalogID = gp.CatalogID
	INNER JOIN Catalog c ON c.ID=gp.CatalogID
	
WHERE DATE_FORMAT(gp.TransDate, '%Y') = DATE_FORMAT(@TransDate, '%Y') ";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransDate", transDate.Date);

            //DataSet ds = DBUtil.ExecuteDataSet(context);
            return DBUtil.ExecuteMapper<DailyGrossProfit>(context, new DailyGrossProfit());

        }
        public static List<DailyGrossProfit> GetGrossProfit(DateTime transDate)
        {
            string query = @"

SELECT 	gp.TransDate as TransDate , 
	gp.CatalogID AS ItemID, 
	c.Name AS Item,
	gp.Quantity, 
	gp.Purchase, 
	gp.Sale, 
	gp.GrossProfit,
	hpp.TotalHPP AS HPP,
	c.Unit FROM dailygrossprofit gp
	LEFT JOIN hpp ON hpp.TransDate = gp.TransDate AND hpp.CatalogID = gp.CatalogID
	INNER JOIN Catalog c ON c.ID=gp.CatalogID
	
WHERE DATE_FORMAT(gp.TransDate, '%m/%Y') = DATE_FORMAT(@TransDate, '%m/%Y')

";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransDate", transDate.Date);

            //DataSet ds = DBUtil.ExecuteDataSet(context);
            return DBUtil.ExecuteMapper<DailyGrossProfit>(context, new DailyGrossProfit());

        }

        private static bool IsExist(DateTime transDate, int catalogID)
        {
            string query = "SELECT count(1) as exist FROM dailygrossprofit WHERE DATE(TransDate) = DATE(@TransDate) AND  CatalogID = @CatalogID ";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransDate", transDate.Date);
            context.AddParameter("@CatalogID", catalogID);

            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
            {
                int exist = 0;
                int.TryParse(string.Format("{0}", obj), out exist);
                if (exist > 0) return true;
            }

            return false;
        }

        public static int DeleteDaily(DateTime transDate)
        {
            int result = -1;
            string queryInsert = @"  
Delete from dailygrossprofit  where TransDate =@TransDate";


            try
            {
                //bool exist = IsExist(transDate, catalogID);
                IDBHelper context = new DBHelper();
                context.CommandText = queryInsert;
                context.CommandType = CommandType.Text;
                context.AddParameter("@TransDate", transDate.Date);
              
                result = DBUtil.ExecuteNonQuery(context);
            }
            catch (Exception)
            {

            }

            return result;
        }

        public static int SaveDaily(DateTime transDate, int catalogID, decimal qty, decimal purchase, decimal sale, decimal grossProfit)
        {
            int result = -1;
            string queryInsert = @"  
Delete from dailygrossprofit  where TransDate =@TransDate and CatalogID =@CatalogID;
INSERT INTO dailygrossprofit  (TransDate,   CatalogID,  Quantity,  Purchase,  Sale,  GrossProfit ) VALUES ( @TransDate,  @CatalogID,  @Quantity,  @Purchase,  @Sale,  @GrossProfit ); ";
            //string queryUpdate = " UPDATE dailygrossprofit  SET  Quantity = @Quantity ,  Purchase = @Purchase ,  Sale = @Sale ,  GrossProfit = @GrossProfit	 WHERE DATE(TransDate )= DATE(@TransDate)  AND CatalogID = @CatalogID ; ";
            try
            {
                //bool exist = IsExist(transDate, catalogID);
                IDBHelper context = new DBHelper();
                context.CommandText = queryInsert;
                context.CommandType = CommandType.Text;
                context.AddParameter("@TransDate", transDate.Date);
                context.AddParameter("@CatalogID", catalogID);
                context.AddParameter("@Quantity", qty);
                context.AddParameter("@Purchase", purchase);
                context.AddParameter("@Sale", sale);
                context.AddParameter("@GrossProfit", grossProfit);
                result = DBUtil.ExecuteNonQuery(context);
            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}
