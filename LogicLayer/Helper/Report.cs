
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogicLayer.Helper
{
    public class Report : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            //width : 8.27in
            //height : 11.69in
            //left / right : 0.49213in
            //buttom/ top: 0.59055in
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>5.51in</PageHeight>
                <MarginTop>0.1in</MarginTop>
                <MarginLeft>0.3in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0.1in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
                       
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            //int left = ev.PageBounds.Left;// -(int)ev.PageSettings.HardMarginX;
            //int top = ev.PageBounds.Top;// -(int)ev.PageSettings.HardMarginY;
            //int width = ev.PageBounds.Width;
            //int height = ev.PageBounds.Height;


            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //Rectangle adjustedRect = new Rectangle(left, top, width, height);
            //ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
            //ev.Graphics.DrawImage(pageImage, adjustedRect);
            ev.Graphics.DrawLine(Pens.Black, 10, 10, 600, 10);

            // Create string to draw.
            String drawString = Convert.ToDecimal(23400000).ToString("n2");

            // Create font and brush.
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create rectangle for drawing.
            float x = 150.0F;
            float y = 150.0F;
            float width = 200.0F;
            float height = 50.0F;
            RectangleF drawRect = new RectangleF(x, y, width, height);

            //ev.Graphics.DrawString(Convert.ToDecimal(23400000).ToString("n2"),new Font(Font.
            ev.Graphics.DrawString(drawString, drawFont, drawBrush, drawRect);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            //int top = printDoc.DefaultPageSettings.Margins.Top;
            //printDoc.DefaultPageSettings.Margins.Top = 10;
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        //    Create a local report for Customer.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Print(string reportPath, string reportName, object dataSource, List<ReportParameter> Params = null)
        {
            LocalReport report = new LocalReport();
            report.ReportPath = reportPath;

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataSource;
            ReportDataSource rptSource = new ReportDataSource(reportName, bindingSource);
            if (Params != null)
                report.SetParameters(Params);
            report.DataSources.Add(rptSource);

            Export(report);
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    }
}
