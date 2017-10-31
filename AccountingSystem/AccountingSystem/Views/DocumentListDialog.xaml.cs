using System.Windows;
using AccountingSystem.Models;
using System.IO;

namespace AccountingSystem.Views
{
    /// <summary>
    /// Interaction logic for DocumentListDialog.xaml
    /// </summary>
    public partial class DocumentListDialog : Window
    {
        Documents data;
        public DocumentListDialog(int MID)
        {
            InitializeComponent();
            data = new Documents();
            data.GetData(MID);
            if (data.MemberSignature != null)
                Signature.IsEnabled = true;
            if (data.CountExistence > 0)
            {
                Document1.IsEnabled = true;
                Label2.Content = data.DocumentsName[0];
            }
            if (data.CountExistence > 1)
            {
                Document2.IsEnabled = true;
                Label3.Content = data.DocumentsName[1];
            }
            if (data.CountExistence > 2)
            {
                Document3.IsEnabled = true;
                Label4.Content = data.DocumentsName[2];
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Signature_Click(object sender, RoutedEventArgs e)
        {
            DocumentDisplayDialog ShowDoc = new DocumentDisplayDialog(data.MemberSignature);
            ShowDoc.ShowDialog();
        }

        private void Document1_Click(object sender, RoutedEventArgs e)
        {
            string tempPath = Path.GetFullPath("Images/" + data.DocumentsAddress[0]);
            DocumentDisplayDialog ShowDoc = new DocumentDisplayDialog(tempPath);
            ShowDoc.ShowDialog();
        }

        private void Document2_Click(object sender, RoutedEventArgs e)
        {
            string tempPath= Path.GetFullPath("Images/" + data.DocumentsAddress[1]);
            DocumentDisplayDialog ShowDoc = new DocumentDisplayDialog(tempPath);
            ShowDoc.ShowDialog();
        }

        private void Document3_Click(object sender, RoutedEventArgs e)
        {
            string tempPath = Path.GetFullPath("Images/" + data.DocumentsAddress[2]);
            DocumentDisplayDialog ShowDoc = new DocumentDisplayDialog(tempPath);
            ShowDoc.ShowDialog();
        }
    }
}
