using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AccountingSystem.Controller
{
    class Connection
    {
        //public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Dotnet_project\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";
        //public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shini\Documents\AccountingSystem\AccountingSystem\AccountingSystem\Database\AccountingSystemDatabase.mdf;Integrated Security=True";

        SqlConnection conn;

        public void OpenConection()
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
            }
            catch (SqlException ex)
            {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        OpenConection();
                    }
                    else
                    {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return;
            }
        }
        public void CloseConnection()
        {
            try { 
                conn.Close();
            }
            catch (SqlException ex)
            {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        CloseConnection();
                    }
                    else
                    {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return;
            }
        }
        public void ExecuteQueries(string Query_)
        {
            try {
                SqlCommand cmd = new SqlCommand(Query_, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        ExecuteQueries(Query_);
                    }
                    else
                    {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return;
            }
        }
        public void ExecuteScalar(string Query_)
        {
            try { 
                SqlCommand cmd = new SqlCommand(Query_, conn);
                cmd.ExecuteScalar();
            }
            catch (SqlException ex) {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        ExecuteScalar(Query_);
                    }
                    else
                    {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return;
            }
}


        public SqlDataReader DataReader(string Query_)
        {
            SqlDataReader dr;
            try {
                SqlCommand cmd = new SqlCommand(Query_, conn);
                dr = cmd.ExecuteReader();
                return dr;
            }
            catch (SqlException ex) {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        dr = DataReader(Query_);
                        return dr;
                    }
                    else {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return null;
            }
}


        public object ShowDataInGridView(string Query_)
        {
            object dataum;
            try {
                SqlDataAdapter dr = new SqlDataAdapter(Query_, ConnectionString);
                DataSet ds = new DataSet();
                dr.Fill(ds);
                dataum = ds.Tables[0];
                return dataum;
            }
            catch (SqlException ex) {
                if (MessageBox.Show("We have Encountered a Problem.\nError:" + ex.Message + "\n\nDo you want to retry?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    if (ex.Number == -2)
                    {
                        dataum = ShowDataInGridView(Query_);
                        return dataum;
                    }
                    else
                    {
                        if (MessageBox.Show("The problem is not recognized.Close and try again.\nError:" + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                        Application.Current.Shutdown();
                    }
                }
                return null;
            }
}
    }
}
