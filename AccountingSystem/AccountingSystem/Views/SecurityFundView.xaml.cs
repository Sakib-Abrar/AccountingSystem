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
    public partial class SecurityFundView : Page
    {
        public SecurityFundView()
        {
            InitializeComponent();
            DataContext = new SecurityFund();
            SecurityFund data = new SecurityFund();
            securityFund.ItemsSource = data.GetData();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private double last_remains()
        {
            Connection conn = new Connection();
            double remains = 0.00;
            string query = "SELECT TOP 1 * FROM SecurityFund ORDER BY Security_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Security_Remains"];
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
            if (CheckForError(Details)|| CheckForError(Deposit)||CheckForError(Expenses)) {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [SecurityFund] (Security_Date, Security_Details, Security_Deposit, Security_Expenses, Security_Remains) VALUES (@Date, @Details, @Deposit, @Expenses, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", Login.GlobalDate);
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
