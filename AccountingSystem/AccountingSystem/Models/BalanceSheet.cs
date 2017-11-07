using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class BalanceSheet : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;

        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private string m_name;
  
        private double? m_share;
        private double? m_loan;
        private double? m_serviceCharge;
        private double? m_weekly;
        private double? m_monthly;
        private double? m_fixed;
        public int SelectedIndex { get; set; }
        private int m_id;
        private int m_year;
        private DateTime? m_date = Login.GlobalDate;
        public int Year
        {
            get
            {
                return m_year;
            }
            set
            {
                m_year = value;
                OnPropertyChanged("Year");
            }
        }

        public int Account
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
                OnPropertyChanged("Account");
            }
        }

        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                OnPropertyChanged("Name");
            }
        }
        public double? Share
        {
            get
            {
                return m_share;
            }
            set
            {
                m_share = value;
                OnPropertyChanged("Share");
            }
        }
        public double? Loan
        {
            get
            {
                return m_loan;
            }
            set
            {
                m_loan = value;
                OnPropertyChanged("Loan");
            }
        }
        public double? ServiceCharge
        {
            get
            {
                return m_serviceCharge;
            }
            set
            {
                m_serviceCharge = value;
                OnPropertyChanged("ServiceCharge");
            }
        }
        public double? Fixed
        {
            get
            {
                return m_fixed;
            }
            set
            {
                m_fixed = value;
                OnPropertyChanged("Fixed");
            }
        }
        public double? Weekly
        {
            get
            {
                return m_weekly;
            }
            set
            {
                m_weekly = value;
                OnPropertyChanged("Weekly");
            }
        }
        public double? Monthly
        {
            get
            {
                return m_monthly;
            }
            set
            {
                m_monthly = value;
                OnPropertyChanged("Monthly");
            }
        }
        public double Remains { get; set; }

        #region PopulateTable
        public List<BalanceSheet> GetData(string month,string year)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<BalanceSheet> entries = new List<BalanceSheet>();
            string query = "SELECT * FROM [BalanceSheet] JOIN Member ON [BalanceSheet].BalanceSheet_Account = MemberId WHERE [BalanceSheet].BalanceSheet_Month='"+month+"' And [BalanceSheet].BalanceSheet_Year='"+year+"' ";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new BalanceSheet()
                {
                    Account = (int)reader["MemberId"],
                    Name = (string)reader["MemberName"],
                    Share = (double)reader["BalanceSheet_Share"],
                    Loan = (double)reader["BalanceSheet_Loan"],
                    ServiceCharge = (double)reader["BalanceSheet_Service"],
                    Weekly = (double)reader["BalanceSheet_Weekly"],
                    Monthly = (double)reader["BalanceSheet_Monthly"],
                    Fixed = (double)reader["BalanceSheet_Fixed"],

                });
            }

      
            return entries;
        }
        #endregion

        #region Validation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get { return "...."; }
        }

        /// <summary>
        /// Will be called for each and every property when ever its value is changed
        /// </summary>
        /// <param name="propertyName">Name of the property whose value is changed</param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                return Validate(propertyName);
            }
        }

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = string.Empty;
            if (_firstLoad)
                return validationMessage;
            switch (propertyName)
            {

                case "Year":
                    if (!double.TryParse(Year.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
         
            }

            return validationMessage;
        }
        #endregion

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Bank Account Information";
            float[] size = new float[] { 3, 3, 3, 3, 3, 3, 3 };
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Deposit", "Interest", "Withdraw", "Service Charge", "Remains" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM BankAccount WHERE CAST(BankAccount_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["BankAccount_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["BankAccount_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["BankAccount_Deposit"].ToString());
                myPDF.AddToTable(reader["BankAccount_Interest"].ToString());
                myPDF.AddToTable(reader["BankAccount_Withdraw"].ToString());
                myPDF.AddToTable(reader["BankAccount_ServiceCharge"].ToString());
                myPDF.AddToTable(reader["BankAccount_Remains"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
