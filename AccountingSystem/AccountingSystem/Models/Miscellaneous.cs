using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;

namespace AccountingSystem.Models
{
    class Miscellaneous : INotifyPropertyChanged, IDataErrorInfo
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
        private string m_details;
        private double? m_expenses;
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
        public string Details
        {
            get
            {
                return m_details;
            }
            set
            {
                if (m_details != value)
                {
                    m_details = value;
                }
                OnPropertyChanged("Details");
                _firstLoad = false;
            }
        }
        public double? Expenses
        {
            get
            {
                return m_expenses;
            }
            set
            {
                if (m_expenses != value)
                {
                    m_expenses = value;
                }
                OnPropertyChanged("Expenses");
            }
        }

        public double Total { get; set; }

        #region PopulateTable
        public List<Miscellaneous> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<Miscellaneous> entries = new List<Miscellaneous>();
            string query = "SELECT * From Miscellaneous";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new Miscellaneous()
                {
                    ID = (int)reader["ME_Id"],
                    Date = (DateTime)reader["ME_Date"],
                    Details = (string)reader["ME_Details"],
                    Expenses = (double)reader["ME_Expenses"],
                    Total = (double)reader["ME_Total"],

                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM Miscellaneous ORDER BY ME_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["ME_Id"] + 1;
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

                case "Expenses":
                    if (!double.TryParse(Expenses.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Details": // property name
                    if (string.IsNullOrWhiteSpace(Details))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
            }

            return validationMessage;
        }
        #endregion

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Miscellaneous Expenses";
            float[] size = new float[] { 4, 4, 4, 4, 4 };
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Details", "Expenses", "Total" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM Miscellaneous WHERE CAST(ME_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["ME_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["ME_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["ME_Details"].ToString());
                myPDF.AddToTable(reader["ME_Expenses"].ToString());
                myPDF.AddToTable(reader["ME_Total"].ToString());

            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
