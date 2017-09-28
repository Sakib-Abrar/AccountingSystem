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
    /// Interaction logic for RemoveDialogView.xaml
    /// </summary>
    public partial class RemoveDialogView : Window
    {
        public RemoveDialogView()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        public string FirstInput
        {
            get { return textBox1.Text; }
        }
        public string SecondInput
        {
            get { return textBox2.Text; }
        }

        public string GetPassword
        {
            get { return passwordBox.Password; }
        }
    }
}
