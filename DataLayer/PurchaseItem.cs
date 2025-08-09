using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public partial class PurchaseItem
    {
        public static List<Variable> GetAllPurchaseCounter()
        {
            List<Variable> result = new List<Variable>();
            IDBHelper context = new DBHelper();
            string commandText = @" 
SELECT s.Counter AS display,   s.Counter CODE, 0 Sequence, '' category
FROM Purchase  s
";

            IDBHelper ictx = new DBHelper();
            ictx.CommandText = commandText;
            ictx.CommandType = CommandType.Text;
            result = DBUtil.ExecuteMapper<Variable>(ictx, new Variable());

            return result;
        }
        public static Purchase GetByCounter(int counter)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT s.*
FROM Purchase s
WHERE s.Counter= @counter 
limit 1
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@counter", counter);
            List<Purchase> result = DBUtil.ExecuteMapper<Purchase>(context, new Purchase());
            Purchase purchase = result.FirstOrDefault();
            if (purchase != null)
            {
                purchase.Details = PurchaseDetailItem.GetByPurchaseNo(purchase.PurchaseNo);
            }
            return purchase;
        }

        //        public static int Insert(Sale item)
        //        {
        //            string itemQuery = @"
        //INSERT INTO sale 
        //	(TransactionID, 
        //	TotalPrice, 
        //	TotalQty, 
        //	TransactionDate, 
        //	Username, 
        //	MemberID, 
        //	Terminal, 
        //	TotalPayment, 
        //	TotalPaymentReturn, 
        //	Notes, 
        //	PaymentType
        //	)
        //	VALUES
        //	(
        //    @TransactionID, 
        //	@TotalPrice, 
        //	@TotalQty, 
        //	@TransactionDate, 
        //	@Username, 
        //	@MemberID, 
        //	@Terminal, 
        //	@TotalPayment, 
        //	@TotalPaymentReturn, 
        //	@Notes, 
        //	@PaymentType
        //	);
        //";

        //            string detailQuery = @"
        //INSERT INTO saledetail 
        //	( 
        //	TransactionID, 
        //	CatalogID, 
        //	Price, 
        //	Discount, 
        //	Quantity, 
        //	TotalPrice
        //	)
        //	VALUES
        //	(
        //	@TransactionID, 
        //	@CatalogID, 
        //	@Price, 
        //	@Discount, 
        //	@Quantity, 
        //	@TotalPrice
        //	);
        //";

        //            int itemResult = 0;
        //            IDBHelper ictx = new DBHelper();
        //            try
        //            {
        //                ictx.BeginTransaction();
        //                ictx.CommandText = itemQuery;
        //                ictx.CommandType = CommandType.Text;
        //                ictx.AddParameter("@TransactionID", item.TransactionID);
        //                ictx.AddParameter("@TotalPrice", item.TotalPrice);
        //                ictx.AddParameter("@TotalQty", item.TotalQty);
        //                ictx.AddParameter("@TransactionDate", item.TransactionDate);
        //                ictx.AddParameter("@Username", item.Username);
        //                ictx.AddParameter("@MemberID", item.MemberID);
        //                ictx.AddParameter("@Terminal", item.Terminal);
        //                ictx.AddParameter("@TotalPayment", item.TotalPayment);
        //                ictx.AddParameter("@TotalPaymentReturn", item.TotalPaymentReturn);
        //                ictx.AddParameter("@Notes", item.Notes);
        //                ictx.AddParameter("@PaymentType", item.PaymentType);
        //                itemResult = DBUtil.ExecuteNonQuery(ictx);
        //                if (itemResult > 0)
        //                {
        //                    ictx.CommandText = detailQuery;
        //                    foreach (SaleDetail deital in item.Details)
        //                    {

        //                        ictx.AddParameter("@TransactionID", item.TransactionID);
        //                        ictx.AddParameter("@CatalogID", deital.CatalogID);
        //                        ictx.AddParameter("@Price", deital.Price);
        //                        ictx.AddParameter("@Discount", deital.Discount);
        //                        ictx.AddParameter("@Quantity", deital.Quantity);
        //                        ictx.AddParameter("@TotalPrice", deital.TotalPrice);
        //                        int result = DBUtil.ExecuteNonQuery(ictx);
        //                        if (result == -1)
        //                        {
        //                            ictx.RollbackTransaction();
        //                        }
        //                    }
        //                    ictx.CommitTransaction();
        //                }
        //            }
        //            catch (Exception)
        //            { ictx.RollbackTransaction(); }

        //            return itemResult;
        //        }

        //        public static Sale GetByTransID(string transactionID)
        //        {
        //            IDBHelper context = new DBHelper();
        //            context.CommandText = @"	SELECT * from Sale WHERE TransactionID = @TransactionID ";
        //            context.CommandType = CommandType.Text;
        //            context.AddParameter("@TransactionID", transactionID);
        //            List<Sale> result = DBUtil.ExecuteMapper<Sale>(context, new Sale());
        //            Sale sale = result.FirstOrDefault();
        //            sale.Details = SaleDetailItem.GetTransID(sale.TransactionID);
        //            return sale;
        //        }

        public static List<Purchase> GetAll()
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @" SELECT * FROM Purchase  ";
            ictx.CommandType = CommandType.Text;

            return DBUtil.ExecuteMapper<Purchase>(ictx, new Purchase());
        }

        public static int GetLastID()
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"

