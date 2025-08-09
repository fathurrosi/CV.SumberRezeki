using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class SaleReturnDetailItem
    {
        internal static List<SaleReturnDetail> GetByReturnNo(string ReturnNo)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"	
SELECT 
	sd.ID, 
	sd.ReturnNo, 
	sd.Catalog, 
	sd.Price, 
	sd.Discount, 
	sd.Qty, sd.Colly,
	sd.TotalPrice, 
	sd.Sequence, 
	c.Unit,
	sd.Created, 
	sd.CreatedBy, c.Name AS CatalogName FROM s_return_detail sd
LEFT JOIN Catalog c ON c.ID = sd.Catalog
WHERE sd.ReturnNo = @ReturnNo  
";
            context.CommandType = CommandType.Text;
            context.AddParameter("@ReturnNo", ReturnNo);
            return DBUtil.ExecuteMapper<SaleReturnDetail>(context, new SaleReturnDetail());

        }
    }
}
