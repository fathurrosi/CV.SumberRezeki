using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;

namespace DataObject
{
    public class Profile : IDataMapper<Profile>
    {

        public string LabelAlamat2 { get; set; }
        public string Alamat2 { get; set; }
        public string LabelAlamat { get; set; }
        public string Alamat { get; set; }
        public string Telp1 { get; set; }
        public string Telp2 { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public string web { get; set; }
        public string email { get; set; }
        public string instagram { get; set; }
        public string facebook { get; set; }
        public string tweeter { get; set; }
        
        public int id { get; set; }
        public byte[] Logo { get; set; }

        public string LogoExtension { get; set; }
        public Profile Map(System.Data.IDataReader reader)
        {
            Profile obj = new Profile();
            obj.id = (reader["id"] is System.DBNull) ? 0 : Convert.ToInt32(reader["id"]);
            obj.Alamat = string.Format("{0}", reader["Alamat"]);
            obj.Alamat2 = string.Format("{0}", reader["Alamat2"]);

            obj.LabelAlamat = string.Format("{0}", reader["label_alamat"]);
            obj.LabelAlamat2 = string.Format("{0}", reader["label_alamat2"]);

            obj.Telp1 = string.Format("{0}", reader["Telp1"]);
            obj.Telp2 = string.Format("{0}", reader["Telp2"]);
            obj.Nama = string.Format("{0}", reader["Nama"]);
            obj.Keterangan = string.Format("{0}", reader["Keterangan"]);
            obj.web = string.Format("{0}", reader["web"]);
            obj.email = string.Format("{0}", reader["email"]);
            obj.instagram = string.Format("{0}", reader["instagram"]);
            obj.facebook = string.Format("{0}", reader["facebook"]);
            obj.tweeter = string.Format("{0}", reader["tweeter"]);
            obj.Logo = (reader["logo"] is System.DBNull) ? null : (byte[])reader["logo"];
            obj.LogoExtension = string.Format("{0}", reader["logo_extension"]);
            return obj;
        }
    }
}

