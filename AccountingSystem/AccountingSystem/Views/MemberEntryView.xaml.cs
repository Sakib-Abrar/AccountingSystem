using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using System.Data.SqlClient;
using System.Windows.Data;
using Microsoft.Win32;
using System;
using System.IO;
using AccountingSystem.Models;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MemberEntryView.xaml
    /// </summary>
    public partial class MemberEntryView : Page 
    {
        Members data;
        public MemberEntryView()
        {
            InitializeComponent();
            data = new Members();
            data.SetMemberID();
            DataContext = data;
        }

        public bool CheckForError(TextBox Selected)
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
                if (CheckForError(MemberID) || CheckForError(MemberName) || CheckForError(MemberVoterId)|| CheckForError(MemberFather) || CheckForError(MemberMother) || CheckForError(MemberCell))
                {
                    MessageBox.Show("Error!Check Input Again");
                    return;
                }
                else
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Member] (MemberID, MemberName, MemberVoterId, MemberDOB, MemberFather, MemberMother, MemberNationality, MemberReligion, MemberProfession, MemberPresentCO, MemberPresentVillage, MemberPresentPost, MemberPresentThana, MemberPresentDistrict, MemberPermanentCO, MemberPermanentVillage, MemberPermanentPost, MemberPermanentThana, MemberPermanentDistrict, MemberNominee, MemberNomineeDOB, MemberCell, MemberNomineeRelation,MemberPhoto,MemberSignature) VALUES (@MemberID, @MemberName, @MemberVoterId, @MemberDOB, @MemberFather, @MemberMother, @MemberNationality, @MemberReligion, @MemberProfession,  @MemberPresentCO, @MemberPresentVillage, @MemberPresentPost, @MemberPresentThana, @MemberPresentDistrict, @MemberPermanentCO, @MemberPermanentVillage, @MemberPermanentPost, @MemberPermanentThana, @MemberPermanentDistrict, @MemberNominee, @MemberNomineeDOB, @MemberCell, @MemberNomineeRelation,@MemberPhoto,@MemberSignature)", conn);
                    conn.Open();

                    //photo upload to the directory
                    //string appStartPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    //string path = appStartPath.Substring(0, appStartPath.Length - 10);
                    
                    string MemberPhoto="";
                    string MemberSignature="";
                    if (PhotoNameLabel.Text != "")
                    {
                        string filename = "Photo_" + MemberName.Text + "_" + MemberID.Text + ".jpg";
                        try
                        {
                            File.Copy(PhotoNameLabel.Text, Path.GetFullPath("Images/" + filename));
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Error\n" + exc, "warning");
                        }
                        MemberPhoto = filename;
                    }
                    ////Signature upload to the directory
                    if (SignatureLabel.Text != "")
                    {
                        string filenameSign = "Sign_" + MemberName.Text + "_" + MemberID.Text + ".jpg";
                        try
                        {
                            File.Copy(SignatureLabel.Text, Path.GetFullPath("Images/" + filenameSign));
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Error\n" + exc, "warning");
                        }
                    MemberSignature = filenameSign;
                    }

                    CmdSql.Parameters.AddWithValue("@MemberID", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@MemberName", MemberName.Text);
                    CmdSql.Parameters.AddWithValue("@MemberVoterId", MemberVoterId.Text);
                    CmdSql.Parameters.AddWithValue("@MemberDOB", MemberDOB.SelectedDate);
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
                    CmdSql.Parameters.AddWithValue("@MemberNomineeDOB", MemberNomineeDOB.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@MemberCell", MemberCell.Text);
                    CmdSql.Parameters.AddWithValue("@MemberNomineeRelation", MemberNomineeRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MemberPhoto", MemberPhoto);
                    CmdSql.Parameters.AddWithValue("@MemberSignature", MemberSignature);
                    try
                    {
                        CmdSql.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        if (exception.ErrorCode == 2627)
                            MessageBox.Show("Error.Id already exists.", "warning");
                        else
                            MessageBox.Show("Error\n" + exception, "warning");
                        return;
                    }
                    conn.Close();
                    data.SetMemberID();
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

        private void ImportPresentAddress(object sender, RoutedEventArgs e)
        {
            MemberPermanentCO.Text=MemberPresentCO.Text;
            MemberPermanentVillage.Text=MemberPresentVillage.Text;
            MemberPermanentPost.Text= MemberPresentPost.Text;
            MemberPermanentThana.Text=MemberPresentThana.Text;
            MemberPermanentDistrict.Text=MemberPresentDistrict.Text;
        }

        private void ImportNullAddress(object sender, RoutedEventArgs e)
        {
            MemberPermanentCO.Text = "";
            MemberPermanentVillage.Text = "";
            MemberPermanentPost.Text = "";
            MemberPermanentThana.Text = "";
            MemberPermanentDistrict.Text = "";
        }
    }
}
