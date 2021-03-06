﻿using System;
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
            SecurityFund.ItemsSource = data.GetData();
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

        private double edited_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT * FROM SecurityFund Order by Security_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                {
                    string rid = reader["Security_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);
                    if (Id > r_id)
                    {
                        total = (double)reader["Security_Remains"];
                        //Console.Write(Id + " > "+r_id);
                    }
                }
            }
            conn.CloseConnection();
            return total;
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
            //code for inserting record
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
                    dateTime = DateTime.Today;

                    string table = "Security Fund";
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
                int temp_id = Id;
                Connection con = new Connection();
                string query = "SELECT * FROM SecurityFund Order by Security_Id Asc";
                con.OpenConection();
                SqlDataReader reader = con.DataReader(query);
                while (reader.Read())
                {
                    string rid = reader["Security_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);
                    string dep = reader["Security_Deposit"].ToString();
                    double depint = Convert.ToDouble(dep);
                    string exp = reader["Security_Expenses"].ToString();
                    double expint = Convert.ToDouble(exp);

                    //code (if block) for updating rest of the table
                    if (temp_id < r_id)
                    {
                        Id = r_id;
                        double remain = this.edited_total();
                        using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                        {
                            SqlCommand CmdSql = new SqlCommand("UPDATE [SecurityFund] SET Security_Date = @Date , Security_Deposit = @Deposit, Security_Expenses = @Expenses, Security_Remains = @Remains WHERE Security_Id=" + r_id, conn);
                            conn.Open();
                            CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                            CmdSql.Parameters.AddWithValue("@Deposit", dep);
                            CmdSql.Parameters.AddWithValue("@Expenses", exp);
                            CmdSql.Parameters.AddWithValue("@Remains", remain + depint - expint);
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

                            SqlCommand CmdSql = new SqlCommand("UPDATE [SecurityFund] SET Security_Date = @Date , Security_Details = @Details, Security_Deposit = @Deposit, Security_Expenses = @Expenses, Security_Remains = @Remains WHERE Security_Id=" + Id, conn);
                            conn.Open();
                            CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                            CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                            CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                            CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                            CmdSql.Parameters.AddWithValue("@Remains", remain + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
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
                        
                    }

                }
                con.CloseConnection();
                Save.Content = "Insert";
                MessageBox.Show("Successfully Updated");
            }

            SecurityFund data = new SecurityFund();
            SecurityFund.ItemsSource = data.GetData();
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
                string query = "SELECT * From SecurityFund WHERE Security_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Security_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Date.SelectedDate = (DateTime)reader["Security_Date"];
                    Details.Text = (string)reader["Security_Details"];
                    Deposit.Text = reader["Security_Deposit"].ToString();
                    Expenses.Text = reader["Security_Expenses"].ToString();
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
                    string table = "Security Fund";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    SecurityFund data = new SecurityFund();
                    SecurityFund.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion

    }
}
