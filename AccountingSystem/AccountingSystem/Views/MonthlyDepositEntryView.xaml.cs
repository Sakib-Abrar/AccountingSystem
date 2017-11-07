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
    /// Interaction logic for MonthlyDepositEntryView.xaml
    /// </summary>
    public partial class MonthlyDepositEntryView : Page
    {
        MonthlyDeposit data;
        public MonthlyDepositEntryView()
        {
            InitializeComponent();
            data = new MonthlyDeposit();
            DataContext = data;
        }

        public void SetForEdit(string m_id)
        {
            int id = Convert.ToInt32(m_id);
            data = new MonthlyDeposit();
            data.GetDataDetails(id);
            DataContext = data;
        }

        private void SaveMember_Click(object sender, RoutedEventArgs e)
        {
            if ((string)SaveMember.Content == "Add Account")
            {
                using (SqlConnection conn = new SqlConnection(@Connection.ConnectionString))
                {
                    SqlCommand CmdSql = new SqlCommand("INSERT INTO [MonthlyDepositDetails] (MDId,MemberId, MDDuration, MDRefererMemberId, MDFNomineeName, MDFNomineeAge, MDFNomineeRelation, MDFNomineeShare, MDFNomineeAddress, MDSNomineeName, MDSNomineeAge, MDSNomineeRelation, MDSNomineeShare, MDSNomineeAddress, MDTNomineeName, MDTNomineeAge, MDTNomineeRelation, MDTNomineeShare, MDTNomineeAddress) VALUES (@MDId, @MemberID, @MDDuration, @MDFNomineeName, @MDFNomineeAge, @MDFNomineeRelation, @MDFNomineeShare, @MDFNomineeAddress, @MDSNomineeName, @MDSNomineeAge, @MDSNomineeRelation, @MDSNomineeShare, @MDSNomineeAddress, @MDTNomineeName, @MDTNomineeAge, @MDTNomineeRelation, @MDTNomineeShare, @MDTNomineeAddress, @MDRefererId)", conn);
                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@MDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@MDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@MDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeAddress", TNAddress.Text);

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
                    SqlCommand CmdSql = new SqlCommand("UPDATE [MonthlyDepositDetails] SET MDId = @MDId, MemberId = @MemberId, MDDuration = @MDDuration, MDRefererId = @MDRefererId, MDFNomineeName = @MDFNomineeName, MDFNomineeAge = @MDFNomineeAge, MDFNomineeRelation = @MDFNomineeRelation, MDFNomineeShare = @MDFNomineeShare, MDFNomineeAddress = @MDFNomineeAddress, MDSNomineeName = @MDSNomineeName, MDSNomineeAge = @MDSNomineeAge, MDSNomineeRelation = @MDSNomineeRelation, MDSNomineeShare = @MDSNomineeShare, MDSNomineeAddress = @MDSNomineeAddress, MDTNomineeName = @MDTNomineeName, MDTNomineeAge = @MDTNomineeAge, MDTNomineeRelation = @MDTNomineeRelation, MDTNomineeShare = @MDTNomineeShare, MDTNomineeAddress = @MDTNomineeAddress WHERE MDId=" + AccountNo.Text, conn);

                    conn.Open();

                    CmdSql.Parameters.AddWithValue("@MDId", AccountNo.Text);
                    CmdSql.Parameters.AddWithValue("@MemberId", MemberID.Text);
                    CmdSql.Parameters.AddWithValue("@MDDuration", GeneralDuration.Text);
                    CmdSql.Parameters.AddWithValue("@MDRefererId", RefererId.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeName", FNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDFomineeAge", FNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeRelation", FNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeShare", FNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDFNomineeAddress", FNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeName", SNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeAge", SNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeRelation", SNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeShare", SNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDSNomineeAddress", SNAddress.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeName", TNominee.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeAge", TNAge.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeRelation", TNRelation.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeShare", TNShare.Text);
                    CmdSql.Parameters.AddWithValue("@MDTNomineeAddress", TNAddress.Text);

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
