using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using DataObject;


namespace LogicLayer.Helper
{
    public class Excel
    {
        public static IFont GetFontNormal(HSSFWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.FontName = "Carlito";
            font.FontHeightInPoints = (short)8.5;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

            return font;
        }

        public static IFont GetFontBold(HSSFWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.FontName = "Carlito";
            font.FontHeightInPoints = (short)8.5;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            return font;
        }

        public static HSSFCellStyle GetHeaderDecimalCellStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle decimalCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            //decimalCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("N2");
            decimalCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,###,###,##0.00");
            decimalCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            decimalCellStyle.FillForegroundColor = IndexedColors.LightGreen.Index; //IndexedColors.Grey25Percent.Index;
            decimalCellStyle.FillPattern = FillPattern.SolidForeground;
            decimalCellStyle.SetFont(GetFontBold(workbook));

            return decimalCellStyle;
        }

        public static HSSFCellStyle GetHeaderCellStyle(HSSFWorkbook workbook)
        {
            // create bordered cell style
            HSSFCellStyle headerCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            headerCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            headerCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            headerCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            headerCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            headerCellStyle.FillForegroundColor = IndexedColors.LightGreen.Index; //IndexedColors.Grey25Percent.Index;
            headerCellStyle.FillPattern = FillPattern.SolidForeground;
            headerCellStyle.SetFont(GetFontBold(workbook));

            return headerCellStyle;
        }

        public static HSSFCellStyle GetHeaderCenterCellStyle(HSSFWorkbook workbook)
        {            // create bordered cell style
            HSSFCellStyle stringCenterCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            stringCenterCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCenterCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCenterCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCenterCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCenterCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stringCenterCellStyle.SetFont(GetFontBold(workbook));
            return stringCenterCellStyle;
        }

        public static HSSFCellStyle GetStringCellStyle(HSSFWorkbook workbook)
        {
            HSSFCellStyle stringCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            stringCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            stringCellStyle.SetFont(GetFontNormal(workbook));
            return stringCellStyle;
        }


        public static HSSFCellStyle GetDecimalCellStyle(HSSFWorkbook workbook)
        {
            //1.575.000
            HSSFCellStyle decimalCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            decimalCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,###,###,##0.00");
            decimalCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            decimalCellStyle.SetFont(GetFontNormal(workbook));
            return decimalCellStyle;
        }


        public static void GenerateExcelFile(string fileName, string title, System.Windows.Forms.DataGridView dgv)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();

            var rowIndex = 0;
            int columnIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            var cell = row.CreateCell(columnIndex);
            cell.SetCellValue(title);
            cell.CellStyle = GetHeaderCenterCellStyle(workbook);
            List<string> skipColumn = new List<string>();
            int firstRow, lastRow, firstCol, lastCol = 0;
            firstRow = 0;
            lastRow = 0;
            firstCol = 0;

            ICellStyle headerStyle= GetHeaderCellStyle(workbook);
            rowIndex++;
            var headerRow = sheet.CreateRow(rowIndex);
            for (int i = 1; i < dgv.Columns.Count + 1; i++)
            {
                if (skipColumn.Contains(dgv.Columns[i - 1].HeaderText)) continue;
                if (dgv.Columns[i - 1].Visible)
                {
                    cell = headerRow.CreateCell(columnIndex);
                    cell.CellStyle = headerStyle;
                    cell.SetCellValue(dgv.Columns[i - 1].HeaderText);
                    columnIndex++;
                }
            }

            lastCol = columnIndex - 1;
            var cra = new NPOI.SS.Util.CellRangeAddress(firstRow, lastRow, firstCol, lastCol);
            sheet.AddMergedRegion(cra);


            rowIndex++;

