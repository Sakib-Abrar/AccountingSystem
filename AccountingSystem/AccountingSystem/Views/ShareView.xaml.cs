using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;
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
            DataContext = new Share();
            Share data = new Share();
            share.ItemsSource = data.GetData();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected void Save_Click(object sender, RoutedEventArgs e)
        {
         
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
        }
    }
}
