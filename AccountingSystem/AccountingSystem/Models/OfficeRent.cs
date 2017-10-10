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

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM OfficeRent ORDER BY Office_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Office_Id"] + 1;
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

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Office Rent";
            float[] size = new float[] { 4, 4, 4, 4, 4};
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Month", "Advance", "Rent"};
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM OfficeRent WHERE CAST(Office_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Office_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Office_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Office_Month"].ToString());
                myPDF.AddToTable(reader["Office_Advance"].ToString());
                myPDF.AddToTable(reader["Office_Rent"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
