using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class LoanDetails : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_sector = "";
        private string m_collection = "";
        private string m_name = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private int m_id;
        private int? m_account;
        private double? m_amount;
        private double? m_service;
        private double? m_lifetime;
        private int? m_installment;
        private double? m_inst_amnt;
        private double? m_total;
        private double? m_balance;
        private DateTime? m_date = Login.GlobalDate;
        private DateTime? m_paid;
        public int SelectedIndex { get; set; }
        public DateTime? SanctionDate
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;
                OnPropertyChanged("SanctionDate");
            }
        }
        public DateTime? LastPaid
        {
            get
            {
                return m_paid;
            }
            set
            {
                m_paid = value;
                OnPropertyChanged("LastPaid");
            }
        }
        public string Sector
        {
            get
            {
                return m_sector;
            }
            set
            {
                m_sector = value;
                OnPropertyChanged("Sector");
                _firstLoad = false;
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
        public string Collection
        {
            get
            {
                return m_collection;
            }
            set
            {
                m_collection = value;
                OnPropertyChanged("Collection");
                _firstLoad = false;
            }
        }
        public int? Account
        {
            get
            {
                return m_account;
            }
            set
            {
                m_account = value;
                OnPropertyChanged("Account");
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
                m_amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public double? ServiceCharge
        {
            get
            {
                return m_service;
            }
            set
            {
                m_service = value;
                OnPropertyChanged("ServiceCharge");
            }
        }
        public double? Lifetime
        {
            get
            {
                return m_lifetime;
            }
            set
            {
                m_lifetime = value;
                OnPropertyChanged("Lifetime");
            }
        }
        public int? Installment
        {
            get
            {
                return m_installment;
            }
            set
            {
                m_installment = value;
                OnPropertyChanged("Installment");
            }
        }
        public double? InstallmentAmount
        {
            get
            {
                return m_inst_amnt;
            }
            set
            {
                m_inst_amnt = value;
                OnPropertyChanged("InstallmentAmmount");
            }
        }
        public double? Total
        {
            get
            {
                return m_total;
            }
            set
            {
                m_total = value;
                OnPropertyChanged("Total");
            }
        }
        public double? Balance
        {
            get
            {
                return m_balance;
            }
            set
            {
                m_balance = value;
                OnPropertyChanged("Total");
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
        public List<LoanDetails> GetData()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<LoanDetails> entries = new List<LoanDetails>();
            string query = "SELECT * From LoanDetails";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                string name = "";
                //string lastPaid = "";
               // DateTime date = Convert.ToDateTime((DateTime)reader["LoanDetails_LastPaid"]);
               // if (reader["LoanDetails_LastPaid"] == null) {  lastPaid = "N/A"; }
               // else { lastPaid = date.ToString("dd'/'MM'/'yyyy HH':'mm':'ss.fff"); }
                Connection conn2 = new Connection();
                string query2 = "SELECT * From [Member] where MemberId =" + reader["LoanDetails_Account"] + " ";
                conn2.OpenConection();
                SqlDataReader reader2 = conn2.DataReader(query2);
                while (reader2.Read())
                {
                    name = (string)reader2["MemberName"];
                }
                conn2.CloseConnection();
               
                entries.Add(new LoanDetails()
                {
                
                    ID = (int)reader["LoanDetails_Id"],
                    SanctionDate = (DateTime)reader["LoanDetails_Sanction"],
                    Sector = (string)reader["LoanDetails_Sector"],
                    Collection = (string)reader["LoanDetails_Collection"],
                    LastPaid = (DateTime)reader["LoanDetails_LastPaid"],
                    Amount = (double)reader["LoanDetails_Amount"],
                    Balance = (double)reader["LoanDetails_Balance"],
                    Lifetime = (double)reader["LoanDetails_Lifetime"],
                    ServiceCharge = (double)reader["LoanDetails_Service"],
                    Installment = (int)reader["LoanDetails_Installment"],
                    Account = Convert.ToInt32(reader["LoanDetails_Account"]),
                    InstallmentAmount = (double)reader["LoanDetails_InstallmentAmount"],
                    Total = (double)reader["LoanDetails_Total"],
                    Name = name,
                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM LoanDetails ORDER BY LoanDetails_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["LoanDetails_Id"] + 1;
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
                case "Sector": // property name
                    if (string.IsNullOrWhiteSpace(Sector))
                    {
                        validationMessage = "No Sector Available";
                    }
                    break;
                case "Collection": // property name
                    if (string.IsNullOrWhiteSpace(Collection))
                    {
                        validationMessage = "No Collection Type Available";
                    }
                    break;
                case "Amount":
                    if (!double.TryParse(Amount.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "ServiceCharge":
                    if (!double.TryParse(ServiceCharge.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Installment":
                    if (!double.TryParse(Installment.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "InstallmentAmount":
                    if (!double.TryParse(InstallmentAmount.ToString(), out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
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
            string pageTitle = "Loanee List";
            float[] size = new float[] { 3, 3, 3, 3, 3, 3, 3, 3 };
            string[] tableHeaders = new String[] { "Loan No.", "Name", "Account ID", "Amount", "Balance", "Method", "Last Paid", "Total" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            Connection conn = new Connection();
            conn.OpenConection();
            string query = "Select m.MemberName, l.* From LoanDetails l Left Join Member m ON l.LoanDetails_Account=m.MemberId";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["LoanDetails_Id"].ToString());
                myPDF.AddToTable(reader["MemberName"].ToString());
                myPDF.AddToTable(reader["LoanDetails_Account"].ToString());
                myPDF.AddToTable(reader["LoanDetails_Amount"].ToString());
                myPDF.AddToTable(reader["LoanDetails_Balance"].ToString());
                myPDF.AddToTable(reader["LoanDetails_Collection"].ToString());
                DateTime OnlyDate = (DateTime)reader["LoanDetails_LastPaid"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["LoanDetails_Total"].ToString());
            }

            conn.CloseConnection();
            myPDF.Done();
    }
        #endregion
    }
}
