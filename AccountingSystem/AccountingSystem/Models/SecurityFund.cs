using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class SecurityFund : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_details="";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private int m_id;
        private double? m_deposit;
        private double? m_expenses ;
        private DateTime? m_date = Login.GlobalDate;
        public int SelectedIndex { get; set; }
        public DateTime? Date {
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
        public string Details {
            get {
                return m_details;
            }
            set
            {
                m_details = value;
                OnPropertyChanged("Details");
                _firstLoad = false;
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
        public double? Expenses
        {
            get
            {
                return m_expenses;
            }
            set
            {
                m_expenses = value;
                OnPropertyChanged("Expenses");
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
        public double Remains { get; set; }
        

        #region PopulateTable
        public List<SecurityFund> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<SecurityFund> entries = new List<SecurityFund>();
            string query = "SELECT * From SecurityFund";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new SecurityFund() {
                ID = (int)reader["Security_Id"],
                Date = (DateTime)reader["Security_Date"],
                Details = (string)reader["Security_Details"],
                Deposit = (double)reader["Security_Deposit"],
                Expenses = (double)reader["Security_Expenses"],
                Remains = (double)reader["Security_Remains"],
                });
            }
           
            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM SecurityFund ORDER BY Security_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Security_Id"]+1;
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
                case "Details": // property name
                    if (string.IsNullOrWhiteSpace(Details))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Deposit":
                    if (!double.TryParse(Deposit.ToString(),out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Expenses":
                    if (!double.TryParse(Expenses.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "ID":
                    break;
            }

            return validationMessage;
        }
        #endregion

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate,DateTime? ToDate)
        {
            string pageTitle = "Security Fund";
            float[] size=new float[] { 4, 2, 3, 4, 3, 3};
            string[] tableHeaders = new String[]{ "Entry No.","Date","Details","Deposit","Expenses","Remains"};
            PDF myPDF=new PDF(pageTitle,size,tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM SecurityFund WHERE CAST(Security_Date AS date) BETWEEN '" + FDate + "' and '" + TDate+"'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Security_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Security_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Security_Details"].ToString());
                myPDF.AddToTable(reader["Security_Deposit"].ToString());
                myPDF.AddToTable(reader["Security_Expenses"].ToString());
                myPDF.AddToTable(reader["Security_Remains"].ToString());
            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
