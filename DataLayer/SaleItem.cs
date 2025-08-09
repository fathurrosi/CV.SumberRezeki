using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataObject.Cstm;

namespace DataLayer
{
    public class SaleItem
    {
        public static List<Variable> GetAllSalesCounter()
        {
            List<Variable> result = new List<Variable>();
            IDBHelper context = new DBHelper();
            string commandText = @" 
SELECT s.Counter AS display,   s.Counter CODE, 0 Sequence, '' category
FROM sale  s
";

            IDBHelper ictx = new DBHelper();
            ictx.CommandText = commandText;
            ictx.CommandType = CommandType.Text;
            result = DBUtil.ExecuteMapper<Variable>(ictx, new Variable());

            return result;
        }
        public static Sale GetByCounter(int counter)
        {
            IDBHelper context = new DBHelper();
            //            context.CommandText = @" 
            //SELECT s.*, c.Fullname CustomerName, v.Display PaymentTypeDesc
            //FROM Sale s
            //LEFT JOIN customer c ON s.MemberID = c.ID
            //LEFT JOIN variable v ON v.category ='PaymentType' AND v.code =s.PaymentType
            //
            //WHERE s.Counter= @counter 
            //limit 1
            //";

            context.CommandText = @" 
SELECT s.*
FROM Sale s
WHERE s.Counter= @counter 
limit 1
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@counter", counter);
            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale());
            Sale sale = result.FirstOrDefault();
            if (sale != null)
            {
                sale.Details = SaleDetailItem.GetTransID(sale.TransactionID);
            }
            return sale;
        }

        public static List<CstmSales> GetTotalSales(DateTime date)
        {
            //SET @CurrentDate ='2017/11/1';
            string query = @"

SELECT  DATE_FORMAT( s.TransactionDate, '%m-%Y') AS Bulan,
sd.CatalogID, SUM( sd.Price * sd.Quantity) AS Sales FROM SaleDetail sd
INNER JOIN Sale s ON s.TransactionId = sd.TransactionID
INNER JOIN Catalog c ON c.ID = sd.CatalogID
WHERE  DATE_FORMAT( s.TransactionDate, '%m-%Y') =  DATE_FORMAT( @CurrentDate, '%m-%Y')
AND c.Type ='Item'
GROUP BY DATE_FORMAT( s.TransactionDate, '%m-%Y'),sd.CatalogID
";
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = query;
            ictx.AddParameter("@CurrentDate", date);
            ictx.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<CstmSales>(ictx, new CstmSales());
        }

        public static List<CstmTotalPurchase> GetAllPurchases()
        {
            string sql = @"
SELECT sd.totalprice AS TotalPrice,
sd.PricePerUnit,
sd.Qty AS Quantity,
CAST(s.PurchaseDate AS DATE) AS PurchaseDate,
c.name AS CatalogName, 
sd.CatalogID  AS CatalogID, c.Unit
FROM purchasedetail sd
INNER JOIN Purchase s ON s.PurchaseNo= sd.PurchaseNo
INNER JOIN Catalog c ON sd.CatalogID= c.ID                                                                  
WHERE  c.Type ='Item'
ORDER BY CAST(s.PurchaseDate AS DATE) ,sd.CatalogID
";
            List<CstmTotalPurchase> result = new List<CstmTotalPurchase>();

            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = sql;
                ictx.CommandType = CommandType.Text;
                result = DBUtil.ExecuteMapper<CstmTotalPurchase>(ictx, new CstmTotalPurchase());
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            return result.OrderBy(t => t.TotalPrice).ToList();
        }
        //public static List<cstmHPP> GetTotalHPH(DateTime start, DateTime end)
        //{
        //    List<cstmHPP> list = new List<cstmHPP>();

        //    List<CstmTotalPurchase> purchaseList = GetAllPurchases(start.AddDays(-1), end);
        //    DateTime _start = purchaseList.Select(t => t.PurchaseDate).Min();
        //    DateTime _end = purchaseList.Select(t => t.PurchaseDate).Max();
        //    for (; _start <= end; _start.AddDays(1))
        //    {
        //        DateTime counterDate = _start;
        //        List<int> catalogIdList = purchaseList.Where(t => t.PurchaseDate.Date == counterDate.Date).Select(t => t.CatalogID).Distinct().ToList();
        //        foreach (int catalogID in catalogIdList)
        //        {
        //            CstmTotalPurchase pNow = purchaseList.Where(t => t.PurchaseDate.Date == counterDate.Date && t.CatalogID == catalogID).FirstOrDefault();
        //            if (pNow != null)
        //            {
        //                CstmTotalPurchase pNext = purchaseList.Where(t => t.PurchaseDate.Date < counterDate.Date && t.CatalogID == catalogID).FirstOrDefault();
        //                if (pNext != null)
        //                {
        //                    //HPP --> harga pokok penjualan
        //                    // HPP = totalharga / totalpenjualan
        //                    decimal nowTotalQty = pNow.Quantity;
        //                    decimal nowTotalPrice = pNow.TotalPrice;
        //                    decimal prevTotalQty = pNext.Quantity;
        //                    decimal provTotalPrice = pNext.TotalPrice;
        //                    //C5 = stok hari ini
        //                    //C7 = stok kemarin
        //                    //E7 = stok kermain * (harga_rata2_kemarin atau hpp_kemarin)
        //                    //E5 = total harga hari ini
        //                    //HPP =(E7+E5)/(C5+C7)
        //                    //----------------------------
        //                    //(Qty kemarin)
        //                }
        //            }
        //        }
        //    }

