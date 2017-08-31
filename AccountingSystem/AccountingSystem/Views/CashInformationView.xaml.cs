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
    public partial class CashInformationView : Page
    {
        public CashInformationView()
        {
            InitializeComponent();
            DataContext = new CashInformation();
            CashInformation data = new CashInformation();
            cashInformation.ItemsSource = data.GetData();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        private double last_remains()
        {
            Connection conn = new Connection();
            double remains = 0.00;
            string query = "SELECT TOP 1 * FROM CashInformation ORDER BY Cash_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Cash_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if ( CheckForError(Deposit) || CheckForError(Expenses))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [CashInformation] (Cash_Date, Cash_Previous, Cash_Deposit, Cash_Expenses, Cash_Remains,Cash_Total) VALUES (@Date, @Previous, @Deposit, @Expenses, @Remains,@Total)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Previous", remains);
                CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                CmdSql.Parameters.AddWithValue("@Total", remains + Convert.ToDouble(Deposit.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            CashInformation data = new CashInformation();
            cashInformation.ItemsSource = data.GetData();
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new SecurityFund().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
