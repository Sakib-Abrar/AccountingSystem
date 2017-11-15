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

        private void LoadGeneralDepositLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GeneralDepositLedgerView());
        }

        private void LoadGeneralDepositView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GeneralDepositView());
        }

        private void LoadMonthlyDepositLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MonthlyDepositLedgerView());
        }

        private void LoadMonthlyDepositView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MonthlyDepositView());
        }

        private void LoadFixedDepositLedgerView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FixedDepositLedgerView());
        }

        private void LoadFixedDepositView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FixedDepositView());
        }

        private void LoadBalanceSheetView(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BalanceSheetView());

        }

        private void Fund_Expanded(object sender, RoutedEventArgs args)
        {
            //Do something when the Expander control expands
            LoanInfo.IsExpanded = false;
            Savings.IsExpanded = false;
            Accounts.IsExpanded = false;
        }

        private void LoanInfo_Expanded(object sender, RoutedEventArgs e)
        {
            Savings.IsExpanded = false;
            Fund.IsExpanded = false;
            Accounts.IsExpanded = false;
            Expenses.IsExpanded = false;
        }

        private void Savings_Expanded(object sender, RoutedEventArgs e)
        {
            LoanInfo.IsExpanded = false;
            Fund.IsExpanded = false;
            Accounts.IsExpanded = false;
            Expenses.IsExpanded = false;
        }

        private void Accounts_Expanded(object sender, RoutedEventArgs e)
        {
            LoanInfo.IsExpanded = false;
            Fund.IsExpanded = false;
            Savings.IsExpanded = false;
            Expenses.IsExpanded = false;
        }
        private void Expenses_Expanded(object sender, RoutedEventArgs e)
        {
            LoanInfo.IsExpanded = false;
            Fund.IsExpanded = false;
            Savings.IsExpanded = false;
            Accounts.IsExpanded = false;
        }
    }
}