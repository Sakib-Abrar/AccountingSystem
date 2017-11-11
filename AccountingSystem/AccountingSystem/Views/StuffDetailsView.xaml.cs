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
    /// Interaction logic for StuffDetailsView.xaml
    /// </summary>
    public partial class StuffDetailsView : Page
    {
        Stuff Object;
        private string stuff_name;
        private string stuff_pass;
        private int Id;
        private DateTime? dateTime;
        public StuffDetailsView()
        {
            InitializeComponent();
            Object = new Stuff();
            DataContext = Object;
        }

        public void SearchWithID(int stuff_id)
        {
            Object.GetData(stuff_id);
        }

        public void SearchWithUnknown(string stuff_unknown)
        {
            try
            {
                if (stuff_unknown[0] == '0')
                    Object.GetDataUnknown(stuff_unknown);
                else
                {
                    int stuff_ID = Int32.Parse(stuff_unknown);
                    Object.GetData(stuff_ID);
                }
            }
            catch (Exception ex)
            {
                Object.GetDataUnknown(stuff_unknown);
            }
        }



        private void Print_Data(object sender, RoutedEventArgs e)
        {
            Object.PublishPDF();
        }


        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            RemoveDialogView handle = new RemoveDialogView();
            handle.ChangeLabelForStuff();
            if (handle.ShowDialog() == true)
            {
                using (SqlConnection con = new SqlConnection(@Connection.ConnectionString))
                {
                    if (handle.FirstInput != handle.SecondInput)
                    {
                        MessageBox.Show("Stuff ID did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                    //next click
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditDialogView handle = new EditDialogView();
            handle.ChangeLabelForStuff();
            if (handle.ShowDialog() == true)
            {
                if (handle.FirstInput != handle.SecondInput)
                {
                    MessageBox.Show("Stuff ID did not match.Try again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                StuffEntryView StfEntryObj = new StuffEntryView();
                StfEntryObj.SetForEdit(handle.FirstInput);
                this.NavigationService.Navigate(StfEntryObj);

            }
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(label_StuffID.Content);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM Stuff WHERE Stuff_Id < " + id + " ORDER BY Stuff_Id DESC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                id = (int)reader["Stuff_Id"];
            }
            conn.CloseConnection();
            this.SearchWithID(id);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(label_StuffID.Content);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT TOP 1 * FROM Stuff WHERE Stuff_Id > " + id + " ORDER BY Stuff_Id ASC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                id = (int)reader["Stuff_Id"];
            }
            conn.CloseConnection();
            this.SearchWithID(id);
        }
    }


}
