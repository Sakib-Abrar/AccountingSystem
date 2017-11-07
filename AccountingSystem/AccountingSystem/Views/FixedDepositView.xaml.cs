using AccountingSystem.Models;
using System.Windows;
using System.Windows.Controls;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for FixedDepositView.xaml
    /// </summary>
    public partial class FixedDepositView : Page
    {
        FixedDepositDetailsView GDInfoObj;
        FixedDepositEntryView GDEntryObj;
        FixedDepositListView GDListObj;
        public FixedDepositView()
        {
            InitializeComponent();
            GDListObj = new FixedDepositListView();
            memberData.Navigate(GDListObj);
            DataContext = new FixedDeposit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            GDInfoObj = new FixedDepositDetailsView();
            GDInfoObj.SearchWithUnknown(searchid.Text);
            memberData.Navigate(GDInfoObj);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            GDEntryObj = new FixedDepositEntryView();
            memberData.Navigate(GDEntryObj);
        }
    }
}
