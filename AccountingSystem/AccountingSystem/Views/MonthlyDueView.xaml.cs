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
    /// Interaction logic for DaulyDueView.xaml
    /// </summary>
    public partial class MonthlyDueView : Page
    {
        public MonthlyDueView()
        {
            InitializeComponent();
            DueModel data = new DueModel();
            dueDetails.ItemsSource = data.GetData("Monthly");
            DataContext = data;
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