        //    return list;
        //}
        public static List<Piutang> GetPiutang(DateTime start, DateTime end)
        {
            string sql = @"
SELECT cust.ID CustomerID, 
cust.Fullname AS CustomerName, 
 s.TransactionID,
s.transactiondate AS TransDate,
s.TotalPrice,
s.TotalPayment,
CASE WHEN (s.TotalPrice - s.TotalPayment) >0 THEN (s.TotalPrice - s.TotalPayment) ELSE 0 END AS Piutang,
s.ExpiredDate
FROM  Sale s 
LEFT JOIN Customer cust ON cust.ID =s.MemberID 

WHERE s.transactiondate BETWEEN @startDate AND @endDate
and (s.TotalPrice - s.TotalPayment) > 0
";

            List<Piutang> result = new List<Piutang>();

            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = sql;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@startDate", start);
                ictx.AddParameter("@endDate", end);
                result = DBUtil.ExecuteMapper<Piutang>(ictx, new Piutang());
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            return result.OrderBy(t => t.TransDate).ToList();

        }
        public static int Update(string TransactionID, DateTime ExpiredDate)
        {
            int result = -1;
            string sql = @"
UPDATE Sale
SET ExpiredDate =@ExpiredDate 
WHERE TransactionID =@TransactionID";

            try
            {
                IDBHelper ictx = new DBHelper();
                ictx.CommandText = sql;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@TransactionID", TransactionID);
                ictx.AddParameter("@ExpiredDate", ExpiredDate);
                result = DBUtil.ExecuteNonQuery(ictx);
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

//        public static int Update(Sale item)
//        {

//            List<SaleDetail> existingDetailList = SaleDetailItem.GetTransID(item.TransactionID);

//            string queryUpdateSale = @"
//UPDATE sale 
//	SET
//	TotalPrice = @TotalPrice , 
//	TotalQty = @TotalQty , 
//	Terminal = @Terminal , 
//	TotalPayment = @TotalPayment , 
//	TotalPaymentReturn = @TotalPaymentReturn , 
//	Notes = @Notes , 
//	PaymentType = @PaymentType , 
//	UpdatedDate = @UpdatedDate , 
//	UpdatedBy = @UpdatedBy	
//	WHERE
//	TransactionID = @TransactionID ; ";

//            int itemResult = 0;
//            IDBHelper ictx = new DBHelper();
//            try
//            {
//                ictx.BeginTransaction();
//                ictx.CommandText = queryUpdateSale;
//                ictx.CommandType = CommandType.Text;
//                ictx.AddParameter("@TransactionID", item.TransactionID);
//                ictx.AddParameter("@TotalPrice", item.TotalPrice);
//                ictx.AddParameter("@TotalQty", item.TotalQty);
//                ictx.AddParameter("@UpdatedBy", item.Username);
//                ictx.AddParameter("@Terminal", item.Terminal);
//                ictx.AddParameter("@TotalPayment", item.TotalPayment);
//                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
//                ictx.AddParameter("@Notes", item.Notes);
//                ictx.AddParameter("@PaymentType", item.PaymentType);
//                ictx.AddParameter("@UpdatedDate", DateTime.Now);

//                itemResult = DBUtil.ExecuteNonQuery(ictx);
//                if (itemResult > 0)
//                {
//                    foreach (SaleDetail detail in item.Details)
//                    {
//                        int result = -1;
//                        SaleDetail existingDetail = existingDetailList.Where(t => t.ID == detail.ID).FirstOrDefault();
//                        if (existingDetail != null)
//                        {
//                            //update
//                            ictx.CommandText = @"
//UPDATE saledetail 
//	SET	
//	CatalogID = @CatalogID , 
//	Price = @Price , 
//	Discount = @Discount , 
//	Quantity = @Quantity , 
//	TotalPrice = @TotalPrice , 
//	Sequence = @Sequence	
//	WHERE
//	ID = @ID ;";

//                            ictx.AddParameter("@CatalogID", detail.CatalogID);
//                            ictx.AddParameter("@Price", detail.Price);
//                            ictx.AddParameter("@Discount", detail.Discount);
//                            ictx.AddParameter("@Quantity", detail.Quantity);
//                            ictx.AddParameter("@TotalPrice", detail.TotalPrice);
//                            ictx.AddParameter("@Sequence", detail.Sequence);
//                            ictx.AddParameter("@ID", detail.ID);
//                            result = DBUtil.ExecuteNonQuery(ictx);
//                        }
//                        else
//                        {

//                            ictx.CommandText = @"
//INSERT INTO saledetail 
//	( 
//	    TransactionID, 
//	    CatalogID, 
//	    Price, 
//	    Discount, 
//	    Quantity, 
//	    TotalPrice,Sequence
//	)
//	VALUES
//	(
//	@TransactionID, 
//	@CatalogID, 
//	@Price, 
//	@Discount, 
//	@Quantity, 
//	@TotalPrice,@Sequence
//	);
//";
//                            ictx.AddParameter("@TransactionID", item.TransactionID);
//                            ictx.AddParameter("@CatalogID", detail.CatalogID);
//                            ictx.AddParameter("@Price", detail.Price);
//                            ictx.AddParameter("@Discount", detail.Discount);
//                            ictx.AddParameter("@Quantity", detail.Quantity);
//                            ictx.AddParameter("@TotalPrice", detail.TotalPrice);
//                            ictx.AddParameter("@Sequence", detail.Sequence);

//                            result = DBUtil.ExecuteNonQuery(ictx);
//                        }

//                        if (result == -1)
//                        {
//                            ictx.RollbackTransaction();
//                        }
//                        else
//                        {
//                            #region Update Current Stock
//                            ictx.CommandText = @" 
//
//SELECT c.ID AS CatalogID, c.Name AS CatalogName, 
//c.Unit, cs.Stock, cs.StockDate
//FROM Catalog c 
//LEFT JOIN CatalogStock cs ON cs.CatalogID = c.ID
//AND IsActive = 1
//where cs.CatalogID=@CatalogID
//";
//                            ictx.AddParameter("@CatalogID", detail.CatalogID);
//                            CurrentStock realStock = DBUtil.ExecuteMapper<CurrentStock>(ictx, new CurrentStock()).FirstOrDefault();

//                            #endregion

//                            #region Update Catalog Stock
//                            // update stock
//                            ictx.CommandText = @" 
//SELECT * FROM catalogstock WHERE CatalogID=@CatalogID AND StockDate=@StockDate
//                            ";
//                            ictx.AddParameter("@CatalogID", detail.CatalogID);
//                            ictx.AddParameter("@StockDate", DateTime.Today);
//                            CatalogStock stockItem = DBUtil.ExecuteMapper<CatalogStock>(ictx, new CatalogStock()).FirstOrDefault();
//                            if (stockItem != null)
//                            {
//                                ictx.CommandText = @"
//Update CatalogStock Set IsActive = 0
//where CatalogID =@CatalogID And IsActive = 1;
//         
//Update CatalogStock 
//Set Stock = @Stock,
//CreatedBy = @Username ,
//IsActive = 1
//Where CatalogID =@CatalogID and StockDate =@StockDate
//";
//                                ictx.AddParameter("@CatalogID", detail.CatalogID);
//                                ictx.AddParameter("@Username", item.Username);
//                                ictx.AddParameter("@StockDate", item.TransactionDate.Date);
//                                ictx.AddParameter("@Stock", realStock != null ? realStock.Stock - detail.Quantity : 0 - detail.Quantity);
//                                DBUtil.ExecuteNonQuery(ictx);
//                            }
//                            else
//                            {
//                                ictx.CommandText = @" 
//
//Update CatalogStock Set IsActive = 0
//where CatalogID =@CatalogID And IsActive = 1;
//
//INSERT INTO catalogstock 
//	(CatalogID, 
//	Stock, 
//	StockDate, 
//	CreatedBy, 
//	IsActive
//	)
//	VALUES
//	(@CatalogID, 
//	@Stock, 
//	@StockDate, 
//	@CreatedBy, 
//	1 );
//";
//                                ictx.AddParameter("@CatalogID", detail.CatalogID);
//                                ictx.AddParameter("@Username", item.Username);
//                                ictx.AddParameter("@StockDate", item.TransactionDate.Date);
//                                ictx.AddParameter("@Stock", realStock != null ? realStock.Stock - detail.Quantity : 0 - detail.Quantity);
//                                DBUtil.ExecuteNonQuery(ictx);
//                            }
//                            #endregion

//                        }
//                    }

//                    foreach (SaleDetail existing in existingDetailList)
//                    {
//                        if (item.Details.Where(t => t.ID == existing.ID).Count() == 0)
//                        {
//                            ictx.CommandText = " delete from saledetail where ID =@ID ";
//                            ictx.AddParameter("@ID", existing.ID);
//                            DBUtil.ExecuteNonQuery(ictx);
//                        }
//                    }

//                    ictx.CommitTransaction();
//                }
//            }
//            catch (Exception) { itemResult = -1; ictx.RollbackTransaction(); }

//            return itemResult;
//        }

        private static int GetSaleRowIndex()
        {
            int result = 0;
            string query = @"
SELECT IFNULL( MAX(counter), 0) LAST_INDEX FROM sale 
  
";

            IDBHelper ictx = new DBHelper();

            ictx.CommandText = query;
            ictx.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(ictx);
            if (obj == null) return 1;
            int.TryParse(obj.ToString(), out result);

            return result + 1;
        }
        //public static int Update(string transID, DateTime updatedDate, string updatedBy)
        //{

        //    IDBHelper ictx = new DBHelper();

        //    ictx.CommandText = "Update Sale Set UpdatedDate=@UpdatedDate, UpdatedBy=@UpdatedBy where TransactionID=@TransactionID ";
        //    ictx.CommandType = CommandType.Text;
        //    ictx.AddParameter("@TransactionID", transID);
        //    ictx.AddParameter("@UpdatedDate", updatedDate);
        //    ictx.AddParameter("@UpdatedBy", updatedBy);
        //    return DBUtil.ExecuteNonQuery(ictx);
        //}

        public static int Update(Sale item)
        {
            string itemQuery = @"
UPDATE sale 
	SET
	TotalPrice = @TotalPrice, 
	TotalQty = @TotalQty, 
	TransactionDate = @TransactionDate, 	
	MemberID = @MemberID, 
	Terminal = @Terminal, 
	TotalPayment = @TotalPayment, 
	TotalPaymentReturn = @TotalPaymentReturn, 
	Notes = @Notes, 
	PaymentType = @PaymentType, 
	UpdatedDate = @UpdatedDate, 
	UpdatedBy = @UpdatedBy
	WHERE
	TransactionID = @TransactionID;
";


            int lastIndex = GetSaleRowIndex();
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = itemQuery;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@TransactionID", item.TransactionID);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TransactionDate", item.TransactionDate);
                ictx.AddParameter("@UpdatedBy", item.Username);
                ictx.AddParameter("@MemberID", item.MemberID);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@TotalPayment", item.TotalPayment);
                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PaymentType", item.PaymentType);
                ictx.AddParameter("@UpdatedDate", DateTime.Now);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    ictx.Clear();
                    ictx.CommandText = @"
delete from saledetail
WHERE TransactionID = @TransactionID 
";
                    ictx.AddParameter("@TransactionID", item.TransactionID);
                    DBUtil.ExecuteNonQuery(ictx);

                    string detailQuery = @"
INSERT INTO saledetail 
	( 
	TransactionID, 
	CatalogID, 
	Price, 
	Discount, 
	Quantity, 
	TotalPrice,Sequence,Coli
	)
	VALUES
	(
	@TransactionID, 
	@CatalogID, 
	@Price, 
	@Discount, 
	@Quantity, 
	@TotalPrice,@Sequence,@Coli
	);
";
                    foreach (SaleDetail detail in item.Details)
                    {
                        ictx.CommandText = detailQuery;
                        ictx.AddParameter("@TransactionID", item.TransactionID);
                        ictx.AddParameter("@CatalogID", detail.CatalogID);
                        ictx.AddParameter("@Price", detail.Price);
                        ictx.AddParameter("@Discount", detail.Discount);
                        ictx.AddParameter("@Quantity", detail.Quantity);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@Sequence", detail.Sequence);
                        ictx.AddParameter("@Coli", detail.Coli);

                        int result = DBUtil.ExecuteNonQuery(ictx);
                    }
                    ictx.CommitTransaction();
                }
            }
            catch (Exception) { itemResult = -1; ictx.RollbackTransaction(); }

