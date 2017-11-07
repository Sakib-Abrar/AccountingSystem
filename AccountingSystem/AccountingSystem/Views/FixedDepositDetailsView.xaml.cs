using AccountingSystem.Controller;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for FixedDepositDetailsView.xaml
    /// </summary>
    public partial class FixedDepositDetailsView : Page
    {
        FixedDeposit Object;
        private string stuff_name;
        private string stuff_pass;
        private int Id;
        private DateTime? dateTime;
        public FixedDepositDetailsView()
        {
            InitializeComponent();
            Object = new FixedDeposit();
            DataContext = Object;
        }

        public void SearchWithID(int member_id)
        {
            Object.GetDataDetails(member_id);
        }

        public void SearchWithUnknown(string member_unknown)
        {
            int uselessParseInt;
            if (!int.TryParse(member_unknown, out uselessParseInt))
            {
                Object.GetDataDetailsUnknown(member_unknown);
            }
            else
            {
                int member_ID = Int32.Parse(member_unknown);
                Object.GetDataDetails(member_ID);
            }
        }

        private void Print_Data(object sender, RoutedEventArgs e)
        {
            Object.PublishPDFDetails();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveDialogView handle = new RemoveDialogView();
            handle.ChangeLabelForMember();
            if (handle.ShowDialog() == true)
            {
                using (SqlConnection con = new SqlConnection(@Connection.ConnectionString))
                {
                    if (handle.FirstInput != handle.SecondInput)
                    {
                        MessageBox.Show("Member ID did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM FixedDepositDetails WHERE MemberId = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "FixedDepositDetails";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    GeneralLedger data = new GeneralLedger();
                    //next click
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditDialogView handle = new EditDialogView();
            handle.ChangeLabelForMember();
            if (handle.ShowDialog() == true)
            {
                if (handle.FirstInput != handle.SecondInput)
                {
                    MessageBox.Show("Member ID did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                FixedDepositEntryView GDEntryObj = new FixedDepositEntryView();
                GDEntryObj.SetForEdit(handle.FirstInput);
                GDEntryObj.SaveMember.Content = "Update Account";
                this.NavigationService.Navigate(GDEntryObj);

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(label_MemberID.Content);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM FixedDepositDetails WHERE MemberId < " + id + " ORDER BY MemberId DESC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                id = (int)reader["MemberId"];
            }
            conn.CloseConnection();
            this.SearchWithID(id);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(label_MemberID.Content);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM FixedDepositDetails WHERE MemberId > " + id + " ORDER BY MemberId ASC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                id = (int)reader["MemberId"];
            }
            conn.CloseConnection();
            this.SearchWithID(id);
        }
    }
}