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
    /// Interaction logic for MiscellaneousView.xaml
    /// </summary>
    public partial class MiscellaneousView : Page
    {
        

        private DateTime dateTime;
        private string stuff_pass;
        private string stuff_name;

        private int Id;

        public MiscellaneousView()
        {
            InitializeComponent();
            Miscellaneous data = new Miscellaneous();
            Miscellaneous.ItemsSource = data.GetData();
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
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Details) || CheckForError(Expenses))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double previous = this.last_total();

            if ((string)Save.Content == "Save")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Miscellaneous] (ME_Date, ME_Details, ME_Expenses, ME_Total) VALUES (@Date, @Details, @Expenses, @Total)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                    CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                    CmdSql.Parameters.AddWithValue("@Total", previous + Convert.ToDouble(Expenses.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table
                    Connection conn2 = new Connection();

                    string query = "SELECT TOP 1 * FROM Miscellaneous ORDER BY ME_Id DESC";
                    conn2.OpenConection();
                    SqlDataReader reader = conn2.DataReader(query);
                    while (reader.Read())
                    {
                        Id = (int)reader["ME_Id"];
                        dateTime = (DateTime)reader["ME_Date"];
                    }
                    conn2.CloseConnection();




                    string table = "Miscellaneous Expenses";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Saved");

                }



            }

            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("UPDATE [Miscellaneous] SET ME_Date = @Date , ME_Details = @Details, ME_Expenses = @Expenses, ME_Total = @Total WHERE ME_Id=" + EntryNo.Text, conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                    CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                    CmdSql.Parameters.AddWithValue("@Total", previous + Convert.ToDouble(Expenses.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Miscellaneous Expenses";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Save";
                MessageBox.Show("Successfully Updated");
            }

            Miscellaneous data = new Miscellaneous();
            Miscellaneous.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new Miscellaneous().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From Miscellaneous WHERE ME_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["ME_Id"].ToString();
                    Date.SelectedDate = (DateTime)reader["ME_Date"];
                    Details.Text = (string)reader["ME_Details"];
                    Expenses.Text = reader["ME_Expenses"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM Miscellaneous WHERE ME_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "Miscellanous Expenses";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    OfficeRent data = new OfficeRent();
                    Miscellaneous.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }


        #endregion
        private double last_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT TOP 1 * FROM Miscellaneous ORDER BY ME_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                total = (double)reader["ME_Total"];
            }
            conn.CloseConnection();
            return total;
        }
        private double edited_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT * FROM Miscellaneous Order by ME_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                {
                    string rid = reader["ME_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);
                    if (Id > r_id)
                        total = (double)reader["ME_Total"];
                }
            }
            conn.CloseConnection();
            return total;
        }

    }
}
