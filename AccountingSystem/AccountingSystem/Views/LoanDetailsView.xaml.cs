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
    /// Interaction logic for LoanDetailsView.xaml
    /// </summary>

    public partial class LoanDetailsView : Page
    {
        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;

        public LoanDetailsView()
        {
            InitializeComponent();
            LoanDetails data = new LoanDetails();
            LoanDetails.ItemsSource = data.GetData();
            DataContext = data;
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

     

        private double edited_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT * FROM LoanDetails Order by Security_Id";
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
                        Console.Write(Id + " > " + r_id);
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
        public string method = "";

        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Daily.IsChecked == true)
                method = "Daily";
            else if (Weekly.IsChecked == true)
                method = "Weekly";
            else if (Monthly.IsChecked == true)
                method = "Monthly";
            if (method == "")
            {
                MessageBox.Show("Error!Select Collection method");
                return;
            }

            if (CheckForError(AccountId) || CheckForError(Sector) || CheckForError(Amount) || CheckForError(ServiceCharge) || CheckForError(LifeTime) || CheckForError(Installment))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
         
            Connection conn2 = new Connection();
            string query2 = "SELECT * From [Member] where MemberId =" + AccountId.Text + " ";
            conn2.OpenConection();
            SqlDataReader reader2 = conn2.DataReader(query2);
            while (!reader2.Read())
            {              
                MessageBox.Show("Error!Account Id invalid");
                return;
            }
            conn2.CloseConnection();

            DateTime dtd = Convert.ToDateTime(SanctionDate.Text);
            DateTime nextDate = dtd;
           
            if (method == "Daily") { nextDate = nextDate.AddDays(1); }
            else if (method == "Weekly") { nextDate = nextDate.AddDays(7); }
            else if(method == "Monthly") { nextDate = nextDate.AddMonths(1); }


            if ((string)Save.Content == "Insert")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    double charge = (Convert.ToDouble(Amount.Text) * Convert.ToDouble(ServiceCharge.Text)) / 100;
                    double total = Convert.ToDouble(Amount.Text) + charge;
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [LoanDetails] (LoanDetails_Account, LoanDetails_Sector, LoanDetails_Amount, LoanDetails_Service, LoanDetails_Sanction,LoanDetails_LastPaid,LoanDetails_NextDate,LoanDetails_Collection, LoanDetails_Lifetime, LoanDetails_Installment, LoanDetails_InstallmentAmount,LoanDetails_Total,LoanDetails_Balance) VALUES (@Account, @Sector, @Amount, @Service, @Sanction, @LastPaid,@NextDate, @Collection, @Lifetime, @Installment, @InstallmentAmount, @Total ,@Balance)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Account", AccountId.Text);
                    CmdSql.Parameters.AddWithValue("@Sector", Sector.Text);
                    CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                    CmdSql.Parameters.AddWithValue("@Service", ServiceCharge.Text);
                    CmdSql.Parameters.AddWithValue("@Sanction", SanctionDate.Text);
                    CmdSql.Parameters.AddWithValue("@LastPaid", SanctionDate.Text);
                    CmdSql.Parameters.AddWithValue("@NextDate", nextDate);
                    CmdSql.Parameters.AddWithValue("@Lifetime", LifeTime.Text);
                    CmdSql.Parameters.AddWithValue("@Collection", method);
                    CmdSql.Parameters.AddWithValue("@Installment", Installment.Text);
                    CmdSql.Parameters.AddWithValue("@InstallmentAmount", total / Convert.ToDouble(Installment.Text));
                    CmdSql.Parameters.AddWithValue("@Total", total);
                    CmdSql.Parameters.AddWithValue("@Balance", total);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Loan Details";
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

                    double charge = (Convert.ToDouble(Amount.Text) * Convert.ToDouble(ServiceCharge.Text)) / 100;
                    double total = Convert.ToDouble(Amount.Text) + charge;
                    SqlCommand CmdSql = new SqlCommand("UPDATE [LoanDetails] SET LoanDetails_Account = @Account, LoanDetails_Sector=@Sector, LoanDetails_Amount=@Amount, LoanDetails_Service=@Service, LoanDetails_Sanction=@Sanction,LoanDetails_LastPaid=@LastPaid,LoanDetails_NextDate=@NextDate,LoanDetails_Collection=@Collection, LoanDetails_Lifetime=@LifeTime, LoanDetails_Installment=@Installment, LoanDetails_InstallmentAmount=@InstallmentAmount,LoanDetails_Total=@Total,LoanDetails_Balance=@Balance", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Account", AccountId.Text);
                    CmdSql.Parameters.AddWithValue("@Sector", Sector.Text);
                    CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                    CmdSql.Parameters.AddWithValue("@Service", ServiceCharge.Text);
                    CmdSql.Parameters.AddWithValue("@Sanction", SanctionDate.Text);
                    CmdSql.Parameters.AddWithValue("@LastPaid", SanctionDate.Text);
                    CmdSql.Parameters.AddWithValue("@NextDate", nextDate);
                    CmdSql.Parameters.AddWithValue("@Lifetime", LifeTime.Text);
                    CmdSql.Parameters.AddWithValue("@Collection", method);
                    CmdSql.Parameters.AddWithValue("@Installment", Installment.Text);
                    CmdSql.Parameters.AddWithValue("@InstallmentAmount", total / Convert.ToDouble(Installment.Text));
                    CmdSql.Parameters.AddWithValue("@Total", total);
                    CmdSql.Parameters.AddWithValue("@Balance", total);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime = DateTime.Today;

                    string table = "Loan Details";
                    string type = "Update";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);
                    MessageBox.Show("Successfully Updated");
                }
                Save.Content = "Insert";
            }
    
            
            LoanDetails data = new LoanDetails();
            LoanDetails.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new LoanDetails().PublishPDF(getDate.FromDate, getDate.ToDate);
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
                string query = "SELECT * From LaonDetails WHERE LoanDetails_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["LoanDetails_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    AccountId.Text = (string)reader["LoanDetails_Account"];
                    Sector.Text = reader["LoanDetails_Sector"].ToString();
                    Amount.Text = reader["LoanDetails_Amount"].ToString();
                    ServiceCharge.Text = reader["LoanDetails_Service"].ToString();
                    SanctionDate.SelectedDate = (DateTime)reader["LoanDetails_Sanction"];
                    LifeTime.Text = reader["LoanDetails_Lifetime"].ToString();
                    Installment.Text = reader["LoanDetails_Installment"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM LoanDetails WHERE Security_Id = " + handle.FirstInput, con))
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
                    LoanDetails data = new LoanDetails();
                    LoanDetails.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }


        #endregion

      
    }
}
