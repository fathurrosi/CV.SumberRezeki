using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;


namespace DataLayer
{
    public class PurchaseReturnItem
    {

        public static int Insert(PurchaseReturn item)
        {
            string itemQuery = @"


INSERT INTO p_return 
	(
	TransactionID, 
	Notes, 
	Created, 
	CreatedBy, 
	ReturnDate, 
	TotalQty, 
	TotalPrice, 
	ReturnNo, 
	Terminal,Counter
	)
	VALUES
	(
	@TransactionID, 
	@Notes, 
	@Created, 
	@CreatedBy, 
	@ReturnDate, 
	@TotalQty, 
	@TotalPrice, 
	@ReturnNo, 
	@Terminal,@Counter
	);


";

            DateTime current = DateTime.Now;
            int itemResult = 0;
            IDBHelper ictx = new DBHelper();
            try
            {
                ictx.BeginTransaction();
                ictx.CommandText = itemQuery;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@ReturnNo", item.ReturnNo);
                ictx.AddParameter("@TransactionID", item.TransactionID);
                ictx.AddParameter("@ReturnDate", item.ReturnDate);
                ictx.AddParameter("@TotalPrice", item.TotalPrice);
                ictx.AddParameter("@TotalQty", item.TotalQty);
                ictx.AddParameter("@Customer", item.Supplier);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@Created", current);
                ictx.AddParameter("@Counter", item.Counter);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);

                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {

                    string detailQuery = @"


INSERT INTO p_return_detail 
	(
	ReturnNo, 
	Catalog, 
	Price, 
	Discount, 
	Qty, Colly,
	TotalPrice, 
	Sequence, 
	Created, 
	CreatedBy
	)
	VALUES
	(
	@ReturnNo, 
	@Catalog, 
	@Price, 
	@Discount, 
	@Qty, @Colly,
	@TotalPrice, 
	@Sequence, 
	@Created, 
	@CreatedBy
	);

";

                    foreach (PurchaseReturnDetail detail in item.Details)
                    {
                        ictx.CommandText = detailQuery;
                        ictx.AddParameter("@ReturnNo", item.ReturnNo);
                        ictx.AddParameter("@Catalog", detail.Catalog);
                        ictx.AddParameter("@Price", detail.Price);
                        ictx.AddParameter("@Discount", detail.Discount);
                        ictx.AddParameter("@Qty", detail.Qty);
                        ictx.AddParameter("@Colly", detail.Colly);
                        ictx.AddParameter("@TotalPrice", detail.TotalPrice);
                        ictx.AddParameter("@Sequence", detail.Sequence);
                        ictx.AddParameter("@Created", current);
                        ictx.AddParameter("@CreatedBy", item.CreatedBy);
                        int result = DBUtil.ExecuteNonQuery(ictx);
                    }
                    ictx.CommitTransaction();


                    //foreach (PurchaseReturnDetail detail in item.Details)
                    //{
                    //    if (detail.Qty > 0)
                    //    {
                    //        CatalogItem.AddStock(detail.Catalog, detail.Qty, item.CreatedBy);
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                itemResult = -1;
                ictx.RollbackTransaction();
            }

            return itemResult;
        }


        public static PurchaseReturn GetByCode(string ReturnNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT sr.*, c.Code Supplier, c.Name SupplierName, '' PaymentTypeDesc, s.Counter NoStruk, s.Counter
FROM p_return sr
INNER JOIN purchase s ON s.PurchaseNo = sr.TransactionID
LEFT JOIN supplier c ON s.SupplierCode = c.Code

WHERE sr.ReturnNo= @ReturnNo

";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReturnNo", ReturnNo);
            List<PurchaseReturn> result = DBUtil.ExecuteMapper<PurchaseReturn>(context, new PurchaseReturn());
            PurchaseReturn PurchaseReturn = result.FirstOrDefault();
            if (PurchaseReturn != null)
            {
                PurchaseReturn.Details = PurchaseReturnDetailItem.GetByReturnNo(PurchaseReturn.ReturnNo);
            }
            return PurchaseReturn;
        }

        public static int GetIndexPerdate(DateTime ReturnDate)
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"


SELECT COUNT(1) LAST_INDEX FROM p_return 
WHERE DATE_FORMAT(ReturnDate,'%Y-%m-%d') =  DATE_FORMAT(@NOW,'%Y-%m-%d')
  
";
            ictx.CommandType = CommandType.Text;
            ictx.AddParameter("@NOW", ReturnDate);
            int lastIndex = 1;
            object item = DBUtil.ExecuteScalar(ictx);
            if (item != null)
            {
                int.TryParse(item.ToString(), out lastIndex);
                return lastIndex + 1;
            }
            return 1;
        }

        public static int GetCstmRecordCount(string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT Count(1) total
FROM p_return sr
INNER JOIN sale s ON s.TransactionID= sr.TransactionID
LEFT JOIN customer c ON s.MemberID = c.ID

WHERE ( 
sr.ReturnNo LIKE CONCAT ('%', @TEXT ,'%') 
OR s.TransactionID LIKE CONCAT ('%', @TEXT ,'%') 
OR sr.Notes LIKE CONCAT ('%', @TEXT ,'%') 
OR c.Fullname LIKE CONCAT ('%', @TEXT ,'%')
)

  
            ";
            context.AddParameter("@Text", text);
            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static List<PurchaseReturn> GetCstmPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT sr.*,p.SupplierCode Supplier, c.Name SupplierName, p.Counter NoStruk
FROM p_return sr
INNER JOIN purchase p ON p.PurchaseNo = sr.TransactionID
LEFT JOIN supplier c ON p.SupplierCode = c.Code


WHERE ( 
sr.ReturnNo LIKE CONCAT ('%', @TEXT ,'%') 
OR p.PurchaseNo LIKE CONCAT ('%', @TEXT ,'%') 
OR sr.Notes LIKE CONCAT ('%', @TEXT ,'%') 
OR c.Name LIKE CONCAT ('%', @TEXT ,'%')
)

ORDER BY sr.ReturnDate DESC

LIMIT  @pageSize OFFSET @OFFSET

            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<PurchaseReturn> result = DBUtil.ExecuteMapper<PurchaseReturn>(context, new PurchaseReturn());
            //result.ForEach(item =>
            //{
            //    if (item != null)
            //    {
            //        item.Details = SaleDetailItem.GetByTransactionID(item.TransactionID);
            //    }
            //});
            return result;
        }

        public static int Delete(string ReturnNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
Delete from  p_return_detail where ReturnNo =@ReturnNo;
Delete from  p_return where ReturnNo =@ReturnNo;

            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReturnNo", ReturnNo);
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
