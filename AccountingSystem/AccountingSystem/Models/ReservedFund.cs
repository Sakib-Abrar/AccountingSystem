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
                ReservedFund entry = new ReservedFund();
                entry.Date = (DateTime)reader["Reserved_Date"];
                entry.Previous = (double)reader["Reserved_Previous"];
                entry.Current = (double)reader["Reserved_Current"];
                entry.Remaining = (double)reader["Reserved_Remaining"];
                entry.Withdraw = (double)reader["Reserved_Withdraw"];
                entry.Total = (double)reader["Reserved_Total"];
                entries.Add(entry);
            }
            // conn.Close();
            conn.CloseConnection();
            return entries;
        }
    }
}
