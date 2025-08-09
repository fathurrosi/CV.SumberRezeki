using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class ReconcileItem
    {
        public static Reconcile GetByID(int reconcileID)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT * FROM reconcile
WHERE ID =@ReconcileID
 ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReconcileID", reconcileID);
            return DBUtil.ExecuteMapper<Reconcile>(context, new Reconcile()).FirstOrDefault();

        }

        public static List<ReconcileDetail> GetDetail(int reconcileID)
        {
            List<ReconcileDetail> items = new List<ReconcileDetail>();
            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT * FROM reconcileDetail
WHERE ReconcileID =@ReconcileID
 ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReconcileID", reconcileID);
            items = DBUtil.ExecuteMapper<ReconcileDetail>(context, new ReconcileDetail());

            return items;
        }
        public static List<Reconcile> GetPaging(string text, int offset, int pageSize)
        {

            IDBHelper context = new DBHelper();
            context.CommandText = @" SELECT * FROM reconcile
WHERE Description LIKE CONCAT ('%', @text ,'%')
ORDER BY id desc
LIMIT  @pageSize OFFSET @offset ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);
            List<Reconcile> result = DBUtil.ExecuteMapper<Reconcile>(context, new Reconcile());
            return result;
        }

        public static int GetRecordCount(string text)
        {
            int result = -1;
            IDBHelper context = new DBHelper();
            context.CommandText = @"SELECT  count(1) FROM reconcile
WHERE Description LIKE CONCAT ('%', @text ,'%') ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);

            context.CommandType = CommandType.Text;
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }

        public static Reconcile Insert(DateTime ProccessDate, string Description, ReconcileDetail detail)
        {
            Reconcile reconcile = null;
            string sql = @"

INSERT INTO reconcile 
	( 
	ProccessDate, 
	Description
	)
	VALUES
	(
	@ProccessDate, 
	@Description
	);

SELECT * FROM reconcile WHERE id = LAST_INSERT_ID();
";
            IDBHelper ictx = new DBHelper();
            try
            {

                ictx.BeginTransaction();
                ictx.CommandText = sql;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@Description", Description);
                ictx.AddParameter("@ProccessDate", ProccessDate);
                reconcile = DBUtil.ExecuteMapper<Reconcile>(ictx, new Reconcile()).FirstOrDefault();
                if (reconcile != null)
                {
                    sql = @"

INSERT INTO reconciledetail 
	(
	CatalogID, 
	CatalogQty, 
	CatalogPrice, 
	ProductID, 
	ProductPrice, 
	ProductQty, 
	CreatedBy, 
	CreatedDate, 
	ReconcileID,CatalogPriceDate
	)
	VALUES
	(
	@CatalogID, 
	@CatalogQty, 
	@CatalogPrice, 
	@ProductID, 
	@ProductPrice, 
	@ProductQty, 
	@CreatedBy, 
	@CreatedDate, 
	@ReconcileID,@CatalogPriceDate
	);
";
                    ictx.CommandText = sql;
                    ictx.CommandType = CommandType.Text;
                    ictx.AddParameter("@ReconcileID", reconcile.ID);
                    ictx.AddParameter("@CatalogID", detail.CatalogID);
                    ictx.AddParameter("@CatalogQty", detail.CatalogQty);
                    ictx.AddParameter("@CatalogPrice", detail.CatalogPrice);
                    ictx.AddParameter("@ProductID", detail.ProductID);
                    ictx.AddParameter("@ProductPrice", detail.ProductPrice);
                    ictx.AddParameter("@ProductQty", detail.ProductQty);
                    ictx.AddParameter("@CreatedBy", detail.CreatedBy);
                    ictx.AddParameter("@CreatedDate", detail.CreatedDate);
                    ictx.AddParameter("@CatalogPriceDate", detail.CatalogPriceDate);
                    //ictx.AddParameter("@ModifiedDate", detail.ModifiedDate);
                    int result = DBUtil.ExecuteNonQuery(ictx);
                    if (result > 0)
                    {
                        string creator = "system";
                        Purchase purchaseItem = new Purchase();
                        purchaseItem.PurchaseDate = ProccessDate;
                        int index = PurchaseItem.GetLastID();
                        purchaseItem.PurchaseNo = string.Format("{0}-{1}", string.Format("{0:yyyyMMddHHmmss}", purchaseItem.PurchaseDate), index);
                        purchaseItem.TotalQty = detail.ProductQty;
                        purchaseItem.TotalPrice = detail.ProductQty * detail.ProductPrice;
                        purchaseItem.Notes = string.Format("{0:N2}({1})", detail.ProductQty, detail.ProductUnit);
                        purchaseItem.CreatedBy = creator;
                        purchaseItem.CreatedDate = detail.CreatedDate;
                        List<PurchaseDetail> pDetail = new List<PurchaseDetail>();
                        pDetail.Add(new PurchaseDetail()
                        {
                            CatalogID = detail.ProductID,
                            PricePerUnit = detail.ProductPrice,
                            Qty = detail.ProductQty,
                            PurchaseNo = purchaseItem.PurchaseNo,
                            Unit = detail.ProductUnit,
                            TotalPrice = detail.ProductQty * detail.ProductPrice,
                            CreatedBy = creator,
                            CreatedDate = detail.CreatedDate
                        });
                        purchaseItem.Details = pDetail;

                        PurchaseItem.SaveStock(purchaseItem);

                        //int saleIndex = SaleItem.GetNewIndex(ProccessDate);
                        string TransNo = string.Format("{0:yyyyMMddHHmmssffff}{1}", DateTime.Now, reconcile.ID); //string.Format((saleIndex <= 1000) ? "{0:ddMMyyyy}{1:000}" : "{0:ddMMyyyy}{1:0000}", ProccessDate, saleIndex);
                        Sale saleItem = new Sale();
                        saleItem.TransactionID = TransNo;
                        saleItem.TransactionDate = ProccessDate;

                        List<SaleDetail> saleDetails = new List<SaleDetail>();
                        SaleDetail saleDetail = new SaleDetail();

                        saleDetail.Discount = 0;
                        saleDetail.Price = detail.CatalogPrice;
                        saleDetail.Quantity = detail.CatalogQty;
                        saleDetail.TotalPrice = detail.CatalogPrice * detail.CatalogQty;
                        saleDetail.TransactionID = TransNo;
                        saleDetail.CatalogID = detail.CatalogID;
                        saleDetail.Sequence = 1;

                        saleDetail.Unit = detail.CatalogUnit;
                        saleDetails.Add(saleDetail);
                        //saleItem.MemberID = 0;
                        saleItem.TotalQty = string.Format("{0:N2}({1})", detail.CatalogQty, detail.CatalogUnit);
                        //saleItem.PaymentType = paymentTipe;
                        saleItem.Tax = 0;
                        //saleItem.Terminal = Utilities.GetComputerName();
                        saleItem.Username = creator;
                        saleItem.TransactionDate = ProccessDate;
                        saleItem.TotalPrice = saleDetail.TotalPrice;
                        saleItem.TotalPaymentReturn = 0;
                        saleItem.TotalPayment = saleDetail.TotalPrice;

                        saleItem.Details = saleDetails;
                        SaleItem.Insert(saleItem);

                        sql = @"

UPDATE reconcile 
	SET purchaseno = @purchaseno , 
	transactionid = @transactionid	
	WHERE ID = @ID ;
";
                        ictx.CommandText = sql;
                        ictx.CommandType = CommandType.Text;
                        ictx.AddParameter("@transactionid", saleItem.TransactionID);
                        ictx.AddParameter("@purchaseno", purchaseItem.PurchaseNo);
                        ictx.AddParameter("@ID", reconcile.ID);
                        DBUtil.ExecuteNonQuery(ictx);
                        ictx.CommitTransaction();
                    }
                }
            }
            catch (Exception)
            {
                //reconcile = -1;
                ictx.RollbackTransaction();
            }
            return reconcile;
        }

        public static int Delete(string ReconcileID)
        {
            int result = -1;
            try
            {

                IDBHelper ictx = new DBHelper();
                string sql = @"

DELETE FROM purchasedetail
WHERE PurchaseNo IN (SELECT purchaseno FROM reconcile WHERE ID=@ReconcileID);

DELETE FROM purchase
WHERE PurchaseNo IN (SELECT purchaseno FROM reconcile WHERE ID=@ReconcileID);

DELETE FROM saledetail
WHERE TransactionID IN (SELECT transactionid FROM reconcile WHERE ID=@ReconcileID);

DELETE FROM sale
WHERE TransactionID IN (SELECT transactionid FROM reconcile WHERE ID=@ReconcileID);

DELETE FROM reconciledetail WHERE ReconcileID =@ReconcileID;

DELETE FROM reconcile WHERE ID =@ReconcileID;
";

                ictx.CommandText = sql;
                ictx.CommandType = CommandType.Text;
                ictx.AddParameter("@ReconcileID", ReconcileID);
                DBUtil.ExecuteNonQuery(ictx);
                result = 1;
            }
            catch (Exception)
            {

                result = -1;
            }

            return result;
        }
    }
}
