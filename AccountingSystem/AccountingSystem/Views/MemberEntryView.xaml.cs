using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.Windows.Data;
using Microsoft.Win32;
namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MemberEntryView.xaml
    /// </summary>
    public partial class MemberEntryView : Page 
    {
        public MemberEntryView()
        {
            InitializeComponent();
        }

        private bool CheckForError(TextBox Selected)
        {
            BindingExpression Trigger = Selected.GetBindingExpression(TextBox.TextProperty);
            Trigger.UpdateSource();
            if (Validation.GetHasError(Selected) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
       
        private void AddMember(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {
                if (CheckForError(MemberID) || CheckForError(MemberName) || CheckForError(VoterId))
                {
                    MessageBox.Show("Error!Check Input Again");
                    return;
                }
                else
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Member] (MemberID, MemberName, MemberVoterId, MemberDOB, MemberFather, MemberMother, MemberNationality, MemberReligion, MemberProfession, MemberPresentCO, MemberPresentVillage, MemberPresentPost, MemberPresentThana, MemberPresentDistrict, MemberPermanentCO, MemberPermanentVillage, MemberPermanentPost, MemberPermanentThana, MemberPermanentDistrict, MemberNominee, MemberNomineeDOB, MemberNomineeCell, MemberNomineeRelation,MemberPhoto,MemberSignature) VALUES (@MemberID, @MemberName, @MemberVoterId, @MemberDOB, @MemberFather, @MemberMother, @MemberNationality, @MemberReligion, @MemberProfession,  @MemberPresentCO, @MemberPresentVillage, @MemberPresentPost, @MemberPresentThana, @MemberPresentDistrict, @MemberPermanentCO, @MemberPermanentVillage, @MemberPermanentPost, @MemberPermanentThana, @MemberPermanentDistrict, @MemberNominee, @MemberNomineeDOB, @MemberNomineeCell, @MemberNomineeRelation,@MemberPhoto,@MemberSignature)", conn);
                    conn.Open();

                    //photo upload to the directory
                    string appStartPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    string path = appStartPath.Substring(0, appStartPath.Length - 10);
                    //  string filename = System.IO.Path.GetFileName(PhotoNameLabel.Text);
                    string filename = MemberName.Text + "_" + MemberID.Text + ".jpg";
                    System.IO.File.Copy(PhotoNameLabel.Text, path + "\\Images\\MemberPhoto\\" + filename);
                   // string MemberPhoto = "\\Images\\MemberPhoto\\" + filename;
                    string MemberPhoto = filename;

                    //Signature upload to the directory

                    string filenameSign = "Sign_"+MemberName.Text + "_" + MemberID.Text + ".jpg";
                    System.IO.File.Copy(SignatureLabel.Text, path + "\\Images\\MemberSign\\" + filenameSign);
                   // string MemberSignature = "\\Images\\MemberSign\\" + filenameSign;
                    string MemberSignature = filenameSign;

                    CmdSql.Parameters.AddWithValue("@MemberID", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@MemberName", MemberName.Text);
                    CmdSql.Parameters.AddWithValue("@MemberVoterId", VoterId.Text);
                    CmdSql.Parameters.AddWithValue("@MemberDOB", MemberDOB.Text);
                    CmdSql.Parameters.AddWithValue("@MemberFather", MemberFather.Text);
                    CmdSql.Parameters.AddWithValue("@MemberMother", MemberMother.Text);
                    CmdSql.Parameters.AddWithValue("@MemberProfession", MemberProfession.Text);
                    CmdSql.Parameters.AddWithValue("@MemberReligion", MemberReligion.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNationality", MemberNationality.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPresentCO", MemberPresentCO.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPresentVillage", MemberPresentVillage.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPresentPost", MemberPresentPost.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPresentThana", MemberPresentThana.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPresentDistrict", MemberPresentDistrict.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPermanentCO", MemberPermanentCO.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPermanentVillage", MemberPermanentVillage.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPermanentPost", MemberPermanentPost.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPermanentThana", MemberPermanentThana.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPermanentDistrict", MemberPermanentDistrict.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNominee", MemberNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNomineeDOB", MemberNomineeDOB.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNomineeCell", MemberNomineeCell.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNomineeRelation", MemberNomineeRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPhoto", MemberPhoto);
                    CmdSql.Parameters.AddWithValue("@MemberSignature", MemberSignature);
                    CmdSql.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Succesfully Added New Member");
             
                }
                

            }
        }

        private void BrowsePhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.jpg; *.jpeg; *; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            open.FilterIndex = 1;
            open.ShowDialog();

            if (open.CheckFileExists)
            {

                // image file path  
                PhotoNameLabel.Text = open.FileName;
                // display image in picture box 
                 
               //PhotoViewer.Image = new Bitmap(open.FileName);
            }
        }

        private void BrowsSignature(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.jpg; *.jpeg; *; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            open.FilterIndex = 1;
            open.ShowDialog();

            if (open.CheckFileExists)
            {

                // image file path  
                SignatureLabel.Text = open.FileName;
                // display image in picture box 

                // PhotoViewer.Image = new Bitmap(open.FileName);
            }
        }
    }
}
