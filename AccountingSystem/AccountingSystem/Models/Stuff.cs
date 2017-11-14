using System;
using System.Collections.Generic;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Layout.Element;
using iText.IO.Image;


namespace AccountingSystem.Models
{
    class Stuff : INotifyPropertyChanged, IDataErrorInfo
    {

        /// <summary>
        /// UselessParse is used for TryParse method which needs an output parameter but we don't.
        /// </summary>
        /// <summary>
        /// _firstLoad is used to prevent auto validation at the startup
        /// </summary>
        private bool _firstLoad = true;
        private string s_name = "";
        /// <summary>
        /// double? is used to make double nullable.Otherwise we would get zero in textbox initially.But we want it empty.
        /// </summary>
        private int s_id = 10;
        private string s_cell;
        private string s_type;
        private string s_password;
        private DateTime? s_date = Login.GlobalDate;
        private string s_stuffVoterID;
        private string s_stuffFather;
        private string s_stuffMother;
        private DateTime s_stuffDOB = new DateTime(1985, 03, 01);

        private string s_stuffProfession;
        private string s_stuffNationality;
        private string s_stuffReligion;
        private string s_stuffPresentCO;
        private string s_stuffPresentVillage;
        private string s_stuffPresentPost;
        private string s_stuffPresentThana;
        private string s_stuffPresentDistrict;
        private string s_stuffPermanentCO;
        private string s_stuffPermanentVillage;
        private string s_stuffPermanentPost;
        private string s_stuffPermanentThana;
        private string s_stuffPermanentDistrict;
        private string s_stuffPhoto;
        public int SelectedIndex { get; set; }
        public DateTime? StuffDate
        {
            get
            {
                return s_date;
            }
            set
            {
                s_date = value;
                OnPropertyChanged("StuffDate");
            }
        }
        public string StuffName
        {
            get
            {
                return s_name;
            }
            set
            {
                s_name = value;
                OnPropertyChanged("StuffName");
                _firstLoad = false;
            }
        }
        public string StuffCell
        {
            get
            {
                return s_cell;
            }
            set
            {
                s_cell = value;
                OnPropertyChanged("StuffCell");
            }
        }

        public string StuffPassword
        {
            get
            {
                return s_password;
            }
            set
            {
                s_password = value;
                OnPropertyChanged("StuffPassword");
            }
        }
        public string StuffType
        {
            get
            {
                return s_type;
            }
            set
            {
                s_type = value;
                OnPropertyChanged("StuffType");
            }
        }
        public int StuffID
        {
            get
            {
                return s_id;
            }
            set
            {
                s_id = value;
                OnPropertyChanged("StuffID");
            }
        }

