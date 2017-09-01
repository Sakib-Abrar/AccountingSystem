using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class CashInformation : INotifyPropertyChanged, IDataErrorInfo
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
        public DateTime Date { get; set; }
    
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
        public double Remains { get; set; }
        public double Previous { get; set; }
        public double Total { get; set; }
        public double ID { get; set; }

        #region PopulateTable
        public List<CashInformation> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<CashInformation> entries = new List<CashInformation>();
            string query = "SELECT * From CashInformation";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new CashInformation()
                {
                    ID = (int)reader["Cash_Id"],
                    Date = (DateTime)reader["Cash_Date"],
                    Previous = (double)reader["Cash_Previous"],
                    Deposit = (double)reader["Cash_Deposit"],
                    Expenses = (double)reader["Cash_Expenses"],
                    Remains = (double)reader["Cash_Remains"],
                    Total = (double)reader["Cash_Total"],
                });
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
    }
}
