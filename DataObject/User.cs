using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObject
{


    public class User : IDataMapper<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool? IsLogin { get; set; }

        public string IPAddress { get; set; }

        public string MachineName { get; set; }

        public bool? IsActive { get; set; }
        public List<Role> Roles { get; set; }

        public string RoleName
        {
            get
            {
                if (Roles != null)
                {
                    return string.Join(", ", Roles.Select(t => t.Name).ToArray());
                }
                return string.Empty;
            }
        }

        public User Map(System.Data.IDataReader reader)
        {
            User obj = new User();
            obj.Username = reader["Username"].ToString();
            obj.Password = reader["Password"].ToString();
            obj.LastLogin = (reader["LastLogin"] is System.DBNull) ? (DateTime?)null : Convert.ToDateTime(reader["LastLogin"]);
            obj.IsLogin = (reader["IsLogin"] is System.DBNull) ? false : Convert.ToBoolean(reader["IsLogin"]);
            obj.IPAddress = reader["IPAddress"].ToString();
            obj.MachineName = reader["MachineName"].ToString();
            obj.IsActive = (reader["IsActive"] is System.DBNull) ? false : Convert.ToBoolean(reader["IsActive"]);

            return obj;
        }
    }
}
