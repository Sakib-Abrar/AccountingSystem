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
    /// Interaction logic for MemberList.xaml
    /// </summary>
    public partial class MemberList : Page
    {
        MemberInfoView MemInfoObj;
        MemberView MemViewObj;

        public MemberList()
        {
            InitializeComponent();
            Members data = new Members();
            memberslist.ItemsSource = data.GetDataList();
            DataContext = data;
        }
        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
     


        private void searchMember(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            Members classObj = memberslist.SelectedItem as Members;
            int id = classObj.MemberID;
            MemViewObj = new MemberView();
            MemInfoObj = new MemberInfoView();
            MemInfoObj.SearchWithID(id);
            this.NavigationService.Navigate(MemInfoObj);

        }
    }
}
