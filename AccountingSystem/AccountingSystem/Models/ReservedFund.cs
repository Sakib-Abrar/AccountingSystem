using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;

namespace AccountingSystem.Models
{
    class ReservedFund
    {
        public DateTime Date { get; set; }
        public double Current { get; set; }
        public double Total { get; set; }
        public double Previous { get; set; }
        public double Withdraw { get; set; }
        public double Remaining { get; set; }



        public List<ReservedFund> GetData()
        {

            Connection conn = new Connection();
            conn.OpenConection();
            List<ReservedFund> entries = new List<ReservedFund>();

            string query = "SELECT * From ReservedFund";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new ReservedFund()
                {
                    Date = (DateTime)reader["Reserved_Date"],
                    Previous = (double)reader["Reserved_Previous"],
                    Current = (double)reader["Reserved_Current"],
                    Remaining = (double)reader["Reserved_Remaining"],
                    Withdraw = (double)reader["Reserved_Withdraw"],
                    Total = (double)reader["Reserved_Total"],
            });
            }
            conn.CloseConnection();
            return entries;
        }
    }
}