        public string StuffVoterID
        {
            get
            {
                return s_stuffVoterID;
            }
            set
            {
                s_stuffVoterID = value;
                OnPropertyChanged("StuffVoterID");
            }
        }
        public string StuffFather
        {
            get
            {
                return s_stuffFather;
            }
            set
            {
                s_stuffFather = value;
                OnPropertyChanged("StuffFather");
            }
        }
        public string StuffMother
        {
            get
            {
                return s_stuffMother;
            }
            set
            {
                if (s_stuffMother != value)
                {
                    s_stuffMother = value;
                }
                OnPropertyChanged("StuffMother");
            }
        }
        public DateTime StuffDOB
        {
            get
            {
                return s_stuffDOB;
            }
            set
            {
                if (s_stuffDOB != value)
                {
                    s_stuffDOB = value;
                }
                OnPropertyChanged("StuffDOB");
            }
        }
        public string StuffProfession
        {
            get
            {
                return s_stuffProfession;
            }
            set
            {
                if (s_stuffProfession != value)
                {
                    s_stuffProfession = value;
                }
                OnPropertyChanged("StuffProfession");
            }
        }
        public string StuffNationality
        {
            get
            {
                return s_stuffNationality;
            }
            set
            {
                if (s_stuffNationality != value)
                {
                    s_stuffNationality = value;
                }
                OnPropertyChanged("StuffNationality");
            }
        }
        public string StuffReligion
        {
            get
            {
                return s_stuffReligion;
            }
            set
            {
                if (s_stuffReligion != value)
                {
                    s_stuffReligion = value;
                }
                OnPropertyChanged("StuffReligion");
            }
        }
        public string StuffPresentCO
        {
            get
            {
                return s_stuffPresentCO;
            }
            set
            {
                if (s_stuffPresentCO != value)
                {
                    s_stuffPresentCO = value;
                }
                OnPropertyChanged("StuffPresentCO");
            }
        }
        public string StuffPresentVillage
        {
            get
            {
                return s_stuffPresentVillage;
            }
            set
            {
                if (s_stuffPresentVillage != value)
                {
                    s_stuffPresentVillage = value;
                }
                OnPropertyChanged("StuffPresentVillage");
            }
        }
        public string StuffPresentPost
        {
            get
            {
                return s_stuffPresentPost;
            }
            set
            {
                if (s_stuffPresentPost != value)
                {
                    s_stuffPresentPost = value;
                }
                OnPropertyChanged("StuffPresentPost");
            }
        }
        public string StuffPresentThana
        {
            get
            {
                return s_stuffPresentThana;
            }
            set
            {
                if (s_stuffPresentThana != value)
                {
                    s_stuffPresentThana = value;
                }
                OnPropertyChanged("StuffPresentThana");
            }
        }
        public string StuffPresentDistrict
        {
            get
            {
                return s_stuffPresentDistrict;
            }
            set
            {
                if (s_stuffPresentDistrict != value)
                {
                    s_stuffPresentDistrict = value;
                }
                OnPropertyChanged("StuffPresentDistrict");
            }
        }
        public string StuffPermanentCO
        {
            get
            {
                return s_stuffPermanentCO;
            }
            set
            {
                if (s_stuffPermanentCO != value)
                {
                    s_stuffPermanentCO = value;
                }
                OnPropertyChanged("StuffPermanentCO");
            }
        }
        public string StuffPermanentVillage
        {
            get
            {
                return s_stuffPermanentVillage;
            }
            set
            {
                if (s_stuffPermanentVillage != value)
                {
                    s_stuffPermanentVillage = value;
                }
                OnPropertyChanged("StuffPermanentVillage");
            }
        }
        public string StuffPermanentPost
        {
            get
            {
                return s_stuffPermanentPost;
            }
            set
            {
                if (s_stuffPermanentPost != value)
                {
                    s_stuffPermanentPost = value;
                }
                OnPropertyChanged("StuffPermanentPost");
            }
        }
        public string StuffPermanentThana
        {
            get
            {
                return s_stuffPermanentThana;
            }
            set
            {
                if (s_stuffPermanentThana != value)
                {
                    s_stuffPermanentThana = value;
                }
                OnPropertyChanged("StuffPermanentThana");
            }
        }
        public string StuffPermanentDistrict
        {
            get
            {
                return s_stuffPermanentDistrict;
            }
            set
            {
                if (s_stuffPermanentDistrict != value)
                {
                    s_stuffPermanentDistrict = value;
                }
                OnPropertyChanged("StuffPermanentDistrict");
            }
        }

        public string StuffPhoto
        {
            get
            {
                return s_stuffPhoto;
            }
            set
            {
                if (s_stuffPhoto != value)
                {
                    s_stuffPhoto = value;
                }
                OnPropertyChanged("StuffPhoto");
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
                    StuffID = (int)reader["Stuff_Id"],
                    // StuffDate = (DateTime)reader["Stuff_Join"],
                    StuffName = (string)reader["Stuff_Name"],
                    StuffCell = (string)reader["Stuff_Cell"],
                    StuffVoterID = (string)reader["Stuff_VoterId"],
                    //  StuffType = (string)reader["Stuff_type"],
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
                s_id = (int)reader["Stuff_Id"] + 1;
            }

            conn.CloseConnection();
            return entries;
        }
        #endregion

