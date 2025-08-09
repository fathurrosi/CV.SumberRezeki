using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class CatalogPriceItem
    {



        public static CatalogPrice GetByCatalogID(int catalogID)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
SELECT  * FROM CatalogPrice
  WHERE CatalogID=@CatalogID
   LIMIT 1
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@CatalogID", catalogID);
            List<CatalogPrice> result = DBUtil.ExecuteMapper<CatalogPrice>(context, new CatalogPrice());

            return result.FirstOrDefault();
        }


        public static int GetRecordCount(string supplierCode, string text)
        {
            int result = 0;
            IDBHelper context = new DBHelper();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("  SELECT cp.*, s.Name AS SupplierName, c.Name AS CatalogName FROM catalogprice cp ");
            sBuilder.AppendLine("  LEFT JOIN Supplier s ON s.Code = cp.SupplierCode ");
            sBuilder.AppendLine("  INNER JOIN Catalog c ON c.ID = cp.CatalogID ");
            sBuilder.AppendLine("  WHERE (c.Name LIKE concat ('%', @text ,'%') ");
            sBuilder.AppendLine("  OR s.Name LIKE concat ('%', @text ,'%') ) ");

            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);

            if (!string.IsNullOrEmpty(supplierCode))
            {
                context.AddParameter("@SupplierCode", supplierCode);
                sBuilder.AppendLine(" AND  SupplierCode=@SupplierCode ");
            }

            context.CommandText = sBuilder.ToString();
            object obj = DBUtil.ExecuteScalar(context);
            if (obj != null)
                int.TryParse(obj.ToString(), out result);
            return result;
        }


        public static List<CstmCatalogPrice> GetBySupplierCode(string supplierCode, string text, int offset, int pageSize)
        {
            IDBHelper context = new DBHelper();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("  SELECT cp.*, s.Name AS SupplierName, c.Name AS CatalogName FROM catalogprice cp ");
            sBuilder.AppendLine("  LEFT JOIN Supplier s ON s.Code = cp.SupplierCode ");
            sBuilder.AppendLine("  INNER JOIN Catalog c ON c.ID = cp.CatalogID ");
            sBuilder.AppendLine("  WHERE (c.Name LIKE concat ('%', @text ,'%') ");
            sBuilder.AppendLine("  OR s.Name LIKE concat ('%', @text ,'%') ) ");


            context.CommandType = CommandType.Text;
            context.AddParameter("@text", text);
            context.AddParameter("@pageSize", pageSize);
            context.AddParameter("@offset", offset);

            if (!string.IsNullOrEmpty(supplierCode))
            {
                context.AddParameter("@SupplierCode", supplierCode);
                sBuilder.AppendLine(" AND  SupplierCode=@SupplierCode ");
            }

            sBuilder.AppendLine("  ORDER BY PriceDate DESC ,CatalogID ASC");
            sBuilder.AppendLine("  LIMIT  @pageSize OFFSET @offset ");
            context.CommandText = sBuilder.ToString();
            List<CstmCatalogPrice> result = DBUtil.ExecuteMapper<CstmCatalogPrice>(context, new CstmCatalogPrice());

            return result;
        }

        public static List<CstmCatalogPrice> GetAll()
        {
            IDBHelper context = new DBHelper();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("  SELECT cp.*, s.Name AS SupplierName, c.Name AS CatalogName FROM catalogprice cp ");
            sBuilder.AppendLine("  LEFT JOIN Supplier s ON s.Code = cp.SupplierCode ");
            sBuilder.AppendLine("  INNER JOIN Catalog c ON c.ID = cp.CatalogID ");
            sBuilder.AppendLine("  ORDER BY PriceDate DESC ");

            context.CommandType = CommandType.Text;
            context.CommandText = sBuilder.ToString();
            List<CstmCatalogPrice> result = DBUtil.ExecuteMapper<CstmCatalogPrice>(context, new CstmCatalogPrice());

            return result;
        }
    }
}
