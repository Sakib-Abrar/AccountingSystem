using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AccountingSystem.Controller
{
    class Connection
    {
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Dotnet_project\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";
        //public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shini\Documents\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";

        SqlConnection conn;

        public void OpenConection()
        {
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
            }
            catch (SqlException ex) {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\nError:" + ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
        public void CloseConnection()
        {
            try { 
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\n\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void ExecuteQueries(string Query_)
        {
            try {
                SqlCommand cmd = new SqlCommand(Query_, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\n\nError:" + ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
}
        public void ExecuteScalar(string Query_)
        {
            try { 
                SqlCommand cmd = new SqlCommand(Query_, conn);
                cmd.ExecuteScalar();
            }
            catch (SqlException ex) {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\n\nError:"+ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
}


        public SqlDataReader DataReader(string Query_)
        {
            try {
                SqlCommand cmd = new SqlCommand(Query_, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch (SqlException ex) {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\n\nError:"+ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return null;
            }
}


        public object ShowDataInGridView(string Query_)
        {
            try {
                SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
                DataSet ds = new DataSet();
                dr.Fill(ds);
                object dataum = ds.Tables[0];
                return dataum;
            }
            catch (SqlException ex) {
                MessageBox.Show("We have Encountered a Problem.Please Try Again.\n\nError:"+ex.Message,"Warning",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return null;
            }
}
    }
}
