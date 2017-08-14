using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;
using System.Windows.Data;
namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for SecurityFundView.xaml
    /// </summary>
    public partial class ShareableProfitView : Page
    {
        public ShareableProfitView()
        {
            InitializeComponent();
            DataContext = new ShareableProfit();
            ShareableProfit data = new ShareableProfit();
            shareableProfit.ItemsSource = data.GetData();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private double last_remains()
        {
            Connection conn = new Connection();
            double remains = 0.00;
            string query = "SELECT TOP 1 * FROM ShareableProfit ORDER BY Shareable_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Shareable_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }
        private bool CheckForError(TextBox Selected)
        {
            BindingExpression Trigger = Selected.GetBindingExpression(TextBox.TextProperty);
            Trigger.UpdateSource();
            if (Validation.GetHasError(Selected) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Deposit) || CheckForError(Expenses))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double previous = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [ShareableProfit] (Shareable_Date, Shareable_Previous, Shareable_Deposit, Shareable_Expenses, Shareable_Remains) VALUES (@Date, @Previous, @Deposit, @Expenses, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Previous", previous);
                CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                CmdSql.Parameters.AddWithValue("@Remains", previous + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            ShareableProfit data = new ShareableProfit();
            shareableProfit.ItemsSource = data.GetData();
        }
    }
}
