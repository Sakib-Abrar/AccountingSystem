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
        string DocAddress;
        bool changePhoto = false;
        bool changeSignature = false;
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
            if ((string)SaveMember.Content == "Add Member")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    if (CheckForError(MemberID) || CheckForError(MemberName) || CheckForError(MemberVoterId) || CheckForError(MemberFather) || CheckForError(MemberMother) || CheckForError(MemberCell))
                    {
                        MessageBox.Show("Error!Check Input Again");
                        return;
                    }
                    else
                    {
                        SqlCommand CmdSql = new SqlCommand("INSERT INTO [Member] (MemberID, MemberName, MemberVoterId, MemberDOB, MemberFather, MemberMother, MemberNationality, MemberReligion, MemberProfession, MemberPresentCO, MemberPresentVillage, MemberPresentPost, MemberPresentThana, MemberPresentDistrict, MemberPermanentCO, MemberPermanentVillage, MemberPermanentPost, MemberPermanentThana, MemberPermanentDistrict, MemberNominee, MemberNomineeDOB, MemberCell, MemberNomineeRelation,MemberPhoto,MemberSignature) VALUES (@MemberID, @MemberName, @MemberVoterId, @MemberDOB, @MemberFather, @MemberMother, @MemberNationality, @MemberReligion, @MemberProfession,  @MemberPresentCO, @MemberPresentVillage, @MemberPresentPost, @MemberPresentThana, @MemberPresentDistrict, @MemberPermanentCO, @MemberPermanentVillage, @MemberPermanentPost, @MemberPermanentThana, @MemberPermanentDistrict, @MemberNominee, @MemberNomineeDOB, @MemberCell, @MemberNomineeRelation,@MemberPhoto,@MemberSignature)", conn);
                        conn.Open();

                        string MemberPhoto = "";
                        string MemberSignature = "";
                        string extension = Path.GetExtension(PhotoNameLabel.Text);

                        if (PhotoNameLabel.Text != "")
                        {
                            string filename = "Photo_" + MemberName.Text + "_" + MemberID.Text + extension;
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
                        string signExtension = Path.GetExtension(SignatureLabel.Text);
                        if (SignatureLabel.Text != "")
                        {
                            string filenameSign = "Sign_" + MemberName.Text + "_" + MemberID.Text + signExtension;
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

                        if ((string)DocumentBrowse.Content == "Found")
                        {
                            if (CheckForError(DocumentName))
                            {
                                MessageBox.Show("Error!Check Input Again");
                                return;
                            }
                            string ext = Path.GetExtension(DocAddress);
                            string filename = "Document_" + MemberName.Text + "_" + DocumentName.Text + ext;
                            try
                            {
                                File.Copy(DocAddress, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            DocAddress = filename;
                            CmdSql = new SqlCommand("INSERT INTO [Documents] ( MemberId, DocumentAddress, DocumentName) VALUES (@MemberId, @DocumentAddress, @DocumentName)", conn);
                            CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                            CmdSql.Parameters.AddWithValue("@DocumentAddress", DocAddress);
                            CmdSql.Parameters.AddWithValue("@DocumentName", DocumentName.Text);

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

                            DocumentBrowse.Content = "Browse";
                        }

                        conn.Close();
                        data.SetMemberID();
                        MessageBox.Show("Succesfully Added New Member");

                    }

                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    if (CheckForError(MemberID) || CheckForError(MemberName) || CheckForError(MemberVoterId) || CheckForError(MemberFather) || CheckForError(MemberMother) || CheckForError(MemberCell) || CheckForError(MemberNominee))
                    {
                        MessageBox.Show("Error!Check Input Again");
                        return;
                    }
                    else
                    {
                        SqlCommand CmdSql = new SqlCommand("UPDATE [Member] SET MemberName = @MemberName, MemberVoterId = @MemberVoterId, MemberDOB = @MemberDOB, MemberFather = @MemberFather, MemberMother = @MemberMother, MemberNationality = @MemberNationality, MemberReligion = @MemberReligion, MemberProfession = @MemberProfession, MemberPresentCO = @MemberPresentCO, MemberPresentVillage = @MemberPresentVillage, MemberPresentPost = @MemberPresentPost, MemberPresentThana = @MemberPresentThana, MemberPresentDistrict = @MemberPresentDistrict, MemberPermanentCO = @MemberPermanentCO, MemberPermanentVillage = @MemberPermanentVillage, MemberPermanentPost = @MemberPermanentPost, MemberPermanentThana = @MemberPermanentThana, MemberPermanentDistrict = @MemberPermanentDistrict, MemberNominee = @MemberNominee, MemberNomineeDOB = @MemberNomineeDOB, MemberCell = @MemberCell, MemberNomineeRelation = @MemberNomineeRelation,MemberPhoto = @MemberPhoto,MemberSignature = @MemberSignature WHERE MemberID=" + MemberID.Text, conn);
                        conn.Open();

                        string MemberPhoto = "";
                        string MemberSignature = "";
                        string extension = Path.GetExtension(PhotoNameLabel.Text);

                        if (changePhoto)
                        {
                            string filename = "Photo_" + MemberName.Text + "_" + MemberID.Text + extension;
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
                        else
                        {
                            MemberPhoto = "Photo_" + MemberName.Text + "_" + MemberID.Text + extension;
                        }
                        ////Signature upload to the directory
                        string signExtension = Path.GetExtension(SignatureLabel.Text);
                        if (changeSignature)
                        {
                            string filenameSign = "Sign_" + MemberName.Text + "_" + MemberID.Text + signExtension;
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
                        else
                        {
                            MemberSignature = "Sign_" + MemberName.Text + "_" + MemberID.Text + signExtension;
                        }

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

                        if ((string)DocumentBrowse.Content == "Found")
                        {
                            if (CheckForError(DocumentName))
                            {
                                MessageBox.Show("Error!Check Input Again");
                                return;
                            }
                            string ext = Path.GetExtension(DocAddress);
                            string filename = "Document_" + MemberName.Text+"_"+DocumentName.Text+ ext;
                            try
                            {
                                File.Copy(DocAddress, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            DocAddress = filename;
                            CmdSql = new SqlCommand("INSERT INTO [Documents] ( MemberId, DocumentAddress, DocumentName) VALUES (@MemberId, @DocumentAddress, @DocumentName)", conn);
                            CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                            CmdSql.Parameters.AddWithValue("@DocumentAddress", DocAddress);
                            CmdSql.Parameters.AddWithValue("@DocumentName", DocumentName.Text);

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

                            DocumentBrowse.Content = "Browse";
                        }

                        conn.Close();
                        MessageBox.Show("Succesfully Updated Member");

                    }

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
                PhotoNameLabel.Text = open.FileName;
                changePhoto = true;
            }
        }

        private void BrowseSignature(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.jpg; *.jpeg; *; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            open.FilterIndex = 1;
            open.ShowDialog();

            if (open.CheckFileExists)
            {
                SignatureLabel.Text = open.FileName;
                changeSignature = true;
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

        public void SetForEdit(string m_id) {
            int id = Convert.ToInt32(m_id);
            data = new Members();
            data.GetData(id);
            DataContext = data;
        }

        private void DocumentBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.jpg; *.jpeg; *; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            open.FilterIndex = 1;
            open.ShowDialog();

            if (open.CheckFileExists)
            {
                DocumentBrowse.Content = "Found";
                DocAddress=open.FileName;
            }
        }
    }
}
