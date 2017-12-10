using AccountingSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    class Loans
    {
        public string[] LoansName = new string[3];
        public int[] LoansAddress = new int[3];
        public int CountExistence { get; private set; }
        public int LoanId { get; set; }
        public string LoanName { get; set; }
        public int MemberId { get; set; }
        public void GetData(int MemID)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT m.MemberId, l.LoanDetails_Id FROM Member m LEFT JOIN LoanDetails l  on m.MemberId = l.LoanDetails_Account WHERE m.MemberId=" + MemID;
            SqlDataReader reader = conn.DataReader(query);
            CountExistence = 0;
            while (reader.Read())
            {
                MemberId = (int)reader["MemberId"];
                if (reader["LoanDetails_Id"] != System.DBNull.Value)
                {
                    LoanId = (int)reader["LoanDetails_Id"];
                    LoanName = "Loan: " + LoanId;

                    LoansName[CountExistence] = LoanName;
                    LoansAddress[CountExistence] = LoanId;
                    CountExistence++;
                }
            }

            conn.CloseConnection();

        }
    }
}
