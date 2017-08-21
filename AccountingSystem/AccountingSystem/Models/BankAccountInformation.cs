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
        public double ID { get; set; }

        #region PopulateTable
        public List<BankAccountInformation> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<BankAccountInformation> entries = new List<BankAccountInformation>();
            string query = "SELECT * From BankAccountInformation";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new BankAccountInformation()
                {
                    ID = (int)reader["Bank_Id"],
                    Date = (DateTime)reader["Bank_Date"],
                    Deposit = (double)reader["Bank_Deposit"],
                    Interest = (double)reader["Bank_Interest"],
                    Withdraw = (double)reader["Bank_Withdraw"],
                    ServiceCharge = (double)reader["Bank_ServiceCharge"],
                    Remains = (double)reader["Bank_Remains"],
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
    }
}
