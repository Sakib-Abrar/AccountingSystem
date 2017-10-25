using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AccountingSystem.Models;
using System.Diagnostics;
using AccountingSystem.Controller;
using System.Data.SqlClient;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MemberInfoView.xaml
    /// </summary>
    public partial class MemberInfoView : Page
    {
        Members Object;
        private string stuff_name;
        private string stuff_pass;
        private int Id;
        private DateTime? dateTime;

        public MemberInfoView()
        {
            InitializeComponent();
            Object = new Members();
            DataContext = Object;
        }

        public void SearchWithID(int member_id)
        {
            Object.GetData(member_id);
        }
        public void SearchWithUnknown(string member_unknown)
        {
            try
            {
                if (member_unknown[0] == '0')
                    Object.GetDataUnknown(member_unknown);
                else
                {
                    int member_ID = Int32.Parse(member_unknown);
                    Object.GetData(member_ID);
                }
            }
            catch (Exception ex)
            {
                Object.GetDataUnknown(member_unknown);
            }
        }

        private void Print_Data(object sender, RoutedEventArgs e)
        {
            Object.PublishPDF();
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

                    using (SqlCommand command = new SqlCommand("DELETE FROM Member WHERE MemberId = " + handle.FirstInput, con))
                    {
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    Id = Convert.ToInt32(handle.FirstInput);
                    dateTime = DateTime.Today;
                    string table = "Members";
                    string type = "Removed";
                    string color = "Red";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, Id, dateTime, color);

                    conn.CloseConnection();
                    Members data = new Members();
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
                MemberEntryView MemEntryObj = new MemberEntryView();
                MemEntryObj.SetForEdit(handle.FirstInput);
                this.NavigationService.Navigate(MemEntryObj);

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(label_MemberID.Content);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM Member WHERE MemberId < " + id + " ORDER BY MemberId DESC";
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
            string query = "SELECT TOP 1 * FROM Member WHERE MemberId > " + id + " ORDER BY MemberId ASC";
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