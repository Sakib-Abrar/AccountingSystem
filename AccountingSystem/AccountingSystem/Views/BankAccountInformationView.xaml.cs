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
            string query = "SELECT TOP 1 * FROM BankAccount ORDER BY BankAccount_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["BankAccount_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }
        private double edited_total()
        {
            double remains = 0.00;
            Connection conn = new Connection();
            string query = "SELECT * FROM BankAccount Order by BankAccount_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                {
                    string rid = reader["BankAccount_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);

                    if (Id > r_id)
                    { remains = (double)reader["BankAccount_Remains"];
                          Console.Write(Id + " " + r_id);
                         }
                }
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
            if ((string)Save.Content == "Save")
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
                int temp_id = Id;
                Connection con = new Connection();
                string query = "SELECT * FROM BankAccount Order by BankAccount_Id Asc";
                con.OpenConection();
                SqlDataReader reader = con.DataReader(query);
                while (reader.Read())
                {
                    string rid = reader["BankAccount_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);
                    string dep = reader["BankAccount_Deposit"].ToString();
                    double depint = Convert.ToDouble(dep);
                    string with = reader["BankAccount_Withdraw"].ToString();
                    double withint = Convert.ToDouble(with);
                    string intr = reader["BankAccount_Deposit"].ToString();
                    double intint = Convert.ToDouble(intr);
                    string sc = reader["BankAccount_ServiceCharge"].ToString();
                    double scint = Convert.ToDouble(sc);

                    //code (if block) for updating rest of the table
                    if (temp_id < r_id)
                    {
                        Id = r_id;
                        double remain = this.edited_total();
                        using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                        {
                            SqlCommand CmdSql = new SqlCommand("UPDATE [BankAccount] SET BankAccount_Date = @Date , BankAccount_Deposit = @Deposit, BankAccount_Withdraw = @Withdraw, BankAccount_Interest = @Interest, BankAccount_Remains = @Remains WHERE BankAccount_Id=" + r_id, conn);
                            conn.Open();
                            CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                            CmdSql.Parameters.AddWithValue("@Interest", intr);
                            CmdSql.Parameters.AddWithValue("@Deposit", dep);
                            CmdSql.Parameters.AddWithValue("@Withdraw", with);
                            CmdSql.Parameters.AddWithValue("@ServiceCharge", sc);
                            CmdSql.Parameters.AddWithValue("@Remains", remain + depint + intint - withint - scint);
                            CmdSql.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    //Code (else if block) for updating expected row
                    else if (temp_id == r_id)
                    {
                        double remain = this.edited_total();
                        using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                        {

                            SqlCommand CmdSql = new SqlCommand("UPDATE [BankAccount] SET BankAccount_Date = @Date , BankAccount_ServiceCharge = @ServiceCharge, BankAccount_Interest = @Interest, BankAccount_Deposit = @Deposit, BankAccount_Withdraw = @Withdraw, BankAccount_Remains = @Remains WHERE BankAccount_Id=" + EntryNo.Text, conn);
                            conn.Open();
                            CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                            CmdSql.Parameters.AddWithValue("@Interest", Interest.Text);
                            CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                            CmdSql.Parameters.AddWithValue("@Withdraw", Withdraw.Text);
                            CmdSql.Parameters.AddWithValue("@ServiceCharge", ServiceCharge.Text);
                            CmdSql.Parameters.AddWithValue("@Remains", remain + Convert.ToDouble(Deposit.Text) + Convert.ToDouble(Interest.Text) - Convert.ToDouble(Withdraw.Text) - Convert.ToDouble(ServiceCharge.Text));
                            CmdSql.ExecuteNonQuery();
                            conn.Close();

                            //Inserting value in Entry table

                            Id = Convert.ToInt32(EntryNo.Text);
                            dateTime = DateTime.Today;

                            string table = "Bank Account";
                            string type = "Updated";
                            string color = "Blue";
                            EntryLog entry = new EntryLog();
                            entry.Add_Entry(table, type, Id, dateTime, color);

                        }
                        Save.Content = "Save";
                    }
                    
                }
                con.CloseConnection();
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
                    Id = Convert.ToInt32(EntryNo.Text);
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM BankAccount WHERE BankAccount_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Successfully Deleted");
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
