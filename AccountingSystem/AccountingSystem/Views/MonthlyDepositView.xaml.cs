using AccountingSystem.Models;
using System.Windows;
using System.Windows.Controls;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MonthlyDepositView.xaml
    /// </summary>
    public partial class MonthlyDepositView : Page
    {
        MonthlyDepositDetailsView GDInfoObj;
        MonthlyDepositEntryView GDEntryObj;
        MonthlyDepositListView GDListObj;
        public MonthlyDepositView()
        {
            InitializeComponent();
            GDListObj = new MonthlyDepositListView();
            memberData.Navigate(GDListObj);
            DataContext = new MonthlyDeposit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            GDInfoObj = new MonthlyDepositDetailsView();
            GDInfoObj.SearchWithUnknown(searchid.Text);
            memberData.Navigate(GDInfoObj);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            GDEntryObj = new MonthlyDepositEntryView();
            memberData.Navigate(GDEntryObj);
        }
    }
}
