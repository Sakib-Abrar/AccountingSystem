using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class Share : INotifyPropertyChanged, IDataErrorInfo
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
        private double? m_collection;
        private double? m_profit;
        private double? m_withdraw;
        public int SelectedIndex { get; set; }
        public DateTime Date { get; set; }
        public double? Collection
        {
            get
            {
                return m_collection;
            }
            set
            {
                if (m_collection != value)
                {
                    m_collection = value;
                }
                OnPropertyChanged("Collection");
                _firstLoad = false;
            }
        }
        public double? Profit
        {
            get
            {
                return m_profit;
            }
            set
            {
                if (m_profit != value)
                {
                    m_profit = value;
                }
                OnPropertyChanged("Profit");
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
                if (m_withdraw != value)
                {
                    m_withdraw = value;
                }
                OnPropertyChanged("Withdraw");
            }
        }
        public double Remains { get; set; }
        public double ID { get; set; }

        #region PopulateTable
        public List<Share> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<Share> entries = new List<Share>();
            string query = "SELECT * From Share";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new Share()
                {
                    ID = (int)reader["Share_Id"],
                    Date = (DateTime)reader["Share_Date"],
                    Collection = (double)reader["Share_Collection"],
                    Profit = (double)reader["Share_Profit"],
                    Withdraw = (double)reader["Share_Withdraw"],
                    Remains = (double)reader["Share_Remains"],
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
                case "Collection": // property name
                    if (!double.TryParse(Collection.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Profit":
                    if (!double.TryParse(Profit.ToString(), out uselessParse))
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
