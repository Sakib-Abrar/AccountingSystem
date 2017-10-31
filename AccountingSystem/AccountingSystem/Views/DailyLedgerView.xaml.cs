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

    public partial class DailyLedgerView : Page
    {
        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;
        public string method = "Daily";
        public DailyLedgerView()
        {
          
            InitializeComponent();
            LoanLedger data = new LoanLedger();
            DailyLedger.ItemsSource = data.GetData(method);
            DataContext = data;
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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


        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Loan) || CheckForError(Collection) || CheckForError(Installment))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            
            if ((string)Save.Content == "Insert")
            {
                  int id = Convert.ToInt32(EntryNo.Text);
                  string collection = Collection.Text;
                  string method = "Daily";
                  string collector = Collector.Text;
                  string loan = Loan.Text;
                  DateTime nextDate = Convert.ToDateTime(NextDate.Text);
                  double balance =Convert.ToDouble( Balance.Text);
                  string installment = Installment.Text;
                  LoanLedger query = new LoanLedger();
                  query.InsertTable(collection,method,collector,loan,installment, balance, nextDate, id, sender,e);
            }
            else
            {
                double remain = this.edited_total();
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("UPDATE [SecurityFund] SET Security_Date = @Date , Security_Details = @Details, Security_Deposit = @Deposit, Security_Expenses = @Expenses, Security_Remains = @Remains WHERE Security_Id=" + EntryNo.Text, conn);
                    conn.Open();
                   // CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                   // CmdSql.Parameters.AddWithValue("@Details", Details.Text);
                   // CmdSql.Parameters.AddWithValue("@Deposit", Deposit.Text);
                   // CmdSql.Parameters.AddWithValue("@Expenses", Expenses.Text);
                    //CmdSql.Parameters.AddWithValue("@Remains", remain + Convert.ToDouble(Deposit.Text) - Convert.ToDouble(Expenses.Text));
                    //CmdSql.ExecuteNonQuery();
                    conn.Close();

                    //Inserting value in Entry table

                    Id = Convert.ToInt32(EntryNo.Text);
                    dateTime =(DateTime)Login.GlobalDate;

                    string table = "Security Fund";
                    string type = "Updated";
                    string color = "Blue";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                }
                Save.Content = "Insert";
                MessageBox.Show("Successfully Updated");
            }

            LoanLedger data = new LoanLedger();
            DailyLedger.ItemsSource = data.GetData(method);
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
                   // Date.SelectedDate = (DateTime)reader["Security_Date"];
                   // Details.Text = (string)reader["Security_Details"];
                   // Deposit.Text = reader["Security_Deposit"].ToString();
                    //Expenses.Text = reader["Security_Expenses"].ToString();
                }

                conn.CloseConnection();
                Save.Content = "Update";
            }
        }
        #endregion
        #region LoanEntry

        private void Loan_Click(object sender, RoutedEventArgs e)
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
                string query = "SELECT * From LoanDetails WHERE LoanDetails_Id = " + handle.FirstInput + "And CONVERT(VARCHAR, LoanDetails_Collection) = '" + method + "'";
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                {
                    MessageBox.Show("This Loan Id is not Valid.Collection type is not " + method);
                    return;
                }
                while (reader.Read())
                {
                    Connection conn2 = new Connection();
                    conn2.OpenConection();
                    string query2 = "SELECT * From [Member] WHERE MemberId ="+ reader["LoanDetails_Account"]+" ";
                    SqlDataReader reader2 = conn2.DataReader(query2);
                    if (reader2 == null)
                        return;
                    while (reader2.Read())
                    {
                        Name.Text = reader2["MemberName"].ToString();
                    }
                    conn2.CloseConnection();
                    Loan.Text = reader["LoanDetails_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    double total = Convert.ToDouble(reader["LoanDetails_InstallmentAmount"]) + Convert.ToDouble(reader["LoanDetails_Fine"]);
                    Account.Text = reader["LoanDetails_Account"].ToString();
                    LastPaid.Text = reader["LoanDetails_LastPaid"].ToString();       
                    TotalLoan.Text = reader["LoanDetails_Total"].ToString();
                    Amount.Text = reader["LoanDetails_InstallmentAmount"].ToString();
                    Sector.Text = reader["LoanDetails_Sector"].ToString();
                    LoanAmount.Text = reader["LoanDetails_Amount"].ToString();
                    Sanction.Text = reader["LoanDetails_Sanction"].ToString();
                    InstallmentNo.Text = reader["LoanDetails_Installment"].ToString();
                    if ((double)reader["LoanDetails_Balance"] != 0)
                    {
                        NextDate.Text = reader["LoanDetails_NextDate"].ToString();
                        Balance.Text = reader["LoanDetails_Balance"].ToString();
                        Fine.Text = reader["LoanDetails_Fine"].ToString();
                        Total.Text = total.ToString();
                    }
                    else
                    {
                        NextDate.Text = "N/A";
                        Balance.Text = "N/A";
                        Fine.Text = "N/A";
                        Total.Text = "Closed";
                    }
                   
                }
                conn.CloseConnection();
                //Save.Content = "Update";
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
                    dateTime = (DateTime)Login.GlobalDate;
                    string table = "Security Fund";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    SecurityFund data = new SecurityFund();
                   // SecurityFund.ItemsSource = data.GetData();
                    DataContext = data;
                }
            }
        }

        #endregion


    }
}
