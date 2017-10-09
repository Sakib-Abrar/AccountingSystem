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
    /// Interaction logic for StuffView.xaml
    /// </summary>
    public partial class StuffView : Page
    {
        string stuffType;
        private string stuff_pass;
        private string stuff_name;
        private int Id;
        private DateTime? dateTime;
        public StuffView()
        {
            InitializeComponent();
            Stuff data = new Stuff();
            stuff.ItemsSource = data.GetData();
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
            if (Type_Stuff.IsChecked == true )
            {
                stuffType = "stuff";
            }
            else if (Type_Admin.IsChecked == true)
            {
                stuffType = "admin";
            }
            if (CheckForError(Name) || CheckForError(Cell) || CheckForError(Password))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
            if ((string)Save.Content == "Insert")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    try
                    {
                        //change query
                        SqlCommand CmdSql = new SqlCommand("INSERT INTO [Stuff] (Stuff_Id, Stuff_Join, Stuff_Name, Stuff_Cell, Stuff_Password, Stuff_Type) VALUES (@Id, @Date, @Name, @Cell, @Password, @Type)", conn);
                        conn.Open();
                        CmdSql.Parameters.AddWithValue("@Id", ID.Text);
                        CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@Name", Name.Text);
                        CmdSql.Parameters.AddWithValue("@Cell", Cell.Text);
                        CmdSql.Parameters.AddWithValue("@Password", Password.Text);
                        CmdSql.Parameters.AddWithValue("@Type", stuffType);
                        CmdSql.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Error! Cell Number should be Unique. Check the entries TWICE");
                        return;
                    }

                }

                Id = Convert.ToInt32(ID.Text);
                dateTime = DateTime.Today;
                string table = "Stuff";
                string type = "Inserted";
                string color = "Green";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, Id, dateTime, color);
            }

            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("UPDATE[Stuff] SET Stuff_Join = @Date, Stuff_Name = @Name, Stuff_Cell = @Cell, Stuff_Password = @Password, Stuff_type = @Type WHERE Stuff_Id = " + ID.Text, conn);
                    
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Id", ID.Text);
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Name", Name.Text);
                    CmdSql.Parameters.AddWithValue("@Cell", Cell.Text);
                    CmdSql.Parameters.AddWithValue("@Password", Password.Text);
                    CmdSql.Parameters.AddWithValue("@Type", stuffType);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(ID.Text);
                    dateTime = DateTime.Today;

                    string table = "Stuff";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Insert";
            }


            Stuff data = new Stuff();
            stuff.ItemsSource = data.GetData();
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
                string query = "SELECT * From Stuff WHERE Stuff_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    ID.Text = reader["Stuff_Id"].ToString();
                    Date.SelectedDate = (DateTime)reader["Stuff_Join"];
                    Name.Text = (string)reader["Stuff_Name"];
                    Password.Text = reader["Stuff_Password"].ToString();
                    Cell.Text = reader["Stuff_Cell"].ToString();
                    stuffType = reader["Stuff_Type"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM Stuff WHERE Stuff_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "Stuff";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    Stuff data = new Stuff();
                    stuff.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}
