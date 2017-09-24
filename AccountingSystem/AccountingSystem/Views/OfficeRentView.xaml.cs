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
    /// Interaction logic for OfficeRentView.xaml
    /// </summary>
    public partial class OfficeRentView : Page
    {
        private DateTime dateTime;

        private int Id;

        public OfficeRentView()
        {
            InitializeComponent();
            OfficeRent data = new OfficeRent();
            officeRent.ItemsSource = data.GetData();
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
            if (CheckForError(Advance) || CheckForError(Rent))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [OfficeRent] (Office_Date, Office_Month, Office_Advance, Office_Rent) VALUES (@Date, @Month, @Advance, @Rent)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                CmdSql.Parameters.AddWithValue("@Month", Month.Text);
                CmdSql.Parameters.AddWithValue("@Advance", Advance.Text);
                CmdSql.Parameters.AddWithValue("@Rent", Rent.Text);
                CmdSql.ExecuteNonQuery();
                conn.Close();

                //Inserting value in Entry table
                Connection conn2 = new Connection();

                string query = "SELECT TOP 1 * FROM OfficeRent ORDER BY Office_Id DESC";
                conn2.OpenConection();
                SqlDataReader reader = conn2.DataReader(query);
                while (reader.Read())
                {
                    Id = (int)reader["Office_Id"];
                    dateTime = (DateTime)reader["Office_Date"];
                }
                conn2.CloseConnection();




                string table = "Office Rent";
                string type = "Inserted";
                string color = "Green";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, Id, dateTime, color);



            }
            OfficeRent data = new OfficeRent();
            officeRent.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new OfficeRent().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        
    }
}
