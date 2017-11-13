using System;
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

        private string m_sector = "";
        private string m_name = "";
        private int m_account;
        private double? m_amount;
        private double? m_service;
        private double? m_lifetime;
        private double? m_inst_amnt;
        private double? m_total;
        private DateTime? m_paid;
        private DateTime? m_nextdate;
        private double? m_fine;

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
                return m_nextdate;
            }
            set
            {
                m_nextdate = value;
                OnPropertyChanged("NextDate");
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
        public int Account
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

                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [LoanCollection] (LoanCollection_Date, LoanCollection_Collection, LoanCollection_Method, LoanCollection_Collector, LoanCollection_Loan, LoanCollection_Installment) VALUES (@Date, @Collection, @Method, @Collector, @Loan, @Installment)", conn);
                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@Date", Login.GlobalDate);
                    CmdSql.Parameters.AddWithValue("@Collection", collection);
                    CmdSql.Parameters.AddWithValue("@Method", method);
                    CmdSql.Parameters.AddWithValue("@Collector", collector);
                    CmdSql.Parameters.AddWithValue("@Loan", loan);
                    CmdSql.Parameters.AddWithValue("@Installment", installment);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();
                //Updating LoanDetails Table         
                    double fine = 0.00;
                    double newBalance = balance - Convert.ToDouble(collection);
                    DateTime dtd = nextDate;
                    if (method == "Daily") { dtd = dtd.AddDays(1); }
                    else if (method == "Weekly") { dtd = dtd.AddDays(7); }
                    else if (method == "Monthly") { dtd = dtd.AddMonths(1); }

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
                    string table = method+" Loan Ledger";
                    string type = "Inserted";
                    string color = "Green";
                    EntryLog entry = new EntryLog();
                    entry.Add_Entry(table, type, id, Login.GlobalDate, color);
                    MessageBox.Show("Successfully Inserted");
                }
        }
        #endregion

        #region Update Entry
        public void UpdateTable(string collection, string method, string collector, string loan, string installment, double balance, DateTime nextDate, int id, object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {

                SqlCommand CmdSql = new SqlCommand("Update [LoanCollection] SET LoanCollection_Date=@Date, LoanCollection_Collection=@Collection, LoanCollection_Method=@Method, LoanCollection_Collector=@Collector, LoanCollection_Loan=@Loan, LoanCollection_Installment=@Installment WHERE LoanCollection_Id="+id, conn);
                conn.Open();
                CmdSql.Parameters.AddWithValue("@Date", Login.GlobalDate);
                CmdSql.Parameters.AddWithValue("@Collection", collection);
                CmdSql.Parameters.AddWithValue("@Method", method);
                CmdSql.Parameters.AddWithValue("@Collector", collector);
                CmdSql.Parameters.AddWithValue("@Loan", loan);
                CmdSql.Parameters.AddWithValue("@Installment", installment);
                CmdSql.ExecuteNonQuery();
                conn.Close();
                //Updating LoanDetails Table         
                double fine = 0.00;
                double newBalance = balance - Convert.ToDouble(collection);
                DateTime dtd = nextDate;
                if (method == "Daily") { dtd = dtd.AddDays(1); }
                else if (method == "Weekly") { dtd = dtd.AddDays(7); }
                else if (method == "Monthly") { dtd = dtd.AddMonths(1); }

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
                string table = method + " Loan Ledger";
                string type = "Updated";
                string color = "Blue";
                EntryLog entry = new EntryLog();
                entry.Add_Entry(table, type, id, Login.GlobalDate, color);
                MessageBox.Show("Successfully Updated");
            }
        }
        #endregion
        #region Due List
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

        public List<LoanLedger> GetDataIndividual(string method,int id, int direction)
        {
            List<LoanLedger> entries = new List<LoanLedger>();
            string query;
            if (id == 0)
            {
                Connection conn5 = new Connection();
                conn5.OpenConection();
                query = "SELECT TOP 1 * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' ORDER BY LoanCollection_Loan ASC";
                SqlDataReader reader5 = conn5.DataReader(query);
                while (reader5.Read())
                {
                    id = (int)reader5["LoanCollection_Loan"];
                }

                conn5.CloseConnection();
                query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan = " + id + " ORDER BY LoanCollection_Loan ASC";
            }
            else if (direction == 1)
            {
                Connection conn5 = new Connection();
                conn5.OpenConection();
                query = "SELECT TOP 1 * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan > " + id + " ORDER BY LoanCollection_Loan ASC";
                SqlDataReader reader5 = conn5.DataReader(query);
                while (reader5.Read())
                {
                    id = (int)reader5["LoanCollection_Loan"];
                }

                conn5.CloseConnection();
                query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan = " + id; ;
            }
            else if (direction == 0)
            {
                Connection conn5 = new Connection();
                conn5.OpenConection();
                query = "SELECT TOP 1 * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan < " + id + " ORDER BY LoanCollection_Loan DESC";
                SqlDataReader reader5 = conn5.DataReader(query);
                while (reader5.Read())
                {
                    id = (int)reader5["LoanCollection_Loan"];
                }
                conn5.CloseConnection();
                query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan = " + id; ;
            }
            else
            {
                query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan = " + id;
            }
            Connection conn = new Connection();
            conn.OpenConection();
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
                direction = 5;
            }
            if (direction != 5)
            {
                MessageBox.Show("Id not found");
                return null;
            }
            conn.CloseConnection();
            /// <summary>
            ///Fill the text boxes
            /// <summary/>

            conn = new Connection();
            conn.OpenConection();
            query = "SELECT * From LoanDetails WHERE LoanDetails_Id = " + id;
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                Connection conn2 = new Connection();
                conn2.OpenConection();
                string query2 = "SELECT * From [Member] WHERE MemberId =" + reader["LoanDetails_Account"] + " ";
                SqlDataReader reader2 = conn2.DataReader(query2);
                if (reader2 == null)
                    return null;
                while (reader2.Read())
                {
                    Name = (string)reader2["MemberName"];
                }
                conn2.CloseConnection();
                Loan= (int)reader["LoanDetails_Id"];
                double total = Convert.ToDouble(reader["LoanDetails_InstallmentAmount"]) + Convert.ToDouble(reader["LoanDetails_Fine"]);
                Account = (int)reader["LoanDetails_Account"];
                LastPaid= (DateTime)reader["LoanDetails_LastPaid"];
                Total= (double)reader["LoanDetails_Total"];
                Installment = (int)reader["LoanDetails_Installment"];
                InstallmentAmount = (double)reader["LoanDetails_InstallmentAmount"];
                Sector= (string)reader["LoanDetails_Sector"];
                Amount= (double)reader["LoanDetails_Amount"];
                SanctionDate= (DateTime)reader["LoanDetails_Sanction"];
                if ((double)reader["LoanDetails_Balance"] != 0)
                {
                    NextDate = (DateTime)reader["LoanDetails_NextDate"];
                    Balance = (double)reader["LoanDetails_Balance"];
                    Fine = (double)reader["LoanDetails_Fine"];
                    Total = total;
                }
                else
                {
                    NextDate= null;
                    Balance= null;
                    Fine= null;
                    Total= null;
                }
            }
            conn.CloseConnection();

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

            Loan = id;

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
        public void PublishPDFIndividualLedger(DateTime? FromDate, DateTime? ToDate, int id, string method)
        {
            string pageTitle = "Loan Ledger";
            float[] size = new float[] { 3, 2, 3, 3, 3, 3};
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Loan ID", "Collection", "Installment", "Balance" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);



            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");

            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND LoanCollection_Loan = " + id +" AND CAST(LoanCollection_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["LoanCollection_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["LoanCollection_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["LoanCollection_Loan"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Collection"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Installment"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Balance"].ToString());
            }

            conn.CloseConnection();

            Connection conn1 = new Connection();
            conn1.OpenConection();
            string firstquery = "Select m.MemberName From LoanDetails l Left Join Member m ON l.LoanDetails_Account=m.MemberId where l.LoanDetails_Id = " + id;
            SqlDataReader reader1 = conn1.DataReader(firstquery);
            string name = "";
            while (reader1.Read())
            {
                name = "Name : " + reader1["MemberName"].ToString();
            }

            myPDF.AddParagraph("Name : "+ name);
            conn1.CloseConnection();

            myPDF.Done();
        }
        #endregion

        public void PublishPDFLedger(DateTime? FromDate, DateTime? ToDate, string method)
        {
            string pageTitle = "Loan Ledger";
            float[] size = new float[] { 3, 2, 3, 3, 3, 3 };
            string[] tableHeaders = new String[] { "Entry No.", "Date", "Loan ID", "Collection", "Installment", "Balance" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);



            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");

            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * FROM [LoanCollection] WHERE LoanCollection_Method = '" + method + "' AND CAST(LoanCollection_Date AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["LoanCollection_Id"].ToString());
                DateTime OnlyDate = (DateTime)reader["LoanCollection_Date"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["LoanCollection_Loan"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Collection"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Installment"].ToString());
                myPDF.AddToTable(reader["LoanCollection_Balance"].ToString());
            }

            conn.CloseConnection();
            myPDF.Done();
        }
    }
}
