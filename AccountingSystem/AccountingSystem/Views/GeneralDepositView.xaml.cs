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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
