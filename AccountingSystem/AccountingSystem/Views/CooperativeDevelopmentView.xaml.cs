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
    /// Interaction logic for CooperativeDevelopmentView.xaml
    /// </summary>
    public partial class CooperativeDevelopmentView : Page
    {
        private DateTime dateTime;
        private string stuff_pass;
        private string stuff_name;

        private int Id;

        public CooperativeDevelopmentView()
        {
            InitializeComponent();
            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
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
            string query = "SELECT TOP 1 * FROM CooperativeDevelopment ORDER BY Cooperative_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Cooperative_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }

        private double edited_total()
        {
            double remains = 0.00;
            Connection conn = new Connection();
            string query = "SELECT * FROM CooperativeDevelopment Order by Cooperative_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                {
                    string rid = reader["Cooperative_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);

                    if (Id > r_id)
                    {
                        remains = (double)reader["Cooperative_Remains"];
                        Console.Write(Id + " " + r_id);
                    }
                }
            }
            conn.CloseConnection();
            return remains;
        }

        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Current) || CheckForError(Paid))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
            if ((string)Save.Content == "Save")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    double previous = this.last_remains();
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [CooperativeDevelopment] (Cooperative_Date, Cooperative_Current, Cooperative_Paid, Cooperative_Previous, Cooperative_Remains) VALUES (@Date, @Current, @Paid, @Previous, @Remains)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Current", Current.Text);
                    CmdSql.Parameters.AddWithValue("@Paid", Paid.Text);
                    CmdSql.Parameters.AddWithValue("@Previous", previous);
                    CmdSql.Parameters.AddWithValue("@Remains", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Paid.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table
                    Connection conn2 = new Connection();

                    string query = "SELECT TOP 1 * FROM CooperativeDevelopment ORDER BY Cooperative_Id DESC";
                    conn2.OpenConection();
                    SqlDataReader reader = conn2.DataReader(query);
                    while (reader.Read())
                    {
                        Id = (int)reader["Cooperative_Id"];
                        dateTime = (DateTime)reader["Cooperative_Date"];
                    }
                    conn2.CloseConnection();

                    string table = "Co-operative developement";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Inserted!");

                }
            }

            else
            {
                double prev = this.edited_total();
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("UPDATE [CooperativeDevelopment] SET Cooperative_Date = @Date , Cooperative_Current = @Current, Cooperative_Paid = @Paid, Cooperative_Previous = @Previous, Cooperative_Remains = @Remains WHERE Cooperative_Id=" + EntryNo.Text, conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Current", Current.Text);
                    CmdSql.Parameters.AddWithValue("@Paid", Paid.Text);
                    CmdSql.Parameters.AddWithValue("@Previous", prev);
                    CmdSql.Parameters.AddWithValue("@Remains", prev + Convert.ToDouble(Current.Text) - Convert.ToDouble(Paid.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Cooperative Development";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Save";
                MessageBox.Show("Successfully Updated!");
            }

            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new CooperativeDevelopment().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From CooperativeDevelopment WHERE Cooperative_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Cooperative_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Date.SelectedDate = (DateTime)reader["Cooperative_Date"];
                    Current.Text = reader["Cooperative_Current"].ToString();
                    Paid.Text = reader["Cooperative_Paid"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM CooperativeDevelopment WHERE Cooperative_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "CooperativeDevelopment";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Deleted!");

                    conn.CloseConnection();
                    CooperativeDevelopment data = new CooperativeDevelopment();
                    cooperativeDevelopment.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}

    