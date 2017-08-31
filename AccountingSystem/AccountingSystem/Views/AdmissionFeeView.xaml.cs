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
    /// Interaction logic for AdmissionFeeView.xaml
    /// </summary>
    public partial class AdmissionFeeView : Page
    {
        public AdmissionFeeView()
        {
            InitializeComponent();
            AdmissionFee data = new AdmissionFee();
            admissionFee.ItemsSource = data.GetData();
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
        private double last_total()
        {
            Connection conn = new Connection();
            double total = 0.00;
            string query = "SELECT TOP 1 * FROM AdmissionFee ORDER BY Admission_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                total = (double)reader["Admission_Total"];
            }
            conn.CloseConnection();
            return total;
        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Collection))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }

            double total = this.last_total();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [AdmissionFee] (Admission_Date, Admission_Collection, Admission_Total) VALUES (@Date, @Collection, @Total)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Collection", Collection.Text);
                CmdSql.Parameters.AddWithValue("@Total", total + Convert.ToDouble(Collection.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            AdmissionFee data = new AdmissionFee();
            admissionFee.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new AdmissionFee().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
