using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class EntryModel
    {
        public string Person { get; set; }
        public string Table { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public int TableId { get; set; }
        public DateTime Date { get; set; }
        public string Color { get; set; }

        #region PopulateTable
        public List<EntryModel> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<EntryModel> entries = new List<EntryModel>();
            string query = "SELECT * From Entry Order By Entry_Id DESC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new EntryModel()
                {
                    ID = (int)reader["Entry_Id"],
                    Date = (DateTime)reader["Entry_Date"],
                    Person = (string)reader["Entry_Person"],
                    Table = (string)reader["Entry_Table"],
                    TableId = (int)reader["Entry_TableId"],
                    Type = (string)reader["Entry_Type"],
                    Color = (string)reader["Entry_Color"],

                });
             
            }
            conn.CloseConnection();
            return entries;
        }
        #endregion
    }
}