            return itemResult;
        }

        public static int Insert(Sale item)
        {
            string itemQuery = @"
INSERT INTO sale 
	(TransactionID, 
	TotalPrice, 
	TotalQty, 
	TransactionDate, 
	Username, 
	MemberID, 
	Terminal, 
	TotalPayment, 
	TotalPaymentReturn, 
	Notes, 
	PaymentType, 
    ExpiredDate, 
    Counter
	)
	VALUES
	(
    @TransactionID, 
	@TotalPrice, 
	@TotalQty, 
	@TransactionDate, 
	@Username, 
	@MemberID, 
	@Terminal, 
	@TotalPayment, 
	@TotalPaymentReturn, 
	@Notes, 
	@PaymentType, 
    @ExpiredDate, 
    @Counter
	);
";


            int lastIndex = GetSaleRowIndex();
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = itemQuery;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@TransactionID", item.TransactionID);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TransactionDate", item.TransactionDate);
                ictx.AddParameter("@Username", item.Username);
                ictx.AddParameter("@MemberID", item.MemberID);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@TotalPayment", item.TotalPayment);
                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@Counter", lastIndex);

                ictx.AddParameter("@PaymentType", item.PaymentType);
                ictx.AddParameter("@ExpiredDate", item.ExpiredDate);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    string detailQuery = @"
