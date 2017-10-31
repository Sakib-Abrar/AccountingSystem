using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;

namespace AccountingSystem.Models
{
    class DueModel : INotifyPropertyChanged
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
        private int m_id;
        private int? m_account;
        private double? m_amount;

        private double? m_fine;
        private int? m_installment;
        private double? m_inst_amnt;
        private double? m_total;
        private double? m_balance;
        private DateTime? m_date = Login.GlobalDate;
        private DateTime? m_paid;
        private DateTime? m_next;
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
        public DateTime? NextDate
        {
            get
            {
                return m_next;
            }
            set
            {
                m_next = value;
                OnPropertyChanged("NextDate");
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

        public double? Fine
        {
            get
            {
                return m_fine;
            }
            set
            {
                m_fine = value;
                OnPropertyChanged("Fine");
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
        public List<DueModel> GetData(string method)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            List<DueModel> entries = new List<DueModel>();
            string query = "SELECT * From LoanDetails Where CONVERT(VARCHAR, LoanDetails_Collection) = '" + method+"' And LoanDetails_Due = 1";
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

                entries.Add(new DueModel()
                {

                    ID = (int)reader["LoanDetails_Id"],
                    SanctionDate = (DateTime)reader["LoanDetails_Sanction"],
                    NextDate = (DateTime)reader["LoanDetails_NextDate"],
                    LastPaid = (DateTime)reader["LoanDetails_LastPaid"],
                    Amount = (double)reader["LoanDetails_Amount"],
                    Balance = (double)reader["LoanDetails_Balance"],
                    Fine = (double)reader["LoanDetails_Fine"],
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

      

        #endregion

    }
}
