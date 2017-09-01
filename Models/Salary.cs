using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class Salary : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_amount;
        private double? m_bonus;
        public int SelectedIndex { get; set; }
        public DateTime Date { get; set; }
      
        public double? Amount 
        {
            get
            {
                return m_amount;
            }
            set
            {
                if (m_amount != value)
                {
                    m_amount = value;
                }
                OnPropertyChanged("Amount");
                _firstLoad = false;
            }
        }
        public double? Bonus
        {
            get
            {
                return m_bonus;
            }
            set
            {
                if (m_bonus != value)
                {
                    m_bonus = value;
                }
                OnPropertyChanged("Bonus");
            }
        }

        public double Total { get; set; }
        public double ID { get; set; }
        #region PopulateTable
        public List<Salary> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<Salary> entries = new List<Salary>();
            string query = "SELECT * From Salary";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new Salary()
                {
                    ID = (int)reader["Salary_Id"],
                    Date = (DateTime)reader["Salary_Date"],
                    Amount = (double)reader["Salary_Amount"],
                    Bonus = (double)reader["Salary_Bonus"],
                    Total = (double)reader["Salary_Total"],
                  
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
              
                
                case "Amount":
                    if (!double.TryParse(Amount.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Bonus":
                    if (!double.TryParse(Bonus.ToString(), out uselessParse))
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
