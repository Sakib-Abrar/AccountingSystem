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
        public OfficeRentView()
        {
            InitializeComponent();
            DataContext = new OfficeRent();
            OfficeRent data = new OfficeRent();
            officeRent.ItemsSource = data.GetData();
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
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Month", Month.Text);
                CmdSql.Parameters.AddWithValue("@Advance", Advance.Text);
                CmdSql.Parameters.AddWithValue("@Rent", Rent.Text);
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            OfficeRent data = new OfficeRent();
            officeRent.ItemsSource = data.GetData();
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Print_Data(object sender, RoutedEventArgs e)
        {

        }
    }
}
