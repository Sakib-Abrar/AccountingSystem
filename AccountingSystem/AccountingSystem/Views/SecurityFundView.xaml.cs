using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for SecurityFundView.xaml
    /// </summary>
    public partial class SecurityFundView : Page
    {
        public SecurityFundView()
        {
            InitializeComponent();
            SecurityFund data = new SecurityFund();
            securityFund.ItemsSource = data.GetData();
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private double last_remains()
        {
            //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Dotnet_project\Test\Test\complete.mdf;Integrated Security=True;");         
            Connection conn = new Connection();
            double remains = 0.00;
            string query = "SELECT TOP 1 * FROM SecurityFund ORDER BY Security_Id DESC";
            // SqlCommand cmd = new SqlCommand(, conn);
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Security_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [SecurityFund] (Security_Date, Security_Details, Security_Deposit, Security_Expenses, Security_Remains) VALUES (@Date, @Details, @Deposit, @Expenses, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            SecurityFund data = new SecurityFund();
            securityFund.ItemsSource = data.GetData();
        }
    }
}
