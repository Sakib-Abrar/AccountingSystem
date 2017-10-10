using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows;
using System.IO;

namespace AccountingSystem.Models
{


    class Members : INotifyPropertyChanged, IDataErrorInfo
    {
        private int uselessIntParse;
        private long uselessParse;
        private int m_memberID;
        private string m_memberName;
        private string m_memberVoterID;
        private string m_memberFather;
        private string m_memberMother;
        private DateTime m_memberDOB=new DateTime(1985,03,01);

        private string m_memberProfession;
        private string m_memberNationality;
        private string m_memberReligion;
        private string m_memberPresentCO;
        private string m_memberPresentVillage;
        private string m_memberPresentPost;
        private string m_memberPresentThana;
        private string m_memberPresentDistrict;
        private string m_memberPermanentCO;
        private string m_memberPermanentVillage;
        private string m_memberPermanentPost;
        private string m_memberPermanentThana;
        private string m_memberPermanentDistrict;
        private string m_memberNominee;
        private DateTime m_memberNomineeDOB= new DateTime(1985, 03, 01);
        private string m_memberNomineeRelation;
        private string m_memberNomineeCell;
        private string m_memberPhoto;
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;

        public int MemberID
        {
            get
            {
                return m_memberID;
            }
            set
            {
                m_memberID = value;
                OnPropertyChanged("MemberID");
            }
        }

        public string MemberName
        {
            get
            {
                return m_memberName;
            }
            set
            {
                m_memberName = value;
                OnPropertyChanged("MemberName");
                _firstLoad = false;
            }
        }
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
        public string MemberPresentCO
        {
            get
            {
                return m_memberPresentCO;
            }
            set
            {
                if (m_memberPresentCO != value)
                {
                    m_memberPresentCO = value;
                }
                OnPropertyChanged("MemberPresentCO");
            }
        }
        public string MemberPresentVillage
        {
            get
            {
                return m_memberPresentVillage;
            }
            set
            {
                if (m_memberPresentVillage != value)
                {
                    m_memberPresentVillage = value;
                }
                OnPropertyChanged("MemberPresentVillage");
            }
        }
        public string MemberPresentPost
        {
            get
            {
                return m_memberPresentPost;
            }
            set
            {
                if (m_memberPresentPost != value)
                {
                    m_memberPresentPost = value;
                }
                OnPropertyChanged("MemberPresentPost");
            }
        }
        public string MemberPresentThana
        {
            get
            {
                return m_memberPresentThana;
            }
            set
            {
                if (m_memberPresentThana != value)
                {
                    m_memberPresentThana = value;
                }
                OnPropertyChanged("MemberPresentThana");
            }
        }
        public string MemberPresentDistrict
        {
            get
            {
                return m_memberPresentDistrict;
            }
            set
            {
                if (m_memberPresentDistrict != value)
                {
                    m_memberPresentDistrict = value;
                }
                OnPropertyChanged("MemberPresentDistrict");
            }
        }
        public string MemberPermanentCO
        {
            get
            {
                return m_memberPermanentCO;
            }
            set
            {
                if (m_memberPermanentCO != value)
                {
                    m_memberPermanentCO = value;
                }
                OnPropertyChanged("MemberPermanentCO");
            }
        }
        public string MemberPermanentVillage
        {
            get
            {
                return m_memberPermanentVillage;
            }
            set
            {
                if (m_memberPermanentVillage != value)
                {
                    m_memberPermanentVillage = value;
                }
                OnPropertyChanged("MemberPermanentVillage");
            }
        }
        public string MemberPermanentPost
        {
            get
            {
                return m_memberPermanentPost;
            }
            set
            {
                if (m_memberPermanentPost != value)
                {
                    m_memberPermanentPost = value;
                }
                OnPropertyChanged("MemberPermanentPost");
            }
        }
        public string MemberPermanentThana
        {
            get
            {
                return m_memberPermanentThana;
            }
            set
            {
                if (m_memberPermanentThana != value)
                {
                    m_memberPermanentThana = value;
                }
                OnPropertyChanged("MemberPermanentThana");
            }
        }
        public string MemberPermanentDistrict
        {
            get
            {
                return m_memberPermanentDistrict;
            }
            set
            {
                if (m_memberPermanentDistrict != value)
                {
                    m_memberPermanentDistrict = value;
                }
                OnPropertyChanged("MemberPermanentDistrict");
            }
        }
        public string MemberNominee
        {
            get
            {
                return m_memberNominee;
            }
            set
            {
                if (m_memberNominee != value)
                {
                    m_memberNominee = value;
                }
                OnPropertyChanged("MemberNominee");
            }
        }
        public DateTime MemberNomineeDOB
        {
            get
            {
                return m_memberNomineeDOB;
            }
            set
            {
                if (m_memberNomineeDOB != value)
                {
                    m_memberNomineeDOB = value;
                }
                OnPropertyChanged("MemberNomineeDOB");
            }
        }
        public string MemberNomineeRelation
        {
            get
            {
                return m_memberNomineeRelation;
            }
            set
            {
                if (m_memberNomineeRelation != value)
                {
                    m_memberNomineeRelation = value;
                }
                OnPropertyChanged("MemberNomineeRelation");
            }
        }
        public string MemberCell
        {
            get
            {
                return m_memberNomineeCell;
            }
            set
            {
                if (m_memberNomineeCell != value)
                {
                    m_memberNomineeCell = value;
                }
                OnPropertyChanged("MemberNomineeCell");
            }
        }
        public string MemberPhoto
        {
            get
            {
                return m_memberPhoto;
            }
            set
            {
                if (m_memberPhoto != value)
                {
                    m_memberPhoto = value;
                }
                OnPropertyChanged("MemberPhoto");
            }
        }
        #region PopulateTable
        public List<Members> GetDataList()
        {
            
            Connection conn = new Connection();
            conn.OpenConection();
            List<Members> entries = new List<Members>();
            string query = "SELECT * From Member";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                entries.Add(new Members()
                {
                    MemberID = (int)reader["MemberId"],
                    MemberName = (String)reader["MemberName"],
                    MemberVoterID = (String)reader["MemberVoterId"],
                    MemberCell = (String)reader["MemberCell"],
                });
            }
            conn.CloseConnection();
            return entries;
        }
        #endregion


