using AccountingSystem.Controller;
using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for GeneralDepositEntryView.xaml
    /// </summary>
    public partial class GeneralDepositEntryView : Page
    {
        GeneralLedger data;
        public GeneralDepositEntryView()
        {
            InitializeComponent();
            data = new GeneralLedger();
            DataContext = data;
        }

        public void SetForEdit(string m_id)
        {
            int id = Convert.ToInt32(m_id);
            data = new GeneralLedger();
            data.GetDataDetails(id);
            DataContext = data;
        }

        private void SaveMember_Click(object sender, RoutedEventArgs e)
        {
            if ((string)SaveMember.Content == "Add Account")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [GeneralDepositDetails] (GDId,MemberId, GDDuration, GDRefererMemberId, GDFNomineeName, GDFNomineeAge, GDFNomineeRelation, GDFNomineeShare, GDFNomineeAddress, GDSNomineeName, GDSNomineeAge, GDSNomineeRelation, GDSNomineeShare, GDSNomineeAddress, GDTNomineeName, GDTNomineeAge, GDTNomineeRelation, GDTNomineeShare, GDTNomineeAddress) VALUES (@GDId, @MemberID, @GDDuration, @GDFNomineeName, @GDFNomineeAge, @GDFNomineeRelation, @GDFNomineeShare, @GDFNomineeAddress, @GDSNomineeName, @GDSNomineeAge, @GDSNomineeRelation, @GDSNomineeShare, @GDSNomineeAddress, @GDTNomineeName, @GDTNomineeAge, @GDTNomineeRelation, @GDTNomineeShare, @GDTNomineeAddress, @GDRefererId)", conn);
                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@GDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@GDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@GDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeAddress", TNAddress.Text);

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
                }
            }
            else if ((string)SaveMember.Content == "Update Account")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("UPDATE [GeneralDepositDetails] SET GDId = @GDId, MemberId = @MemberId, GDDuration = @GDDuration, GDRefererId = @GDRefererId, GDFNomineeName = @GDFNomineeName, GDFNomineeAge = @GDFNomineeAge, GDFNomineeRelation = @GDFNomineeRelation, GDFNomineeShare = @GDFNomineeShare, GDFNomineeAddress = @GDFNomineeAddress, GDSNomineeName = @GDSNomineeName, GDSNomineeAge = @GDSNomineeAge, GDSNomineeRelation = @GDSNomineeRelation, GDSNomineeShare = @GDSNomineeShare, GDSNomineeAddress = @GDSNomineeAddress, GDTNomineeName = @GDTNomineeName, GDTNomineeAge = @GDTNomineeAge, GDTNomineeRelation = @GDTNomineeRelation, GDTNomineeShare = @GDTNomineeShare, GDTNomineeAddress = @GDTNomineeAddress WHERE GDId=" + AccountNo.Text, conn);

                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@GDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@GDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@GDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@GDTNomineeAddress", TNAddress.Text);

                    try
                    {
                        CmdSql.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        MessageBox.Show("Error\n" + exception, "warning");
                        return;
                    }

                    conn.Close();
                    MessageBox.Show("Succesfully Updated Account");
                }
            }
        }
    }
}
