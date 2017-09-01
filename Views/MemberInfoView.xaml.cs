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
using AccountingSystem.Models;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for MemberInfoView.xaml
    /// </summary>
    public partial class MemberInfoView : Page
    {
        Members Object;
        public MemberInfoView()
        {
            InitializeComponent();
            Object = new Members();
            DataContext = Object;
        }

        public void ShowData(string member_ID) {
            Object.GetData(member_ID);
        }
    }
}
