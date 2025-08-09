using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{

    public class Customer : IDataMapper<Customer>
    {
        public Customer() { }
        public Customer(int id, string fullname)
        {
            this.ID = id;
            this.FullName = fullname;
        }


        public int ID { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
        public string Sales { get; set; }
        public string PaymentType { get; set; }
        public string PaymentTypeDesc { get; set; }
        public Customer Map(System.Data.IDataReader reader)
        {
            Customer obj = new Customer();
            obj.ID = Convert.ToInt32(reader["ID"]);
            obj.FullName = reader["FullName"].ToString();
            obj.Address = reader["Address"].ToString();
            obj.Phone = reader["Phone"].ToString();
            obj.Sales = reader["Sales"].ToString();
            obj.PaymentType = reader["PaymentType"].ToString();
            obj.PaymentTypeDesc = reader["PaymentTypeDesc"].ToString();

            return obj;
        }
    }

}
