using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class AdmissionFee : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_details = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private double? m_deposit;
        private double? m_collection;
        public int SelectedIndex { get; set; }
        public DateTime Date { get; set; }
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
        public double? Collection
        {
            get
            {
                return m_collection;
            }
            set
            {
                m_collection = value;
                OnPropertyChanged("Collection");
            }
        }
        public double Total { get; set; }
        public double ID { get; set; }

        #region PopulateTable
        public List<AdmissionFee> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<AdmissionFee> entries = new List<AdmissionFee>();
            string query = "SELECT * From AdmissionFee";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new AdmissionFee()
                {
                    ID = (int)reader["Admission_Id"],
                    Date = (DateTime)reader["Admission_Date"],
                    Collection = (double)reader["Admission_Collection"],
                    Total = (double)reader["Admission_Total"],
                   
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
                case "Details": // property name
                    if (string.IsNullOrWhiteSpace(Details))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Deposit":
                    if (!double.TryParse(Deposit.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Collection":
                    if (!double.TryParse(Collection.ToString(), out uselessParse))
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
