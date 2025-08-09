using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class HPPItem
    {

//        public static DataTable GetDailyGrossProfitTable(DateTime date)
//        {

//            string str = @"  
//SELECT  DATE(s.TransactionDate ) AS Tanggal, c.Name Item, c.Unit, SUM(sd.Quantity) Quantity,
//h.TotalHPP HPP, SUM(sd.Quantity) * h.TotalHPP AS Purchase, SUM(s.TotalPrice) AS Sales
//FROM SaleDetail sd
//INNER JOIN Sale s ON s.TransactionId = sd.TransactionID
//INNER JOIN Catalog c  ON c.ID = sd.CatalogID
//
//LEFT JOIN HPP h ON DATE(h.TransDate) = DATE(s.TransactionDate) AND  h.CatalogID = sd.CatalogID
//WHERE DATE_FORMAT( s.TransactionDate, '%m-%Y') =  DATE_FORMAT( @CurrentDate, '%m-%Y')
//AND c.Type ='Item'
//GROUP BY DATE(s.TransactionDate ) ,sd.CatalogID
//
//            ";
//            IDBHelper context = new DBHelper
//            {
//                CommandText = str
//            };
//            context.AddParameter("@CurrentDate", date);
//            context.CommandType = CommandType.Text;
//            DataSet ds = DBUtil.ExecuteDataSet(context);
//            if (ds != null && ds.Tables.Count > 0) return ds.Tables[0];
//            return null;
//        }

        public static List<TotalSale> GetTotalSalePermonth(DateTime month)
        {
           string sql = @"
               
SELECT SUM( s.totalPrice) TotalSale ,DATE( s.TransactionDate) TransDate FROM Sale s
WHERE s.username !='system'
AND DATE_FORMAT(  s.TransactionDate, '%m/%Y') =  DATE_FORMAT( @TransDate, '%m/%Y')
GROUP BY DATE( s.TransactionDate) 
    ";
            IDBHelper context = new DBHelper
            {
                CommandText = sql
            };
            context.AddParameter("@TransDate", month);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<TotalSale>(context, new TotalSale());
        }


        public static List<DailyGrossProfit> GetGPPermonth(DateTime month)
        {
            string str = @"  

SELECT sd.TransDate,sd.Purchase,
 c.Name Item,  c.ID ItemID, c.Unit, Quantity, h.TotalHPP HPP, 0 Sale, 0 GrossProfit
 FROM dailygrossprofit sd
 INNER JOIN Catalog c  ON c.ID = sd.CatalogID
LEFT JOIN HPP h ON DATE(h.TransDate) = DATE(sd.TransDate) AND  h.CatalogID = sd.CatalogID

WHERE DATE_FORMAT( sd.TransDate, '%m/%Y') =  DATE_FORMAT( @CurrentDate, '%m/%Y')


            ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CurrentDate", month);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<DailyGrossProfit>(context, new DailyGrossProfit());
        }


        public static List<DailyGrossProfit> GetGrossProfitPermonth(DateTime month)
        {
            string str = @"  
SELECT  DATE(s.TransactionDate ) AS TransDate, c.Name Item,  c.ID ItemID, c.Unit, SUM(sd.Quantity) Quantity,
h.TotalHPP HPP, SUM(sd.Quantity) * h.TotalHPP AS Purchase, SUM(s.TotalPrice) AS Sale, 0 as GrossProfit

FROM SaleDetail sd
INNER JOIN Sale s ON s.TransactionId = sd.TransactionID
INNER JOIN Catalog c  ON c.ID = sd.CatalogID

LEFT JOIN HPP h ON DATE(h.TransDate) = DATE(s.TransactionDate) AND  h.CatalogID = sd.CatalogID
WHERE DATE_FORMAT( s.TransactionDate, '%m/%Y') =  DATE_FORMAT( @CurrentDate, '%m/%Y')
AND c.Type ='Item' 
GROUP BY DATE(s.TransactionDate ) ,sd.CatalogID

            ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CurrentDate", month);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<DailyGrossProfit>(context, new DailyGrossProfit());
        }

//        public static List<DailyGrossProfit> GetDailyGrossProfit(DateTime year)
//        {
//            string str = @"  
//SELECT  DATE(s.TransactionDate ) AS TransDate, c.Name Item,  c.ID ItemID, c.Unit, SUM(sd.Quantity) Quantity,
//h.TotalHPP HPP, SUM(sd.Quantity) * h.TotalHPP AS Purchase, SUM(s.TotalPrice) AS Sale, 0 as GrossProfit
//
//FROM SaleDetail sd
//INNER JOIN Sale s ON s.TransactionId = sd.TransactionID
//INNER JOIN Catalog c  ON c.ID = sd.CatalogID
//
//LEFT JOIN HPP h ON DATE(h.TransDate) = DATE(s.TransactionDate) AND  h.CatalogID = sd.CatalogID
//WHERE DATE_FORMAT( s.TransactionDate, '%Y') =  DATE_FORMAT( @CurrentDate, '%Y')
//AND c.Type ='Item'
//GROUP BY DATE(s.TransactionDate ) ,sd.CatalogID
//
//            ";
//            IDBHelper context = new DBHelper
//            {
//                CommandText = str
//            };
//            context.AddParameter("@CurrentDate", year);
//            context.CommandType = CommandType.Text;
//            return DBUtil.ExecuteMapper<DailyGrossProfit>(context, new DailyGrossProfit());
//        }

        public static DataTable GetHPPTablePerMonth(DateTime date, int catalogID)
        {
            string sql = @"
SELECT  CatalogId, c.Name AS CatalogName,   
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '01' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col1,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '02' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col2,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '03' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col3,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '04' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col4,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '05' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col5,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '06' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col6,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '07' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col7,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '08' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col8,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '09' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col9,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '10' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col10,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '11' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col11,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '12' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col12,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '13' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col13,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '14' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col14,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '15' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col15,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '16' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col16,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '17' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col17,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '18' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col18,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '19' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col19,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '20' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col20,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '21' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col21,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '22' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col22,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '23' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col23,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '24' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col24,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '25' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col25,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '26' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col26,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '27' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col27,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '28' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col28,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '29' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col29,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '30' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col30,
   (SELECT TotalHPP FROM hpp WHERE DATE_FORMAT(TransDate, '%d' ) = '31' AND CatalogId=@CatalogId and DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') ) AS col31
FROM hpp 
INNER JOIN Catalog c ON c.id = hpp.CatalogID
WHERE DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') 
and  CatalogId=@CatalogId
GROUP BY CatalogId


";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@CatalogId", catalogID);
            context.AddParameter("@currentDate", date);
            context.CommandType = CommandType.Text;
            DataSet ds = DBUtil.ExecuteDataSet(context);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;
        }
        public static List<cstmHPP> GetPerMonth(DateTime date)
        {
            string str = @"  
 SELECT hpp.*, c.name AS CatalogName FROM hpp    
 LEFT JOIN Catalog c ON c.ID= hpp.CatalogID   
 WHERE DATE_FORMAT(TransDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y') 
            ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@currentDate", date);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }


        public static int DeleteAll()
        {
            string str = "   delete FROM hpp where TransDate between       ";
            IDBHelper context = new DBHelper
            {
                CommandText = str,
                CommandType = CommandType.Text
            };
            return DBUtil.ExecuteNonQuery(context);
        }

        public static int Delete(DateTime start, DateTime end)
        {
            string str = "   delete FROM hpp   where TransDate between @start and @end    ";
            IDBHelper context = new DBHelper
            {
                CommandText = str,
                CommandType = CommandType.Text,
            };

            context.AddParameter("@start", start.Date);
            context.AddParameter("@end", end.Date);
            return DBUtil.ExecuteNonQuery(context);
        }
        public static List<cstmHPP> GetAll(DateTime start, DateTime end)
        {
            string str = "   SELECT hpp.*, c.name AS CatalogName FROM hpp    LEFT JOIN Catalog c ON c.ID= hpp.CatalogID   where TransDate between @start and @end    ORDER BY transdate desc   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@start", start);
            context.AddParameter("@end", end);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }

        public static List<cstmHPP> GetAll(DateTime start, DateTime end, int? catalogID)
        {
            string str = "   Select * FROM hpp    WHERE CatalogID =@CatalogID AND    TransDate between @Start and @End   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@catalogID", catalogID);
            context.AddParameter("@Start", start);
            context.AddParameter("@End", end);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }

        public static List<cstmHPP> GetAll(int catalodID, DateTime start, DateTime end)
        {
            string str = "   SELECT hpp.*, c.name AS CatalogName FROM hpp    LEFT JOIN Catalog c ON c.ID= hpp.CatalogID   where  hpp.CatalogID=@CatalogID   and TransDate between @start and @end    ORDER BY transdate desc   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CatalogID", catalodID);
            context.AddParameter("@start", start);
            context.AddParameter("@end", end);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }

        public static List<cstmHPP> GetHPPPerCatalogByDate(DateTime transDate, int catalogID)
        {
            string str = "   SELECT hpp.*, c.name AS CatalogName FROM hpp    LEFT JOIN Catalog c ON c.ID= hpp.CatalogID   WHERE TransDate = @TransDate and hpp.CatalogID=@CatalogID      ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CatalogID", catalogID);
            context.AddParameter("@TransDate", transDate.Date);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }
        public static List<cstmHPP> GetHPP(DateTime startDate, DateTime endDate)
        {
            string str = @"
SELECT hpp.*, c.name AS CatalogName FROM hpp 
LEFT JOIN Catalog c ON c.ID= hpp.CatalogID
WHERE  TransDate BETWEEN @startDate AND @endDate
";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };

            context.AddParameter("@startDate", startDate.Date);
            context.AddParameter("@endDate", endDate.Date);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP());
        }
        public static cstmHPP GetLastHpp(int catalogID, DateTime transDate)
        {
            string str = @"   
SELECT hpp.*, c.name AS CatalogName FROM hpp    
LEFT JOIN Catalog c ON c.ID= hpp.CatalogID   
WHERE CatalogID=@catalogID and  TransDate < @TransDate    ORDER BY transDate DESC   LIMIT 1   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CatalogID", catalogID);
            context.AddParameter("@TransDate", transDate.Date);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<cstmHPP>(context, new cstmHPP()).FirstOrDefault<cstmHPP>();
        }

        private static int Insert(DateTime transDate, int catalogID, decimal hpp, decimal PrevStock, decimal PrevHPP, decimal TotalQty, decimal TotalPrice)
        {
            string str = @"      INSERT INTO hpp       (TransDate,       TotalHPP,       CatalogId,      PrevStock,       PrevHPP,       TotalQty,       TotalPrice      )      VALUES      (@TransDate,       @TotalHPP,       @CatalogId,      @PrevStock,       @PrevHPP,       @TotalQty,       @TotalPrice      );      ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CatalogId", catalogID);
            context.AddParameter("@TransDate", transDate.Date);
            context.AddParameter("@TotalHPP", hpp);
            context.AddParameter("@PrevStock", PrevStock);
            context.AddParameter("@PrevHPP", PrevHPP);
            context.AddParameter("@TotalQty", TotalQty);
            context.AddParameter("@TotalPrice", TotalPrice);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }

        private static bool IsExistHPP(DateTime transDate, int catalogID)
        {
            string str = "   SELECT  COUNT(*) FROM hpp    WHERE TransDate = @TransDate AND CatalogID =@CatalogID   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@catalogID", catalogID);
            context.AddParameter("@TransDate", transDate.Date);
            context.CommandType = CommandType.Text;
            object obj2 = DBUtil.ExecuteScalar(context);
            int result = 0;
            int.TryParse(string.Format("{0}", obj2), out result);
            return (result > 0);
        }

        public static int Save(cstmHPP item, decimal PrevStock, decimal PrevHPP, decimal TotalQty, decimal TotalPrice)
        {
            DateTime transDate = item.TransDate;
            int catalogID = item.CatalogID;
            decimal hpp = item.HPP.Value;
            if (IsExistHPP(transDate.Date, catalogID))
            {
                return Update(transDate.Date, catalogID, hpp, PrevStock, PrevHPP, TotalQty, TotalPrice);
            }
            return Insert(transDate.Date, catalogID, hpp, PrevStock, PrevHPP, TotalQty, TotalPrice);
        }

        private static int Update(DateTime transDate, int catalogID, decimal hpp, decimal PrevStock, decimal PrevHPP, decimal TotalQty, decimal TotalPrice)
        {
            string str = "   Update hpp    set TotalHpp =@hpp      PrevStock = @PrevStock ,       PrevHPP = @PrevHPP ,       TotalQty = @TotalQty ,       TotalPrice = @TotalPrice   WHERE TransDate = @TransDate AND CatalogID =@CatalogID   ";
            IDBHelper context = new DBHelper
            {
                CommandText = str
            };
            context.AddParameter("@CatalogID", catalogID);
            context.AddParameter("@TransDate", transDate.Date);
            context.AddParameter("@hpp", hpp);
            context.AddParameter("@PrevStock", PrevStock);
            context.AddParameter("@PrevHPP", PrevHPP);
            context.AddParameter("@TotalQty", TotalQty);
            context.AddParameter("@TotalPrice", TotalPrice);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
