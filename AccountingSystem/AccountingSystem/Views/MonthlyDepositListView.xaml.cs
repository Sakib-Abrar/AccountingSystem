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
    /// Interaction logic for MonthlyDepositListView.xaml
    /// </summary>
    public partial class MonthlyDepositListView : Page
    {
        private MonthlyDepositDetailsView GDDetailsObj;
        private MonthlyDepositView GDViewObj;

        public MonthlyDepositListView()
        {
            InitializeComponent();
            MonthlyDeposit data = new MonthlyDeposit();
            generalDepositlist.ItemsSource = data.GetDataList();
            DataContext = data;
        }

        private void generalDepositlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void generalDepositlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MonthlyDeposit classObj = generalDepositlist.SelectedItem as MonthlyDeposit;
            int id = classObj.MemberId;
            GDViewObj = new MonthlyDepositView();
            GDDetailsObj = new MonthlyDepositDetailsView();
            GDDetailsObj.SearchWithID(id);
            this.NavigationService.Navigate(GDDetailsObj);
        }
    }
}
