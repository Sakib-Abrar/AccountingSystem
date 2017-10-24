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
    /// Interaction logic for SalaryView.xaml
    /// </summary>
    public partial class SalaryView : Page
    {
        private int Id;
        private DateTime dateTime;
        private string stuff_pass;
        private string stuff_name;

        public SalaryView()
        {
            InitializeComponent();
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();
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
            if (CheckForError(Amount) || CheckForError(Bonus))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
            //double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {
                if ((string)Save.Content == "Insert")
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Salary] (Salary_Date, Salary_Amount, Salary_Bonus, Salary_Total) VALUES (@Date, @Amount, @Bonus, @Total)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                    CmdSql.Parameters.AddWithValue("@Bonus", Bonus.Text);
                    CmdSql.Parameters.AddWithValue("@Total", Convert.ToDouble(Amount.Text) + Convert.ToDouble(Bonus.Text));
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table
                    Connection conn2 = new Connection();

                    string query = "SELECT TOP 1 * FROM Salary ORDER BY Salary_Id DESC";
                    conn2.OpenConection();
                    SqlDataReader reader = conn2.DataReader(query);
                    while (reader.Read())
                    {
                        Id = (int)reader["Salary_Id"];
                        dateTime = (DateTime)reader["Salary_Date"];
                    }
                    conn2.CloseConnection();




                    string table = "Salary";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                }
                else
                {
                    
                    using (SqlConnection con = new SqlConnection(@Connection.ConnectionString))
                    {
                        SqlCommand CmdSql = new SqlCommand("UPDATE [Salary] SET Salary_Date = @Date , Salary_Amount = @Amount, Salary_Bonus = @Bonus, Salary_Total = @Total WHERE Salary_Id=" + EntryNo.Text, conn);
                        conn.Open();
                        CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                        CmdSql.Parameters.AddWithValue("@Bonus", Bonus.Text);
                        CmdSql.Parameters.AddWithValue("@Total", Convert.ToDouble(Amount.Text) + Convert.ToDouble(Bonus.Text));
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
        }
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new Salary().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From Salary WHERE Salary_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Salary_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Date.SelectedDate = (DateTime)reader["Salary_Date"];
                    Bonus.Text = reader["Salary_Bonus"].ToString();
                    Amount.Text = reader["Salary_Amount"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM Salary WHERE Salary_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "Salary";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    Salary data = new Salary();
                    salary.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}
