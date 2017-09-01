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

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM CooperativeDevelopment ORDER BY Cooperative_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Cooperative_Id"] + 1;
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

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Cooperative Development Fund";
            float[] size = new float[] { 3, 4, 3, 3, 4, 4};
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Current", "Paid", "Previous", "Remains"};
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM CooperativeDevelopment WHERE CAST(Cooperative_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Cooperative_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Cooperative_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Cooperative_Current"].ToString());
                myPDF.AddToTable(reader["Cooperative_Paid"].ToString());
                myPDF.AddToTable(reader["Cooperative_Previous"].ToString());
                myPDF.AddToTable(reader["Cooperative_Remains"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }

}
