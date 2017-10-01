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
using System.Diagnostics;

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

        public void SearchWithID(int member_id)
        {
            Object.GetData(member_id);
        }
        public void SearchWithUnknown(string member_unknown)
        {
            try
            {
                if (member_unknown[0] == '0')
                    Object.GetDataUnknown(member_unknown);
                else
                {
                    int member_ID = Int32.Parse(member_unknown);
                    Object.GetData(member_ID);
                }
            }
            catch (Exception ex)
            {
                Object.GetDataUnknown(member_unknown);
            }
        }
    }
}
