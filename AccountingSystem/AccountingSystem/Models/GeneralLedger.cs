using System;
using System.ComponentModel;

namespace AccountingSystem.Models
{
    class GeneralLedger : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        private int uselessParseInt;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_details = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private double? m_deposit;
        private double? m_withdraw;
        public int SelectedIndex { get; set; }
        private int m_accountno;
        private DateTime? m_date = Login.GlobalDate;
        private string m_name;

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
                return m_accountno;
            }
            set
            {
                m_accountno = value;
                OnPropertyChanged("ID");
            }
        }

        public int AccountNo
        {
            get
            {
                return m_accountno;
            }
            set
            {
                m_accountno = value;
                OnPropertyChanged("AccountNo");
            }
        }
        public string Details
        {
            get
            {
                return m_details;
            }
            set
            {
                m_details = value;
                OnPropertyChanged("Details");
                _firstLoad = false;
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
        public double Balance { get; set; }

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
                case "Name": // property name
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "AccountNo":
                    if (!int.TryParse(AccountNo.ToString(), out uselessParseInt))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Deposit":
                    if (!double.TryParse(Deposit.ToString(), out uselessParse))
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
