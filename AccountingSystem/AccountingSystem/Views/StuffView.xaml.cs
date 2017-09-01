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
    /// Interaction logic for StuffView.xaml
    /// </summary>
    public partial class StuffView : Page
    {
        string stuffType;
        public StuffView()
        {
            InitializeComponent();
            Stuff data = new Stuff();
            stuff.ItemsSource = data.GetData();
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
            if (Type_Stuff.IsChecked == true )
            {
                stuffType = "stuff";
            }
            else if (Type_Admin.IsChecked == true)
            {
                stuffType = "admin";
            }
            if (CheckForError(Name) || CheckForError(Cell) || CheckForError(Password))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

           using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {
                try {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Stuff] (Stuff_Id, Stuff_Join, Stuff_Name, Stuff_Cell, Stuff_Password, Stuff_Type) VALUES (@Id, @Date, @Name, @Cell, @Password, @Type)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Id", ID.Text);
                    CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@Name", Name.Text);
                    CmdSql.Parameters.AddWithValue("@Cell", Cell.Text);
                    CmdSql.Parameters.AddWithValue("@Password", Password.Text);
                    CmdSql.Parameters.AddWithValue("@Type", stuffType);
                    CmdSql.ExecuteNonQuery();
                    conn.Close(); }
                catch {
                    MessageBox.Show("Error! Cell Number should be Unique. Check the entries TWICE");
                    return;
                }

            }
            Stuff data = new Stuff();
            stuff.ItemsSource = data.GetData();
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
    }
}
