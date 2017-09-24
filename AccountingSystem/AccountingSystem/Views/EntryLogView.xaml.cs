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
    /// Interaction logic for EntryLogView.xaml
    /// </summary>
    public partial class EntryLogView : Page
    {
        public EntryLogView()
        {
            InitializeComponent();
            EntryModel data = new EntryModel();
            entryLog.ItemsSource = data.GetData();
            DataContext = data;
        }
    }
}
