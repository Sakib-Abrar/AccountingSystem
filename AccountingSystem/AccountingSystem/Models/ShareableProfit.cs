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
                if (m_deposit != value)
                {
                    m_deposit = value;
                }
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
                if (m_expenses != value)
                {
                    m_expenses = value;
                }
                OnPropertyChanged("Expenses");
            }
        }
        public double Remains { get; set; }
        public double Previous { get; set; }
        public double ID { get; set; }

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