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
    public partial class WeeklyDueView : Page
    {
        public string method = "Weekly";
        public WeeklyDueView()
        {
            InitializeComponent();

            DueModel data = new DueModel();
            dueDetails.ItemsSource = data.GetData(method);
            DataContext = data;

            LoanLedger data2 = new LoanLedger();
            WeeklyLedger.ItemsSource = data2.GetData(method);
            DataContext = data2;
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        protected void Print_Data(object sender, RoutedEventArgs e)
        {
            PrintDialogView getDate = new PrintDialogView();
            if (getDate.ShowDialog() == true)
            {
                new SecurityFund().PublishPDF(getDate.FromDate, getDate.ToDate);
            }
        }
    }
}
