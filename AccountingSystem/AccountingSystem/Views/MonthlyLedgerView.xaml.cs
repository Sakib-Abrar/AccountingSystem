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
    /// Interaction logic for MonthlyLedgerView.xaml
    /// </summary>

    public partial class MonthlyLedgerView : Page
    {
        private int Id;
        private DateTime? dateTime;
        private string stuff_pass;
        private string stuff_name;
        public string method = "Monthly";
        public MonthlyLedgerView()
        {

            InitializeComponent();
            LoanLedger data = new LoanLedger();
            MonthlyLedger.ItemsSource = data.GetDataIndividual(method, 0, 0);
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

            int id = Convert.ToInt32(EntryNo.Text);
            string collection = Collection.Text;
            string collector = Collector.Text;
            string loan = Loan.Text;
            DateTime nextDate = Convert.ToDateTime(NextDate.Text);
            double balance = Convert.ToDouble(Balance.Text);
            string installment = Installment.Text;
            LoanLedger query = new LoanLedger();

            if ((string)Save.Content == "Insert")
            {
                
                query.InsertTable(collection, method, collector, loan, installment, balance, nextDate, id, sender, e);
            }
            else
            {
                query.UpdateTable(collection, method, collector, loan, installment, balance, nextDate, id, sender, e);
                Save.Content = "Insert";
            }

            LoanLedger data = new LoanLedger();
            int tempId = Convert.ToInt32(Loan.Text);
            MonthlyLedger.ItemsSource = data.GetDataIndividual(method, tempId, 2);
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new LoanLedger().PublishPDFIndividualLedger(getDate.FromDate, getDate.ToDate, Convert.ToInt32(Loan.Text), method);
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
                string query = "SELECT * From LoanCollection WHERE LoanCollection_Id = " + handle.FirstInput;
                SqlDataReader reader = conn.DataReader(query);
                if (reader == null)
                    return;
                while (reader.Read())
                {
                    EntryNo.Text = reader["LoanCollection_Id"].ToString();
                    Id = Convert.ToInt32(EntryNo.Text);
                    Collection.Text = reader["LoanCollection_Collection"].ToString();
                    InstallmentNo.Text = reader["LoanCollection_Installment"].ToString();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM LoanCollection WHERE LoanCollection_Id = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = (DateTime)Login.GlobalDate;
                    string table = "Loan Collection Ledger";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();

                    LoanLedger data = new LoanLedger();
                    int tempId = Convert.ToInt32(Loan.Text);
                    MonthlyLedger.ItemsSource = data.GetDataIndividual(method, tempId, 2);
                    DataContext = data;
                }
            }
        }


        #endregion

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            LoanLedger data = new LoanLedger();
            int tempId = Convert.ToInt32(Loan.Text);
            MonthlyLedger.ItemsSource = data.GetDataIndividual(method, tempId, 0);
            DataContext = data;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            LoanLedger data = new LoanLedger();
            int tempId = Convert.ToInt32(Loan.Text);
            MonthlyLedger.ItemsSource = data.GetDataIndividual(method, tempId, 1);
            DataContext = data;
        }

        private void Loan_LostFocus(object sender, RoutedEventArgs e)
        {
            LoanLedger data = new LoanLedger();
            int tempId = Convert.ToInt32(Loan.Text);
            MonthlyLedger.ItemsSource = data.GetDataIndividual(method, tempId, 2);
            DataContext = data;
        }
    }
}
