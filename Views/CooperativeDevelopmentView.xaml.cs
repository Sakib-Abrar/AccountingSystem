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
    /// Interaction logic for CooperativeDevelopmentView.xaml
    /// </summary>
    public partial class CooperativeDevelopmentView : Page
    {
        public CooperativeDevelopmentView()
        {
            InitializeComponent();
            DataContext = new CooperativeDevelopment();
            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
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
        private double last_remains()
        {
            Connection conn = new Connection();
            double remains = 0.00;
            string query = "SELECT TOP 1 * FROM CooperativeDevelopment ORDER BY Cooperative_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                remains = (double)reader["Cooperative_Remains"];
            }
            conn.CloseConnection();
            return remains;
        }

        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForError(Current) || CheckForError(Paid))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {
                double previous = this.last_remains();
                SqlCommand CmdSql = new SqlCommand("INSERT INTO [CooperativeDevelopment] (Cooperative_Date, Cooperative_Current, Cooperative_Paid, Cooperative_Previous, Cooperative_Remains) VALUES (@Date, @Current, @Paid, @Previous, @Remains)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", new DateTime(2017, 2, 23));
                CmdSql.Parameters.AddWithValue("@Current", Current.Text);
                CmdSql.Parameters.AddWithValue("@Paid", Paid.Text);
                CmdSql.Parameters.AddWithValue("@Previous", previous);
                CmdSql.Parameters.AddWithValue("@Remains", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Paid.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();


            }
            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
        }
    }
}
