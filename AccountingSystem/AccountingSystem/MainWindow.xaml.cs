using System.Windows;
using AccountingSystem.Views;

namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            MainFrame.Navigate(new login());
        }

        private void LoadSecurityFundView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SecurityFundView());
        }

        private void LoadReservedFundView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservedFundView());
        }
        private void LoadSalaryView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SalaryView());
        }
        private void LoadOfficeRentView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OfficeRentView());
        }
        private void LoadCooperativeDevelopmentView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CooperativeDevelopmentView());
        }
        private void LoadShareableProfitView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ShareableProfitView());
        }
        private void LoadShareView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ShareView());
        }
        private void LoadAdmissionFeeView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdmissionFeeView());
        }
        private void LoadCashInformationView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CashInformationView());
        }
        private void LoadBankAccountInformationView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BankAccountInformationView());
        }
    }
    }
