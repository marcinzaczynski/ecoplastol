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
    /// Interaction logic for frmTools.xaml
    /// </summary>
    public partial class frmTools : Window
    {
        List<uzytkownicy> listaUzytkownikow;
        public frmTools()
        {
            InitializeComponent();
        }

        private void btnOperatorzy_Click(object sender, RoutedEventArgs e)
        {
            listaUzytkownikow = frmTools_db.PobierzUzytkownikow();
            foreach (var item in listaUzytkownikow)
            {

            }
        }
    }
}
