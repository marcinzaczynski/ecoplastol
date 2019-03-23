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
    /// Interaction logic for frmParametry2.xaml
    /// </summary>
    public partial class frmKonfiguracja : Window
    {
        private List<itf_kategorie> listITFkategorie;
        private List<itf_litery> listITFlitery;
        private List<itf_icc> listITFicc;
        private List<itf_cc> listITFcc;
        private List<itf_sr> listITFsr;
        private List<itf_trn> listITFtrn;
        private List<itf_odch> listITFodch;

        public frmKonfiguracja()
        {
            InitializeComponent();
        }

        private void DgParametry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol1.IsEnabled = false;
            dgParametry.IsEnabled = false;

            btnITFkategoria.IsEnabled = false;
            btnITFlitery.IsEnabled = false;
            btnITFicc.IsEnabled = false;
            btnITFcc.IsEnabled = false;
            btnITFsr.IsEnabled = false;
            btnITFtrn.IsEnabled = false;
            btnITFodch.IsEnabled = false;

            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            btnZamknij.IsEnabled = false;

            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
        }

        private void BtnKlonuj_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol1.IsEnabled = false;
            dgParametry.IsEnabled = false;

            btnITFkategoria.IsEnabled = false;
            btnITFlitery.IsEnabled = false;
            btnITFicc.IsEnabled = false;
            btnITFcc.IsEnabled = false;
            btnITFsr.IsEnabled = false;
            btnITFtrn.IsEnabled = false;
            btnITFodch.IsEnabled = false;

            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            btnZamknij.IsEnabled = false;

            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol1.IsEnabled = false;
            dgParametry.IsEnabled = false;

            btnITFkategoria.IsEnabled = false;
            btnITFlitery.IsEnabled = false;
            btnITFicc.IsEnabled = false;
            btnITFcc.IsEnabled = false;
            btnITFsr.IsEnabled = false;
            btnITFtrn.IsEnabled = false;
            btnITFodch.IsEnabled = false;

            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            btnZamknij.IsEnabled = false;

            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol1.IsEnabled = true;
            dgParametry.IsEnabled = true;

            btnITFkategoria.IsEnabled = true;
            btnITFlitery.IsEnabled = true;
            btnITFicc.IsEnabled = true;
            btnITFcc.IsEnabled = true;
            btnITFsr.IsEnabled = true;
            btnITFtrn.IsEnabled = true;
            btnITFodch.IsEnabled = true;

            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            btnZamknij.IsEnabled = true;

            txtIndeks.IsEnabled = false;
            txtParametr.IsEnabled = false;
            txtWartosc.IsEnabled = false;
            txtOpis.IsEnabled = false;
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol1.IsEnabled = true;
            dgParametry.IsEnabled = true;

            btnITFkategoria.IsEnabled = true;
            btnITFlitery.IsEnabled = true;
            btnITFicc.IsEnabled = true;
            btnITFcc.IsEnabled = true;
            btnITFsr.IsEnabled = true;
            btnITFtrn.IsEnabled = true;
            btnITFodch.IsEnabled = true;

            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            btnZamknij.IsEnabled = true;

            txtIndeks.IsEnabled = false;
            txtParametr.IsEnabled = false;
            txtWartosc.IsEnabled = false;
            txtOpis.IsEnabled = false;
        }

        private void UstawPrzyciski(int i)
        {
            // i == 0 - nie ma żadnego rekordu z tabeli
            // i == 1 - jest co najmniej jeden rekord z tabeli
            switch (i)
            {
                case 0:
                    btnDodaj.IsEnabled = true;
                    btnKlonuj.IsEnabled = false;
                    btnPopraw.IsEnabled = false;
                    btnUsun.IsEnabled = false;
                    btnAnuluj.IsEnabled = false;
                    btnZatwierdz.IsEnabled = false;
                    break;
                case 1:
                    btnDodaj.IsEnabled = true;
                    btnKlonuj.IsEnabled = true;
                    btnPopraw.IsEnabled = true;
                    btnUsun.IsEnabled = true;
                    btnAnuluj.IsEnabled = false;
                    btnZatwierdz.IsEnabled = false;
                    break;
            }
        }

        private void BtnITFkategoria_Click(object sender, RoutedEventArgs e)
        {
            listITFkategorie = frmKonfiguracja_db.PobierzITFKategorie();
            dgParametry.ItemsSource = listITFkategorie;

            if (listITFkategorie.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Kategorie";
        }

        private void BtnITFlitery_Click(object sender, RoutedEventArgs e)
        {
            listITFlitery = frmKonfiguracja_db.PobierzITFLitery();
            dgParametry.ItemsSource = listITFlitery;

            if (listITFlitery.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Litery";
        }

        private void BtnITFicc_Click(object sender, RoutedEventArgs e)
        {
            listITFicc = frmKonfiguracja_db.PobierzITFicc();
            dgParametry.ItemsSource = listITFicc;

            if (listITFicc.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Indykacja czasu chłodzenia";
        }

        private void BtnITFcc_Click(object sender, RoutedEventArgs e)
        {
            listITFcc = frmKonfiguracja_db.PobierzITFcc();
            dgParametry.ItemsSource = listITFcc;

            if (listITFcc.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Czas chłodzenia";
        }

        private void BtnITFsr_Click(object sender, RoutedEventArgs e)
        {
            listITFsr = frmKonfiguracja_db.PobierzITFsr();
            dgParametry.ItemsSource = listITFsr;

            if (listITFsr.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Średnice";
        }

        private void BtnITFtrn_Click(object sender, RoutedEventArgs e)
        {
            listITFtrn = frmKonfiguracja_db.PobierzITFtrn();
            dgParametry.ItemsSource = listITFtrn;

            if (listITFtrn.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Typ regulacji napięcia";
        }

        private void BtnITFodch_Click(object sender, RoutedEventArgs e)
        {
            listITFodch = frmKonfiguracja_db.PobierzITFodch();
            dgParametry.ItemsSource = listITFodch;

            if (listITFodch.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);

            lblITFinfo.Content = "Odchylenia";
        }
    }
}
