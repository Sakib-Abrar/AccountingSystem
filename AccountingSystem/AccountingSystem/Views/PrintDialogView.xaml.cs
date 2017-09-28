using System;
using System.Windows;


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
