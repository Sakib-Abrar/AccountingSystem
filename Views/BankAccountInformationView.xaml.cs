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
    /// Interaction logic for BankAccountInformationView.xaml
    /// </summary>
    public partial class BankAccountInformationView : Page
    {
        public BankAccountInformationView()
        {
            InitializeComponent();
            DataContext = new BankAccountInformation();
            BankAccountInformation data = new BankAccountInformation();
            bankAccountInformation.ItemsSource = data.GetData();
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
            string query = "SELECT TOP 1 * FROM BankAccountInformation ORDER BY Bank_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Bank_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Interest) || CheckForError(Deposit) || CheckForError(Withdraw) || CheckForError(ServiceCharge))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [BankAccountInformation] (Bank_Date, Bank_Interest, Bank_Deposit, Bank_Withdraw, Bank_ServiceCharge,Bank_Remains) VALUES (@Date, @Interest, @Deposit, @Withdraw,@ServiceCharge, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Interest", Interest.Text);
                CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                CmdSql.Parameters.AddWithValue("@Withdraw", ServiceCharge.Text);
                CmdSql.Parameters.AddWithValue("@ServiceCharge", Withdraw.Text);
                CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) + Convert.ToDouble(Interest.Text) - Convert.ToDouble(Withdraw.Text) - Convert.ToDouble(ServiceCharge.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            BankAccountInformation data = new BankAccountInformation();
            bankAccountInformation.ItemsSource = data.GetData();
        }
    }
}
