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
        private string stuff_pass;
        private string stuff_name;


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
            if ((string)Save.Content == "Insert")
            {
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

                    string query = "SELECT TOP 1 * FROM BankAccount ORDER BY BankAccount_Id DESC";
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

            }

            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("UPDATE [SecurityFund] SET Security_Date = @Date , Security_Details = @Details, Security_Deposit = @Deposit, Security_Expenses = @Expenses, Security_Remains = @Remains WHERE Security_Id=" + EntryNo.Text, conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Interest", Interest.Text);
                    CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                    CmdSql.Parameters.AddWithValue("@Withdraw", Withdraw.Text);

                    CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                    CmdSql.Parameters.AddWithValue("@ServiceCharge", ServiceCharge.Text);
                    CmdSql.Parameters.AddWithValue("@Remains", CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) + Convert.ToDouble(Interest.Text) - Convert.ToDouble(Withdraw.Text) - Convert.ToDouble(ServiceCharge.Text)));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Security Fund";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Insert";
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

        #region editEntry

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
                string query = "SELECT * From BankAccount WHERE BankAccount_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["BankAccount_Id"].ToString();
                    Date.SelectedDate = (DateTime)reader["BankAccount_Date"];
                    Interest.Text = reader["BankAccount_Interest"].ToString();
                    Deposit.Text = reader["BankAccount_Deposit"].ToString();
                    Withdraw.Text = reader["BankAccount_Withdraw"].ToString();
                    ServiceCharge.Text = reader["BankAccount_ServiceCharge"].ToString();
                }

                conn.CloseConnection();
                Save.Content = "Update";
            }
        }

        #endregion

        #region removeEntry
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM SecurityFund WHERE Security_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "BankAccount";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    BankAccountInformation data = new BankAccountInformation();
                    bankAccountInformation.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}
