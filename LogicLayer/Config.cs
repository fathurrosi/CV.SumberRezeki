using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicLayer
{
    public class Config
    {

        //<add key="X_No" value="7"/>
        //<add key="X_Item" value="35"/>
        //<add key="X_Colly" value="570"/>
        //<add key="X_Qty" value="635"/>
        //<add key="X_Harga" value="700"/>
        //<add key="X_TotalHarga" value="800"/>

        public static string GetCommaSeparator()
        {
            return Get("CommaSeparator");
        }

        public static string GetThousandSeparator()
        {
            return Get("ThousandSeparator");
        }

        public static string GetSuperAdmin()
        {
            return "superadmin";
        }

        public static bool GetIsUsingTemplateFromat()
        {
            int _temp = 0;
            int.TryParse(Get("TemplateFromat"), out _temp);
            return (_temp == 1) ? true : false;
        }
        public static float GetX_TotalHarga()
        {
            float _temp = 0;
            float.TryParse(Get("X_TotalHarga"), out _temp);
            return _temp;
        }
        public static float GetX_Harga()
        {
            float _temp = 0;
            float.TryParse(Get("X_Harga"), out _temp);
            return _temp;
        }
        public static float GetX_Qty()
        {
            float _temp = 0;
            float.TryParse(Get("X_Qty"), out _temp);
            return _temp;
        }
        public static float GetX_Colly()
        {
            float _temp = 0;
            float.TryParse(Get("X_Colly"), out _temp);
            return _temp;
        }
        public static float GetX_Item()
        {
            float _temp = 0;
            float.TryParse(Get("X_Item"), out _temp);
            return _temp;
        }
        public static float GetX_No()
        {
            float _temp = 0;
            float.TryParse(Get("X_No"), out _temp);
            return _temp;
        }

        public static object[] GetCatalogUnit()
        {
            return Get("CatalogUnit").Split(',');
        }

        public static float TopMargin()
        {
            float TopMargin = 0;
            float.TryParse(Get("TopMargin"), out TopMargin);
            return TopMargin;
        }

        public static int GetPageSize()
        {
            int pageSize = 0;
            int.TryParse(Get("PageSize"), out pageSize);
            return pageSize;
        }

        private static string Get(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(key);
        }
    }
}
