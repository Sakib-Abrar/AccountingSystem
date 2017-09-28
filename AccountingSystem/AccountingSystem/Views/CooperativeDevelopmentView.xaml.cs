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
        private DateTime dateTime;

        private int Id;

        public CooperativeDevelopmentView()
        {
            InitializeComponent();
            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
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
                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                CmdSql.Parameters.AddWithValue("@Current", Current.Text);
                CmdSql.Parameters.AddWithValue("@Paid", Paid.Text);
                CmdSql.Parameters.AddWithValue("@Previous", previous);
                CmdSql.Parameters.AddWithValue("@Remains", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Paid.Text));
                CmdSql.ExecuteNonQuery();
                conn.Close();

                //Inserting value in Entry table
                Connection conn2 = new Connection();

                string query = "SELECT TOP 1 * FROM CooperativeDevelopment ORDER BY Cooperative_Id DESC";
                conn2.OpenConection();
                SqlDataReader reader = conn2.DataReader(query);
                while (reader.Read())
                {
                    Id = (int)reader["Cooperative_Id"];
                    dateTime = (DateTime)reader["Cooperative_Date"];
                }
                conn2.CloseConnection();

                string table = "Co-operative developement";
                string type = "Inserted";
                string color = "Green";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, Id, dateTime, color);


            }
            CooperativeDevelopment data = new CooperativeDevelopment();
            cooperativeDevelopment.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new CooperativeDevelopment().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
