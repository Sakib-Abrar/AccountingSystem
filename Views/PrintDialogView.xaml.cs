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
using System.Windows.Shapes;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for PrintDialogView.xaml
    /// </summary>
    public partial class PrintDialogView : Window
    {
        public PrintDialogView()
        {
            InitializeComponent();
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public DateTime? FromDate
        {
            get { return FromDatePicker.SelectedDate; }
        }
        public DateTime? ToDate
        {
            get { return ToDatePicker.SelectedDate; }
        }



    }
}
