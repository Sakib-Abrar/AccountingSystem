using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;

namespace AccountingSystem.Models
{
    class SecurityFund
    {
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public double Deposit { get; set; }
        public double Expenses { get; set; }
        public double Remains { get; set; }

        public List<SecurityFund> LoadCollectionData()
        {
            List<SecurityFund> entry = new List<SecurityFund>();
            entry.Add(new SecurityFund()
            {
                Date = new DateTime(2017, 2, 23),
                Details = "Kuddus",
                Deposit = 123,
                Expenses = 321,
                Remains = 456,
            });
            return entry;
        }

        public List<SecurityFund> GetData()
        {
            // SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Dotnet_project\Test\Test\complete.mdf;Integrated Security=True;");
            // conn.Open();
            Connection conn = new Connection();
            conn.OpenConection();
            List<SecurityFund> entries = new List<SecurityFund>();
            //  SqlCommand cmd = new SqlCommand("SELECT * From SecurityFund", conn);
            // SqlDataReader reader = cmd.ExecuteReader();
            string query = "SELECT * From SecurityFund";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                SecurityFund entry = new SecurityFund();
                entry.Date = (DateTime)reader["Security_Date"];
                entry.Details = (string)reader["Security_Details"];
                entry.Deposit = (double)reader["Security_Deposit"];
                entry.Expenses = (double)reader["Security_Expenses"];
                entry.Remains = (double)reader["Security_Remains"];
                entries.Add(entry);
            }
            // conn.Close();
            conn.CloseConnection();
            return entries;
        }
    }
}
