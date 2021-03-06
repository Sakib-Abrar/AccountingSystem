﻿using AccountingSystem.Controller;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace AccountingSystem.Models
{
    class FixedDeposit : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        private double uselessParse;
        private int uselessParseInt;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string m_details = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private double? m_deposit;
        private double? m_withdraw;
        public int SelectedIndex { get; set; }
        private int m_accountno;
        private DateTime? m_date = Login.GlobalDate;
        private string m_name;
        private int m_id;
        private int m_mid;

        private string m_memberVoterID;
        private string m_memberFather;
        private string m_memberMother;
        private DateTime m_memberDOB = new DateTime(1985, 03, 01);

        private string m_memberProfession;
        private string m_memberNationality;
        private string m_memberReligion;
        private string m_fnominee;
        private float m_fnage;
        private string m_fnrelation;
        private string m_fnshare;
        private string m_fnaddress;
        private string m_snominee;
        private float m_snage;
        private string m_snrelation;
        private string m_snshare;
        private string m_snaddress;
        private string m_tnominee;
        private float m_tnage;
        private string m_tnrelation;
        private string m_tnshare;
        private string m_tnaddress;
        private string m_memberCell;
        private double m_duration;
        private int m_refererid;
        private string m_referercell;
        private string m_referername;

        public string MemberVoterID
        {
            get
            {
                return m_memberVoterID;
            }
            set
            {
                m_memberVoterID = value;
                OnPropertyChanged("MemberVoterID");
            }
        }
        public string MemberFather
        {
            get
            {
                return m_memberFather;
            }
            set
            {
                m_memberFather = value;
                OnPropertyChanged("MemberFather");
            }
        }
        public string MemberMother
        {
            get
            {
                return m_memberMother;
            }
            set
            {
                if (m_memberMother != value)
                {
                    m_memberMother = value;
                }
                OnPropertyChanged("MemberMother");
            }
        }
        public DateTime MemberDOB
        {
            get
            {
                return m_memberDOB;
            }
            set
            {
                if (m_memberDOB != value)
                {
                    m_memberDOB = value;
                }
                OnPropertyChanged("MemberDOB");
            }
        }
        public string MemberProfession
        {
            get
            {
                return m_memberProfession;
            }
            set
            {
                if (m_memberProfession != value)
                {
                    m_memberProfession = value;
                }
                OnPropertyChanged("MemberProfession");
            }
        }
        public string MemberNationality
        {
            get
            {
                return m_memberNationality;
            }
            set
            {
                if (m_memberNationality != value)
                {
                    m_memberNationality = value;
                }
                OnPropertyChanged("MemberNationality");
            }
        }
        public string MemberReligion
        {
            get
            {
                return m_memberReligion;
            }
            set
            {
                if (m_memberReligion != value)
                {
                    m_memberReligion = value;
                }
                OnPropertyChanged("MemberReligion");
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

        public int MemberId
        {
            get
            {
                return m_mid;
            }
            set
            {
                m_mid = value;
                OnPropertyChanged("MemberID");
                SetMemberDetails();
            }
        }
        public int AccountNo
        {
            get
            {
                return m_accountno;
            }
            set
            {
                m_accountno = value;
                OnPropertyChanged("AccountNo");
                _firstLoad = false;
            }
        }

        public int RefererId
        {
            get
            {
                return m_refererid;
            }
            set
            {
                m_refererid = value;
                OnPropertyChanged("RefererId");
            }
        }
        public double Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
                OnPropertyChanged("Duration");
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
                m_details = value;
                OnPropertyChanged("Details");
            }
        }

        public string FNominee
        {
            get
            {
                return m_fnominee;
            }
            set
            {
                if (m_fnominee != value)
                {
                    m_fnominee = value;
                }
                OnPropertyChanged("FNominee");
            }
        }
        public float FNAge
        {
            get
            {
                return m_fnage;
            }
            set
            {
                if (m_fnage != value)
                {
                    m_fnage = value;
                }
                OnPropertyChanged("FNAge");
            }
        }
        public string FNRelation
        {
            get
            {
                return m_fnrelation;
            }
            set
            {
                if (m_fnrelation != value)
                {
                    m_fnrelation = value;
                }
                OnPropertyChanged("FNRelation");
            }
        }
        public string FNShare
        {
            get
            {
                return m_fnshare;
            }
            set
            {
                if (m_fnshare != value)
                {
                    m_fnshare = value;
                }
                OnPropertyChanged("FNShare");
            }
        }

        public string FNAddress
        {
            get
            {
                return m_fnaddress;
            }
            set
            {
                if (m_fnaddress != value)
                {
                    m_fnaddress = value;
                }
                OnPropertyChanged("FNAddress");
            }
        }

        public string SNominee
        {
            get
            {
                return m_snominee;
            }
            set
            {
                if (m_snominee != value)
                {
                    m_snominee = value;
                }
                OnPropertyChanged("SNominee");
            }
        }
        public float SNAge
        {
            get
            {
                return m_snage;
            }
            set
            {
                if (m_snage != value)
                {
                    m_snage = value;
                }
                OnPropertyChanged("SNAge");
            }
        }
        public string SNRelation
        {
            get
            {
                return m_snrelation;
            }
            set
            {
                if (m_snrelation != value)
                {
                    m_snrelation = value;
                }
                OnPropertyChanged("SNRelation");
            }
        }
        public string SNShare
        {
            get
            {
                return m_snshare;
            }
            set
            {
                if (m_snshare != value)
                {
                    m_snshare = value;
                }
                OnPropertyChanged("SNShare");
            }
        }

        public string SNAddress
        {
            get
            {
                return m_snaddress;
            }
            set
            {
                if (m_snaddress != value)
                {
                    m_snaddress = value;
                }
                OnPropertyChanged("SNAddress");
            }
        }

        public string TNominee
        {
            get
            {
                return m_tnominee;
            }
            set
            {
                if (m_tnominee != value)
                {
                    m_tnominee = value;
                }
                OnPropertyChanged("TNominee");
            }
        }
        public float TNAge
        {
            get
            {
                return m_tnage;
            }
            set
            {
                if (m_tnage != value)
                {
                    m_tnage = value;
                }
                OnPropertyChanged("TNAge");
            }
        }
        public string TNRelation
        {
            get
            {
                return m_tnrelation;
            }
            set
            {
                if (m_tnrelation != value)
                {
                    m_tnrelation = value;
                }
                OnPropertyChanged("TNRelation");
            }
        }
        public string TNShare
        {
            get
            {
                return m_tnshare;
            }
            set
            {
                if (m_tnshare != value)
                {
                    m_tnshare = value;
                }
                OnPropertyChanged("TNShare");
            }
        }

        public string TNAddress
        {
            get
            {
                return m_tnaddress;
            }
            set
            {
                if (m_tnaddress != value)
                {
                    m_tnaddress = value;
                }
                OnPropertyChanged("TNAddress");
            }
        }
        public string MemberCell
        {
            get
            {
                return m_memberCell;
            }
            set
            {
                if (m_memberCell != value)
                {
                    m_memberCell = value;
                }
                OnPropertyChanged("MemberCell");
            }
        }

        public string RefererCell
        {
            get
            {
                return m_referercell;
            }
            set
            {
                if (m_referercell != value)
                {
                    m_referercell = value;
                }
                OnPropertyChanged("MemberCell");
            }
        }
        public string RefererName
        {
            get
            {
                return m_referername;
            }
            set
            {
                if (m_referername != value)
                {
                    m_referername = value;
                }
                OnPropertyChanged("MemberCell");
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
        public double? Deposit
        {
            get
            {
                return m_deposit;
            }
            set
            {
                m_deposit = value;
                OnPropertyChanged("Deposit");
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
                m_withdraw = value;
                OnPropertyChanged("Withdraw");
            }
        }
        public double Balance { get; set; }

        #region PopulateTable

        public List<FixedDeposit> GetDataList()
        {

            Connection conn = new Connection();
            conn.OpenConection();
            List<FixedDeposit> entries = new List<FixedDeposit>();
            string query = "SELECT m.*, g.* FROM Member m INNER JOIN FixedDepositLedger g  ON m.MemberId = g.MemberId INNER JOIN ( SELECT q.FixedId, MAX(FixedEntryId) MaxId FROM FixedDepositLedger q GROUP BY q.FixedId) d ON g.FixedId=d.FixedId AND g.FixedEntryId=d.MaxId ORDER BY d.maxid DESC";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new FixedDeposit()
                {
                    MemberId = (int)reader["MemberId"],
                    Name = (string)reader["MemberName"],
                    AccountNo = (int)reader["FixedId"],
                    Balance = (double)reader["FixedBalance"],
                });
            }
            conn.CloseConnection();
            return entries;
        }
        public List<FixedDeposit> GetDataLedger(int id, int front)
        {

            Connection conn1 = new Connection();
            string query1;
            if (id == 0)
            {
                query1 = "SELECT TOP 1 * FROM FixedDepositLedger ORDER BY FixedId ASC";
            }
            else if (front == 1)
            {
                query1 = "SELECT TOP 1 * FROM FixedDepositLedger WHERE FixedId > " + id + " ORDER BY FixedId ASC";
            }
            else if (front == 0)
            {
                query1 = "SELECT TOP 1 * FROM FixedDepositLedger WHERE FixedId < " + id + " ORDER BY FixedId DESC";
            }
            else
            {
                query1 = "SELECT TOP 1 * FROM FixedDepositLedger WHERE FixedId = " + id;
            }
            conn1.OpenConection();
            SqlDataReader reader1 = conn1.DataReader(query1);
            while (reader1.Read())
            {
                AccountNo = (int)reader1["FixedId"];
                id = AccountNo;
                front = 5;
            }
            if (front != 5)
            {
                MessageBox.Show("Id not found");
                return null;
            }
            conn1.CloseConnection();

            Connection conn = new Connection();
            conn.OpenConection();
            List<FixedDeposit> entries = new List<FixedDeposit>();
            string query = "SELECT * From FixedDepositLedger where FixedId = " + id;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new FixedDeposit()
                {
                    ID = (int)reader["FixedEntryId"],
                    AccountNo = (int)reader["FixedId"],
                    MemberId = (int)reader["MemberId"],
                    Date = (DateTime)reader["FixedDate"],
                    Details = (string)reader["FixedDetails"],
                    Deposit = (double)reader["FixedDeposit"],
                    Withdraw = (double)reader["FixedWithdraw"],
                    Balance = (double)reader["FixedBalance"],

                });
            }

            conn.CloseConnection();
            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM FixedDepositLedger ORDER BY FixedEntryId DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_id = (int)reader["FixedEntryId"] + 1;
            }

            conn.CloseConnection();

            conn = new Connection();
            conn.OpenConection();
            query = "SELECT Top 1 * From FixedDepositLedger where FixedId = " + id + " ORDER BY FixedEntryId DESC";
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                AccountNo = (int)reader["FixedId"];
                MemberId = (int)reader["MemberId"];
                Balance = (double)reader["FixedBalance"];
            }

            conn.CloseConnection();

            query = "SELECT * FROM Member WHERE MemberId = " + MemberId;
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_name = (string)reader["MemberName"];
            }

            conn.CloseConnection();

            return entries;
        }

        public void GetDataDetails(int id)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT m.*, g.* From Member m LEFT JOIN FixedDepositDetails g ON m.MemberId=g.MemberId where g.MemberId = " + id;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                ID = (int)reader["FDId"];
                MemberId = (int)reader["MemberId"];
                Name = (string)reader["MemberName"];
                MemberVoterID = (string)reader["MemberVoterId"];
                MemberFather = (string)reader["MemberFather"];
                MemberMother = (string)reader["MemberMother"];
                MemberDOB = (DateTime)reader["MemberDOB"];
                MemberProfession = (string)reader["MemberProfession"];
                MemberReligion = (string)reader["MemberReligion"];
                MemberNationality = (string)reader["MemberNationality"];
                MemberCell = (string)reader["MemberCell"];
                Duration = (double)reader["FDDuration"];
                RefererId = (int)reader["FDRefererMemberId"];
                FNominee = (string)reader["FDFNomineeName"];
                FNAge = (int)reader["FDFNomineeAge"];
                FNRelation = (string)reader["FDFNomineeRelation"];
                FNShare = (string)reader["FDFNomineeShare"];
                FNAddress = (string)reader["FDFNomineeAddress"];
                SNominee = (string)reader["FDNomineeName"];
                SNAge = (int)reader["FDSNomineeAge"];
                SNRelation = (string)reader["FDSNomineeRelation"];
                SNShare = (string)reader["FDSNomineeShare"];
                SNAddress = (string)reader["FDSNomineeAddress"];
                TNominee = (string)reader["FDTNomineeName"];
                TNAge = (int)reader["FDTNomineeAge"];
                TNRelation = (string)reader["FDTNomineeRelation"];
                TNShare = (string)reader["FDTNomineeShare"];
                TNAddress = (string)reader["FDTNomineeAddress"];
            }
            conn.CloseConnection();
            if (RefererId != 0)
            {
                conn.OpenConection();
                query = "SELECT * From Member WHERE MemberId = " + RefererId;
                reader = conn.DataReader(query);
                while (reader.Read())
                {
                    RefererName = (string)reader["MemberName"];
                    RefererCell = (string)reader["MemberCell"];

                }
                conn.CloseConnection();
            }
        }

        public void GetDataDetailsUnknown(string memberUnknown)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT m.*, g.* From Member m LEFT JOIN FixedDepositDetails g ON m.MemberId=g.MemberId";
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {
                if ((string)reader["MemberName"] == memberUnknown)
                {
                    if (reader.IsDBNull(reader.GetOrdinal("FDId")))
                        break;
                    ID = (int)reader["FDId"];
                    MemberId = (int)reader["MemberId"];
                    Name = (string)reader["MemberName"];
                    MemberVoterID = (string)reader["MemberVoterId"];
                    MemberFather = (string)reader["MemberFather"];
                    MemberMother = (string)reader["MemberMother"];
                    MemberDOB = (DateTime)reader["MemberDOB"];
                    MemberProfession = (string)reader["MemberProfession"];
                    MemberReligion = (string)reader["MemberReligion"];
                    MemberNationality = (string)reader["MemberNationality"];
                    MemberCell = (string)reader["MemberCell"];
                    Duration = (double)reader["FDDuration"];
                    RefererId = (int)reader["FDRefererMemberId"];
                    FNominee = (string)reader["FDFNomineeName"];
                    FNAge = (int)reader["FDFNomineeAge"];
                    FNRelation = (string)reader["FDFNomineeRelation"];
                    FNShare = (string)reader["FDFNomineeShare"];
                    FNAddress = (string)reader["FDFNomineeAddress"];
                    SNominee = (string)reader["FDNomineeName"];
                    SNAge = (int)reader["FDSNomineeAge"];
                    SNRelation = (string)reader["FDSNomineeRelation"];
                    SNShare = (string)reader["FDSNomineeShare"];
                    SNAddress = (string)reader["FDSNomineeAddress"];
                    TNominee = (string)reader["FDTNomineeName"];
                    TNAge = (int)reader["FDTNomineeAge"];
                    TNRelation = (string)reader["FDTNomineeRelation"];
                    TNShare = (string)reader["FDTNomineeShare"];
                    TNAddress = (string)reader["FDTNomineeAddress"];
                    checkExistence = 1;
                    break;
                }
            }
            if (checkExistence == 0)
            {
                MessageBox.Show("Member may not have a fixed deposit account.Check Input again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            conn.CloseConnection();
        }

        public void SetMemberDetails()
        {
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From Member WHERE MemberId = " + MemberId;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                Name = (string)reader["MemberName"];
                MemberVoterID = (string)reader["MemberVoterId"];
                MemberFather = (string)reader["MemberFather"];
                MemberMother = (string)reader["MemberMother"];
                MemberDOB = (DateTime)reader["MemberDOB"];
                MemberProfession = (string)reader["MemberProfession"];
                MemberReligion = (string)reader["MemberReligion"];
                MemberNationality = (string)reader["MemberNationality"];
                MemberCell = (string)reader["MemberCell"];
            }
            conn.CloseConnection();
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
                case "Details": // property name
                    if (string.IsNullOrWhiteSpace(Details))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Name": // property name
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberFather": // property name
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberMother": // property name
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "AccountNo":
                    if (!int.TryParse(AccountNo.ToString(), out uselessParseInt))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "Deposit":
                    if (!double.TryParse(Deposit.ToString(), out uselessParse))
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

        #region PDFCreation
        public void PublishPDFDetails(int id)
        {
            string pageTitle = "Fixed Deposit Information";
            string filename = pageTitle + MemberId;
            PdfWriter writer = new PdfWriter(Path.GetFullPath("PDF/" + filename + ".pdf"));

            PdfDocument pdf = new PdfDocument(writer);
            Document doc = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            PdfFont italic = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLDOBLIQUE);
            Paragraph p1 = new Paragraph("Rasulganj Multipurpose Co-operative Society Ltd").SetFont(bold).SetFontSize(15).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            Paragraph p2 = new Paragraph(pageTitle).SetFont(bold).SetFontSize(14).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            doc.Add(p1);
            doc.Add(p2);

            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT m.*, g.* From Member m LEFT JOIN FixedDepositDetails g ON m.MemberId=g.MemberId where g.MemberId = " + id;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                ID = (int)reader["FDId"];
                MemberId = (int)reader["MemberId"];
                Name = (string)reader["MemberName"];
                MemberVoterID = (string)reader["MemberVoterId"];
                MemberFather = (string)reader["MemberFather"];
                MemberMother = (string)reader["MemberMother"];
                MemberDOB = (DateTime)reader["MemberDOB"];
                MemberProfession = (string)reader["MemberProfession"];
                MemberReligion = (string)reader["MemberReligion"];
                MemberNationality = (string)reader["MemberNationality"];
                MemberCell = (string)reader["MemberCell"];
                Duration = (double)reader["FDDuration"];
                RefererId = (int)reader["FDRefererMemberId"];
                FNominee = (string)reader["FDFNomineeName"];
                FNAge = (int)reader["FDFNomineeAge"];
                FNRelation = (string)reader["FDFNomineeRelation"];
                FNShare = (string)reader["FDFNomineeShare"];
                FNAddress = (string)reader["FDFNomineeAddress"];
                SNominee = (string)reader["FDSNomineeName"];
                SNAge = (int)reader["FDSNomineeAge"];
                SNRelation = (string)reader["FDSNomineeRelation"];
                SNShare = (string)reader["FDSNomineeShare"];
                SNAddress = (string)reader["FDSNomineeAddress"];
                TNominee = (string)reader["FDTNomineeName"];
                TNAge = (int)reader["FDTNomineeAge"];
                TNRelation = (string)reader["FDTNomineeRelation"];
                TNShare = (string)reader["FDTNomineeShare"];
                TNAddress = (string)reader["FDTNomineeAddress"];
            }
            conn.CloseConnection();
            if (RefererId != 0)
            {
                conn.OpenConection();
                query = "SELECT * From Member WHERE MemberId = " + RefererId;
                reader = conn.DataReader(query);
                while (reader.Read())
                {
                    RefererName = (string)reader["MemberName"];
                    RefererCell = (string)reader["MemberCell"];

                }
            }
            Paragraph line0 = new Paragraph("MemberID : " + MemberId + "           Deposit Account No: " + ID);
            doc.Add(line0);
            line0 = new Paragraph("Name : " + Name);
            doc.Add(line0);
            Paragraph line1 = new Paragraph("National ID : " + MemberVoterID);
            doc.Add(line1);
            Paragraph line2 = new Paragraph("Father/Husband : " + MemberFather);
            doc.Add(line2);
            Paragraph line3 = new Paragraph("Mother : " + MemberMother);
            doc.Add(line3);
            Paragraph line4 = new Paragraph("Profession : " + MemberProfession);
            doc.Add(line4);
            Paragraph line5 = new Paragraph("Nationality : " + MemberNationality);
            doc.Add(line5);
            Paragraph line6 = new Paragraph("Religion : " + MemberReligion);
            doc.Add(line6);
            Paragraph line7 = new Paragraph("Cell No : " + MemberCell);
            doc.Add(line7);
            line7 = new Paragraph("Date of Birth : " + MemberDOB);
            doc.Add(line7);
            line7 = new Paragraph("Account Duration : " + Duration + " year(s)");
            doc.Add(line7);
            line7 = new Paragraph("First Nominee Name : " + FNominee);
            doc.Add(line7);
            line7 = new Paragraph("Age : " + FNAge + "   Relation : " + FNRelation);
            doc.Add(line7);
            line7 = new Paragraph("Share: " + FNShare + "Address: " + FNAddress);
            doc.Add(line7);
            line7 = new Paragraph("Second Nominee Name : " + SNominee);
            doc.Add(line7);
            line7 = new Paragraph("Age : " + SNAge + "   Relation : " + SNRelation);
            doc.Add(line7);
            line7 = new Paragraph("Share: " + SNShare + "Address: " + SNAddress);
            doc.Add(line7);
            line7 = new Paragraph("Third Nominee Name : " + TNominee);
            doc.Add(line7);
            line7 = new Paragraph("Age : " + TNAge + "   Relation : " + TNRelation);
            doc.Add(line7);
            line7 = new Paragraph("Share: " + TNShare + "Address: " + TNAddress);
            doc.Add(line7);
            line7 = new Paragraph("Referer ID: " + RefererId + "Name: " + RefererName + "Cell: " + RefererCell);
            doc.Add(line7);

            conn.CloseConnection();
            doc.Close();
        }

        public void PublishPDFLedger(DateTime? FromDate, DateTime? ToDate, int id)
        {
            string pageTitle = "Fixed Deposit";
            float[] size = new float[] { 3, 2, 3, 3, 3, 3, 3, 3 };
            string[] tableHeaders = new String[] { "Entry No.", "Account No", "Member ID", "Date", "Details", "Deposit", "Withdraw", "Balance" };
            PDF myPDF = new PDF(pageTitle, size, tableHeaders);

            string FDate = FromDate?.ToString("yyyyMMdd");
            string TDate = ToDate?.ToString("yyyyMMdd");
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From FixedDepositLedger where FixedId = " + id + " AND CAST(FixedDate AS date) BETWEEN '" + FDate + "' and '" + TDate + "'";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                myPDF.AddToTable(reader["FixedEntryId"].ToString());
                myPDF.AddToTable(reader["FixedId"].ToString());
                myPDF.AddToTable(reader["MemberId"].ToString());
                DateTime OnlyDate = (DateTime)reader["FixedDate"];
                myPDF.AddToTable(OnlyDate.ToString("dd-MM-yyyy"));
                myPDF.AddToTable(reader["FixedDetails"].ToString());
                myPDF.AddToTable(reader["FixedDeposit"].ToString());
                myPDF.AddToTable(reader["FixedWithdraw"].ToString());
                myPDF.AddToTable(reader["FixedBalance"].ToString());
            }

            conn.CloseConnection();
            myPDF.Done();
        }
        #endregion
    }
}
