using AccountingSystem.Models;
using System.Windows;
using System.Windows.Controls;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for GeneralDepositView.xaml
    /// </summary>
    public partial class GeneralDepositView : Page
    {
        GeneralDepositDetailsView GDInfoObj;
        GeneralDepositEntryView GDEntryObj;
        GeneralDepositListView GDListObj;
        public GeneralDepositView()
        {
            InitializeComponent();
            GDListObj = new GeneralDepositListView();
            memberData.Navigate(GDListObj);
            DataContext = new GeneralLedger();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            GDInfoObj = new GeneralDepositDetailsView();
            GDInfoObj.SearchWithUnknown(searchid.Text);
            memberData.Navigate(GDInfoObj);
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            GDEntryObj = new GeneralDepositEntryView();
            memberData.Navigate(GDEntryObj);
        }
    }
}
