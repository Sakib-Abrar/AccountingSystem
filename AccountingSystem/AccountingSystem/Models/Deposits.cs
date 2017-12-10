using AccountingSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    class Deposits
    {
        public string[] DepositsName = new string[3];
        public int[] DepositsAddress = new int[3];
        public int CountExistence { get; private set; }
        public int DepositId { get; set; }
        public string DepositName { get; set; }
        public string DepositAddress { get; set; }
        public int MemberId { get; set; }
        public void GetData(int MemID)
        {
            DepositsAddress[0] = 0;
            DepositsAddress[1] = 0;
            DepositsAddress[2] = 0;

            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM GeneralDepositLedger WHERE MemberId=" + MemID;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                MemberId = (int)reader["MemberId"];
                if (reader["GeneralId"] != System.DBNull.Value)
                {
                    DepositId = (int)reader["GeneralId"];
                    DepositName = " " + DepositId;

                    DepositsName[0] = DepositName;
                    DepositsAddress[0] = DepositId;
                }
            }
            conn.CloseConnection();
            conn.OpenConection();
            query = "SELECT * FROM FixedDepositLedger WHERE MemberId=" + MemID;
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                MemberId = (int)reader["MemberId"];
                if (reader["FixedId"] != System.DBNull.Value)
                {
                    DepositId = (int)reader["FixedId"];
                    DepositName = " " + DepositId;

                    DepositsName[1] = DepositName;
                    DepositsAddress[1] = DepositId;
                }
            }
            conn.CloseConnection();
            conn.OpenConection();

            query = "SELECT * FROM MonthlyDepositLedger WHERE MemberId=" + MemID;
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                MemberId = (int)reader["MemberId"];
                if (reader["MonthlyId"] != System.DBNull.Value)
                {
                    DepositId = (int)reader["MonthlyId"];
                    DepositName = " " + DepositId;

                    DepositsName[1] = DepositName;
                    DepositsAddress[1] = DepositId;
                }
            }

            conn.CloseConnection();

        }
    }
}
