using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class OfficeRent : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_advance;
        private double? m_rent;
        public int SelectedIndex { get; set; }
        public DateTime Date { get; set; }
        public double? Advance
        {
            get
            {
                return m_advance;
            }
            set
            {
                if (m_advance != value)
                {
                    m_advance = value;
                }
                OnPropertyChanged("Advance");
                _firstLoad = false;
            }
        }
        public double? Rent
        {
            get
            {
                return m_rent;
            }
            set
            {
                if (m_rent != value)
                {
                    m_rent = value;
                }
                OnPropertyChanged("Rent");
            }
        }
     
        public string Month { get; set; }
        public double ID { get; set; }

        #region PopulateTable
        public List<OfficeRent> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<OfficeRent> entries = new List<OfficeRent>();
            string query = "SELECT * From OfficeRent";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new OfficeRent()
                {
                    ID = (int)reader["Office_Id"],
                    Date = (DateTime)reader["Office_Date"],
                    Month = (string)reader["Office_Month"],
                    Advance = (double)reader["Office_Advance"],
                    Rent = (double)reader["Office_Rent"],
                    
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
                
                case "Advance":
                    if (!double.TryParse(Advance.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Rent":
                    if (!double.TryParse(Rent.ToString(), out uselessParse))
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
