using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Windows;

namespace AccountingSystem.Models
{


    class Members : INotifyPropertyChanged
    {

        private int m_memberID;
        private String m_memberName;
        private String m_memberVoterID;
        private String m_memberFather;
        private String m_memberMother;
        private DateTime m_memberDOB;
        private String t_id;


        private String m_memberProfession;
        private String m_memberNationality;
        private String m_memberReligion;
        private String m_memberPresentCO;
        private String m_memberPresentVillage;
        private String m_memberPresentPost;
        private String m_memberPresentThana;
        private String m_memberPresentDistrict;
        private String m_memberPermanentCO;
        private String m_memberPermanentVillage;
        private String m_memberPermanentPost;
        private String m_memberPermanentThana;
        private String m_memberPermanentDistrict;
        private String m_memberNominee;
        private DateTime m_memberNomineeDOB;
        private String m_memberNomineeRelation;
        private String m_memberNomineeCell;
        private String m_memberPhoto;

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
                if (m_memberID != value)
                {
                    m_memberID = value;
                }
                OnPropertyChanged("MemberID");
                _firstLoad = false;
            }
        }

        public String MemberName
        {
            get
            {
                return m_memberName;
            }
            set
            {
                if (m_memberName != value)
                {
                    m_memberName = value;
                }
                OnPropertyChanged("MemberName");
            }
        }
        public String MemberVoterID
        {
            get
            {
                return m_memberVoterID;
            }
            set
            {
                if (m_memberVoterID != value)
                {
                    m_memberVoterID = value;
                }
                OnPropertyChanged("MemberVoterID");
            }
        }
        public String MemberFather
        {
            get
            {
                return m_memberFather;
            }
            set
            {
                if (m_memberFather != value)
                {
                    m_memberFather = value;
                }
                OnPropertyChanged("MemberFather");
            }
        }
        public String MemberMother
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
        public String MemberProfession
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
        public String MemberNationality
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
        public String MemberReligion
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
        public String MemberPresentCO
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
        public String MemberPresentVillage
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
        public String MemberPresentPost
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
        public String MemberPresentThana
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
        public String MemberPresentDistrict
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
        public String MemberPermanentCO
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
        public String MemberPermanentVillage
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
        public String MemberPermanentPost
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
        public String MemberPermanentThana
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
        public String MemberPermanentDistrict
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
        public String MemberNominee
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
        public String MemberNomineeRelation
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
        public String MemberCell
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
        public String MemberPhoto
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
        public List<Members> GetDatas()
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

            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            query = "SELECT TOP 1 * FROM Member ORDER BY MemberId DESC";
            conn.OpenConection();
            reader = conn.DataReader(query);
            while (reader.Read())
            {
                m_memberID = (int)reader["MemberId"] + 1;
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
            string query = "SELECT * From Member WHERE MemberID = "+memberID;
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {
                MemberName = (String)reader["MemberName"];
                MemberVoterID= (String)reader["MemberVoterId"];
                MemberFather = (String)reader["MemberFather"];
                MemberMother = (String)reader["MemberMother"];
                MemberDOB = (DateTime)reader["MemberDOB"];
                MemberProfession = (String)reader["MemberProfession"];
                MemberReligion = (String)reader["MemberReligion"];
                MemberNationality = (String)reader["MemberNationality"];
                MemberPresentCO = (String)reader["MemberPresentCO"];
                MemberPresentVillage = (String)reader["MemberPresentVillage"];
                MemberPresentPost = (String)reader["MemberPresentPost"];
                MemberPresentThana = (String)reader["MemberPresentThana"];
                MemberPresentDistrict = (String)reader["MemberPresentDistrict"];
                MemberPermanentCO = (String)reader["MemberPresentCO"];
                MemberPermanentVillage = (String)reader["MemberPermanentVillage"];
                MemberPermanentPost = (String)reader["MemberPermanentPost"];
                MemberPermanentThana = (String)reader["MemberPermanentThana"];
                MemberPermanentDistrict = (String)reader["MemberPermanentDistrict"];
                MemberNominee = (String)reader["MemberNominee"];
                MemberNomineeDOB = (DateTime)reader["MemberNomineeDOB"];
                MemberNomineeRelation = (String)reader["MemberNomineeRelation"];
                MemberCell = (String)reader["MemberCell"];
                //string appStartPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                //string path = appStartPath.Substring(0, appStartPath.Length - 10);
                // String path = "/AccountingSystem;component";
                //  MemberPhoto = path + (String)reader["MemberPhoto"];
                // var yourImage = new BitmapImage(new Uri(String.Format("Images/MemberPhoto/{0}.jpg", "aaa_1204041"), UriKind.Relative));
                MemberPhoto = (String)reader["MemberPhoto"];
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
            catch (Exception ex)
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
                    MemberID = (int)reader["MemberID"];
                    MemberName = (String)reader["MemberName"];
                    MemberVoterID = (String)reader["MemberVoterId"];
                    MemberFather = (String)reader["MemberFather"];
                    MemberMother = (String)reader["MemberMother"];
                    MemberDOB = (DateTime)reader["MemberDOB"];
                    MemberProfession = (String)reader["MemberProfession"];
                    MemberReligion = (String)reader["MemberReligion"];
                    MemberNationality = (String)reader["MemberNationality"];
                    MemberPresentCO = (String)reader["MemberPresentCO"];
                    MemberPresentVillage = (String)reader["MemberPresentVillage"];
                    MemberPresentPost = (String)reader["MemberPresentPost"];
                    MemberPresentThana = (String)reader["MemberPresentThana"];
                    MemberPresentDistrict = (String)reader["MemberPresentDistrict"];
                    MemberPermanentCO = (String)reader["MemberPresentCO"];
                    MemberPermanentVillage = (String)reader["MemberPermanentVillage"];
                    MemberPermanentPost = (String)reader["MemberPermanentPost"];
                    MemberPermanentThana = (String)reader["MemberPermanentThana"];
                    MemberPermanentDistrict = (String)reader["MemberPermanentDistrict"];
                    MemberNominee = (String)reader["MemberNominee"];
                    MemberNomineeDOB = (DateTime)reader["MemberNomineeDOB"];
                    MemberNomineeRelation = (String)reader["MemberNomineeRelation"];
                    MemberCell = (String)reader["MemberCell"];
                    //string appStartPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    //string path = appStartPath.Substring(0, appStartPath.Length - 10);
                    // String path = "/AccountingSystem;component";
                    //  MemberPhoto = path + (String)reader["MemberPhoto"];
                    // var yourImage = new BitmapImage(new Uri(String.Format("Images/MemberPhoto/{0}.jpg", "aaa_1204041"), UriKind.Relative));
                    MemberPhoto = (String)reader["MemberPhoto"];
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


                case "MemberID":
                    
                    break;
                case "MemberName":

                    break;
            }

            return validationMessage;
        }
        #endregion

    }
   
}
