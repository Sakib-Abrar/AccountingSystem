using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class Stuff : INotifyPropertyChanged, IDataErrorInfo
    {

        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_name = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private int m_id = 10;
        private string m_cell;
        private string m_type;
        private string m_password;
        private DateTime? m_date = Login.GlobalDate;
        public int SelectedIndex { get; set; }
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
                _firstLoad = false;
            }
        }
        public string Cell
        {
            get
            {
                return m_cell;
            }
            set
            {
                m_cell = value;
                OnPropertyChanged("Cell");
            }
        }
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
                OnPropertyChanged("Type");
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


        #region PopulateTable
        public List<Stuff> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<Stuff> entries = new List<Stuff>();
            string query = "SELECT * From Stuff";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new Stuff()
                {
                    ID = (int)reader["Stuff_Id"],
                    Date = (DateTime)reader["Stuff_Join"],
                    Name = (string)reader["Stuff_Name"],
                    Cell = (string)reader["Stuff_Cell"],
                    Password = (string)reader["Stuff_Password"],
                    Type = (string)reader["Stuff_type"],
                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM Stuff ORDER BY Stuff_Join DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["Stuff_Id"] + 1;
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
                case "Name": // property name
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Cell":
                    if (string.IsNullOrWhiteSpace(Cell))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Password":
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "ID":
                    break;
            }

            return validationMessage;
        }
        #endregion


        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {


            string pageTitle = "Stuff";
            float[] size = new float[] { 4, 2, 3, 4, 3, 3 };
            string[] tableHeaders = new String[] { "Stuff ID", "Join Date", "Name", "Cell", "Stuff Type"};
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM Stuff WHERE CAST(Stuff_Join AS date) BETWEEN '" + FromDate + "' and '" + ToDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Stuff_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Stuff_Join"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Stuff_Name"].ToString());
                myPDF.AddToTable(reader["Stuff_Cell"].ToString());
                myPDF.AddToTable(reader["Security_Type"].ToString());
            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion

    }
}
