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

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for flmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public static users LoggedUser;

        public frmLogin()
        {
            InitializeComponent();
            cbbLogin.ItemsSource = frmUzytkownicy_db.PobierzUzytkownikow();
            cbbLogin.DisplayMemberPath = "login";
            cbbLogin.SelectedIndex = 0;
        }

        private void btnZaloguj_Click(object sender, RoutedEventArgs e)
        {

            string user = cbbLogin.Text;
            string pass = psbHaslo.Password;

            LoggedUser = frmUzytkownicy_db.PobierzUzytkownika(user);
            if (LoggedUser.pass == pass)
            {
                this.Hide();
                frmMain frmMain = new frmMain(this);
                frmMain.Show();
            }
            else
            {
                MessageBox.Show("Niepopawna nazwa użytkownika lub hasło!");
            }
        }

    }
}
