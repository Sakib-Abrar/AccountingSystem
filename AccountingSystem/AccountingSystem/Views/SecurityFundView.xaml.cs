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
        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;

        public SecurityFundView()
        {
            InitializeComponent();
            SecurityFund data = new SecurityFund();
            securityFund.ItemsSource = data.GetData();
            DataContext = data;
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
   
        public bool CheckForError(TextBox Selected)
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
            if (CheckForError(Details) || CheckForError(Deposit) || CheckForError(Expenses)) {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double remains = this.last_remains();
            if ((string)Save.Content == "Insert")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [SecurityFund] (Security_Date, Security_Details, Security_Deposit, Security_Expenses, Security_Remains) VALUES (@Date, @Details, @Deposit, @Expenses, @Remains)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                    CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                    CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                    CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = Date.SelectedDate;

                    string table = "Security Fund";
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

                    SqlCommand CmdSql = new SqlCommand("UPDATE [SecurityFund] SET Security_Date = @Date , Security_Details = @Details, Security_Deposit = @Deposit, Security_Expenses = @Expenses, Security_Remains = @Remains WHERE Security_Id="+ EntryNo.Text, conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                    CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                    CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                    CmdSql.Parameters.AddWithValue("@Remains", remains + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = Date.SelectedDate;

                    string table = "Security Fund";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Insert";
            }
            SecurityFund data = new SecurityFund();
            securityFund.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new SecurityFund().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditDialogView handle = new EditDialogView();
            if (handle.ShowDialog() == true)
            {
                if (handle.FirstInput != handle.SecondInput)
                    return;
                Connection conn = new Connection();
                conn.OpenConection();
                string query = "SELECT * From SecurityFund WHERE Security_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Security_Id"].ToString();
                    Date.SelectedDate = (DateTime)reader["Security_Date"];
                    Details.Text = (string)reader["Security_Details"];
                    Deposit.Text = reader["Security_Deposit"].ToString();
                    Expenses.Text = reader["Security_Expenses"].ToString();
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
                        return;
                    Connection conn = new Connection();
                    conn.OpenConection();
                    int isLogin = 0;
                    string query = "SELECT * From Stuff ";//WHERE Stuff_Cell = 12345";
                    SqlDataReader reader = conn.DataReader(query);
                    while (reader.Read())
                    {
                        stuff_name = (string)reader["Stuff_Name"];
                        stuff_pass = (String)reader["Stuff_Password"];
                        if (stuff_name.Equals(Login.GlobalStuffName) && stuff_pass.Equals(handle.GetPassword))
                        {
                            isLogin = 1;
                            break;
                        }
                    }
                    if (isLogin != 1)
                        return;

                    using (SqlCommand command = new SqlCommand("DELETE FROM SecurityFund WHERE Security_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    conn.CloseConnection();
                }
            }
        }

    }
}
