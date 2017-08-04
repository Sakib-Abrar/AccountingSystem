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
    }
    }
