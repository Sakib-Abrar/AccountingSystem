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
            MainFrame.Navigate(new MemberView());
        }

        private void StuffView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StuffView());
        }

        private void LoadLoanDetailsView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoanDetailsView());
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
        private void MemberInformationView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MemberView());
        }
        private void LoadEntryLogView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EntryLogView());
        }
        private void LoadDailyLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DailyLedgerView());
        }
        private void LoadWeeklyLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new WeeklyLedgerView());
        }
        private void LoadMonthlyLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MonthlyLedgerView());
        }
        private void LoadDailyDueLView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DailyDueView());
        }

        private void LoadWeeklyDueLView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new WeeklyDueView());
        }

        private void LoadMonthlyDueLView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MonthlyDueView());
        }
<<<<<<< Updated upstream

        private void LoadGeneralDepositLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GeneralDepositLedgerView());
        }

        private void LoadGeneralDepositView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GeneralDepositView());
=======
        private void LoadBalanceSheetView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BalanceSheetView());
>>>>>>> Stashed changes
        }
    }
    }