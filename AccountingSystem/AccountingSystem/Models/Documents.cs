using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.Windows;
using System.IO;

namespace AccountingSystem.Models
{
    class Documents
    {
        public string[] DocumentsName = new string[3];
        public string[] DocumentsAddress = new string[3];
        public int CountExistence { get; private set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentAddress { get; set; }
        public int MemberId { get; set; }
        public string MemberSignature { get; set; }

        public void GetData(int MemID) {
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT m.MemberId, d.DocumentName, d.DocumentAddress FROM Member m LEFT JOIN Documents d  on m.MemberId = d.MemberID WHERE m.MemberId=" +MemID;
            SqlDataReader reader = conn.DataReader(query);
            CountExistence = 0;
            while (reader.Read())
            {
                MemberId = (int)reader["MemberId"];
                if (reader["DocumentAddress"] != System.DBNull.Value)
                {
                    DocumentAddress = (string)reader["DocumentAddress"];
                    DocumentName = (string)reader["DocumentName"];

                    DocumentsName[CountExistence] = DocumentName;
                    DocumentsAddress[CountExistence] = DocumentAddress;
                    CountExistence++;
                }
            }

            conn.CloseConnection();

            conn = new Connection();
            conn.OpenConection();

            query = "SELECT * From Member WHERE MemberId = " + MemID;
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                if (reader["MemberSignature"]!=null)
                MemberSignature = Path.GetFullPath("Images/" + (string)reader["MemberSignature"]);
            }

            conn.CloseConnection();
        }
    }
}
