using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;

namespace AccountingSystem.Models
{
    class ReservedFund : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private double? m_current;
        private double? m_withdraw;
        public DateTime Date { get; set; }
        public double? Current {
            get
            {
                return m_current;
            }
            set
            {
                m_current = value;
                OnPropertyChanged("Current");
                _firstLoad = false;
            }
        }
        public double Total { get; set; }
        public double Previous { get; set; }
        public double? Withdraw {
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
        public double Remaining { get; set; }



        public double ID { get; set; }

        public List<ReservedFund> GetData()
        {

            Connection conn = new Connection();
            conn.OpenConection();
            List<ReservedFund> entries = new List<ReservedFund>();

            string query = "SELECT * From ReservedFund";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new ReservedFund()
                {
                    ID = (int)reader["Reserved_Id"],
                    Date = (DateTime)reader["Reserved_Date"],
                    Previous = (double)reader["Reserved_Previous"],
                    Current = (double)reader["Reserved_Current"],
                    Remaining = (double)reader["Reserved_Remaining"],
                    Withdraw = (double)reader["Reserved_Withdraw"],
                    Total = (double)reader["Reserved_Total"],
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
            if (_firstLoad)
                return validationMessage;
            switch (propertyName)
            {
                case "Current":
                    if (!double.TryParse(Current.ToString(), out uselessParse))
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
            }

            return validationMessage;
        }
        #endregion
    }
}
