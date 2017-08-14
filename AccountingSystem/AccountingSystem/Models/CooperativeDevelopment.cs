using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class CooperativeDevelopment : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_current;
        private double? m_paid;
      
        public int SelectedIndex { get; set; }
        public DateTime Date { get; set; }
   
        public double? Current
        {
            get
            {
                return m_current;
            }
            set
            {
                if (m_current != value)
                {
                    m_current = value;
                }
                OnPropertyChanged("Current");
                _firstLoad = false;
            }
        }
        public double? Paid
        {
            get
            {
                return m_paid;
            }
            set
            {
                if (m_paid != value)
                {
                    m_paid = value;
                }
                OnPropertyChanged("Paid");
            }
        }
        public double Previous { get; set; }
        public double Remains { get; set; }
        public double ID { get; set; }

        #region PopulateTable
        public List<CooperativeDevelopment> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<CooperativeDevelopment> entries = new List<CooperativeDevelopment>();
            string query = "SELECT * From CooperativeDevelopment";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new CooperativeDevelopment()
                {
                    ID = (int)reader["Cooperative_Id"],
                    Date = (DateTime)reader["Cooperative_Date"],
                    Current = (double)reader["Cooperative_Current"],
                    Paid = (double)reader["Cooperative_Paid"],
                    Previous = (double)reader["Cooperative_Previous"],
                    Remains = (double)reader["Cooperative_Remains"],
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
             
                case "Current":
                    if (!double.TryParse(Current.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Paid":
                    if (!double.TryParse(Paid.ToString(), out uselessParse))
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