            ICellStyle stringStyle = GetStringCellStyle(workbook);
            ICellStyle decimalStyle = GetDecimalCellStyle(workbook);
            for (int i = 0; i <= dgv.Rows.Count - 1; i++)
            {

                row = sheet.CreateRow(rowIndex);

                columnIndex = 0;
                for (int j = 1; j < dgv.Columns.Count + 1; j++)
                //for (int j = 0; j < columns.Count; j++)
                {
                    if (dgv.Columns[j - 1].Visible)
                    {
                        var o = dgv.Rows[i].Cells[j - 1].Value;
                        cell = row.CreateCell(columnIndex);
                        if (o != null)
                        {
                            if (o.GetType() == typeof(decimal))
                            {
                                cell.CellStyle = decimalStyle;
                                cell.SetCellType(CellType.Numeric);
                                cell.SetCellValue(o == null ? 0 : Convert.ToDouble(o));
                            }
                            else
                            {
                                cell.CellStyle = stringStyle;
                                cell.SetCellValue(string.Format("{0}", o));
                            }
                        }
                        else {
                            cell.CellStyle = stringStyle;
                            //cell.SetCellValue("");
                        }

                        columnIndex++;
                    }

                }
                rowIndex++;
            }

            // Declare one MemoryStream variable for write file in stream  
            var stream = new MemoryStream();
            workbook.Write(stream);

