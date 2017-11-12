using AccountingSystem.Controller;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MonthlyDepositLedgerView.xaml
    /// </summary>
    public partial class MonthlyDepositLedgerView : Page
    {
        MonthlyDeposit data;

        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;
        public MonthlyDepositLedgerView()
        {
            InitializeComponent();
            data = new MonthlyDeposit();
            generalDepositLedger.ItemsSource = data.GetDataLedger(0, 0);
            DataContext = data;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            int tempId = Convert.ToInt32(AccountNo.Text);
            generalDepositLedger.ItemsSource = data.GetDataLedger(tempId, 0);
            DataContext = data;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int tempId = Convert.ToInt32(AccountNo.Text);
            generalDepositLedger.ItemsSource = data.GetDataLedger(tempId, 1);
            DataContext = data;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditDialogView handle = new EditDialogView();
            if (handle.ShowDialog() == true)
            {
                if (handle.FirstInput != handle.SecondInput)
                {
                    MessageBox.Show("Entry No. did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                Connection conn = new Connection();
                conn.OpenConection();
                string query = "SELECT * From MonthlyDepositLedger WHERE MonthlyEntryId = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["MonthlyEntryId"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Date.SelectedDate = (DateTime)reader["MonthlyDate"];
                    Details.Text = (string)reader["MonthlyDetails"];
                    Deposit.Text = reader["MonthlyDeposit"].ToString();
                    Withdraw.Text = reader["MonthlyWithdraw"].ToString();
                }

                conn.CloseConnection();
                Save.Content = "Update";
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveDialogView handle = new RemoveDialogView();
            if (handle.ShowDialog() == true)
            {
                using (SqlConnection con = new SqlConnection(@Connection.ConnectionString))
                {
                    if (handle.FirstInput != handle.SecondInput)
                    {
                        MessageBox.Show("Entry No. did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    Connection conn = new Connection();
                    conn.OpenConection();
                    int isLogin = 0;
                    string query = "SELECT * From Stuff ";
                    SqlDataReader reader = conn.DataReader(query);
                    while (reader.Read())
                    {
                        stuff_name = (string)reader["Stuff_Name"];
                        stuff_pass = (string)reader["Stuff_Password"];
                        if (stuff_name.Equals(Login.GlobalStuffName) && stuff_pass.Equals(handle.GetPassword))
                        {
                            isLogin = 1;
                            break;
                        }
                    }
                    if (isLogin != 1)
                    {
                        MessageBox.Show("Wrong Password.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    using (SqlCommand command = new SqlCommand("DELETE FROM MonthlyDepositLedger WHERE MonthlyEntryId = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "Monthly Deposit Ledger";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    int tempId = Convert.ToInt32(AccountNo.Text);
                    generalDepositLedger.ItemsSource = data.GetDataLedger(tempId, 2);
                    DataContext = data;
                }
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                data.PublishPDFLedger(getDate.FromDate, getDate.ToDate, Convert.ToInt32(AccountNo.Text));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Save.Content == "Insert")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [MonthlyDepositLedger] (MonthlyDate, MonthlyId, MemberId, MonthlyDetails, MonthlyDeposit, MonthlyWithdraw, MonthlyBalance) VALUES (@Date, @Id , @MemberId,@Details, @Deposit, @Withdraw, @Balance)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Id", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", data.MemberId);
                    CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                    CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                    CmdSql.Parameters.AddWithValue("@Withdraw", Withdraw.Text);
                    CmdSql.Parameters.AddWithValue("@Balance", data.Balance + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Monthly Deposit Ledger";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Inserted");
                }
            }

            // code for updating record
            else
            {
            }
            int tempId = Convert.ToInt32(AccountNo.Text);
            generalDepositLedger.ItemsSource = data.GetDataLedger(tempId, 2);
            DataContext = data;
        }

        private void generalDepositLedger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AccountNo_LostFocus(object sender, RoutedEventArgs e)
        {
            int tempId = Convert.ToInt32(AccountNo.Text);
            generalDepositLedger.ItemsSource = data.GetDataLedger(tempId, 2);
            DataContext = data;
        }
    }
}
