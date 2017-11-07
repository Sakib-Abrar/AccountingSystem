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
    /// Interaction logic for FixedDepositListView.xaml
    /// </summary>
    public partial class FixedDepositListView : Page
    {
        private FixedDepositDetailsView GDDetailsObj;
        private FixedDepositView GDViewObj;

        public FixedDepositListView()
        {
            InitializeComponent();
            FixedDeposit data = new FixedDeposit();
            generalDepositlist.ItemsSource = data.GetDataList();
            DataContext = data;
        }

        private void generalDepositlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void generalDepositlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FixedDeposit classObj = generalDepositlist.SelectedItem as FixedDeposit;
            int id = classObj.MemberId;
            GDViewObj = new FixedDepositView();
            GDDetailsObj = new FixedDepositDetailsView();
            GDDetailsObj.SearchWithID(id);
            this.NavigationService.Navigate(GDDetailsObj);
        }
    }
}
