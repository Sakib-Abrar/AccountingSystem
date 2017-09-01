using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Windows.Data;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for ReservedFundView.xaml
    /// </summary>
    public partial class ReservedFundView : Page
    {
            public ReservedFundView()
            {
                InitializeComponent();
                DataContext = new ReservedFund();
                ReservedFund data = new ReservedFund();
                reservedFund.ItemsSource = data.GetData();
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
                string query = "SELECT TOP 1 * FROM ReservedFund ORDER BY Reserved_Id DESC";
                conn.OpenConection();
                SqlDataReader reader = conn.DataReader(query);
                while (reader.Read())
                {
                    total = (double)reader["Reserved_Total"];
                }
                conn.CloseConnection();
                return total;
            }
            protected void Save_Click(object sender, RoutedEventArgs e)
            {
                if (CheckForError(Current) || CheckForError(Withdraw))
                {
                    MessageBox.Show("Error!Check Input Again");
                    return;
                }
            double previous = this.last_total();
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [ReservedFund] (Reserved_Date, Reserved_Remaining, Reserved_Current, Reserved_Previous, Reserved_Total,Reserved_Withdraw) VALUES (@ReservedFund_date, @ReservedFund_remainig, @ReservedFund_current, @ReservedFund_previous, @ReservedFund_total,@ReservedFund_withdraw)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@ReservedFund_date", new DateTime(2017, 2, 23));
                    CmdSql.Parameters.AddWithValue("@ReservedFund_remainig", Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.Parameters.AddWithValue("@ReservedFund_current", Current.Text);
                    CmdSql.Parameters.AddWithValue("@ReservedFund_previous", previous);
                    CmdSql.Parameters.AddWithValue("@ReservedFund_total", previous + Convert.ToDouble(Current.Text) - Convert.ToDouble(Withdraw.Text));
                    CmdSql.Parameters.AddWithValue("@ReservedFund_withdraw", Withdraw.Text);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();


                }
                ReservedFund data = new ReservedFund();
                reservedFund.ItemsSource = data.GetData();
            }

        }
    }
