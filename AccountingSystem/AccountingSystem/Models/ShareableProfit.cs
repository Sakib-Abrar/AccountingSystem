using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class ShareableProfit : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_expenses;
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
                if (m_deposit != value)
                {
                    m_deposit = value;
                }
                OnPropertyChanged("Deposit");
                _firstLoad = false;
            }
        }
        public double? Expenses
        {
            get
            {
                return m_expenses;
            }
            set
            {
                if (m_expenses != value)
                {
                    m_expenses = value;
                }
                OnPropertyChanged("Expenses");
            }
        }
        public double Remains { get; set; }
        public double Previous { get; set; }

        #region PopulateTable
        public List<ShareableProfit> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<ShareableProfit> entries = new List<ShareableProfit>();
            string query = "SELECT * From ShareableProfit";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new ShareableProfit()
                {
                    ID = (int)reader["Shareable_Id"],
                    Date = (DateTime)reader["Shareable_Date"],
                    Previous = (double)reader["Shareable_Previous"],
                    Deposit = (double)reader["Shareable_Deposit"],
                    Expenses = (double)reader["Shareable_Expenses"],
                    Remains = (double)reader["Shareable_Remains"],
                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM ShareableProfit ORDER BY Shareable_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Shareable_Id"] + 1;
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
                case "Expenses":
                    if (!double.TryParse(Deposit.ToString(), out uselessParse))
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
            string pageTitle = "Shareable Profit";
            float[] size = new float[] { 4, 4, 3, 3, 3, 4};
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Previous", "Deposit", "Expenses", "Remains"};
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM ShareableProfit WHERE CAST(Shareable_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Shareable_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Shareable_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Shareable_Previous"].ToString());
                myPDF.AddToTable(reader["Shareable_Deposit"].ToString());
                myPDF.AddToTable(reader["Shareable_Expenses"].ToString());
                myPDF.AddToTable(reader["Shareable_Remains"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}