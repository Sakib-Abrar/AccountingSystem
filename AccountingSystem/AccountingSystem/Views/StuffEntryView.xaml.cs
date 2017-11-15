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
    /// Interaction logic for StuffEntryView.xaml
    /// </summary>
    public partial class StuffEntryView : Page
    {
        Stuff data;
        string DocAddress;
        bool changePhoto = false;
        bool changeSignature = false;
        String stuffType;
        public StuffEntryView()
        {
            InitializeComponent();
            data = new Stuff();
            data.SetStuffID();
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

        private void AddStuff(object sender, RoutedEventArgs e)
        {
            
            if (Type_Stuff.IsChecked == true)
            {
                stuffType = "stuff";
            }
            else if (Type_Admin.IsChecked == true)
            {
                stuffType = "admin";
            }
            if ((string)SaveStuff.Content == "Add Stuff")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    if (CheckForError(StuffID) || CheckForError(StuffName) || CheckForError(StuffVoterId) || CheckForError(StuffFather) || CheckForError(StuffMother) || CheckForError(StuffCell))
                    {
                        MessageBox.Show("Error!Check Input Again");
                        return;
                    }
                    else
                    {
                        SqlCommand CmdSql = new SqlCommand("INSERT INTO [Stuff] (Stuff_Id, Stuff_Name, Stuff_Password, Stuff_VoterId, Stuff_DOB, Stuff_Join, Stuff_Father, Stuff_Mother, Stuff_Nationality, Stuff_Religion, Stuff_Profession, Stuff_PresentCO, Stuff_PresentVillage, Stuff_PresentPost, Stuff_PresentThana, Stuff_PresentDistrict, Stuff_PermanentCO, Stuff_PermanentVillage, Stuff_PermanentPost, Stuff_PermanentThana, Stuff_PermanentDistrict, Stuff_Cell, Stuff_Type, Stuff_Photo, Stuff_Signature) VALUES (@StuffID, @StuffName, @StuffPassword, @StuffVoterId, @StuffDOB, @StuffJoin, @StuffFather, @StuffMother, @StuffNationality, @StuffReligion, @StuffProfession,  @StuffPresentCO, @StuffPresentVillage, @StuffPresentPost, @StuffPresentThana, @StuffPresentDistrict, @StuffPermanentCO, @StuffPermanentVillage, @StuffPermanentPost, @StuffPermanentThana, @StuffPermanentDistrict, @StuffCell, @StuffType, @StuffPhoto, @StuffSignature)", conn);
                        conn.Open();

                        string StuffPhoto = "";
                        string StuffSignature = "";
                        string extension = Path.GetExtension(PhotoNameLabel.Text);

                        if (PhotoNameLabel.Text != "")
                        {
                            string filename = "Photo_" + StuffName.Text + "_" + StuffID.Text + extension;
                            try
                            {
                                File.Copy(PhotoNameLabel.Text, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            StuffPhoto = filename;
                        }
                        ////Signature upload to the directory
                        string signExtension = Path.GetExtension(SignatureLabel.Text);
                        if (SignatureLabel.Text != "")
                        {
                            string filenameSign = "Sign_" + StuffName.Text + "_" + StuffID.Text + signExtension;
                            try
                            {
                                File.Copy(SignatureLabel.Text, Path.GetFullPath("Images/" + filenameSign));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            StuffSignature = filenameSign;
                        }

                        CmdSql.Parameters.AddWithValue("@StuffID", StuffID.Text);
                        CmdSql.Parameters.AddWithValue("@StuffName", StuffName.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPassword", StuffPassword.Text);
                        CmdSql.Parameters.AddWithValue("@StuffVoterId", StuffVoterId.Text);
                        CmdSql.Parameters.AddWithValue("@StuffDOB", StuffDOB.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@StuffJoin", StuffJoin.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@StuffFather", StuffFather.Text);
                        CmdSql.Parameters.AddWithValue("@StuffMother", StuffMother.Text);
                        CmdSql.Parameters.AddWithValue("@StuffProfession", StuffProfession.Text);
                        CmdSql.Parameters.AddWithValue("@StuffReligion", StuffReligion.Text);
                        CmdSql.Parameters.AddWithValue("@StuffNationality", StuffNationality.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentCO", StuffPresentCO.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentVillage", StuffPresentVillage.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentPost", StuffPresentPost.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentThana", StuffPresentThana.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentDistrict", StuffPresentDistrict.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentCO", StuffPermanentCO.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentVillage", StuffPermanentVillage.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentPost", StuffPermanentPost.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentThana", StuffPermanentThana.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentDistrict", StuffPermanentDistrict.Text);
                        CmdSql.Parameters.AddWithValue("@StuffCell", StuffCell.Text);
                        CmdSql.Parameters.AddWithValue("@StuffType", stuffType);
                        CmdSql.Parameters.AddWithValue("@StuffPhoto", StuffPhoto);
                        CmdSql.Parameters.AddWithValue("@StuffSignature", StuffSignature);
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
                            string filename = "Document_" + StuffName.Text + "_" + StuffName.Text + ext;
                            try
                            {
                                File.Copy(DocAddress, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            DocAddress = filename;
                            CmdSql = new SqlCommand("INSERT INTO [Documents] ( Stuff_Id, DocumentAddress, DocumentName) VALUES (@StuffId, @DocumentAddress, @DocumentName)", conn);
                            CmdSql.Parameters.AddWithValue("@StuffId", StuffID.Text);
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
                        data.SetStuffID();
                        MessageBox.Show("Succesfully Added New Stuff");

                    }

                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    if (CheckForError(StuffID) || CheckForError(StuffName) || CheckForError(StuffVoterId) || CheckForError(StuffFather) || CheckForError(StuffMother) || CheckForError(StuffCell))
                    {
                        MessageBox.Show("Error!Check Input Again");
                        return;
                    }
                    else
                    {
                        SqlCommand CmdSql = new SqlCommand("UPDATE [Stuff] SET Stuff_Name = @StuffName, Stuff_VoterId = @StuffVoterId, Stuff_DOB = @StuffDOB, Stuff_Join = @StuffJoin, Stuff_Father = @StuffFather, Stuff_Mother = @StuffMother, Stuff_Nationality = @StuffNationality, Stuff_Religion = @StuffReligion, Stuff_Profession = @StuffProfession, Stuff_PresentCO = @StuffPresentCO, Stuff_PresentVillage = @StuffPresentVillage, Stuff_PresentPost = @StuffPresentPost, Stuff_PresentThana = @StuffPresentThana, Stuff_PresentDistrict = @StuffPresentDistrict, Stuff_PermanentCO = @StuffPermanentCO, Stuff_PermanentVillage = @StuffPermanentVillage, Stuff_PermanentPost = @StuffPermanentPost, Stuff_PermanentThana = @StuffPermanentThana, Stuff_PermanentDistrict = @StuffPermanentDistrict, Stuff_Cell = @StuffCell, Stuff_Type=@StuffType, Stuff_Photo = @StuffPhoto, Stuff_Signature = @StuffSignature WHERE Stuff_Id=" + StuffID.Text, conn);
                        conn.Open();

                        string StuffPhoto = "";
                        string StuffSignature = "";
                        string extension = Path.GetExtension(PhotoNameLabel.Text);

                        if (changePhoto)
                        {
                            string filename = "Photo_" + StuffName.Text + "_" + StuffID.Text + extension;
                            try
                            {
                                File.Copy(PhotoNameLabel.Text, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            StuffPhoto = filename;
                        }
                        else
                        {
                            StuffPhoto = "Photo_" + StuffName.Text + "_" + StuffID.Text + extension;
                        }
                        ////Signature upload to the directory
                        string signExtension = Path.GetExtension(SignatureLabel.Text);
                        if (changeSignature)
                        {
                            string filenameSign = "Sign_" + StuffName.Text + "_" + StuffID.Text + signExtension;
                            try
                            {
                                File.Copy(SignatureLabel.Text, Path.GetFullPath("Images/" + filenameSign));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            StuffSignature = filenameSign;
                        }
                        else
                        {
                            StuffSignature = "Sign_" + StuffName.Text + "_" + StuffID.Text + signExtension;
                        }

                        CmdSql.Parameters.AddWithValue("@StuffName", StuffName.Text);
                        CmdSql.Parameters.AddWithValue("@StuffVoterId", StuffVoterId.Text);
                        CmdSql.Parameters.AddWithValue("@StuffDOB", StuffDOB.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@StuffJoin", StuffJoin.SelectedDate);
                        CmdSql.Parameters.AddWithValue("@StuffFather", StuffFather.Text);
                        CmdSql.Parameters.AddWithValue("@StuffMother", StuffMother.Text);
                        CmdSql.Parameters.AddWithValue("@StuffProfession", StuffProfession.Text);
                        CmdSql.Parameters.AddWithValue("@StuffReligion", StuffReligion.Text);
                        CmdSql.Parameters.AddWithValue("@StuffNationality", StuffNationality.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentCO", StuffPresentCO.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentVillage", StuffPresentVillage.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentPost", StuffPresentPost.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentThana", StuffPresentThana.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPresentDistrict", StuffPresentDistrict.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentCO", StuffPermanentCO.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentVillage", StuffPermanentVillage.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentPost", StuffPermanentPost.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentThana", StuffPermanentThana.Text);
                        CmdSql.Parameters.AddWithValue("@StuffPermanentDistrict", StuffPermanentDistrict.Text);
                        CmdSql.Parameters.AddWithValue("@StuffCell", StuffCell.Text);
                        CmdSql.Parameters.AddWithValue("@StuffType", stuffType);
                        CmdSql.Parameters.AddWithValue("@StuffPhoto", StuffPhoto);
                        CmdSql.Parameters.AddWithValue("@StuffSignature", StuffSignature);

                        try
                        {
                            CmdSql.ExecuteNonQuery();
                        }
                        catch (SqlException exception)
                        {
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
                            string filename = "Document_" + StuffName.Text + "_" + DocumentName.Text + ext;
                            try
                            {
                                File.Copy(DocAddress, Path.GetFullPath("Images/" + filename));
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("Error\n" + exc, "warning");
                            }
                            DocAddress = filename;
                            CmdSql = new SqlCommand("INSERT INTO [Documents] ( StuffId, DocumentAddress, DocumentName) VALUES (@StuffId, @DocumentAddress, @DocumentName)", conn);
                            CmdSql.Parameters.AddWithValue("@StuffId", StuffID.Text);
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
                        MessageBox.Show("Succesfully Updated Stuff");

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
            StuffPermanentCO.Text = StuffPresentCO.Text;
            StuffPermanentVillage.Text = StuffPresentVillage.Text;
            StuffPermanentPost.Text = StuffPresentPost.Text;
            StuffPermanentThana.Text = StuffPresentThana.Text;
            StuffPermanentDistrict.Text = StuffPresentDistrict.Text;
        }

        private void ImportNullAddress(object sender, RoutedEventArgs e)
        {
            StuffPermanentCO.Text = "";
            StuffPermanentVillage.Text = "";
            StuffPermanentPost.Text = "";
            StuffPermanentThana.Text = "";
            StuffPermanentDistrict.Text = "";
        }

        public void SetForEdit(string s_id)
        {
            int id = Convert.ToInt32(s_id);
            data = new Stuff();
            data.GetData(id);
            stuffType = data.StuffType;

            if (stuffType=="stuff") 
            {
                Type_Stuff.IsChecked = true;
            }
            else
            {
                Type_Admin.IsChecked = true;
            }
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
                DocAddress = open.FileName;
            }
        }


    }
}
