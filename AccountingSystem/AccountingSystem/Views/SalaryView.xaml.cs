using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;
namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for SalaryView.xaml
    /// </summary>
    public partial class SalaryView : Page
    {
        public SalaryView()
        {
            InitializeComponent();
            DataContext = new Salary();
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();


        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            //double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [Salary] (Salary_Date, Salary_Amount, Salary_Bonus, Salary_Total) VALUES (@Date, @Amount, @Bonus, @Total)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                CmdSql.Parameters.AddWithValue("@Bonus", Bonus.Text);
                CmdSql.Parameters.AddWithValue("@Total", Convert.ToDouble(Amount.Text) + Convert.ToDouble(Bonus.Text));
                 CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();
        }
    }
}
