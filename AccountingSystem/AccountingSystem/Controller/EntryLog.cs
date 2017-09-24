using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Controller
{
    class EntryLog
    {

        public void Add_Entry(string table,string type,int Id,DateTime dateTime,string color)
        {
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [Entry] (Entry_TableID,Entry_Table,Entry_Person,Entry_Date,Entry_Type,Entry_Color) VALUES (@TableID,@Table,@Person,@Date,@Type,@Color )", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@TableID", Id);
                CmdSql.Parameters.AddWithValue("@Table", table);
                CmdSql.Parameters.AddWithValue("@Person", "Aoyan");
                CmdSql.Parameters.AddWithValue("@Date", dateTime);
                CmdSql.Parameters.AddWithValue("@Type", type);
                CmdSql.Parameters.AddWithValue("@Color", color);
                CmdSql.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
