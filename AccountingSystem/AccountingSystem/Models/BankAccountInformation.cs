using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class BankAccountInformation : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_deposit;
        private double? m_interest;
        private double? m_withdraw;
        private double? m_serviceCharge;
        public int SelectedIndex { get; set; }
        private int m_id;
        private DateTime? m_date = Login.GlobalDate;
        public DateTime? Date
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;
                OnPropertyChanged("Date");
            }
        }
        public int ID
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
                OnPropertyChanged("ID");
            }
        }

        public double? Deposit
        {
            get
            {
                return m_deposit;
            }
            set
            {
                m_deposit = value;
                OnPropertyChanged("Deposit");
            }
        }
        public double? Interest
        {
            get
            {
                return m_interest;
            }
            set
            {
                m_interest = value;
                OnPropertyChanged("Interest");
            }
        }
        public double? Withdraw
        {
            get
            {
                return m_withdraw;
            }
            set
            {
                m_withdraw = value;
                OnPropertyChanged("Withdraw");
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
        public double Remains { get; set; }

        #region PopulateTable
        public List<BankAccountInformation> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<BankAccountInformation> entries = new List<BankAccountInformation>();
            string query = "SELECT * From BankAccount";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new BankAccountInformation()
                {
                    ID = (int)reader["BankAccount_Id"],
                    Date = (DateTime)reader["BankAccount_Date"],
                    Deposit = (double)reader["BankAccount_Deposit"],
                    Interest = (double)reader["BankAccount_Interest"],
                    Withdraw = (double)reader["BankAccount_Withdraw"],
                    ServiceCharge = (double)reader["BankAccount_ServiceCharge"],
                    Remains = (double)reader["BankAccount_Remains"],
                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM BankAccount ORDER BY BankAccount_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["BankAccount_Id"] + 1;
            }
            conn.CloseConnection();
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
              
                case "Deposit":
                    if (!double.TryParse(Deposit.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Interest":
                    if (!double.TryParse(Interest.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Withdraw":
                    if (!double.TryParse(Withdraw.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "ServiceCharge":
                    if (!double.TryParse(ServiceCharge.ToString(), out uselessParse))
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
