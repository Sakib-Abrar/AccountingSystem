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
    /// Interaction logic for LoanListDialog.xaml
    /// </summary>
    public partial class LoanListDialog : Window
    {
        Loans data;
        public LoanListDialog(int MID)
        {
            InitializeComponent();
            data = new Loans();
            data.GetData(MID);
            if (data.CountExistence > 0)
            {
                Loan1.IsEnabled = true;
                Label1.Content = data.LoansName[0];
            }
            if (data.CountExistence > 1)
            {
                Loan2.IsEnabled = true;
                Label2.Content = data.LoansName[1];
            }
            if (data.CountExistence > 2)
            {
                Loan3.IsEnabled = true;
                Label3.Content = data.LoansName[2];
            }
            if (data.CountExistence > 3)
            {
                Loan4.IsEnabled = true;
                Label4.Content = data.LoansName[3];
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Loan1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoanDetailsView());
        }

        private void Loan2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoanDetailsView());
        }

        private void Loan3_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoanDetailsView());
        }

        private void Loan4_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new LoanDetailsView());
        }
    }
}
