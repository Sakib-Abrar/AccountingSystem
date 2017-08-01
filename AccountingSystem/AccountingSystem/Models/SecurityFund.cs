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
        private string m_name="";
        public DateTime Date { get; set; }
        public int SelectedIndex { get; set; }
        public string Details {
            get {
                return m_name;
            }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                }
                OnPropertyChanged("Details");
            }
        }
        public double Deposit { get; set; }
        public double Expenses { get; set; }
        public double Remains { get; set; }

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
                Date = (DateTime)reader["Security_Date"],
                Details = (string)reader["Security_Details"],
                Deposit = (double)reader["Security_Deposit"],
                Expenses = (double)reader["Security_Expenses"],
                Remains = (double)reader["Security_Remains"],
                });
            }
            conn.CloseConnection();
            return entries;
        }


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
                case "Details": // property name
                    if (string.IsNullOrWhiteSpace(Details))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
            }

            return validationMessage;
        }
        #endregion
    }
}