SELECT MAX(ID) AS AutoID FROM Purchase
  
";
            ictx.CommandType = CommandType.Text;
            int lastIndex = 1;
            object item = DBUtil.ExecuteScalar(ictx);
            if (item != null)
            {
                int.TryParse(item.ToString(), out lastIndex);
                return lastIndex + 1;
            }
            return 1;
        }

        public static Purchase GetByCode(string purchaseNo)
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"
SELECT * FROM Purchase
  where PurchaseNo =@PurchaseNo
";
            ictx.CommandType = CommandType.Text;
            ictx.AddParameter("@PurchaseNo", purchaseNo);
            Purchase item = DBUtil.ExecuteMapper<Purchase>(ictx, new Purchase()).FirstOrDefault();
            if (item != null)
            {
                item.Details = PurchaseDetailItem.GetByPurchaseNo(item.PurchaseNo);
            }
            return item;
        }

        public static List<CstmPurchasePriceRate> GetHargaBeliRata(string catalogID, DateTime transDate)
        {
            IDBHelper context = new DBHelper();
            //            context.CommandText = @"
            //SELECT CASE WHEN a.PricePerUnit IS NULL THEN 0 ELSE a.PricePerUnit END AS PricePerUnit, b.PurchaseDate FROM purchasedetail a
            //INNER JOIN Purchase b  ON a.PurchaseNo =b.PurchaseNo 
            //WHERE a.catalogID = @catalogID AND b.PurchaseDate <=@PurchaseDate
            //ORDER BY b.PurchaseDate DESC
            //limit 3

            //";
            //            SELECT SUM(CASE WHEN a.PricePerUnit IS NULL THEN 0 ELSE a.PricePerUnit END)/ COUNT(1) AS PricePerUnit, COUNT(1) AS totalItem, DATE_FORMAT(b.PurchaseDate ,'%d-%m-%Y') AS PurchaseDate FROM purchasedetail a
            //INNER JOIN Purchase b  ON a.PurchaseNo =b.PurchaseNo 
            //WHERE a.catalogID = @catalogID AND b.PurchaseDate <=@PurchaseDate
            //GROUP BY CAST(s.transactiondate AS DATE) , a.CatalogID
            //ORDER BY b.PurchaseDate DESC
            //LIMIT 2
            context.CommandText = @"
SELECT SUM(CASE WHEN a.PricePerUnit IS NULL THEN 0 ELSE a.PricePerUnit END)/ COUNT(1) AS PricePerUnit, COUNT(1) AS totalItem,  CAST(b.PurchaseDate AS DATE) AS PurchaseDate FROM purchasedetail a
INNER JOIN Purchase b  ON a.PurchaseNo =b.PurchaseNo 
WHERE a.catalogID =  @catalogID AND CAST( b.PurchaseDate as Date) <= CAST( @PurchaseDate as Date)
GROUP BY CAST(b.PurchaseDate AS DATE) , a.CatalogID
ORDER BY b.PurchaseDate DESC
LIMIT 2 

 ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@catalogID", catalogID);
            context.AddParameter("@PurchaseDate", transDate);
            List<CstmPurchasePriceRate> result = DBUtil.ExecuteMapper<CstmPurchasePriceRate>(context, new CstmPurchasePriceRate());
            return result;

        }

        public static CstmPurchaseDetail GetCatalogReconcilePrice(int catalogID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"

SELECT pd.*,c.Name AS CatalogName, p.PurchaseDate FROM Purchase  p
LEFT JOIN Supplier s ON s.Code = p.SupplierCode
LEFT JOIN purchasedetail pd ON pd.PurchaseNo =p.PurchaseNo 
INNER JOIN Catalog c ON c.ID =pd.CatalogID
WHERE pd.CatalogID = @catalogID
ORDER BY p.PurchaseDate desc
limit 5
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@catalogID", catalogID);
            List<CstmPurchaseDetail> result = DBUtil.ExecuteMapper<CstmPurchaseDetail>(context, new CstmPurchaseDetail());

            return result.FirstOrDefault();
        }


        public static List<CstmPurchase> GetCstmPaging(string text, int offset, int pageSize)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT p.*,CASE WHEN p.createdby ='system' THEN 'System' ELSE s.Name END AS SupplierName FROM Purchase  p
