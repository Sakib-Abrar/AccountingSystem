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
using AccountingSystem.Controller;
using AccountingSystem.Models;
using System.Data.SqlClient;


namespace AccountingSystem
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {

        private string cell { get; set; }
        private string pass { get; set; }
        public string m_error_msg { get; private set; }
        public string Error_msg { get; private set; }
        

        private string stuff_cell;
        private string stuff_pass;
        private string stuff_name;


        public LoginPage()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            DataContext = new Login();
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        protected void Save_Click(object sender, RoutedEventArgs e)
        {
            Connection conn = new Connection();
            conn.OpenConection();
            int isLogin = 0;
            string query = "SELECT * From Stuff ";//WHERE Stuff_Cell = 12345";
            SqlDataReader reader = conn.DataReader(query);
            while (reader.Read())
            {
                stuff_name = (string)reader["Stuff_Name"];
                stuff_cell = (String)reader["Stuff_Cell"];
                stuff_pass = (String)reader["Stuff_Password"];
                if (stuff_cell.Equals(Cell.Text) && stuff_pass.Equals(Passwordbox.Password))
                {
                    isLogin = 1;
                    break;
                }
            }


            if(isLogin!=1)
            {   //Console.Write("logged_out" + stuff_cell + " go" + Cell.Text + " " + stuff_pass + " " + Password.Text+" "+SelectedDate);
                
                MessageBox.Show("Sorry! Wrong Password!!!");
                
            }
            conn.CloseConnection();
            if (isLogin == 1)
            {
                //Console.Write("logged_in" + stuff_cell + " " + Cell.Text + " " + stuff_pass + " " + Password.Text+" "+SelectedDate);
                new Login().SetStuffName(stuff_name);
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
                LoanLedger Due = new LoanLedger();
                Due.DueChecker();
            }

        }
    }
}
