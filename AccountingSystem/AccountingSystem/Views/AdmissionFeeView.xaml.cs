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
    /// Interaction logic for AdmissionFeeView.xaml
    /// </summary>
    public partial class AdmissionFeeView : Page
    {
        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;

        public AdmissionFeeView()
        {
            InitializeComponent();
            AdmissionFee data = new AdmissionFee();
            admissionFee.ItemsSource = data.GetData();
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
            string query = "SELECT TOP 1 * FROM AdmissionFee ORDER BY Admission_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                total = (double)reader["Admission_Total"];
            }
            conn.CloseConnection();
            return total;
        }

        private double edited_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT * FROM AdmissionFee Order by Admission_Id";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                {
                    string rid = reader["Admission_Id"].ToString();
                    int r_id = Convert.ToInt32(rid);
                    if (Id > r_id)
                    {
                        total = (double)reader["Admission_Total"];
                    }
                }
            }
            conn.CloseConnection();
            return total;
        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Collection))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }


            
                double total = this.last_total();
                if ((string)Save.Content == "Save")
                {
                    using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                    {
                        SqlCommand CmdSql = new SqlCommand("INSERT INTO [AdmissionFee] (Admission_Date, Admission_Collection, Admission_Total) VALUES (@Date, @Collection, @Total)", conn);
                        conn.Open();
                        CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@Collection", Collection.Text);
                        CmdSql.Parameters.AddWithValue("@Total", total + Convert.ToDouble(Collection.Text));
                        CmdSql.ExecuteNonQuery();
                        conn.Close();

                        //Inserting value in Entry table
                        Connection conn2 = new Connection();

                        string query = "SELECT TOP 1 * FROM AdmissionFee ORDER BY Admission_Id DESC";
                        conn2.OpenConection();
                        SqlDataReader reader = conn2.DataReader(query);
                        while (reader.Read())
                        {
                            Id = (int)reader["Admission_Id"];
                            dateTime = (DateTime)reader["Admission_Date"];
                        }

                        conn2.CloseConnection();
                        AdmissionFee data = new AdmissionFee();
                        admissionFee.ItemsSource = data.GetData();
                        DataContext = data;
                }




                    string table = "Admission Fee";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Inserted");
                }


                else
                {
                    int temp_id = Id;
                    Connection conc = new Connection();
                    string query = "SELECT * FROM AdmissionFee Order by Admission_Id Asc";
                    conc.OpenConection();
                    SqlDataReader reader = conc.DataReader(query);
                    while (reader.Read())
                    {
                        string rid = reader["Admission_Id"].ToString();
                        int r_id = Convert.ToInt32(rid);
                        string col = reader["Admission_Collection"].ToString();
                        int colint = Convert.ToInt32(col);

                        if (temp_id < r_id)
                        {
                        Id = r_id;
                        double tot = this.edited_total();
                        using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                            {
                                SqlCommand CmdSql = new SqlCommand("UPDATE [AdmissionFee] SET Admission_Date = @Date , Admission_Collection = @Collection, Admission_Total = @Total WHERE Admission_Id=" + r_id, conn);
                                conn.Open();
                                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                                CmdSql.Parameters.AddWithValue("@Collection", col);
                                CmdSql.Parameters.AddWithValue("@Total", (tot + colint));
                                CmdSql.ExecuteNonQuery();
                                conn.Close();
                            }
                        Console.Write(r_id + " " + col + " " + tot+"...");
                        
                        }

                        else if (temp_id == r_id)
                        {
                        double tot = this.edited_total();
                        using (SqlConnection con = new SqlConnection(@Connection.ConnectionString))
                            {
                                Console.Write(r_id + " " + col + " " + tot + "---");
                                SqlCommand CmdSql = new SqlCommand("UPDATE [AdmissionFee] SET Admission_Date = @Date , Admission_Total = @Total , Admission_Collection = @Collection WHERE Admission_Id=" + EntryNo.Text, con);
                                con.Open();
                                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                                CmdSql.Parameters.AddWithValue("@Collection", Collection.Text);
                                CmdSql.Parameters.AddWithValue("@Total", tot + Convert.ToDouble(Collection.Text));
                                CmdSql.ExecuteNonQuery();
                                con.Close();

                                //Inserting value in Entry table

                                Id = Convert.ToInt32(EntryNo.Text);
                                dateTime = DateTime.Today;

                                string table = "AdmissionFee";
                                string type = "Updated";
                                string color = "Blue";
                                EntryLog entry = new EntryLog();
                                entry.Add_Entry(table, type, Id, dateTime, color);
                                Save.Content = "Save";
                                MessageBox.Show("Successfully Updated");
                            }
                        }
                        

                    }
                    conc.CloseConnection();

                    AdmissionFee data = new AdmissionFee();
                    admissionFee.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new AdmissionFee().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From AdmissionFee WHERE Admission_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["Admission_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Date.SelectedDate = (DateTime)reader["Admission_Date"];
                    Collection.Text = reader["Admission_Collection"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM AdmissionFee WHERE Admission_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "AdmissionFee";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    AdmissionFee data = new AdmissionFee();
                    admissionFee.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion
    }
}