LEFT JOIN Supplier s ON s.Code = p.SupplierCode

WHERE ( p.Notes LIKE concat ('%', @text ,'%') OR s.Name  LIKE concat ('%', @text ,'%') )
and p.purchaseno NOT IN( SELECT DISTINCT purchaseno FROM reconcile)
order by p.Counter desc
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<CstmPurchase> result = DBUtil.ExecuteMapper<CstmPurchase>(context, new CstmPurchase());

            return result;
        }


        public static int GetCstmRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT count(1) from Purchase p
left JOIN Supplier s ON s.Code = p.SupplierCode
WHERE ( Notes LIKE concat ('%', @text ,'%')  OR s.Name  LIKE concat ('%', @text ,'%') )
and p.purchaseno NOT IN( SELECT DISTINCT purchaseno FROM reconcile)
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static int GetRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT count(*) from Purchase 
WHERE PurchaseNo LIKE concat ('%', @text ,'%') 
OR Notes LIKE concat ('%', @text ,'%') 
  
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static List<Purchase> GetPaging(string text, int offset, int pageSize)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT * from Purchase 
WHERE PurchaseNo LIKE concat ('%', @text ,'%') 
OR Notes LIKE concat ('%', @text ,'%') 
order by PurchaseDate desc
LIMIT  @pageSize OFFSET @offset
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Purchase> result = DBUtil.ExecuteMapper<Purchase>(context, new Purchase());
            result.ForEach(item =>
            {
                if (item != null)
                {
                    item.Details = PurchaseDetailItem.GetByPurchaseNo(item.PurchaseNo);
                }
            });
            return result;
        }



        public static int Save(Purchase item)
        {
            Purchase existing = GetByCode(item.PurchaseNo);
            if (existing != null)
                throw new Exception(string.Format("{0} already exist!", item.PurchaseNo));
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"

INSERT INTO purchase 
	(PurchaseNo, 
	PurchaseDate, 
	Notes, 
	CreatedDate, 
	CreatedBy, 
	SupplierCode, 
	TotalQty, 
	TotalPrice
	)
	VALUES
	(@PurchaseNo, 
	@PurchaseDate, 
	@Notes, 
	@CreatedDate, 
	@CreatedBy, 
	@SupplierCode, 
	@TotalQty, 
	@TotalPrice
	);
";
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);
                ictx.AddParameter("@CreatedDate", DateTime.Now);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {

                    ictx.CommandType = CommandType.Text;
                    foreach (PurchaseDetail itemDetail in item.Details)
                    {
                        ictx.CommandText = @"

INSERT INTO purchasedetail 
	(PurchaseNo, 
	CatalogID, 
	Qty, 
	PricePerUnit, 
	CreatedDate, 
	CreatedBy, 
	TotalPrice, 
	Unit
	)
	VALUES
	(@PurchaseNo, 
	@CatalogID, 
	@Qty, 
	@PricePerUnit, 
	@CreatedDate, 
	@CreatedBy, 
	@TotalPrice, 
	@Unit
	);

                    ";
                        ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        ictx.AddParameter("@Qty", itemDetail.Qty);
                        ictx.AddParameter("@PricePerUnit", itemDetail.PricePerUnit);
                        ictx.AddParameter("@CreatedDate", DateTime.Now);
                        ictx.AddParameter("@CreatedBy", itemDetail.CreatedBy);
                        ictx.AddParameter("@TotalPrice", itemDetail.TotalPrice);
                        ictx.AddParameter("@Unit", itemDetail.Unit);
                        int detailResult = DBUtil.ExecuteNonQuery(ictx);


                        #region Update HargaBeli
                        //update harga jual
                        ictx.CommandText = @" 
                            SELECT * FROM catalogPrice
WHERE CatalogID=@CatalogID
ORDER BY PriceDate desc
LIMIT 1
                            ";
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        CatalogPrice priceItem = DBUtil.ExecuteMapper<CatalogPrice>(ictx, new CatalogPrice()).FirstOrDefault();
                        if (priceItem != null && priceItem.PriceDate.Date == DateTime.Today)
                        {
                            //update
                            ictx.CommandText = @" 
UPDATE catalogprice 
	SET
	BuyPricePerUnit = @BuyPricePerUnit, CreatedBy =@Username
	WHERE
	CatalogID = @CatalogID
	AND PriceDate =@PriceDate
";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@BuyPricePerUnit", itemDetail.PricePerUnit);
                            ictx.AddParameter("@PriceDate", DateTime.Today);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@SupplierCode", item.SupplierCode);
                            DBUtil.ExecuteNonQuery(ictx);

                        }
                        else
                        {
                            // insert
                            ictx.CommandText = @" 

INSERT INTO catalogprice    (CatalogID, BuyPricePerUnit,  PriceDate,  CreatedBy,  SellPrice, SupplierCode )
                    VALUES  (@CatalogID,  @BuyPricePerUnit,  @PriceDate,  @Username, @SellPrice , @SupplierCode );
";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@SellPrice", priceItem == null ? 0 : priceItem.SellPrice);
                            ictx.AddParameter("@PriceDate", DateTime.Today);
                            ictx.AddParameter("@BuyPricePerUnit", itemDetail.PricePerUnit);
                            ictx.AddParameter("@SupplierCode", item.SupplierCode);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        #endregion

                        #region UPdateStock
                        ictx.CommandText = @" 
SELECT * FROM catalogstock WHERE CatalogID=@CatalogID
ORDER BY StockDate DESC
LIMIT 1
                            ";
                        ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                        CatalogStock stockItem = DBUtil.ExecuteMapper<CatalogStock>(ictx, new CatalogStock()).FirstOrDefault();
                        if (stockItem != null && stockItem.StockDate.Date == DateTime.Today)
                        {
                            ictx.CommandText = @"           
Update catalogstock 
Set Stock = @Stock,
CreatedBy = @Username 
Where CatalogID =@CatalogID 
AND StockDate =@StockDate
";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Stock", itemDetail.Qty + stockItem.Stock);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            ictx.AddParameter("@StockDate", DateTime.Today);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        else
                        {
                            ictx.CommandText = @" 
INSERT INTO catalogstock 
	(CatalogID, 
	Stock, 
	StockDate, 
	CreatedBy
	)
	VALUES
	(@CatalogID, 
	@Stock, 
	@StockDate, 
	@Username
	);
";
                            ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                            ictx.AddParameter("@Stock", (stockItem == null) ? itemDetail.Qty : (itemDetail.Qty + stockItem.Stock));
                            ictx.AddParameter("@StockDate", DateTime.Today);
                            ictx.AddParameter("@Username", item.CreatedBy);
                            DBUtil.ExecuteNonQuery(ictx);
                        }
                        #endregion
                    }

                    ictx.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return itemResult;
        }

        private static int GetPurchaseRowIndex()
        {
            int result = 0;
            string query = @"
SELECT IFNULL( MAX(counter), 0) LAST_INDEX FROM purchase 
  
";

            IDBHelper ictx = new DBHelper();

            ictx.CommandText = query;
            ictx.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(ictx);
            if (obj == null) return 1;
            int.TryParse(obj.ToString(), out result);

            return result + 1;
        }

        public static int SaveStock(Purchase item)
        {
            int itemResult = -1;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"

INSERT INTO purchase 
	(PurchaseNo, 
	PurchaseDate, 
	Notes, 
	CreatedDate, 
	CreatedBy, 
	SupplierCode, 
	TotalQty, 
	TotalPrice, Counter
	)
	VALUES
	(@PurchaseNo, 
	@PurchaseDate, 
	@Notes, 
	@CreatedDate, 
	@CreatedBy, 
	@SupplierCode, 
	@TotalQty, 
	@TotalPrice, @Counter
	);
";

                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);
                ictx.AddParameter("@CreatedDate", DateTime.Now);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@Counter", GetPurchaseRowIndex());
                
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                foreach (PurchaseDetail itemDetail in item.Details)
                {
                    ictx.CommandType = CommandType.Text;
                    ictx.CommandText = @"

INSERT INTO purchasedetail 
	(PurchaseNo, 
	CatalogID, 
	Qty, 
	PricePerUnit, 
	CreatedDate, 
	CreatedBy, 
	TotalPrice, 
	Unit,Coli
	)
	VALUES
	(@PurchaseNo, 
	@CatalogID, 
	@Qty, 
	@PricePerUnit, 
	@CreatedDate, 
	@CreatedBy, 
	@TotalPrice, 
	@Unit,@Coli
	);

                    ";
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    ictx.AddParameter("@CatalogID", itemDetail.CatalogID);
                    ictx.AddParameter("@Qty", itemDetail.Qty);
                    ictx.AddParameter("@PricePerUnit", itemDetail.PricePerUnit);
                    ictx.AddParameter("@CreatedDate", DateTime.Now);
                    ictx.AddParameter("@CreatedBy", itemDetail.CreatedBy);
                    ictx.AddParameter("@TotalPrice", itemDetail.TotalPrice);
                    ictx.AddParameter("@Unit", itemDetail.Unit);
                    ictx.AddParameter("@Coli", itemDetail.Coli);
                    int detailResult = DBUtil.ExecuteNonQuery(ictx);


                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception)
            {
                itemResult = -1;
            }

            return itemResult;
        }


        public static int EditStock(Purchase item)
        {
            int itemResult = -1;
            Purchase existingItem = PurchaseItem.GetByCode(item.PurchaseNo);
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = @"

UPDATE purchase 
	SET Notes = @Notes, 
	TotalQty = @TotalQty,
	TotalPrice = @TotalPrice,
	PurchaseDate = @PurchaseDate,
	SupplierCode = @SupplierCode
WHERE
	PurchaseNo = @PurchaseNo

";

                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);

                ictx.AddParameter("@PurchaseDate", item.PurchaseDate);
                ictx.AddParameter("@SupplierCode", item.SupplierCode);

                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    itemResult = 0;
                    ictx.CommandText = " Delete from purchasedetail where PurchaseNo=@PurchaseNo ";
                    ictx.CommandType = CommandType.Text;
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                }

                foreach (PurchaseDetail detail in item.Details)
                {
                    ictx.CommandType = CommandType.Text;
                    ictx.CommandText = @"

INSERT INTO purchasedetail 
	(PurchaseNo, 
	CatalogID, 
	Qty, 
	PricePerUnit, 
	CreatedDate, 
	CreatedBy, 
	TotalPrice, 
	Unit,Coli
	)
	VALUES
	(@PurchaseNo, 
	@CatalogID, 
	@Qty, 
	@PricePerUnit, 
	@CreatedDate, 
	@CreatedBy, 
	@TotalPrice, 
	@Unit,@Coli
	);

                    ";
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    ictx.AddParameter("@CatalogID", detail.CatalogID);
                    ictx.AddParameter("@Qty", detail.Qty);
                    ictx.AddParameter("@PricePerUnit", detail.PricePerUnit);
                    ictx.AddParameter("@CreatedDate", DateTime.Now);
                    ictx.AddParameter("@CreatedBy", detail.CreatedBy);
                    ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                    ictx.AddParameter("@Unit", detail.Unit);
                    ictx.AddParameter("@Coli", detail.Coli);
                    int detailResult = DBUtil.ExecuteNonQuery(ictx);

                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception)
            {
                itemResult = -1;
            }

            return itemResult;
        }

        public static int DeleteStock(string PurchaseNo)
        {
            int itemResult = -1;
            Purchase item = PurchaseItem.GetByCode(PurchaseNo);
            IDBHelper ictx = new DBHelper();
            try
            {
                //int index = GetLastID();
                ictx.BeginTransaction();
                ictx.CommandText = " Delete from purchasedetail where PurchaseNo=@PurchaseNo ";
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {
                    // balikin stok dulu
                    itemResult = 0;
                    ictx.CommandText = @" delete from  purchase WHERE PurchaseNo = @PurchaseNo ";
                    ictx.CommandType = CommandType.Text;
                    ictx.AddParameter("@PurchaseNo", item.PurchaseNo);
                    itemResult = DBUtil.ExecuteNonQuery(ictx);
                }

                ictx.CommitTransaction();
                itemResult = 1;

            }
            catch (Exception)
            {
                itemResult = -1;
            }

            return itemResult;
        }


    }
}