        public void GetData(int stuffID)
        {
            StuffID = stuffID;
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From Stuff WHERE Stuff_Id = " + stuffID;
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {
                StuffName = (string)reader["Stuff_Name"];
                StuffVoterID = (string)reader["Stuff_VoterId"];
                StuffPassword = (string)reader["Stuff_Password"];
                StuffFather = (string)reader["Stuff_Father"];
                StuffMother = (string)reader["Stuff_Mother"];
                StuffDOB = (DateTime)reader["Stuff_DOB"];
                StuffProfession = (string)reader["Stuff_Profession"];
                StuffReligion = (string)reader["Stuff_Religion"];
                StuffNationality = (string)reader["Stuff_Nationality"];
                StuffPresentCO = (string)reader["Stuff_PresentCO"];
                StuffPresentVillage = (string)reader["Stuff_PresentVillage"];
                StuffPresentPost = (string)reader["Stuff_PresentPost"];
                StuffPresentThana = (string)reader["Stuff_PresentThana"];
                StuffPresentDistrict = (string)reader["Stuff_PresentDistrict"];
                StuffPermanentCO = (string)reader["Stuff_PresentCO"];
                StuffPermanentVillage = (string)reader["Stuff_PermanentVillage"];
                StuffPermanentPost = (string)reader["Stuff_PermanentPost"];
                StuffPermanentThana = (string)reader["Stuff_PermanentThana"];
                StuffPermanentDistrict = (string)reader["Stuff_PermanentDistrict"];
                StuffCell = (string)reader["Stuff_Cell"];
                StuffPhoto = Path.GetFullPath("Images/" + (string)reader["Stuff_Photo"]);
                checkExistence = 1;

            }
            if (checkExistence == 0)
            {
                MessageBox.Show("System can not find member.Check Input again.\n", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            conn.CloseConnection();
        }



        public void GetDataUnknown(string stuffUnknown)
        {
            string searchTerm;
            try
            {
                if (stuffUnknown[0] == '0')
                    searchTerm = "Stuff_Cell";
                else
                {
                    long cellorvoter = Int64.Parse(stuffUnknown);
                    if (cellorvoter % 100000000000 == 88)
                        searchTerm = "Stuff_Cell";
                    else
                        searchTerm = "Stuff_VoterID";
                }
            }
            catch (Exception)
            {
                searchTerm = "Stuff_Name";
            }
            Connection conn = new Connection();
            conn.OpenConection();
            string query = "SELECT * From Stuff";
            SqlDataReader reader = conn.DataReader(query);
            int checkExistence = 0;
            while (reader.Read())
            {

                if ((string)reader[searchTerm] == stuffUnknown)
                {
                    StuffID = (int)reader["Stuff_Id"];
                    StuffName = (string)reader["Stuff_Name"];
                    StuffVoterID = (string)reader["Stuff_VoterId"];
                    StuffFather = (string)reader["Stuff_Father"];
                    StuffMother = (string)reader["Stuff_Mother"];
                    StuffDOB = (DateTime)reader["Stuff_DOB"];
                    StuffProfession = (string)reader["Stuff_Profession"];
                    StuffReligion = (string)reader["Stuff_Religion"];
                    StuffNationality = (string)reader["Stuff_Nationality"];
                    StuffPresentCO = (string)reader["Stuff_PresentCO"];
                    StuffPresentVillage = (string)reader["Stuff_PresentVillage"];
                    StuffPresentPost = (string)reader["Stuff_PresentPost"];
                    StuffPresentThana = (string)reader["Stuff_PresentThana"];
                    StuffPresentDistrict = (string)reader["Stuff_PresentDistrict"];
                    StuffPermanentCO = (string)reader["Stuff_PresentCO"];
                    StuffPermanentVillage = (string)reader["Stuff_PermanentVillage"];
                    StuffPermanentPost = (string)reader["Stuff_PermanentPost"];
                    StuffPermanentThana = (string)reader["Stuff_PermanentThana"];
                    StuffPermanentDistrict = (string)reader["Stuff_PermanentDistrict"];
                    StuffCell = (string)reader["Stuff_Cell"];
                    StuffPhoto = Path.GetFullPath("Images/" + (string)reader["Stuff_Photo"]);
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

        public void SetStuffID()
        {
            Connection conn = new Connection();
            /// <summary>
            ///Select Last Entry No
            /// <summary/>

            string query = "SELECT TOP 1 * FROM Stuff ORDER BY Stuff_Id DESC";
            conn.OpenConection();
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                s_id = (int)reader["Stuff_Id"] + 1;
            }

            conn.CloseConnection();
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
                    if (string.IsNullOrWhiteSpace(StuffName))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Cell":
                    if (string.IsNullOrWhiteSpace(StuffCell))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "Password":
                    if (string.IsNullOrWhiteSpace(StuffPassword))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "ID":
                    break;
                case "StuffVoterID":
                    if (string.IsNullOrWhiteSpace(StuffVoterID))
                    {
                        validationMessage = "Only Digits Are Allowed";
                    }
                    break;
                case "StuffFather":
                    if (string.IsNullOrWhiteSpace(StuffFather))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffMother":
                    if (string.IsNullOrWhiteSpace(StuffMother))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffProfession":
                    if (string.IsNullOrWhiteSpace(StuffProfession))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffReligion":
                    if (string.IsNullOrWhiteSpace(StuffReligion))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffNationality":
                    if (string.IsNullOrWhiteSpace(StuffNationality))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPresentCO":
                    if (string.IsNullOrWhiteSpace(StuffPresentCO))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPresentVillage":
                    if (string.IsNullOrWhiteSpace(StuffPresentVillage))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPresentPost":
                    if (string.IsNullOrWhiteSpace(StuffPresentPost))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPresentThana":
                    if (string.IsNullOrWhiteSpace(StuffPresentThana))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPresentDistrict":
                    if (string.IsNullOrWhiteSpace(StuffPresentDistrict))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPermanentCO":
                    if (string.IsNullOrWhiteSpace(StuffPermanentCO))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPermanentVillage":
                    if (string.IsNullOrWhiteSpace(StuffPermanentVillage))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPermanentPost":
                    if (string.IsNullOrWhiteSpace(StuffPermanentPost))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPermanentThana":
                    if (string.IsNullOrWhiteSpace(StuffPermanentThana))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPermanentDistrict":
                    if (string.IsNullOrWhiteSpace(StuffPermanentDistrict))
                    {
                        validationMessage = "No Details Available";
                    }
                    break;
                case "StuffPhoto":
                    if (string.IsNullOrWhiteSpace(StuffPhoto))
                    {
                        validationMessage = "No Picture Available";
                    }
                    break;
            }

            return validationMessage;
        }
        #endregion


        #region PDFCreation
        public void PublishPDF()
        {
            string pageTitle = "Stuff Information";
            string filename = pageTitle + StuffID;
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
            string query = "SELECT * From Stuff WHERE Stuff_Id = " + StuffID;
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                StuffName = (string)reader["Stuff_Name"];
                StuffVoterID = (string)reader["Stuff_VoterId"];
                StuffFather = (string)reader["Stuff_Father"];
                StuffMother = (string)reader["Stuff_Mother"];
                StuffDOB = (DateTime)reader["Stuff_DOB"];
                StuffProfession = (string)reader["Stuff_Profession"];
                StuffReligion = (string)reader["Stuff_Religion"];
                StuffNationality = (string)reader["Stuff_Nationality"];
                StuffPresentCO = (string)reader["Stuff_PresentCO"];
                StuffPresentVillage = (string)reader["Stuff_PresentVillage"];
                StuffPresentPost = (string)reader["Stuff_PresentPost"];
                StuffPresentThana = (string)reader["Stuff_PresentThana"];
                StuffPresentDistrict = (string)reader["Stuff_PresentDistrict"];
                StuffPermanentCO = (string)reader["Stuff_PresentCO"];
                StuffPermanentVillage = (string)reader["Stuff_PermanentVillage"];
                StuffPermanentPost = (string)reader["Stuff_PermanentPost"];
                StuffPermanentThana = (string)reader["Stuff_PermanentThana"];
                StuffPermanentDistrict = (string)reader["Stuff_PermanentDistrict"];
                StuffCell = (string)reader["Stuff_Cell"];
                StuffPhoto = Path.GetFullPath("Images/" + (string)reader["Stuff_Photo"]);
            }

            Image Photo = new Image(ImageDataFactory.Create(StuffPhoto)).SetWidth(40).SetHeight(52);
            Paragraph imageP = new Paragraph("Stuff No : " + StuffID + "                                                                                                ").Add(Photo);
            doc.Add(imageP);
            Paragraph line0 = new Paragraph("Name : " + StuffName);
            doc.Add(line0);
            Paragraph line1 = new Paragraph("National ID : " + StuffVoterID);
            doc.Add(line1);
            Paragraph line2 = new Paragraph("Father/Husband : " + StuffFather);
            doc.Add(line2);
            Paragraph line3 = new Paragraph("Mother : " + StuffMother);
            doc.Add(line3);
            Paragraph line4 = new Paragraph("Profession : " + StuffProfession);
            doc.Add(line4);
            Paragraph line5 = new Paragraph("Nationality : " + StuffNationality);
            doc.Add(line5);
            Paragraph line6 = new Paragraph("Religion : " + StuffReligion);
            doc.Add(line6);
            Paragraph line7 = new Paragraph("Present Address : " + "C/O :" + StuffPresentCO);
            doc.Add(line7);
            Paragraph line8 = new Paragraph("|                           Village : " + StuffPresentVillage + "     Post : " + StuffPresentPost);
            doc.Add(line8);
            Paragraph line9 = new Paragraph("|                           Thana : " + StuffPresentThana + "      District : " + StuffPresentDistrict);
            doc.Add(line9);
            Paragraph line10 = new Paragraph("Permanent Address : " + "C/O : " + StuffPermanentCO);
            doc.Add(line10);
            Paragraph line11 = new Paragraph("|                          Village : " + StuffPermanentVillage + "     Post : " + StuffPermanentPost);
            doc.Add(line11);
            Paragraph line12 = new Paragraph("|                          Thana : " + StuffPermanentThana + "     District : " + StuffPermanentDistrict);
            doc.Add(line12);
            Paragraph line13 = new Paragraph("Cell No : " + StuffCell);
            doc.Add(line13);
            conn.CloseConnection();
            doc.Close();
        }
        #endregion

    }

}