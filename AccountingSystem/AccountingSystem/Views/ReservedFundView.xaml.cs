using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Windows.Data;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for ReservedFundView.xaml
    /// </summary>
    public partial class ReservedFundView : Page
    {
        private DateTime dateTime;
        private string stuff_pass;
        private string stuff_name;

        private int Id;

        public ReservedFundView()
            {
                InitializeComponent();
                ReservedFund data = new ReservedFund();
                reservedFund.ItemsSource = data.GetData();
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
        private double last_total()
            {
                Connection conn = new Connection();
                double total = 0.00;
                string query = "SELECT TOP 1 * FROM ReservedFund ORDER BY Reserved_Id DESC";
                conn.OpenConection();
                SqlDataReader reader = conn.DataReader(query);
                while (reader.Read())
                {
                    total = (double)reader["Reserved_Total"];
                }
                conn.CloseConnection();
                return total;
            }
            protected void Save_Click(object sender, RoutedEventArgs e)
            {
                if (CheckForError(Current) || CheckForError(Withdraw))
                {
                    MessageBox.Show("Error!Check Input Again");
                    return;
                }
            double previous = this.last_total();
            if ((string)Save.Content == "Insert")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [ReservedFund] (Reserved_Date, Reserved_Remaining, Reserved_Current, Reserved_Previous, Reserved_Total,Reserved_Withdraw) VALUES (@ReservedFund_date, @ReservedFund_remainig, @ReservedFund_current, @ReservedFund_previous, @ReservedFund_total,@ReservedFund_withdraw)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@ReservedFund_date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@ReservedFund_remainig", Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.Parameters.AddWithValue("@ReservedFund_current", Current.Text);
                    CmdSql.Parameters.AddWithValue("@ReservedFund_previous", previous);
                    CmdSql.Parameters.AddWithValue("@ReservedFund_total", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.Parameters.AddWithValue("@ReservedFund_withdraw", Withdraw.Text);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table
                    Connection conn2 = new Connection();

                    string query = "SELECT TOP 1 * FROM ReservedFund ORDER BY Reserved_Id DESC";
                    conn2.OpenConection();
                    SqlDataReader reader = conn2.DataReader(query);
                    while (reader.Read())
                    {
                        Id = (int)reader["Reserved_Id"];
                        dateTime = (DateTime)reader["Reserved_Date"];
                    }
                    conn2.CloseConnection();

                    string table = "Reserved Fund";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Inserted");
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("UPDATE [ReservedFund] SET Reserved_Date = @Date , Reserved_Current = @Current, Reserved_Withdraw = @Withdraw, Reserved_Previous = @Previous, Reserved_Remaining = @Remaining WHERE Reserved_Id=" + EntryNo.Text, conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Current", Current.Text);
                    CmdSql.Parameters.AddWithValue("@Withdraw", Withdraw.Text);
                    CmdSql.Parameters.AddWithValue("@Previous", previous);
                    CmdSql.Parameters.AddWithValue("@Total", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.Parameters.AddWithValue("@Remaining", Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Reserved Fund";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    Save.Content = "Insert";
                    MessageBox.Show("Successfully Updated");
                }
                
            }
            ReservedFund data = new ReservedFund();
                reservedFund.ItemsSource = data.GetData();
                DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new ReservedFund().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From ReservedFund WHERE Reserved_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Reserved_Id"].ToString();
                    Date.SelectedDate = (DateTime)reader["Reserved_Date"];
                    Current.Text = reader["Reserved_Current"].ToString();
                    Withdraw.Text = reader["Reserved_Withdraw"].ToString();
                    
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM ReservedFund WHERE Reserved_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "ReservedFund";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    ReservedFund data = new ReservedFund();
                    reservedFund.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}
