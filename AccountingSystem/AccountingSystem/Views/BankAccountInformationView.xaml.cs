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
        private int Id;
        private DateTime dateTime;

        public BankAccountInformationView()
        {
            InitializeComponent();
            BankAccountInformation data = new BankAccountInformation();
            bankAccountInformation.ItemsSource = data.GetData();
            DataContext = data;
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
            string query = "SELECT TOP 1 * FROM BankAccount ORDER BY BankAccount_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["BankAccount_Remains"];
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

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [BankAccount] (BankAccount_Date, BankAccount_Interest, BankAccount_Deposit, BankAccount_Withdraw, BankAccount_ServiceCharge,BankAccount_Remains) VALUES (@Date, @Interest, @Deposit, @Withdraw,@ServiceCharge, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                CmdSql.Parameters.AddWithValue("@Interest", Interest.Text);
                CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                CmdSql.Parameters.AddWithValue("@Withdraw", ServiceCharge.Text);
                CmdSql.Parameters.AddWithValue("@ServiceCharge", Withdraw.Text);
                CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) + Convert.ToDouble(Interest.Text) - Convert.ToDouble(Withdraw.Text) - Convert.ToDouble(ServiceCharge.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();

                //Inserting value in Entry table
                Connection conn2 = new Connection();

                string query = "SELECT TOP 1 * FROM BankAccount ORDER BY Admission_Id DESC";
                conn2.OpenConection();
                SqlDataReader reader = conn2.DataReader(query);
                while (reader.Read())
                {
                    Id = (int)reader["BankAccount_Id"];
                    dateTime = (DateTime)reader["BankAccount_Date"];
                }
                conn2.CloseConnection();




                string table = "Bank Account";
                string type = "Inserted";
                string color = "Green";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, Id, dateTime, color);


            }
            BankAccountInformation data = new BankAccountInformation();
            bankAccountInformation.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new BankAccountInformation().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