            //string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), fileName);
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            //Write to file using file stream  
            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            stream.WriteTo(file);
            file.Close();
            stream.Close();

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(path);
            }

        }



        public static string WriteExcel(String extension, DataTable dataTable, DateTime date)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            string path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                DataColumn column = dataTable.Columns[j];
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = column.ColumnName.Replace("col", string.Format("{0:MMM}-", date));
                if (columnName.ToLower() == "CatalogID".ToLower()) continue;
                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }


            int rowIndex = 0;
            IRow row;
            foreach (DataRow dr in dataTable.Rows)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                colIndex = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataColumn column = dataTable.Columns[j];
                    ICell cell = row.CreateCell(colIndex);
                    cell.CellStyle = decimalStyle;
                    String columnName = column.ColumnName;
                    if (columnName.ToLower() == "CatalogID".ToLower()) continue;
                    //if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                    //else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                    //else 
                    //if (columnName == "Purchase")
                    //{
                    //    cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                    //    totalPurchase += item.Purchase;
                    //    totalSales += item.Sale;
                    //}
                    //else if (columnName == "Tanggal")
                    //{
                    //    cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                    //}
                    //else if (columnName == "Item")
                    //{
                    cell.SetCellValue(dr[column].ToString());
                    //}
                    colIndex++;
                }
            }

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = string.Format(@"{0}\{1}.xlsx", dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //string path = Path.GetTempFileName();
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                path = fileName;
                workbook.Write(fs);
            }

            return path;

        }

        public static string WriteExcel(String extension, DataTable dataTable)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            string path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                DataColumn column = dataTable.Columns[j];
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = column.ColumnName;
                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }


            int rowIndex = 0;
            IRow row;
            foreach (DataRow dr in dataTable.Rows)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                colIndex = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    DataColumn column = dataTable.Columns[j];
                    ICell cell = row.CreateCell(colIndex);
                    cell.CellStyle = decimalStyle;
                    String columnName = column.ColumnName;
                    //if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                    //else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                    //else if (columnName == "Purchase")
                    //{
                    //    cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                    //    totalPurchase += item.Purchase;
                    //    totalSales += item.Sale;
                    //}
                    //else if (columnName == "Tanggal")
                    //{
                    //    cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                    //}
                    //else if (columnName == "Item")
                    //{
                    cell.SetCellValue(dr[column].ToString());
                    //}
                    colIndex++;
                }
            }

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = string.Format(@"{0}\{1}.xlsx", dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //string path = Path.GetTempFileName();
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                path = fileName;
                workbook.Write(fs);
            }

            return path;

        }

        public static void WriteExcel(String extension, List<DailyGrossProfit> dataList, List<TotalSale> totalSale, out string path)
        {
            // dll refered NPOI.dll and NPOI.OOXML  
            path = string.Empty;
            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            XSSFCellStyle decimalStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            decimalStyle.WrapText = true;
            decimalStyle.Alignment = HorizontalAlignment.Right;
            decimalStyle.VerticalAlignment = VerticalAlignment.Top;
            decimalStyle.BorderBottom = BorderStyle.Thin;
            decimalStyle.BorderTop = BorderStyle.Thin;
            decimalStyle.BorderLeft = BorderStyle.Thin;
            decimalStyle.BorderRight = BorderStyle.Thin;

            XSSFCellStyle headerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            headerStyle.WrapText = true;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = VerticalAlignment.Top;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.FillForegroundColor = IndexedColors.LightCornflowerBlue.Index;

            XSSFCellStyle footerStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            footerStyle.WrapText = true;
            footerStyle.Alignment = HorizontalAlignment.Center;
            footerStyle.VerticalAlignment = VerticalAlignment.Top;
            footerStyle.BorderBottom = BorderStyle.Thin;
            footerStyle.BorderTop = BorderStyle.Thin;
            footerStyle.BorderLeft = BorderStyle.Thin;
            footerStyle.BorderRight = BorderStyle.Thin;
            footerStyle.FillPattern = FillPattern.SolidForeground;
            footerStyle.FillForegroundColor = IndexedColors.LightGreen.Index;

            string[] Columns = new string[] { "Tanggal", "Item", "Quantity", "HPP", "Purchase", "Sales", "Gross Profit" };

            string reportName = "Daily Gross Profit";

            //make a header row  
            IRow row1 = sheet1.CreateRow(0);
            int colIndex = 0;
            for (int j = 0; j < Columns.Count(); j++)
            {
                ICell cell = row1.CreateCell(colIndex);
                sheet1.SetColumnWidth(colIndex, 5000);
                String columnName = Columns[j].ToString();
                if (columnName == "TransDate")
                    columnName = "Tanggal";
                else if (columnName == "CatalogName")
                {
                    columnName = "Item";
                }
                else if (columnName == "Qty")
                    columnName = "Quantity";

                if (columnName == "Unit") continue;

                cell.SetCellValue(columnName);
                cell.CellStyle = headerStyle;
                colIndex++;
            }

            decimal Total_Purchase = 0;
            decimal Total_Sales = 0;
            decimal Total_GP = 0;

            List<DateTime> dateList = dataList.Select(t => t.TransDate).Distinct().ToList();
            int rowIndex = 0;
            int prevTotalRowINdex = 0;
            IRow row;
            foreach (DateTime tanggal in dateList)
            {
                decimal totalSales = 0;
                TotalSale ts = totalSale.Where(t => t.TransDate == tanggal).FirstOrDefault();
                if (ts != null)
                {
                    totalSales = ts.Amount;
                }
                List<DailyGrossProfit> items = dataList.Where(t => t.TransDate == tanggal).ToList();
                //loops through data  
                decimal totalPurchase = 0;

                for (int i = 0; i < items.Count; i++)
                {
                    DailyGrossProfit item = items[i];
                    rowIndex++;
                    row = sheet1.CreateRow(rowIndex);

                    colIndex = 0;
                    for (int j = 0; j < Columns.Count(); j++)
                    {
                        ICell cell = row.CreateCell(colIndex);
                        cell.CellStyle = decimalStyle;
                        String columnName = Columns[j].ToString();
                        if (columnName == "Quantity") cell.SetCellValue(string.Format("{0:N2}{1}", item.Quantity, item.Unit));
                        else if (columnName == "HPP") cell.SetCellValue(string.Format("Rp. {0:N2}", item.HPP));
                        else if (columnName == "Purchase")
                        {
                            cell.SetCellValue(string.Format("Rp. {0:N2}", item.Purchase));
                            totalPurchase += item.Purchase;
                            //totalSales += item.Sale;
                        }
                        else if (columnName == "Tanggal")
                        {
                            cell.SetCellValue(string.Format("{0:dd MMM yyyy}", item.TransDate));
                        }
                        else if (columnName == "Item")
                        {
                            cell.SetCellValue(item.Item);
                        }
                        colIndex++;
                    }

                    /*****************************************************************
                     *          MERGE TANGGAL KOLOM PERTAMA
                     * ***************************************************************/
                    if ((rowIndex) < items.Count && string.Format("{0:ddMMyyyy}", item.TransDate) == string.Format("{0:ddMMyyyy}", items[rowIndex].TransDate))
                    {
                        int nextRowIndex = rowIndex + 1;
                        CellRangeAddress crTanggal = new CellRangeAddress(rowIndex, nextRowIndex, 0, 0);
                        sheet1.AddMergedRegion(crTanggal);
                    }
                }

                rowIndex++;
                row = sheet1.CreateRow(rowIndex);

                if (prevTotalRowINdex + 1 != rowIndex - 1)
                {
                    CellRangeAddress crSales = new CellRangeAddress(prevTotalRowINdex + 1, rowIndex - 1, 5, 5);
                    sheet1.AddMergedRegion(crSales);

                    CellRangeAddress crGrossProfit = new CellRangeAddress(prevTotalRowINdex + 1, rowIndex - 1, 6, 6);
                    sheet1.AddMergedRegion(crGrossProfit);
                }

                prevTotalRowINdex = rowIndex;

                colIndex = 0;
                for (int j = 0; j < Columns.Count(); j++)
                {
                    ICell cell1 = row.CreateCell(colIndex);
                    cell1.CellStyle = decimalStyle;
                    String columnName = Columns[j].ToString();
                    if (columnName == "Unit") continue;

                    if (columnName == "Tanggal")
                    {
                        cell1.SetCellValue("Total");
                        cell1.CellStyle = headerStyle;
                    }
                    else if (columnName == "Purchase")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalPurchase));
                        Total_Purchase += totalPurchase;
                    }
                    else if (columnName == "Sales")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalSales));
                        Total_Sales += totalSales;

                    }
                    else if (columnName == "Gross Profit")
                    {
                        cell1.SetCellValue(string.Format("Rp. {0:N2}", totalSales - totalPurchase));
                        Total_GP += totalSales - totalPurchase;
                    }
                    colIndex++;

                }

                CellRangeAddress cra1 = new CellRangeAddress(rowIndex, rowIndex, 0, 3);
                sheet1.AddMergedRegion(cra1);
            }


            for (int i = 0; i < 3; i++)
            {
                rowIndex++;
                row = sheet1.CreateRow(rowIndex);
                colIndex = 0;
                for (int j = 0; j < Columns.Count(); j++)
                {
                    ICell cell1 = row.CreateCell(colIndex);
                    if (i == 0)
                    {
                        cell1.CellStyle = footerStyle;
                    }
                    else
                    {
                        cell1.CellStyle = decimalStyle;
                    }

                    String columnName = Columns[j].ToString();
                    if (columnName == "Tanggal")
                    {
                        cell1.SetCellValue(i == 0 ? "Summary per Month" : "");
                    }
                    else if (columnName == "Purchase")
                    {
                        if (i == 2)
                        {

                            //cell1.CellStyle = footerStyle;
                            cell1.SetCellValue("Percentage");
                        }
                        else cell1.SetCellValue(i == 0 ? "Total Purchase" : string.Format("Rp. {0:N2}", Total_Purchase));
                    }
                    else if (columnName == "Sales")
                    {
                        if (i == 2)
                        {
                            CellRangeAddress cra1 = new CellRangeAddress(rowIndex, rowIndex, 4, 5);
                            sheet1.AddMergedRegion(cra1);
                        }
                        else cell1.SetCellValue(i == 0 ? "Total Sales" : string.Format("Rp. {0:N2}", Total_Sales));
                    }
                    else if (columnName == "Gross Profit")
                    {
                        if (i == 2) cell1.SetCellValue(string.Format("{0:N2}%", Total_GP / Total_Sales));
                        else cell1.SetCellValue(i == 0 ? "Total GP" : string.Format("Rp. {0:N2}", Total_GP));
                    }
                    colIndex++;
                }

                if (i == 2)
                {
                    CellRangeAddress craLast = new CellRangeAddress(rowIndex - 2, rowIndex, 0, 3);
                    sheet1.AddMergedRegion(craLast);
                }

            }



            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = string.Format(@"{0}\{1}.xlsx", dir, DateTime.Now.ToString("ddMMMyyyyHHmmss"));
            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(stream);
            }

            //string path = Path.GetTempFileName();
            using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                path = fileName;
                workbook.Write(fs);
            }

        }

    }
}
