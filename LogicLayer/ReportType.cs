using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicLayer
{
    public enum ReportType
    {
        TotalSalesPerCustomer = 0,
        TotalSalesPerCatalog = 1,
        SalesPerformancePerMonth = 3,
        MonthlyGrossProfit = 4,
        DailyGrossProfit = 5,
        StockDetail = 6,
        Piutang=7,
        TotalDailySale,
        DailySalesPerCatalog,
        Stock,
        DailyPurchases
    }
}
