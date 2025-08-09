using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;


namespace DataLayer
{
    public class SaleReturnItem
    {

        public static int Insert(SaleReturn item)
        {
            string itemQuery = @"


INSERT INTO s_return 
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
                ictx.AddParameter("@Customer", item.Customer);
                ictx.AddParameter("@Terminal", item.Terminal);
                ictx.AddParameter("@Notes", item.Notes);
                ictx.AddParameter("@Created", current);
                ictx.AddParameter("@Counter", item.Counter);
                ictx.AddParameter("@CreatedBy", item.CreatedBy);

                itemResult = DBUtil.ExecuteNonQuery(ictx);
                if (itemResult > 0)
                {

                    string detailQuery = @"


INSERT INTO s_return_detail 
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

                    foreach (SaleReturnDetail detail in item.Details)
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


                    //foreach (SaleReturnDetail detail in item.Details)
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


        public static SaleReturn GetByCode(string ReturnNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @" 
SELECT sr.*, c.ID Customer, c.Fullname CustomerName, v.Display PaymentTypeDesc, s.Counter NoStruk, s.Counter
FROM s_return sr
INNER JOIN Sale s ON s.TransactionID = sr.TransactionID
LEFT JOIN customer c ON s.MemberID = c.ID
LEFT JOIN variable v ON v.category ='PeymentType' AND v.code =s.PaymentType
WHERE sr.ReturnNo= @ReturnNo

";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReturnNo", ReturnNo);
            List<SaleReturn> result = DBUtil.ExecuteMapper<SaleReturn>(context, new SaleReturn());
            SaleReturn saleReturn = result.FirstOrDefault();
            if (saleReturn != null)
            {
                saleReturn.Details = SaleReturnDetailItem.GetByReturnNo(saleReturn.ReturnNo);
            }
            return saleReturn;
        }

        public static int GetIndexPerdate(DateTime ReturnDate)
        {
            IDBHelper ictx = new DBHelper();
            ictx.CommandText = @"


SELECT COUNT(1) LAST_INDEX FROM s_return 
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
FROM s_return sr
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

        public static List<SaleReturn> GetCstmPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT sr.*,s.MemberID Customer, c.Fullname CustomerName, s.Counter NoStruk
FROM s_return sr
INNER JOIN sale s ON s.TransactionID= sr.TransactionID
LEFT JOIN customer c ON s.MemberID = c.ID


WHERE ( 
sr.ReturnNo LIKE CONCAT ('%', @TEXT ,'%') 
OR s.TransactionID LIKE CONCAT ('%', @TEXT ,'%') 
OR sr.Notes LIKE CONCAT ('%', @TEXT ,'%') 
OR c.Fullname LIKE CONCAT ('%', @TEXT ,'%')
)

ORDER BY sr.ReturnDate DESC

LIMIT  @pageSize OFFSET @OFFSET

            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<SaleReturn> result = DBUtil.ExecuteMapper<SaleReturn>(context, new SaleReturn());
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
Delete from  s_return_detail where ReturnNo =@ReturnNo;
Delete from  s_return where ReturnNo =@ReturnNo;

            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReturnNo", ReturnNo);
            return DBUtil.ExecuteNonQuery(context);
        }
    }
}
