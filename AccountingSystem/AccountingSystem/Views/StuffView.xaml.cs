using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;
using System.Windows.Data;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for StuffView.xaml
    /// </summary>
    public partial class StuffView : Page
    {
        StuffDetailsView StuffDetObj;
        StuffEntryView StuffEntryObj;
        StuffListView StuffListObj;

        public StuffView()
        {
            InitializeComponent();
            StuffListObj = new StuffListView();
            stuffData.Navigate(StuffListObj);
            DataContext = new Stuff();
        }


        private void Search(object sender, RoutedEventArgs e)
        {
            StuffDetObj = new StuffDetailsView();
            StuffDetObj.SearchWithUnknown(searchid.Text);
            stuffData.Navigate(StuffDetObj);
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            StuffEntryObj = new StuffEntryView();
            stuffData.Navigate(StuffEntryObj);
        }

    }
}