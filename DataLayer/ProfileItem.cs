using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
using DataObject;

namespace DataLayer
{
    public class ProfileItem
    {
        public static int Update(Profile item)
        {
            Profile existing = GetByID(item.id);
            string query = @"

INSERT INTO profile 
	(Alamat, Alamat2
	Telp1, 
	Telp2, 
	Nama, 
	Keterangan, 
	web, 
	email, logo,logo_extension,label_alamat, label_alamat2,instagram, 
	facebook, 
	tweeter
	)
	VALUES
	(
    @Alamat, @Alamat2
	@Telp1, 
	@Telp2, 
	@Nama, 
	@Keterangan, 
	@web, 
	@email, @logo,@logo_extension,@label_alamat,@label_alamat2,@instagram, 
	@facebook, 
	@tweeter
	
	);

";
            if (existing != null)
            {
                query = @"
UPDATE profile 
	SET
	Alamat = @Alamat , Alamat2= @Alamat2 , 
	Telp1 = @Telp1 , 
	Telp2 = @Telp2 , 
	Nama = @Nama , 
	Keterangan = @Keterangan , 
	web = @web , 
	email = @email , logo = @logo, logo_extension=@logo_extension,
    label_alamat=@label_alamat,label_alamat2=@label_alamat2,instagram=@instagram, 
	facebook=@facebook, 
	tweeter=@tweeter
    WHERE
	id = @id ;
            ";
            }

            IDBHelper context = new DBHelper();
            context.CommandText = query;
            context.CommandType = CommandType.Text;
            context.AddParameter("@Alamat", item.Alamat);
            context.AddParameter("@Alamat2", item.Alamat2);
            context.AddParameter("@label_alamat", item.LabelAlamat);
            context.AddParameter("@label_alamat2", item.LabelAlamat2);
            context.AddParameter("@Telp1", item.Telp1);
            context.AddParameter("@Telp2", item.Telp2);
            context.AddParameter("@Nama", item.Nama);
            context.AddParameter("@Keterangan", item.Keterangan);
            context.AddParameter("@web", item.web);
            context.AddParameter("@email", item.email);
            context.AddParameter("@id", item.id);
            context.AddParameter("@logo", item.Logo);
            context.AddParameter("@instagram", item.instagram);
            context.AddParameter("@facebook", item.facebook);
            context.AddParameter("@tweeter", item.tweeter);
            context.AddParameter("@logo_extension", item.LogoExtension);
            return DBUtil.ExecuteNonQuery(context);
        }


        public static Profile GetByID(int id)
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
select * from profile WHERE
	id = @id ;
            ";
            context.CommandType = CommandType.Text;
            context.AddParameter("@id", id);
            return DBUtil.ExecuteMapper<Profile>(context, new Profile()).FirstOrDefault();
        }

        public static Profile GetProfile()
        {
            IDBHelper context = new DBHelper();
            context.CommandText = @"
select * from profile 
order by id asc
limit 1 ;
            ";
            context.CommandType = CommandType.Text;
            return DBUtil.ExecuteMapper<Profile>(context, new Profile()).FirstOrDefault();
        }
    }
}
