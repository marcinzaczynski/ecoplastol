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

namespace ecoplastol_maszyna
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static operatorzy_maszyn ZalogowanyOperator;

        public LoginWindow()
        {
            InitializeComponent();
            cbbLogin.ItemsSource = LoginWindow_db.PobierzOperatorow();
            cbbLogin.DisplayMemberPath = "login";
            cbbLogin.SelectedIndex = 0;
        }

        private void btnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            string user = cbbLogin.Text;
            string pass = psbHaslo.Password;

            ZalogowanyOperator = cbbLogin.SelectedItem as operatorzy_maszyn;

            if (ZalogowanyOperator.haslo == pass)
            {
                this.Hide();
                MainWindow MainWindow = new MainWindow(this);
                MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Niepopawna nazwa użytkownika lub hasło!");
            }
        }
    }
}
