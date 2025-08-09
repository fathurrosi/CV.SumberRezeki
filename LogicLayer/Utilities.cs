using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using DataObject;
using System.Text.RegularExpressions;

namespace LogicLayer
{
    public class MonthHelper
    {
        public MonthHelper(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class Utilities
    {

        public static List<Privilege> PrivilegeList { get; set; }

        public static List<MonthHelper> GetAllMonth()
        {
            string[] months = new string[] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };

            List<MonthHelper> list = new List<MonthHelper>();
            for (int i = 1; i <= months.Length; i++)
            {
                list.Add(new MonthHelper(i, months[i - 1]));
            }
            return list;
        }
        /// <summary>
        /// input format 10,000.32
        /// output format 10000,32
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToString(decimal d)
        {
            return d.ToString(FORMAT_Money);
            //return sTemp.Replace(",", "");
        }

        public static string ToString(decimal d, string format)
        {
            return d.ToString(format);
            //return sTemp.Replace(",", "");
        }

        public static decimal ToDecimal(string p)
        {
            //string sTemp = p.Replace(",", ".");
            decimal dTemp = 0;
            decimal.TryParse(p, out dTemp);
            return dTemp;
        }
        public static string ToIndonesiaFormatStrukBesar(DateTime date)
        {
            string[] day = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jum\'at", "Sabtu" };
            string[] month = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember" };

            string today = day[(int)date.DayOfWeek];
            string this_month = month[date.Month - 1];
            string full_date = string.Format("{0}, {1} {2} {3}", "Jakarta", date.Day, this_month, date.Year);
            return full_date;
        }
        public static string ToIndonesiaFormat(DateTime date)
        {
            string[] day = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jum\'at", "Sabtu" };
            string[] month = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember" };

            string today = day[(int)date.DayOfWeek];
            string this_month = month[date.Month - 1];
            string full_date = string.Format("{0}, {1} {2} {3}", today, date.Day, this_month, date.Year);
            return full_date;
        }
        //public static bool IsValidNumberWithComma(object sender, char keyChar)
        //{
        //    if ((Keys)keyChar == Keys.Back)
        //        return true;
        //    else if (char.IsNumber(keyChar))
        //        return true;
        //    else if (string.Format("{0}", keyChar) == ".")
        //    {
        //        TextBox tb = (TextBox)sender;
        //        string[] temps = tb.Text.Split('.');
        //        //hanya boleh satu tanda titik
        //        if (temps.Length > 1) return false;
        //        return true;
        //    }

        //    return false;
        //}

        //public static bool IsValidNumber(char keyChar)
        //{
        //    if ((Keys)keyChar == Keys.Back)
        //        return true;
        //    else if (char.IsNumber(keyChar))
        //        return true;
        //    //else if (string.Format("{0}", keyChar) == ",") return true;

        //    return false;
        //}
        public static string ThousandSeparator
        {
            get
            {

                return Config.GetThousandSeparator();
            }
        }

        public static string CommaSeparator
        {
            get
            {

                return Config.GetCommaSeparator();
            }
        }
        public static bool IsValidNumberWithComma(object sender, char keyChar)
        {
            string separator = Utilities.CommaSeparator;
            if ((Keys)keyChar == Keys.Back)
                return true;
            else if (char.IsNumber(keyChar))
                return true;
            else if (string.Format("{0}", keyChar) == separator)
            {
                TextBox tb = (TextBox)sender;
                string[] temps = tb.Text.Split(char.Parse(separator));
                //hanya boleh satu tanda titik
                if (temps.Length > 1) return false;
                return true;
            }

            return false;
        }

        public static bool IsValidNumber(char keyChar)
        {
            if ((Keys)keyChar == Keys.Back)
                return true;
            else if (char.IsNumber(keyChar))
                return true;
            //else if (string.Format("{0}", keyChar) == ",") return true;

            return false;
        }

        public static DialogResult ShowInformation(string text)
        {
            return MessageBox.Show(text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static DialogResult ShowValidation(string text)
        {
            return MessageBox.Show(text, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public const string FORMAT_Date = "dd MMM yyyy";
        public const string FORMAT_DateTime = "dd MMM yyyy HH:mm:ss";
        public const string FORMAT_DateTime_Flat = "yyyyMMddHHmmss";
        public const string FORMAT_Date_Flat = "ddMMyyyy";
        public const string FORMAT_Money = "N0";

        public static string FormatToMoney(decimal money)
        {
            //id-ID
            return money.ToString(Utilities.FORMAT_Money, CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static bool IsSuperAdmin()
        {
            List<Role> roleList = Utilities.CurrentUser.Roles;
            return roleList.Where(t => string.Format("{0}", t.Name).Replace(" ", "").ToLower() == Config.GetSuperAdmin()).Count() > 0;
        }

        public static Image GetImage(Stream stream)
        {
            return Image.FromStream(stream);
        }

        public static byte[] GetBytes(Image image)
        {
            return (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));
        }

        public static byte[] StreamToBytes(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static Image BytesToImage(byte[] bytes)
        {
            if (bytes == null) return null;
            MemoryStream ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        public static Bitmap Resize(byte[] bytes, Size size)
        {
            int newWidth;
            int newHeight;
            Image img = BytesToImage(bytes);
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            float percentWidth = (float)size.Width / (float)originalWidth;
            float percentHeight = (float)size.Height / (float)originalHeight;
            float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
            newWidth = (int)(originalWidth * percent);
            newHeight = (int)(originalHeight * percent);

            return new Bitmap(img, newWidth, newHeight);
        }

        public static Bitmap Resize(Stream stream, int width, int height)
        {
            int newWidth;
            int newHeight;
            Image img = Image.FromStream(stream);
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            float percentWidth = (float)width / (float)originalWidth;
            float percentHeight = (float)height / (float)originalHeight;
            float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
            newWidth = (int)(originalWidth * percent);
            newHeight = (int)(originalHeight * percent);

            return new Bitmap(img, newWidth, newHeight);
        }
        public static string CorrectFormat(string textValue)
        {
            //string[] temps = textValue.Split('.');
            //string temp = string.Empty;
            //if (temps.Length == 1)
            //    temp = temps[0];
            //else if (temps.Length > 1)
            //    temp = string.Format("{0},{1}", temps[0], Utilities.Crop(temps[1], 2));

            decimal decTemp = 0;
            decimal.TryParse(textValue, out decTemp);

            return ToString(decTemp);
        }

        public static string CorrectFormat(string textValue, string format)
        {
            //string[] temps = textValue.Split('.');
            //string temp = string.Empty;
            //if (temps.Length == 1)
            //    temp = temps[0];
            //else if (temps.Length > 1)
            //    temp = string.Format("{0},{1}", temps[0], Utilities.Crop(temps[1], 2));

            decimal decTemp = 0;
            decimal.TryParse(textValue, out decTemp);

            return ToString(decTemp, format);
        }

        public static string RawNumberFormat(string textValue)
        {
            //textValue = Regex.Replace(textValue, "^[0-9]", string.Empty);

            textValue = textValue.Replace(Utilities.ThousandSeparator, string.Empty);
            return textValue;
        }
        //public static string RawNumberFormat(string textValue)
        //{
        //    //textValue = Regex.Replace(textValue, "^[0-9]", string.Empty);

        //    textValue = textValue.Replace(",", string.Empty);
        //    return textValue;
        //}
        public static string Crop(string text, int length)
        {
            if (text == null) text = string.Empty;
            if (text.Length > length)
            {
                text = text.Substring(0, length);
            }
            return text;
        }

        public static string GetComputerName()
        {
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return hostEntry.HostName;
        }

        public static string GetIpAddress()
        {
            string IpAddress = string.Empty;
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in hostEntry.AddressList)
            {
                if (item.AddressFamily.ToString() == "InterNetwork")
                {
                    IpAddress = item.ToString();
                }
            }

            return Utilities.Crop(IpAddress, 15);
        }


        public static string Username { get; set; }
        public static DataObject.User CurrentUser { get; set; }
    }
}