        public void GetData(int memberID)
        {
            MemberID = memberID;
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From Member WHERE MemberId = "+memberID;
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {
                MemberName = (string)reader["MemberName"];
                MemberVoterID= (string)reader["MemberVoterId"];
                MemberFather = (string)reader["MemberFather"];
                MemberMother = (string)reader["MemberMother"];
                MemberDOB = (DateTime)reader["MemberDOB"];
                MemberProfession = (string)reader["MemberProfession"];
                MemberReligion = (string)reader["MemberReligion"];
                MemberNationality = (string)reader["MemberNationality"];
                MemberPresentCO = (string)reader["MemberPresentCO"];
                MemberPresentVillage = (string)reader["MemberPresentVillage"];
                MemberPresentPost = (string)reader["MemberPresentPost"];
                MemberPresentThana = (string)reader["MemberPresentThana"];
                MemberPresentDistrict = (string)reader["MemberPresentDistrict"];
                MemberPermanentCO = (string)reader["MemberPresentCO"];
                MemberPermanentVillage = (string)reader["MemberPermanentVillage"];
                MemberPermanentPost = (string)reader["MemberPermanentPost"];
                MemberPermanentThana = (string)reader["MemberPermanentThana"];
                MemberPermanentDistrict = (string)reader["MemberPermanentDistrict"];
                MemberNominee = (string)reader["MemberNominee"];
                MemberNomineeDOB = (DateTime)reader["MemberNomineeDOB"];
                MemberNomineeRelation = (string)reader["MemberNomineeRelation"];
                MemberCell = (string)reader["MemberCell"];
                MemberPhoto = Path.GetFullPath("Images/" + (string)reader["MemberPhoto"]);
                checkExistence = 1;

            }
            if (checkExistence == 0) {
                MessageBox.Show("System can not find member.Check Input again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            conn.CloseConnection();
        }

        public void GetDataUnknown(string memberUnknown)
        {
            string searchTerm;
            try
            {
                if (memberUnknown[0] == '0')
                    searchTerm = "MemberCell";
                else
                {
                    long cellorvoter = Int64.Parse(memberUnknown);
                    if (cellorvoter % 100000000000 == 88)
                        searchTerm = "MemberCell";
                    else
                        searchTerm = "MemberVoterID";
                }
            }
            catch (Exception)
            {
                searchTerm = "MemberName";
            }
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From Member";
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {
                
                if ((string)reader[searchTerm] == memberUnknown)
                {
                    MemberID = (int)reader["MemberId"];
                    MemberName = (string)reader["MemberName"];
                    MemberVoterID = (string)reader["MemberVoterId"];
                    MemberFather = (string)reader["MemberFather"];
                    MemberMother = (string)reader["MemberMother"];
                    MemberDOB = (DateTime)reader["MemberDOB"];
                    MemberProfession = (string)reader["MemberProfession"];
                    MemberReligion = (string)reader["MemberReligion"];
                    MemberNationality = (string)reader["MemberNationality"];
                    MemberPresentCO = (string)reader["MemberPresentCO"];
                    MemberPresentVillage = (string)reader["MemberPresentVillage"];
                    MemberPresentPost = (string)reader["MemberPresentPost"];
                    MemberPresentThana = (string)reader["MemberPresentThana"];
                    MemberPresentDistrict = (string)reader["MemberPresentDistrict"];
                    MemberPermanentCO = (string)reader["MemberPresentCO"];
                    MemberPermanentVillage = (string)reader["MemberPermanentVillage"];
                    MemberPermanentPost = (string)reader["MemberPermanentPost"];
                    MemberPermanentThana = (string)reader["MemberPermanentThana"];
                    MemberPermanentDistrict = (string)reader["MemberPermanentDistrict"];
                    MemberNominee = (string)reader["MemberNominee"];
                    MemberNomineeDOB = (DateTime)reader["MemberNomineeDOB"];
                    MemberNomineeRelation = (string)reader["MemberNomineeRelation"];
                    MemberCell = (string)reader["MemberCell"];
                    MemberPhoto = Path.GetFullPath("Images/" + (string)reader["MemberPhoto"]);
                    checkExistence = 1;
                    break;
                }
            }
            if (checkExistence == 0)
            {
                MessageBox.Show("System can not find member.Check Input again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            conn.CloseConnection();
        }
        public void SetMemberID()
        {
            Connection conn = new Connection();
            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            string query = "SELECT TOP 1 * FROM Member ORDER BY MemberId DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_memberID = (int)reader["MemberId"] + 1;
            }

            conn.CloseConnection();
        }

        #region validation
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
                case "MemberID": // property name
                    if (!int.TryParse(MemberID.ToString(), out uselessIntParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "MemberName":
                    if (string.IsNullOrWhiteSpace(MemberName))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberCell":
                    if (!long.TryParse(MemberCell, out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "MemberVoterID":
                    if (!long.TryParse(MemberVoterID, out uselessParse))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "MemberFather":
                    if (string.IsNullOrWhiteSpace(MemberFather))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberMother":
                    if (string.IsNullOrWhiteSpace(MemberMother))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberProfession":
                    if (string.IsNullOrWhiteSpace(MemberProfession))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberReligion":
                    if (string.IsNullOrWhiteSpace(MemberReligion))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberNationality":
                    if (string.IsNullOrWhiteSpace(MemberNationality))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPresentCO":
                    if (string.IsNullOrWhiteSpace(MemberPresentCO))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPresentVillage":
                    if (string.IsNullOrWhiteSpace(MemberPresentVillage))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPresentPost":
                    if (string.IsNullOrWhiteSpace(MemberPresentPost))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPresentThana":
                    if (string.IsNullOrWhiteSpace(MemberPresentThana))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPresentDistrict":
                    if (string.IsNullOrWhiteSpace(MemberPresentDistrict))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPermanentCO":
                    if (string.IsNullOrWhiteSpace(MemberPermanentCO))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPermanentVillage":
                    if (string.IsNullOrWhiteSpace(MemberPermanentVillage))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPermanentPost":
                    if (string.IsNullOrWhiteSpace(MemberPermanentPost))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPermanentThana":
                    if (string.IsNullOrWhiteSpace(MemberPermanentThana))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPermanentDistrict":
                    if (string.IsNullOrWhiteSpace(MemberPermanentDistrict))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberNominee":
                    if (string.IsNullOrWhiteSpace(MemberNominee))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberNomineeRelation":
                    if (string.IsNullOrWhiteSpace(MemberNomineeRelation))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "MemberPhoto":
                    if (string.IsNullOrWhiteSpace(MemberPhoto))
                    {
                        validationMessage = "No Picture Available";
                    }
                    break;
                case "MemberSignature":
                    if (string.IsNullOrWhiteSpace(MemberPhoto))
                    {
                        validationMessage = "No Picture Available";
                    }
                    break;


            }

            return validationMessage;
        }
        #endregion

    }
   
}