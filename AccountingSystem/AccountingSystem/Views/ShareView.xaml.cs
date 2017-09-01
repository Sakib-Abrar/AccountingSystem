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
    /// Interaction logic for ShareView.xaml
    /// </summary>
    public partial class ShareView : Page
    {
        public ShareView()
        {
            InitializeComponent();
            Share data = new Share();
            share.ItemsSource = data.GetData();
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
            if (CheckForError(Collection) || CheckForError(Profit) || CheckForError(Withdraw))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }


            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [Share] (Share_Date, Share_Collection, Share_Profit, Share_Withdraw, Share_Remains) VALUES (@Date, @Collection, @Profit, @Withdraw, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Collection", Collection.Text);
                CmdSql.Parameters.AddWithValue("@Profit", Profit.Text);
                CmdSql.Parameters.AddWithValue("@Withdraw", Withdraw.Text);
                CmdSql.Parameters.AddWithValue("@Remains", Convert.ToDouble(Collection.Text) + Convert.ToDouble(Profit.Text) - Convert.ToDouble(Withdraw.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            Share data = new Share();
            share.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new Share().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
