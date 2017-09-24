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
    /// Interaction logic for SalaryView.xaml
    /// </summary>
    public partial class SalaryView : Page
    {
        private int Id;
        private DateTime dateTime;

        public SalaryView()
        {
            InitializeComponent();
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();
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
            if (CheckForError(Amount) || CheckForError(Bonus))
            {
                MessageBox.Show("Error!Check Input Again");
                return;
            }
            //double remains = this.last_remains();
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("INSERT INTO [Salary] (Salary_Date, Salary_Amount, Salary_Bonus, Salary_Total) VALUES (@Date, @Amount, @Bonus, @Total)", conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", Date.SelectedDate);
                CmdSql.Parameters.AddWithValue("@Amount", Amount.Text);
                CmdSql.Parameters.AddWithValue("@Bonus", Bonus.Text);
                CmdSql.Parameters.AddWithValue("@Total", Convert.ToDouble(Amount.Text) + Convert.ToDouble(Bonus.Text));
                 CmdSql.ExecuteNonQuery();
                conn.Close();

                //Inserting value in Entry table
                Connection conn2 = new Connection();

                string query = "SELECT TOP 1 * FROM Salary ORDER BY Salary_Id DESC";
                conn2.OpenConection();
                SqlDataReader reader = conn2.DataReader(query);
                while (reader.Read())
                {
                    Id = (int)reader["Salary_Id"];
                    dateTime = (DateTime)reader["Salary_Date"];
                }
                conn2.CloseConnection();




                string table = "Salary";
                string type = "Inserted";
                string color = "Green";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, Id, dateTime, color);

            }
            Salary data = new Salary();
            salary.ItemsSource = data.GetData();
            DataContext = data;
        }
        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new Salary().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
