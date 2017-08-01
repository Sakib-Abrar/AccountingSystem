using System.Data;
using System.Data.SqlClient;

namespace AccountingSystem.Controller
{
    class Connection
    {
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shini\Documents\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";
        SqlConnection conn;

        public void OpenConection()
        {
            conn = new SqlConnection(ConnectionString);
            conn.Open();
        }
        public void CloseConnection()
        {
            conn.Close();
        }
        public void ExecuteQueries(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, conn);
            cmd.ExecuteNonQuery();
        }
        public void ExecuteScalar(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, conn);
            cmd.ExecuteScalar();
        }


        public SqlDataReader DataReader(string Query_)
        {
            SqlCommand cmd = new SqlCommand(Query_, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }


        public object ShowDataInGridView(string Query_)
        {
            SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
            DataSet ds = new DataSet();
            dr.Fill(ds);
            object dataum = ds.Tables[0];
            return dataum;
        }
    }
}