INSERT INTO saledetail 
	( 
	TransactionID, 
	CatalogID, 
	Price, 
	Discount, 
	Quantity, 
	TotalPrice,Sequence,Coli
	)
	VALUES
	(
	@TransactionID, 
	@CatalogID, 
	@Price, 
	@Discount, 
	@Quantity, 
	@TotalPrice,@Sequence,@Coli
	);
";
                    foreach (SaleDetail detail in item.Details)
                    {
                        ictx.CommandText = detailQuery;
                        ictx.AddParameter("@TransactionID", item.TransactionID);
                        ictx.AddParameter("@CatalogID", detail.CatalogID);
                        ictx.AddParameter("@Price", detail.Price);
                        ictx.AddParameter("@Discount", detail.Discount);
                        ictx.AddParameter("@Quantity", detail.Quantity);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@Sequence", detail.Sequence);
                        ictx.AddParameter("@Coli", detail.Coli);

                        int result = DBUtil.ExecuteNonQuery(ictx);
                    }
                    ictx.CommitTransaction();
                }
            }
            catch (Exception) { itemResult = -1; ictx.RollbackTransaction(); }

            return itemResult;
        }

        public static Sale GetByTransID(string transactionID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"	SELECT * from Sale WHERE TransactionID = @TransactionID ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransactionID", transactionID);
            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale());
            Sale sale = result.FirstOrDefault();
            if (sale != null)
            {
                sale.Details = SaleDetailItem.GetTransID(sale.TransactionID);
            }
            return sale;
        }


        public static int GetRecordCount(string text)
        {

            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT  count(1) from Sale s
LEFT JOIN customer c ON s.MemberID = c.ID
WHERE ( s.TransactionID LIKE concat ('%', @text ,'%') 
OR s.Notes LIKE concat ('%', @text ,'%') 
OR c.Fullname like concat ('%', @text ,'%') )
AND s.TransactionID NOT IN( SELECT DISTINCT TransactionID FROM reconcile)
  
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static List<Sale> GetPaging(string text, int offset, int pageSize)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT s.* from Sale s
LEFT JOIN customer c ON s.MemberID = c.ID
WHERE ( s.TransactionID LIKE concat ('%', @text ,'%') 
OR s.Notes LIKE concat ('%', @text ,'%') 
OR c.Fullname like concat ('%', @text ,'%') )
AND s.TransactionID NOT IN( SELECT DISTINCT TransactionID FROM reconcile)
Order By s.TransactionDate desc
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale());
            result.ForEach(item =>
            {
                if (item != null)
                {
                    if (item.Username.ToLower() == "system")
                    {
                        item.CustomerName = "System";
                    }
                    else if (item.MemberID.HasValue)
                    {
                        Customer cust = CustomerItem.GetByID(item.MemberID.Value);
                        item.CustomerName = cust == null ? string.Empty : cust.FullName;
                    }
                    item.Details = SaleDetailItem.GetTransID(item.TransactionID);
                }
            });
            return result;
        }

        public static int GetNewIndex(DateTime currentDate)
        {
            int result = -1;
            //            string sql = @"SELECT COUNT(1) FROM Sale
            //            WHERE DATE_FORMAT(transactiondate,'%d-%m-%Y') = DATE_FORMAT(@TransactionDate,'%d-%m-%Y') ";
            string sql = @"
           SELECT transactionid FROM Sale
  WHERE transactionid NOT IN (SELECT DISTINCT transactionid FROM reconcile )
  AND CAST(transactiondate AS DATE) = CAST(@TransactionDate AS DATE)
  ORDER BY transactiondate DESC
  LIMIT 1
            ";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;
            context.AddParameter("@TransactionDate", currentDate);
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
            {
                //24072017027
                string temp = string.Format("{0}", obj);
                if (temp.Length > 8)
                {
                    result = Convert.ToInt32(temp.Substring(8)) + 1;
                }
                else
                {
                    Int32.TryParse(obj.ToString(), out result);
                    result = result + 1;
                }
            }
            else
            {
                result = 1;
            }

            return result;

        }
        public static List<CstmMonthlyGrossProfit> GetMonthlyGrossProfit(DateTime start, DateTime end)
        {

            string sqlQuery = @"

SELECT c.ID AS CatalogID, c.Name AS CatalogName,c.Unit, c.MONTH_YEAR , CASE WHEN tt.TotalSale IS NULL THEN 0 ELSE tt.TotalSale END AS TotalSale,CASE WHEN yy.TotalPurchase IS NULL THEN 0 ELSE yy.TotalPurchase END AS TotalPurchase ,
((CASE WHEN tt.TotalSale IS NULL THEN 0 ELSE tt.TotalSale END)-( CASE WHEN yy.TotalPurchase IS NULL THEN 0 ELSE yy.TotalPurchase END) ) AS MonthlyGrossProfit, TempMonth
FROM (
SELECT temp2.ID,temp2.Name,temp2.Unit, DATE_FORMAT(tempMonth,'%b %Y') AS MONTH_YEAR, TempMonth FROM (
SELECT DISTINCT TempMonth FROM 
(
SELECT  DATE_FORMAT(TransactionDate, '%Y-%m-1') AS TempMonth FROM sale
WHERE transactionid NOT IN (SELECT DISTINCT transactionid FROM reconcile)
UNION ALL
SELECT DATE_FORMAT(PurchaseDate , '%Y-%m-1') AS TempMonth FROM Purchase
) dates
) temp1
JOIN  ( SELECT ID, NAME, Unit FROM Catalog ) temp2
) c

LEFT JOIN(
SELECT SUM(sd.totalPrice) AS TotalSale, DATE_FORMAT(s.transactiondate,'%b %Y') AS MONTH_YEAR ,sd.CatalogID FROM saleDetail sd
INNER JOIN Sale s ON sd.TransactionID =s.TransactionID
WHERE s.transactionid NOT IN (SELECT DISTINCT transactionid FROM reconcile)
GROUP BY  DATE_FORMAT(s.transactiondate,'%b %Y'),  sd.CatalogID 
) tt ON tt.CatalogID = c.ID AND  tt.MONTH_YEAR = c.MONTH_YEAR


LEFT JOIN(
SELECT SUM(pd.totalPrice) AS TotalPurchase,  DATE_FORMAT(p.PurchaseDate,'%b %Y') AS MONTH_YEAR , pd.CatalogID FROM PurchaseDetail pd
INNER JOIN Purchase p ON pd.PurchaseNo =p.PurchaseNo 
WHERE pd.purchaseno NOT IN (SELECT DISTINCT purchaseno FROM reconcile)
GROUP BY  DATE_FORMAT(p.PurchaseDate,'%b %Y'),  pd.CatalogID
) yy ON yy.CatalogID =c.ID AND yy.MONTH_YEAR = c.MONTH_YEAR

WHERE  (totalSale >0 OR totalpurchase >0)
AND TempMonth BETWEEN @startDate AND @endDate


";

            IDBHelper context = new DBHelper();
            context.CommandText = sqlQuery;

            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmMonthlyGrossProfit> result = DBUtil.ExecuteMapper<CstmMonthlyGrossProfit>(context, new CstmMonthlyGrossProfit());
            return result;
        }

        public static List<CstmDailySalesPerCatalog> GetDailySalesPerCatalog(DateTime start, DateTime end)
        {
            string sql = @"
SELECT SUM(sd.totalprice) AS TOTAL_SALE,
SUM(sd.Quantity) AS Quantity,
CAST(s.transactiondate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.transactiondate,'%d %M %Y') AS MONTH_YEAR,
c.name AS CATALOG_NAME, 
sd.CatalogID  AS CATALOG_ID, c.Unit
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Catalog c ON sd.CatalogID= c.ID                                                                  


WHERE  s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
AND CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
GROUP BY CAST(s.transactiondate AS DATE) ,sd.CatalogID

";
            sql = @"

SELECT * FROM (
SELECT SUM(sd.totalprice) AS TOTAL_SALE,
SUM(sd.Quantity) AS Quantity,
CAST(s.transactiondate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.transactiondate,'%d %M %Y') AS MONTH_YEAR,
c.name AS CATALOG_NAME, 
sd.CatalogID  AS CATALOG_ID, c.Unit
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Catalog c ON sd.CatalogID= c.ID                                                                  
WHERE  s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
GROUP BY CAST(s.transactiondate AS DATE) ,sd.CatalogID
UNION ALL

SELECT (IFNULL( SUM(sd.totalprice),0)  * -1) AS TOTAL_SALE,
(IFNULL( SUM(sd.Qty),0) * -1) AS Quantity,
CAST(s.transactiondate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.transactiondate,'%d %M %Y') AS MONTH_YEAR,
CONCAT('Retur ',ca.Name) AS CATALOG_NAME,
sd.Catalog  AS CATALOG_ID, ca.Unit
FROM s_return_detail sd
INNER JOIN s_return sr ON sr.ReturnNo = sd.ReturnNo
INNER JOIN Sale s ON s.transactionID = sr.transactionID
INNER JOIN Catalog ca ON sd.Catalog = ca.ID

GROUP BY CAST(s.transactiondate AS DATE) ,sd.Catalog
) A

WHERE A.YEAR_MONTH_DAY BETWEEN @startDate AND @endDate
GROUP BY A.YEAR_MONTH_DAY , A.CATALOG_NAME
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmDailySalesPerCatalog> result = DBUtil.ExecuteMapper<CstmDailySalesPerCatalog>(context, new CstmDailySalesPerCatalog());
            return result.OrderByDescending(t => t.TransDate).ToList();
        }


        public static List<CstmDailySale> GetTotalDailyPurchases(DateTime start, DateTime end)
        {

            string sql = @"

SELECT * FROM (
SELECT IFNULL( SUM(sd.totalprice),0) AS TOTAL_SALE,
IFNULL( SUM(sd.Qty),0) AS Qty,
CAST(s.PurchaseDate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.PurchaseDate,'%d %M %Y') AS MONTH_YEAR,
c.Name AS CUSTOMER_NAME, 
sd.CatalogID  AS CATALOG_ID,
ca.Name AS CATALOG_NAME,
'' Sales 
FROM PurchaseDetail sd
INNER JOIN Purchase s ON s.PurchaseNo = sd.PurchaseNo
INNER JOIN Supplier c ON s.SupplierCode = c.Code
INNER JOIN Catalog ca ON sd.CatalogID = ca.ID

WHERE  s.PurchaseNo NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
GROUP BY CAST(s.PurchaseDate AS DATE) ,c.Name
UNION ALL


SELECT (IFNULL( SUM(sd.totalprice),0)  * -1) AS TOTAL_SALE,
(IFNULL( SUM(sd.Qty),0) * -1) AS Qty,
CAST(s.PurchaseDate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.PurchaseDate,'%d %M %Y') AS MONTH_YEAR,
c.Name AS CUSTOMER_NAME, 
sd.Catalog  AS CATALOG_ID,
CONCAT('Retur ',ca.Name) AS CATALOG_NAME,
'' Sales 
FROM p_return_detail sd
INNER JOIN p_return sr ON sr.ReturnNo = sd.ReturnNo
INNER JOIN Purchase s ON s.PurchaseNo = sr.TransactionID
INNER JOIN Supplier c ON s.SupplierCode = c.Code
INNER JOIN Catalog ca ON sd.Catalog = ca.ID 

GROUP BY CAST(s.PurchaseDate AS DATE) ,c.Name

) A


WHERE A.YEAR_MONTH_DAY BETWEEN @startDate AND @endDate 
GROUP BY A.YEAR_MONTH_DAY ,A.CUSTOMER_NAME, A.CATALOG_NAME
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmDailySale> result = DBUtil.ExecuteMapper<CstmDailySale>(context, new CstmDailySale());
            return result.OrderByDescending(t => t.TransDate).ToList();

        }


        public static List<CstmDailySale> GetTotalDailySales(DateTime start, DateTime end)
        {
            
            string sql = @"

SELECT * FROM (
SELECT IFNULL( SUM(sd.totalprice),0) AS TOTAL_SALE,
IFNULL( SUM(sd.Quantity),0) AS Qty,
CAST(s.transactiondate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.transactiondate,'%d %M %Y') AS MONTH_YEAR,
c.Fullname AS CUSTOMER_NAME, 
sd.CatalogID  AS CATALOG_ID,
ca.Name AS CATALOG_NAME,
c.Sales 
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Customer c ON s.MemberID = c.ID
INNER JOIN Catalog ca ON sd.CatalogID = ca.ID

WHERE  s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
GROUP BY CAST(s.transactiondate AS DATE) ,s.MemberID
UNION ALL


SELECT (IFNULL( SUM(sd.totalprice),0)  * -1) AS TOTAL_SALE,
(IFNULL( SUM(sd.Qty),0) * -1) AS Qty,
CAST(s.transactiondate AS DATE) AS YEAR_MONTH_DAY,
DATE_FORMAT(s.transactiondate,'%d %M %Y') AS MONTH_YEAR,
c.Fullname AS CUSTOMER_NAME, 
sd.Catalog  AS CATALOG_ID,
CONCAT('Retur ',ca.Name) AS CATALOG_NAME,
c.Sales 
FROM s_return_detail sd
INNER JOIN s_return sr ON sr.ReturnNo = sd.ReturnNo
INNER JOIN Sale s ON s.transactionID = sr.transactionID
INNER JOIN Customer c ON s.MemberID = c.ID
INNER JOIN Catalog ca ON sd.Catalog = ca.ID 

GROUP BY CAST(s.transactiondate AS DATE) ,s.MemberID

) A


WHERE A.YEAR_MONTH_DAY BETWEEN @startDate AND @endDate 
GROUP BY A.YEAR_MONTH_DAY ,A.CUSTOMER_NAME, A.CATALOG_NAME
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmDailySale> result = DBUtil.ExecuteMapper<CstmDailySale>(context, new CstmDailySale());
            return result.OrderByDescending(t => t.TransDate).ToList();

        }

        public static List<CstmTotalSalePerMonth> GetTotalSalePerCatalog(DateTime start, DateTime end)
        {
            //            string sql = @"
            //SELECT SUM(sd.totalprice) AS TOTAL_SALE,
            //DATE_FORMAT(s.transactiondate,'%M %Y') AS MONTH_YEAR,
            // DATE_FORMAT(s.transactiondate,'%Y/%m/01') AS YEAR_MONTH_DAY,
            //c.Name AS CATALOG_NAME, 
            //sd.CatalogID  AS CATALOG_ID
            //FROM SaleDetail sd
            //INNER JOIN Sale s ON s.transactionID = sd.transactionID
            //INNER JOIN Catalog c ON sd.CatalogID = c.ID
            //where CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
            //GROUP BY (MONTH(s.transactiondate)),sd.CatalogID
            //";
            string sql = @"

SELECT SUM(sd.totalprice) AS Sale,
SUM(sd.Quantity) as Quantity,
DATE_FORMAT(s.transactiondate,'%M %Y') AS MONTH_YEAR,
 DATE_FORMAT(s.transactiondate,'%Y/%m/01') AS TransDate,
c.Name AS Item, c.Unit,  sd.CatalogID  AS ItemID
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Catalog c ON sd.CatalogID = c.ID
WHERE s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
AND CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
GROUP BY (MONTH(s.transactiondate)),sd.CatalogID
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmTotalSalePerMonth> result = DBUtil.ExecuteMapper<CstmTotalSalePerMonth>(context, new CstmTotalSalePerMonth());
            return result;

        }


        public static List<CstmPerformancePerMonth> GetPerformancePerMonth(DateTime start, DateTime end)
        {
            string sql = @"


SELECT SUM(sd.totalprice) AS TOTAL_SALE,
DATE_FORMAT(s.transactiondate,'%M %Y') AS MONTH_YEAR,
 DATE_FORMAT(s.transactiondate,'%Y/%m/01') AS YEAR_MONTH_DAY
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Catalog c ON sd.CatalogID = c.ID
WHERE  s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
AND CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate 
GROUP BY (MONTH(s.transactiondate))
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmPerformancePerMonth> result = DBUtil.ExecuteMapper<CstmPerformancePerMonth>(context, new CstmPerformancePerMonth());
            return result;

        }


        public static List<CstmTotalSalePerCustomer> GetTotalSalePerCustomer(DateTime start, DateTime end)
        {
            string sql = @"
SELECT SUM(sd.totalprice) AS TOTAL_SALE,
DATE_FORMAT(s.transactiondate,'%b %Y') AS MONTH_YEAR,
DATE_FORMAT(s.transactiondate,'%Y/%m/01') AS YEAR_MONTH_DAY,
 

cust.FullName  AS CustomerName
FROM SaleDetail sd
INNER JOIN Sale s ON s.transactionID = sd.transactionID
INNER JOIN Customer cust ON cust.ID = s.MemberID


WHERE CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate

GROUP BY (MONTH(s.transactiondate)),s.MemberID
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            context.CommandType = CommandType.Text;
            List<CstmTotalSalePerCustomer> result = DBUtil.ExecuteMapper<CstmTotalSalePerCustomer>(context, new CstmTotalSalePerCustomer());
            return result;

        }



        public static List<CstmTotalSalePerDay> GetTotalSales(DateTime start, DateTime end)
        {
            string sql = @"
SELECT  SUM( sd.totalprice) AS TotalSales, SUM(sd.Quantity) AS Qty, c.name AS  CatalogName ,sd.CatalogID, CAST(s.TransactionDate  AS DATE) AS TransactionDate, c.Unit FROM saleDetail sd
INNER JOIN Sale s ON s.TransactionID =sd.TransactionID 
INNER JOIN Catalog c ON c.ID =sd.CatalogID
WHERE CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
AND s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
GROUP BY  CAST(s.TransactionDate  AS DATE) , sd.CatalogID ";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.AddParameter("@startDate", start.Date);
            context.AddParameter("@endDate", end.Date);
            context.CommandType = CommandType.Text;
            List<CstmTotalSalePerDay> result = DBUtil.ExecuteMapper<CstmTotalSalePerDay>(context, new CstmTotalSalePerDay());
            return result;
        }
        public static List<CstmDailyGrossProfit> GetDailyGrossProfit(DateTime start, DateTime end)
        {
            List<CstmDailyGrossProfit> list = new List<CstmDailyGrossProfit>();
            List<CstmTotalSalePerDay> penjualan = GetTotalSales(start, end);
            List<cstmHPP> hppList = HPPItem.GetHPP(start.Date, end.Date);

            foreach (CstmTotalSalePerDay item in penjualan)
            {
                DateTime lastTransDate = item.TransDate;


                CstmDailyGrossProfit dgp = new CstmDailyGrossProfit();
                dgp.Transdate = item.TransDate;
                dgp.TotalSales = item.TotalSales;
                dgp.Qty = item.Qty;
                dgp.Unit = item.Unit;
                dgp.CatalogID = item.CatalogID;
                dgp.CatalogName = item.CatalogName;


                List<CstmPurchasePriceRate> listx = PurchaseItem.GetHargaBeliRata(item.CatalogID.ToString(), item.TransDate);
                decimal price = listx.Count > 0 ? listx.Sum(t => t.Price) / listx.Count : 0;

                dgp.HPP = hppList.Where(t => t.CatalogID == item.CatalogID && t.TransDate.Date == item.TransDate.Date).Select(t => t.HPP).Sum();
                dgp.DailyGrossProfit = dgp.TotalSales - (dgp.Qty * dgp.HPP.Value);
                list.Add(dgp);
            }


            return list;
        }


        public static int Delete(string transactionID)
        {
            Sale item = GetByTransID(transactionID);
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = " Delete from saledetail where TransactionID=@TransactionID ";
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@TransactionID", transactionID);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    itemResult = 0;
                    ictx.CommandText = " Delete from sale where TransactionID=@TransactionID ";
                    ictx.CommandType = CommandType.Text;
                    ictx.AddParameter("@TransactionID", transactionID);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                    if (itemResult > 0)
                    {
                        ictx.CommitTransaction();
                        itemResult = 1;
                    }
                }
            }
            catch (Exception)
            {
                ictx.RollbackTransaction();
            }

            return itemResult;
        }


        //        public static List<CstmSellPrice> GetSellPrices(DateTime start, DateTime end)
        //        {
        //            // total harga jual        
        //            string sql = @"
        //SELECT CatalogID,c.Name AS CatalogName, c.Unit, 
        //SUM( sd.TotalPrice ) TotalSellPrice, 
        //SUM(sd.Quantity) AS TotalQty, 
        //SUM(sd.Price) AS TotalPrice, 
        //SUM(sd.Price)/COUNT(1) AS SellPrice, 
        //CAST(s.TransactionDate  AS DATE) AS TransDate, COUNT(1) AS TotalItem FROM saleDetail sd
        //INNER JOIN Sale s ON s.TransactionID = sd.TransactionID 
        //INNER JOIN Catalog c ON c.ID = sd.CatalogID
        //
        //WHERE CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
        //AND s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
        //GROUP BY CatalogID, TransDate ";

        //            //            sql = @"
        //            //SELECT c.FUllName AS CustomerName, cat.Unit, s.MemberID as CustomerID, cat.ID as CatalogID,
        //            //SUM( sd.TotalPrice ) TotalSellPrice, 
        //            //SUM(sd.Quantity) AS TotalQty, 
        //            //SUM(sd.Price) AS TotalPrice, 
        //            //SUM(sd.Price)/COUNT(1) AS SellPrice, 
        //            //CAST(s.TransactionDate  AS DATE) AS TransDate, COUNT(1) AS TotalItem FROM saleDetail sd
        //            //INNER JOIN Sale s ON s.TransactionID = sd.TransactionID 
        //            //INNER JOIN Customer c ON c.ID = s.MemberID
        //            //INNER JOIN Catalog cat ON Cat.ID = sd.CatalogID
        //            //
        //            //WHERE CAST(s.TransactionDate  AS DATE) BETWEEN @startDate AND @endDate
        //            //AND s.TransactionID NOT IN (SELECT DISTINCT TransactionID FROM reconcile )
        //            //GROUP BY MemberID, TransDate
        //            //
        //            //";

        //            IDBHelper context = new DBHelper();
        //            context.CommandText = sql;
        //            context.CommandType = CommandType.Text;
        //            context.AddParameter("@startDate", start);
        //            context.AddParameter("@endDate", end);
        //            List<CstmSellPrice> result = DBUtil.ExecuteMapper<CstmSellPrice>(context, new CstmSellPrice());
        //            return result;

        //        }

        public static CstmBuyPrice GetPrevBuyPrices(DateTime start, int catalogID)
        {// daftar harga beli       
            string sql = @"
SELECT CatalogID,c.Name AS CatalogName, c.Unit, 
SUM(pd.TotalPrice ) TotalBuyPrice, 
SUM(pd.Qty) AS TotalQty, 
SUM(pd.PricePerUnit) AS TotalPrice, 
SUM(pd.PricePerUnit)/COUNT(1) AS BuyPrice, 
CAST(p.PurchaseDate  AS DATE) AS TransDate, COUNT(1) AS TotalItem FROM purchasedetail pd
INNER JOIN purchase p ON p.PurchaseNo= pd.PurchaseNo 
INNER JOIN Catalog c ON c.ID = pd.CatalogID
where 
p.PurchaseNo NOT IN (SELECT DISTINCT purchaseno FROM reconcile  )
AND CAST(p.PurchaseDate  AS DATE)  < CAST(@startDate  AS DATE)  and pd.CatalogID =@CatalogID
GROUP BY CatalogID, TransDate  desc
";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;
            context.AddParameter("@startDate", start);
            context.AddParameter("@CatalogID", catalogID);

            List<CstmBuyPrice> result = DBUtil.ExecuteMapper<CstmBuyPrice>(context, new CstmBuyPrice());
            return result.FirstOrDefault();
        }


        public static List<CstmBuyPrice> GetBuyPrices(DateTime start, DateTime end)
        {// daftar harga beli  

            /*
SELECT CatalogID,c.Name AS CatalogName, c.Unit, 
SUM(pd.TotalPrice ) TotalBuyPrice, 
SUM(pd.Qty) AS TotalQty, 
SUM(pd.PricePerUnit) AS TotalPrice, 
SUM(pd.PricePerUnit)/COUNT(1) AS BuyPrice, 
CAST(p.PurchaseDate  AS DATE) AS TransDate, COUNT(1) AS TotalItem FROM purchasedetail pd
INNER JOIN purchase p ON p.PurchaseNo= pd.PurchaseNo 
INNER JOIN Catalog c ON c.ID = pd.CatalogID
where CAST(p.PurchaseDate  AS DATE) BETWEEN @startDate AND @endDate
GROUP BY CatalogID, TransDate
             */
            string sql = @"

SELECT CatalogID,c.Name AS CatalogName, c.Unit, 
pd.TotalPrice AS TotalBuyPrice, 
pd.Qty AS TotalQty, 
pd.TotalPrice AS TotalPrice, 
pd.PricePerUnit  AS BuyPrice, 
p.CreatedBy,
CAST(p.PurchaseDate  AS DATE) AS TransDate FROM purchasedetail pd
INNER JOIN purchase p ON p.PurchaseNo= pd.PurchaseNo 
INNER JOIN Catalog c ON c.ID = pd.CatalogID
WHERE CAST(p.PurchaseDate  AS DATE) BETWEEN @startDate AND @endDate 

";
            IDBHelper context = new DBHelper();
            context.CommandText = sql;
            context.CommandType = CommandType.Text;
            context.AddParameter("@startDate", start);
            context.AddParameter("@endDate", end);
            List<CstmBuyPrice> result = DBUtil.ExecuteMapper<CstmBuyPrice>(context, new CstmBuyPrice());
            return result;
        }

    }
}
