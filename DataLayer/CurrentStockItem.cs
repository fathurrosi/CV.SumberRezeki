using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataAccessLayer;
using System.Data;

namespace DataLayer
{
    public class CurrentStockItem
    {

        #region New Method


        public static List<ColiPerCatalog> GetAllColiSales()
        {
            string query = @"
SELECT SUM( IF(Coli IS NULL,0,Coli)  ) AS TotalColi, catalogID FROM saledetail
GROUP BY catalogID
";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<ColiPerCatalog>(context, new ColiPerCatalog());

        }

        public static List<ColiPerCatalog> GetAllColiPurchase()
        {
            string query = @"
SELECT SUM( IF(Coli IS NULL,0,Coli)  ) AS TotalColi, catalogID FROM Purchasedetail
GROUP BY catalogID
";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<ColiPerCatalog>(context, new ColiPerCatalog());

        }

        #endregion
        public static CatalogStock GetCatalogStock(int catalogID)
        {
            string query = @"
SELECT * FROM CatalogStock
WHERE catalogID = @catalogID
AND IsActive =1 ";
            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.AddParameter("@catalogID", catalogID);
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<CatalogStock>(context, new CatalogStock()).FirstOrDefault();

        }

        public static CurrentStock GetStock(int catalogID, DateTime TransDate)
        {

            List<CurrentStock> list = new List<CurrentStock>();

            //SET @TransDate ='2017-10-12 00:00:00';
            //SET @CatalogID=1;
            string sql = @"

SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
c.Unit, 0 +(CASE WHEN cs.Stock  IS NULL THEN 0 ELSE cs.Stock  END ) +  (CASE WHEN k.Qty IS NULL THEN 0 ELSE k.Qty END ) - (CASE WHEN t.Qty IS NULL THEN 0 ELSE t.Qty END )AS Stock , cs.StockDate,
0 +(CASE WHEN cs.Colly  IS NULL THEN 0 ELSE cs.Colly  END ) +  (CASE WHEN k.coli IS NULL THEN 0 ELSE k.coli END ) - (CASE WHEN t.coli IS NULL THEN 0 ELSE t.coli END )AS Coli 
FROM Catalog c 
LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
AND  CAST(cs.StockDate AS DATE) <=@TransDate
AND cs.CatalogID =@CatalogID
AND IsActive = 1
LEFT JOIN (
SELECT SUM(pd.Qty) AS Qty, pd.coli, CatalogID FROM purchasedetail pd
INNER JOIN Purchase p ON p.PurchaseNo =pd.PurchaseNo
WHERE CAST(p.PurchaseDate AS DATE) <=@TransDate
AND CatalogID =@CatalogID
GROUP BY CatalogID
) k ON k.CatalogID= c.ID

LEFT JOIN (

SELECT SUM(sd.Quantity) AS Qty, sd.Coli, CatalogID FROM saledetail sd
INNER JOIN Sale s ON s.TransactionID = sd.TransactionID
WHERE CAST(s.TransactionDate AS DATE) <=@TransDate
AND CatalogID =@CatalogID
GROUP BY CatalogID
) t ON t.CatalogID= c.ID
WHERE c.ID =@CatalogID

";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@TransDate", TransDate.Date);
            context.AddParameter("@CatalogID", catalogID);

            context.CommandType = CommandType.Text;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock());
            return list.FirstOrDefault();
        }

        public static List<CurrentStock> GetAllStock(string text, int offset, int pageSize)
        {
            List<CurrentStock> list = new List<CurrentStock>();

            string sql = @"

SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
c.Unit, 
0 +IFNULL(cs.Stock, 0) +  IFNULL(k.Qty, 0) - IFNULL(t.Qty, 0) + IFNULL(r.Qty, 0) - IFNULL(p.Qty, 0) AS Stock ,
0 +IFNULL( cs.Colly, 0) +  IFNULL(k.Colly,0) - IFNULL(t.Colly,0) + IFNULL(r.Colly,0) - IFNULL(p.Colly,0) AS Coli ,
cs.StockDate
FROM Catalog c 
LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
AND IsActive = 1
LEFT JOIN (


SELECT IFNULL( SUM(pd.Qty),0) AS Qty, IFNULL( SUM(pd.Coli),0) AS Colly, CatalogID FROM purchasedetail pd
GROUP BY CatalogID
) k ON k.CatalogID= c.ID

LEFT JOIN (

SELECT IFNULL( SUM(pd.Quantity),0) AS Qty, IFNULL( SUM(pd.COli),0) AS Colly, CatalogID FROM saledetail pd
GROUP BY CatalogID
) t ON t.CatalogID = c.ID


LEFT JOIN (

SELECT IFNULL( SUM(srd.Qty),0) AS Qty, IFNULL( SUM(srd.Colly), 0) AS Colly, Catalog AS CatalogID FROM s_return_detail srd
GROUP BY Catalog
) r ON r.CatalogID = c.ID



LEFT JOIN (

SELECT IFNULL( SUM(prd.Qty),0) AS Qty, IFNULL( SUM(prd.Colly), 0) AS Colly, Catalog AS CatalogID 
FROM p_return_detail prd
GROUP BY Catalog
) p ON p.CatalogID = c.ID


WHERE c.Name LIKE CONCAT ('%', @TEXT ,'%') 
ORDER BY c.Name 
LIMIT  @pageSize OFFSET @OFFSET

";

            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            context.CommandType = CommandType.Text;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock());
            return list;
        }

        public static List<CurrentStock> GetAllStock(string text)
        {
            List<CurrentStock> list = new List<CurrentStock>();
            string sql = @"
SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
(CASE WHEN cs.Stock IS NULL THEN 0 ELSE cs.Stock END) AS Stock,
c.Unit
FROM Catalog c 
LEFT JOIN CurrentStock cs ON c.ID = cs.CatalogID
where c.Name LIKE concat ('%', @text ,'%') 
 ";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@text", text);
            context.CommandType = CommandType.Text;
            list = DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock());

            //List<TotalSales> totalSales = SaleDetailItem.GetTotalSales();
            //list.ForEach(t =>
            //{
            //    var totalSale = totalSales.Where(x => x.CatalogID == t.CatalogID).FirstOrDefault();
            //    t.TotalSales = totalSale == null ? 0 : totalSale.Total;
            //});


            return list;
        }

        public static CurrentStock GetStock(int catalogID)
        {
            List<CurrentStock> list = new List<CurrentStock>();
            string sql = @"
SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
(CASE WHEN cs.Stock IS NULL THEN 0 ELSE cs.Stock END) AS Stock,
c.Unit

FROM CurrentStock cs 
INNER JOIN Catalog c  ON c.ID = cs.CatalogID

WHERE cs.CatalogID =@CatalogID  


LIMIT  @pageSize OFFSET @offset
 ";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@CatalogID", catalogID);

            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<CurrentStock>(context, new CurrentStock()).FirstOrDefault();

        }

        public static int GetRecordCount(string text)
        {

            int result = 0;

            string sql = @"

SELECT count(1) as total
FROM Catalog c 
LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
AND IsActive = 1
LEFT JOIN (


SELECT SUM(pd.Qty) AS Qty, CatalogID FROM purchasedetail pd
GROUP BY CatalogID
) k ON k.CatalogID= c.ID

LEFT JOIN (

SELECT SUM(pd.Quantity) AS Qty, CatalogID FROM saledetail pd
GROUP BY CatalogID
) t ON t.CatalogID= c.ID


where c.Name LIKE concat ('%', @text ,'%') 
ORDER BY c.Name 


";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }
    }
}
