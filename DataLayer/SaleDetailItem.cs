using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class SaleDetailItem
    {
        public static List<CstmSalesPerCustomerMonthly> GetTotalSalesPerCustomerMonthly(int customerID, DateTime month)
        {
            string sql = @"
SELECT c.Name AS Item ,SUM(sd.Quantity) AS Quantity,  SUM( sd.TotalPrice) AS TotalSalesAmount FROM SaleDetail sd
INNER JOIN Sale s ON s.TransactionID=sd.TransactionID
INNER JOIN Catalog c ON c.ID = sd.CatalogID
WHERE s.MemberID =@CustomerID
AND  DATE_FORMAT(s.TransactionDate, '%m%Y') = DATE_FORMAT(@currentDate,'%m%Y')
GROUP BY c.Name ASC ";

            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@CustomerID", customerID);

            context.AddParameter("@currentDate", month);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<CstmSalesPerCustomerMonthly>(context, new CstmSalesPerCustomerMonthly());

        }
        public static DataTable GetPerCustomerPerYear(int customerID, int catalogID, DateTime year)
        {
            /*
SET @currentDate ='2017-12-18 00:00:00';
SET @CustomerID = 26;
SET @CatalogID =1;
             */
            string sql = @"

SELECT c.Name AS CatalogName, c.ID AS CatalogID,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '01') AS Januari,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '02') AS Februari,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '03') AS Maret,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '04') AS April,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '05') AS Mei,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '06') AS Juni,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '07') AS Juli,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '08') AS Agustus,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '09') AS September,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '10') AS Oktober,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '11') AS November,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')  AND DATE_FORMAT(s.TransactionDate, '%m' ) = '12') AS Desember

FROM Catalog c
WHERE ID =@CatalogID
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@CustomerID", customerID);
            context.AddParameter("@CatalogID", catalogID);
            context.AddParameter("@currentDate", year);
            context.CommandType = CommandType.Text;
            DataSet ds = DBUtil.ExecuteDataSet(context);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;

        }



        public static DataTable GetPerCustomerPerMonth(int customerID, DateTime month, int catalogID)
        {
            /*
SET @currentDate ='2017-12-18 00:00:00';
SET @CustomerID = 26;
SET @CatalogID =1;
             */
            string sql = @"

SELECT c.Name AS CatalogName, c.ID as CatalogID,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '01') AS Col01,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '02') AS Col02,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '03') AS Col03,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '04') AS Col04,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '05') AS Col05,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '06') AS Col06,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '07') AS Col07,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '08') AS Col08,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '09') AS Col09,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '10') AS Col10,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '11') AS Col11,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '12') AS Col12,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '13') AS Col13,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '14') AS Col14,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '15') AS Col15,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '16') AS Col16,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '17') AS Col17,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '18') AS Col18,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '19') AS Col19,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '20') AS Col20,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '21') AS Col21,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '22') AS Col22,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '23') AS Col23,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '24') AS Col24,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '25') AS Col25,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '26') AS Col26,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '27') AS Col27,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '28') AS Col28,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '29') AS Col29,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '30') AS Col30,
(SELECT SUM(sd.Quantity) AS Qty FROM saledetail sd INNER JOIN sale s ON s.TransactionID=sd.TransactionID WHERE sd.CatalogID=@CatalogID AND s.MemberID =@CustomerID  AND DATE_FORMAT(s.TransactionDate, '%m/%Y') = DATE_FORMAT(@currentDate,'%m/%Y')  AND DATE_FORMAT(s.TransactionDate, '%d' ) = '31') AS Col31
FROM Catalog c
WHERE ID =@CatalogID
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@CustomerID", customerID);
            context.AddParameter("@CatalogID", catalogID);
            context.AddParameter("@currentDate", month);
            context.CommandType = CommandType.Text;
            DataSet ds = DBUtil.ExecuteDataSet(context);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;

        }

        public static List<CstmSalesPercustomerPerYear> GetTotalSalesPerCustomer(int customerID, DateTime year)
        {
            string sql = @"

SELECT SUM(sd.Quantity) AS TotalQty, sd.CatalogID, c.Name AS CatalogName, DATE_FORMAT(s.TransactionDate, '%Y/%m/1')  AS Bulan  FROM SaleDetail sd
INNER JOIN Sale s ON sd.TransactionID = s.TransactionID 
INNER JOIN Catalog c ON c.ID = sd.CatalogID
WHERE memberid =@CustomerID
AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')

GROUP BY  DATE_FORMAT(s.TransactionDate, '%m') ASC, sd.CatalogID 

";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;
            context.AddParameter("@currentDate", year);
            context.AddParameter("@CustomerID", customerID);
            return DBUtil.ExecuteMapper<CstmSalesPercustomerPerYear>(context, new CstmSalesPercustomerPerYear());
        }

        public static List<Catalog> GetCatalog(int customerID, DateTime year)
        {
            string sql = @"
SELECT DISTINCT c.*, sd.CatalogID FROM SaleDetail sd
inner join Sale s on s.TransactionID= sd.TransactionID
INNER JOIN Catalog c ON c.id=sd.CatalogID
WHERE s.MemberID =@CustomerID
AND DATE_FORMAT(s.TransactionDate, '%Y') = DATE_FORMAT(@currentDate,'%Y')
ORDER BY c.id ";

            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;
            context.AddParameter("@CustomerID", customerID);
            context.AddParameter("@currentDate", year);
            return DBUtil.ExecuteMapper<Catalog>(context, new Catalog());

        }

        public static List<TotalSales> GetTotalSales()
        {
            string sql = @"
SELECT SUM(sd.Quantity) AS TotalSale, sd.CatalogID FROM SaleDetail sd
INNER JOIN Sale s ON sd.TransactionID = s.TransactionID 
GROUP BY  sd.CatalogID ";

            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<TotalSales>(context, new TotalSales());

        }

        internal static List<SaleDetail> GetTransID(string transactionID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"	
SELECT sd.*, c.Name AS CatalogName, c.Unit FROM SaleDetail sd
INNER JOIN Catalog c ON c.ID = sd.CatalogID
WHERE sd.TransactionID = @TransactionID 
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransactionID", transactionID);
            return DBUtil.ExecuteMapper<SaleDetail>(context, new SaleDetail());

        }


        internal static int DeleteByTransID(string transactionID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"	
delete from saledetail
WHERE TransactionID = @TransactionID 
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransactionID", transactionID);
            return DBUtil.ExecuteNonQuery(context);

        }


        //        internal static List<SaleDetail> GetBySaleNo(string SaleNo)
        //        {
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @"	
        //SELECT 
        //	sd.ID, 
        //	sd.SaleNo, 
        //	sd.Catalog, 
        //	sd.Price, 
        //	sd.Discount, 
        //	sd.Qty, 
        //	sd.TotalPrice, 
        //	sd.Sequence, 
        //	c.Unit, u.name UnitName, 
        //	sd.Created, 
        //	sd.CreatedBy, c.Name AS CatalogName FROM SaleDetail sd
        //LEFT JOIN Catalog c ON c.code = sd.Catalog
        //LEFT JOIN Unit u ON u.code = c.unit
        //
        //WHERE sd.SaleNo = @SaleNo 
        //";
        //            context.CommandType = CommandType.Text;
        //            context.AddParameter("@SaleNo", SaleNo);
        //            return DBUtil.ExecuteMapper<SaleDetail>(context, new SaleDetail());

        //        }
    }
}
