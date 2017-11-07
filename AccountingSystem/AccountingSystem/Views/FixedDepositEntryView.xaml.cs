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

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for FixedDepositEntryView.xaml
    /// </summary>
    public partial class FixedDepositEntryView : Page
    {
        FixedDeposit data;
        public FixedDepositEntryView()
        {
            InitializeComponent();
            data = new FixedDeposit();
            DataContext = data;
        }

        public void SetForEdit(string m_id)
        {
            int id = Convert.ToInt32(m_id);
            data = new FixedDeposit();
            data.GetDataDetails(id);
            DataContext = data;
        }

        private void SaveMember_Click(object sender, RoutedEventArgs e)
        {
            if ((string)SaveMember.Content == "Add Account")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [FixedDepositDetails] (FDId,MemberId, FDDuration, FDRefererMemberId, FDFNomineeName, FDFNomineeAge, FDFNomineeRelation, FDFNomineeShare, FDFNomineeAddress, FDSNomineeName, FDSNomineeAge, FDSNomineeRelation, FDSNomineeShare, FDSNomineeAddress, FDTNomineeName, FDTNomineeAge, FDTNomineeRelation, FDTNomineeShare, FDTNomineeAddress) VALUES (@FDId, @MemberID, @FDDuration, @FDFNomineeName, @FDFNomineeAge, @FDFNomineeRelation, @FDFNomineeShare, @FDFNomineeAddress, @FDSNomineeName, @FDSNomineeAge, @FDSNomineeRelation, @FDSNomineeShare, @FDSNomineeAddress, @FDTNomineeName, @FDTNomineeAge, @FDTNomineeRelation, @FDTNomineeShare, @FDTNomineeAddress, @FDRefererId)", conn);
                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@FDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@FDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@FDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeAddress", TNAddress.Text);

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
                    SqlCommand CmdSql = new SqlCommand("UPDATE [FixedDepositDetails] SET FDId = @FDId, MemberId = @MemberId, FDDuration = @FDDuration, FDRefererId = @FDRefererId, FDFNomineeName = @FDFNomineeName, FDFNomineeAge = @FDFNomineeAge, FDFNomineeRelation = @FDFNomineeRelation, FDFNomineeShare = @FDFNomineeShare, FDFNomineeAddress = @FDFNomineeAddress, FDSNomineeName = @FDSNomineeName, FDSNomineeAge = @FDSNomineeAge, FDSNomineeRelation = @FDSNomineeRelation, FDSNomineeShare = @FDSNomineeShare, FDSNomineeAddress = @FDSNomineeAddress, FDTNomineeName = @FDTNomineeName, FDTNomineeAge = @FDTNomineeAge, FDTNomineeRelation = @FDTNomineeRelation, FDTNomineeShare = @FDTNomineeShare, FDTNomineeAddress = @FDTNomineeAddress WHERE FDId=" + AccountNo.Text, conn);

                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@FDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@FDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@FDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@FDTNomineeAddress", TNAddress.Text);

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

