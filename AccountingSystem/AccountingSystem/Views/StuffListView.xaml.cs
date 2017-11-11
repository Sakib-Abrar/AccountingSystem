using System;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Data;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for StuffListView.xaml
    /// </summary>
    public partial class StuffListView : Page
    {
        StuffDetailsView StuffDetObj;
        StuffView StuffViewObj;
        public StuffListView()
        {
            InitializeComponent();
            Stuff data = new Stuff();
            stufflist.ItemsSource = data.GetData();
            DataContext = data;
        }


        private void searchStuff(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Stuff classObj = stufflist.SelectedItem as Stuff;
            int id = classObj.StuffID;
            StuffViewObj = new StuffView();
            StuffDetObj = new StuffDetailsView();
            StuffDetObj.SearchWithID(id);
            this.NavigationService.Navigate(StuffDetObj);
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}