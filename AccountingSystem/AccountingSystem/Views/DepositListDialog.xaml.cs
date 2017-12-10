using AccountingSystem.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for DepositListDialog.xaml
    /// </summary>
    public partial class DepositListDialog : Window
    {
        Deposits data;
        public DepositListDialog(int MID)
        {
            InitializeComponent();
            InitializeComponent();
            data = new Deposits();
            data.GetData(MID);
            if (data.DepositsAddress[0] !=0)
            {
                Deposit1.IsEnabled = true;
            }
            if (data.DepositsAddress[1] != 0)
            {
                Deposit2.IsEnabled = true;
            }
            if (data.DepositsAddress[2] != 0)
            {
                Deposit3.IsEnabled = true;
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Deposit1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new GeneralDepositView());
        }

        private void Deposit3_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new FixedDepositView());
        }

        private void Deposit2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new MonthlyDepositView());
        }

        private void Deposit4_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoanDetailsView());
        }
    }
}
