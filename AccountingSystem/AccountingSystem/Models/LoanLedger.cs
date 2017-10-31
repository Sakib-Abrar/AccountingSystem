﻿using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace AccountingSystem.Models
{
    class LoanLedger : INotifyPropertyChanged, IDataErrorInfo
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
        private int m_id;
        private int m_loan;
        private double? m_collection;
        private double? m_balance;
        private int m_installment;
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
        public int Loan
        {
            get
            {
                return m_loan;
            }
            set
            {
                m_loan = value;
                OnPropertyChanged("Loan");
                _firstLoad = false;
            }
        }
        public double? Collection
        {
            get
            {
                return m_collection;
            }
            set
            {
                m_collection = value;
                OnPropertyChanged("Collection");
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
                OnPropertyChanged("Balance");
            }
        }
        public int Installment
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

        #region InsertTable
        public void InsertTable(string collection,string method,string collector, string loan, string installment,double balance,DateTime nextDate, int id, object sender, RoutedEventArgs e)
        {
              using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [LoanCollection] (LoanCollection_Date, LoanCollection_Collection, LoanCollection_Method, LoanCollection_Collector, LoanCollection_Loan, LoanCollection_Installment) VALUES (@Date, @Collection, @Methode, @Collector, @Loan, @Installment)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Login.GlobalDate);
                    CmdSql.Parameters.AddWithValue("@Collection", collection);
                    CmdSql.Parameters.AddWithValue("@Methode", method);
                    CmdSql.Parameters.AddWithValue("@Collector", collector);
                    CmdSql.Parameters.AddWithValue("@Loan", loan);
                    CmdSql.Parameters.AddWithValue("@Installment", installment);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();
                //Updating LoanDetails Table         
                    int inc = 0;
                    if (method == "Daily") { inc = 1; }  
                    else if (method == "Weekly") { inc = 7; }
                    else { inc = 30; }

                    double fine = 0.00;
                    double newBalance = balance - Convert.ToDouble(collection);
                    DateTime dtd = nextDate;
                    dtd = dtd.AddDays(inc);

                    SqlCommand CmdSql2 = new SqlCommand("UPDATE [LoanDetails] SET LoanDetails_LastPaid = @Date , LoanDetails_NextDate = @NextDate, LoanDetails_Fine = @Fine, LoanDetails_Due = @Due, LoanDetails_Balance = @Balance WHERE LoanDetails_Id=" + loan, conn);
                    conn.Open();

                    CmdSql2.Parameters.AddWithValue("@Date", Login.GlobalDate);
                    CmdSql2.Parameters.AddWithValue("@NextDate", dtd);
                    CmdSql2.Parameters.AddWithValue("@Fine", fine);
                    CmdSql2.Parameters.AddWithValue("@Due", 0);
                    CmdSql2.Parameters.AddWithValue("@Balance", newBalance);

                   CmdSql2.ExecuteNonQuery();
                    conn.Close();

                //Inserting value in Entry table
                    string table = method+" Ledger(Loan)";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, id, Login.GlobalDate, color);
                    MessageBox.Show("Successfully Inserted");
                }
        }


        #endregion
        #region
        public void DueChecker()
        {
            float fineRate = 1;
            Connection conn = new Connection();
            conn.OpenConection();
            List<LoanLedger> entries = new List<LoanLedger>();
            string query = "SELECT * From [LoanDetails] where LoanDetails_Due = 0";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                int result = DateTime.Compare( (DateTime)reader["LoanDetails_NextDate"], (DateTime)Login.GlobalDate);
                if (result < 0 && (double)reader["LoanDetails_Balance"]!=0)
                {
                    int id = (int)reader["LoanDetails_Id"];
                    double fine = ((Double)(reader["LoanDetails_InstallmentAmount"]) * fineRate) / 100;
                    double total = fine + (Double)(reader["LoanDetails_Total"]);
                    using (SqlConnection conn2 = new SqlConnection(@Connection.ConnectionString))
                    {
                        SqlCommand CmdSql2 = new SqlCommand("UPDATE [LoanDetails] SET LoanDetails_Due = @Due , LoanDetails_Fine = @Fine, LoanDetails_Total = @Total WHERE LoanDetails_Id=" + id, conn2);
                        conn2.Open();

                        CmdSql2.Parameters.AddWithValue("@Due",1);
                        CmdSql2.Parameters.AddWithValue("@Fine", fine);
                        CmdSql2.Parameters.AddWithValue("@Total", total);

                        CmdSql2.ExecuteNonQuery();
                        conn2.Close();
                    }
                }
            }
            conn.CloseConnection();
        }
        #endregion

            #region PopulateTable
        public List<LoanLedger> GetData(string method)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<LoanLedger> entries = new List<LoanLedger>();
            string query = "SELECT * From [LoanCollection] where LoanCollection_Method = '"+ method + "' ";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                double balance = 0.00;
                Connection conn2 = new Connection();
                string query2 = "SELECT * From [LoanDetails] where LoanDetails_Id =" + reader["LoanCollection_Loan"] + " ";
                conn2.OpenConection();
                SqlDataReader reader2 = conn2.DataReader(query2);
                while (reader2.Read())
                {
                    balance = (double)reader2["LoanDetails_Balance"];
                }
                conn2.CloseConnection();
                entries.Add(new LoanLedger()
                {
                    ID = (int)reader["LoanCollection_Id"],
                    Date = (DateTime)reader["LoanCollection_Date"],
                    Loan = (int)reader["LoanCollection_Loan"],
                    Collection = (double)reader["LoanCollection_Collection"],
                    Installment = (int)reader["LoanCollection_Installment"],
                    Balance = balance,
                });
            }

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM LoanCollection ORDER BY LoanCollection_Id DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["LoanCollection_Id"] + 1;
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
            
                case "Collection":
                    if (!double.TryParse(Collection.ToString(), out uselessParse))
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
                case "ID":
                    break;
            }

            return validationMessage;
        }
        #endregion

        #region PDFCreation
        public void PublishPDF(DateTime? FromDate, DateTime? ToDate)
        {
            string pageTitle = "Security Fund";
            float[] size = new float[] { 4, 2, 3, 4, 3, 3 };
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Details", "Deposit", "Expenses", "Remains" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM SecurityFund WHERE CAST(Security_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["Security_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["Security_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["Security_Details"].ToString());
                myPDF.AddToTable(reader["Security_Deposit"].ToString());
                myPDF.AddToTable(reader["Security_Expenses"].ToString());
                myPDF.AddToTable(reader["Security_Remains"].ToString());
            }
            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}