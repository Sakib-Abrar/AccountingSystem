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
    /// Interaction logic for BalanceSheetView.xaml
    /// </summary>
    public partial class BalanceSheetView : Page
    {
        public BalanceSheetView()
        {
          
            InitializeComponent();
            CreateBalanceSheet();
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void CreateBalanceSheet()
        {
            DateTime date = (DateTime)Login.GlobalDate;
            //end date of current month
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            int compare = DateTime.Compare(date, endDate);
            if (compare == 0)
            {
                //Checking for unique balance sheet!!
                int c = 0;
                Connection conc2 = new Connection();
                string query2 = "Select * from BalanceDate where BalanceDate_Date = '" +date.Date + "' ";
                conc2.OpenConection();
                SqlDataReader reader2 = conc2.DataReader(query2);
                while (reader2.Read())
                {
                    c++;
                    
                }
                conc2.CloseConnection();
                if (c == 0)
                {
                    string Month = date.ToString("MMMM");
                    string Year = date.Year.ToString();
                    Connection conc = new Connection();
                    string query = "Select Member.MemberId ,Member.MemberName,Share.Share_Remains,LoanDetails.LoanDetails_Amount,LoanDetails.LoanDetails_Balance,LoanDetails.LoanDetails_Service,GeneralDepositLedger.GeneralBalance,MonthlyDepositLedger.MonthlyBalance,FixedDepositLedger.FixedBalance from Member  inner join Share on Member.MemberId =Share.Member_Id inner join LoanDetails on Member.MemberId =LoanDetails.LoanDetails_Account inner join GeneralDepositLedger on Member.MemberId = GeneralDepositLedger.MemberId inner join MonthlyDepositLedger on Member.MemberId = MonthlyDepositLedger.MemberId inner join FixedDepositLedger on Member.MemberId = FixedDepositLedger.MemberId";
                    conc.OpenConection();
                    SqlDataReader reader = conc.DataReader(query);
                    while (reader.Read())
                    {
                        int Account = (int)reader["MemberId"];
                        string MemberName = (string)reader["MemberName"];
                        double Share = (double)reader["Share_Remains"];
                        double ServiceCharge = ((double)reader["LoanDetails_Amount"] * (double)reader["LoanDetails_Service"]) / 100;
                        double Loan = (double)reader["LoanDetails_Balance"] - ServiceCharge;
                        double Weekly = (double)reader["GeneralBalance"];
                        double Monthly = (double)reader["MonthlyBalance"];
                        double Fixed = (double)reader["FixedBalance"];


                        using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                        {
                            SqlCommand CmdSql = new SqlCommand("INSERT INTO [BalanceSheet] (BalanceSheet_Account, BalanceSheet_Share, BalanceSheet_Loan,BalanceSheet_Service,BalanceSheet_Weekly,BalanceSheet_Monthly,BalanceSheet_Fixed,BalanceSheet_Month,BalanceSheet_Year) VALUES (@Account, @Share, @Loan, @Service, @Weekly, @Monthly, @Fixed, @Month, @Year)", conn);
                            conn.Open();
                            CmdSql.Parameters.AddWithValue("@Account", Account);
                            CmdSql.Parameters.AddWithValue("@Share", Share);
                            CmdSql.Parameters.AddWithValue("@Loan", Loan);
                            CmdSql.Parameters.AddWithValue("@Service", ServiceCharge);
                            CmdSql.Parameters.AddWithValue("@Weekly", Weekly);
                            CmdSql.Parameters.AddWithValue("@Monthly", Monthly);
                            CmdSql.Parameters.AddWithValue("@Fixed", Fixed);
                            CmdSql.Parameters.AddWithValue("@Month", Month);
                            CmdSql.Parameters.AddWithValue("@Year", Year);
                            CmdSql.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    //inserting BalanceSheet date for creating one balance sheet per month
                    using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                    {
                        SqlCommand CmdSql = new SqlCommand("INSERT INTO [BalanceDate] ( BalanceDate_Date) VALUES (@Date)", conn);
                        conn.Open();
                        CmdSql.Parameters.AddWithValue("@Date", date);
                        CmdSql.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        private void BalanceSheet_Click(object sender, RoutedEventArgs e)
        {
            string month = Month.Text;
            string year = Year.Text;
            BalanceSheet data = new BalanceSheet();
            balanceSheet.ItemsSource = data.GetData(month, year);
            DataContext = data;

        }
    }
}
