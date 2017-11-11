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
            using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
            {
                if (CheckForError(StuffID) || CheckForError(StuffName) || CheckForError(StuffVoterId) || CheckForError(StuffFather) || CheckForError(StuffMother) || CheckForError(StuffCell))
                {
                    MessageBox.Show("Error!Check Input Again");
                    return;
                }
                else
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [Stuff] (Stuff_ID, Stuff_Name, Stuff_VoterId, Stuff_DOB, Stuff_Father, Stuff_Mother, Stuff_Nationality, Stuff_Religion, Stuff_Profession, Stuff_PresentCO, Stuff_PresentVillage, Stuff_PresentPost, Stuff_PresentThana, Stuff_PresentDistrict, Stuff_PermanentCO, Stuff_PermanentVillage, Stuff_PermanentPost, Stuff_PermanentThana, Stuff_PermanentDistrict, Stuff_Cell, Stuff_Photo, Stuff_Password, Stuff_Signature) VALUES (@StuffID, @StuffName, @StuffVoterId, @StuffDOB, @StuffFather, @StuffMother, @StuffNationality, @StuffReligion, @StuffProfession,  @StuffPresentCO, @StuffPresentVillage, @StuffPresentPost, @StuffPresentThana, @StuffPresentDistrict, @StuffPermanentCO, @StuffPermanentVillage, @StuffPermanentPost, @StuffPermanentThana, @StuffPermanentDistrict, @StuffCell, @StuffPhoto, @StuffPassword, @StuffSignature)", conn);
                    conn.Open();

                    //photo upload to the directory
                    //string appStartPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    //string path = appStartPath.Substring(0, appStartPath.Length - 10);

                    string StuffPhoto = "";
                    string StuffSignature = "";
                    if (PhotoNameLabel.Text != "")
                    {
                        string filename = "Photo_" + StuffName.Text + "_" + StuffID.Text + ".jpg";
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
                    if (SignatureLabel.Text != "")
                    {
                        string filenameSign = "Sign_" + StuffName.Text + "_" + StuffID.Text + ".jpg";
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
                    CmdSql.Parameters.AddWithValue("@StuffVoterId", StuffVoterId.Text);
                    CmdSql.Parameters.AddWithValue("@StuffDOB", StuffDOB.SelectedDate);
                    CmdSql.Parameters.AddWithValue("@StuffFather", StuffFather.Text);
                    CmdSql.Parameters.AddWithValue("@StuffMother", StuffMother.Text);
                    CmdSql.Parameters.AddWithValue("@StuffProfession", StuffProfession.Text);
                    CmdSql.Parameters.AddWithValue("@StuffReligion", StuffReligion.Text);
                    CmdSql.Parameters.AddWithValue("@StuffNationality", StuffNationality.Text);
                    CmdSql.Parameters.AddWithValue("@StuffPassword", StuffPassword.Text);
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
                    conn.Close();
                    data.SetStuffID();
                    MessageBox.Show("Succesfully Added New Stuff");

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
            DataContext = data;
        }


    }
}
