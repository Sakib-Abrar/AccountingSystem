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
    /// Interaction logic for GeneralDepositListView.xaml
    /// </summary>
    public partial class GeneralDepositListView : Page
    {
        private GeneralDepositDetailsView GDDetailsObj;
        private GeneralDepositView GDViewObj;

        public GeneralDepositListView()
        {
            InitializeComponent();
            GeneralLedger data = new GeneralLedger();
            generalDepositlist.ItemsSource = data.GetDataList();
            DataContext = data;
        }

        private void generalDepositlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void generalDepositlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GeneralLedger classObj = generalDepositlist.SelectedItem as GeneralLedger;
            int id = classObj.MemberId;
            GDViewObj = new GeneralDepositView();
            GDDetailsObj = new GeneralDepositDetailsView();
            GDDetailsObj.SearchWithID(id);
            this.NavigationService.Navigate(GDDetailsObj);
        }
    }
}
