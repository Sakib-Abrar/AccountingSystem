﻿using System;
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
using AccountingSystem.Models;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    public partial class MemberView : Page
    {
        MemberInfoView MemInfoObj;
        MemberEntryView MemEntryObj;
        public MemberView()
        {
            InitializeComponent();
            MemInfoObj = new MemberInfoView();
            memberData.Navigate(MemInfoObj);
            DataContext = new Members();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            MemInfoObj.ShowData(searchid.Text);
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            MemEntryObj = new MemberEntryView();
            memberData.Navigate(MemEntryObj);
        }
    }
}