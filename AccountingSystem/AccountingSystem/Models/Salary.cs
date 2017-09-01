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
        private int m_id;
        private DateTime? m_date = Login.GlobalDate;
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
                return m_id;
            }
            set
            {
                m_id = value;
                OnPropertyChanged("ID");
            }
        }

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

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM Salary ORDER BY Salary_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Salary_Id"] + 1;
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

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Salary";
            float[] size = new float[] { 4, 4, 4, 4, 4};
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Amount", "Bonus", "Total" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM Salary WHERE CAST(Salary_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Salary_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Salary_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Salary_Amount"].ToString());
                myPDF.AddToTable(reader["Salary_Bonus"].ToString());
                myPDF.AddToTable(reader["Salary_Total"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
