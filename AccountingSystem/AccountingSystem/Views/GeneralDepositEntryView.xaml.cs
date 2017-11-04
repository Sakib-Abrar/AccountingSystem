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
    /// Interaction logic for GeneralDepositEntryView.xaml
    /// </summary>
    public partial class GeneralDepositEntryView : Page
    {
        GeneralLedger data;
        public GeneralDepositEntryView()
        {
            InitializeComponent();
        }

        public void SetForEdit(string m_id)
        {
            int id = Convert.ToInt32(m_id);
            data = new GeneralLedger();
            data.GetDataDetails(id);
            DataContext = data;
        }

        private void SaveMember_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
