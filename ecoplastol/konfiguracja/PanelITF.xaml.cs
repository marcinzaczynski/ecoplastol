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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ecoplastol.konfiguracja
{
    /// <summary>
    /// Interaction logic for PanelITF.xaml
    /// </summary>
    public partial class PanelITF : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private itf_kategorie rowITFkategoria;
        private itf_litery rowITFlitery;
        private itf_icc rowITFicc;
        private itf_cc rowITFcc;
        private itf_sr rowITFsr;
        private itf_trn rowITFtrn;
        private itf_odch rowITFodch;

        private List<itf_kategorie> listITFkategoria;
        private List<itf_litery> listITFlitery;
        private List<itf_icc> listITFicc;
        private List<itf_cc> listITFcc;
        private List<itf_sr> listITFsr;
        private List<itf_trn> listITFtrn;
        private List<itf_odch> listITFodch;

        private int co;
        // co - numer ustawiany w evencie przycisku na frmKonfiguracja2
        // 1 - ITF Kategorie
        // 2 - ITF znaki / litery
        // 3 - ITF Indykacja czasu chłodzenia
        // 4 - ITF Czas chłodzenia
        // 5 - ITF Średnice
        // 6 - ITF Typ regulacji napięcia
        // 7 - ITF Odchylenia

        public PanelITF(int i, List<itf_kategorie> lista)
        {
            InitializeComponent();
            co = i;
            listITFkategoria = lista;
            grdLista.ItemsSource = listITFkategoria;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_litery> lista)
        {
            InitializeComponent();
            co = i;
            listITFlitery = lista;
            grdLista.ItemsSource = listITFlitery;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_icc> lista)
        {
            InitializeComponent();
            co = i;
            listITFicc = lista;
            grdLista.ItemsSource = listITFicc;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_cc> lista)
        {
            InitializeComponent();
            co = i;
            listITFcc = lista;
            grdLista.ItemsSource = listITFcc;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_sr> lista)
        {
            InitializeComponent();
            co = i;
            listITFsr = lista;
            grdLista.ItemsSource = listITFsr;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_trn> lista)
        {
            InitializeComponent();
            co = i;
            listITFtrn = lista;
            grdLista.ItemsSource = listITFtrn;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }
        public PanelITF(int i, List<itf_odch> lista)
        {
            InitializeComponent();
            co = i;
            listITFodch = lista;
            grdLista.ItemsSource = listITFodch;
            if (lista.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                grdLista.Focus();
                grdLista.SelectedIndex = 0;

                GrdLista_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (co)
            {
                // 1 - ITF Kategorie
                case 1 :
                    rowITFkategoria = grdLista.SelectedItem as itf_kategorie;
                    grdPozycje.DataContext = rowITFkategoria;
                    break;
                // 2 - ITF znaki / litery
                case 2:
                    rowITFlitery = grdLista.SelectedItem as itf_litery;
                    grdPozycje.DataContext = rowITFkategoria;
                    break;
                // 3 - ITF Indykacja czasu chłodzenia
                case 3:
                    rowITFicc = grdLista.SelectedItem as itf_icc;
                    grdPozycje.DataContext = rowITFicc;
                    break;
                // 4 - ITF Czas chłodzenia
                case 4:
                    rowITFcc = grdLista.SelectedItem as itf_cc;
                    grdPozycje.DataContext = rowITFcc;
                    break;
                // 5 - ITF Średnice
                case 5:
                    rowITFsr = grdLista.SelectedItem as itf_sr;
                    grdPozycje.DataContext = rowITFsr;
                    break;
                // 6 - ITF Typ regulacji napięcia
                case 6:
                    rowITFtrn = grdLista.SelectedItem as itf_trn;
                    grdPozycje.DataContext = rowITFtrn;
                    break;
                // 7 - ITF Odchylenia
                case 7:
                    rowITFodch = grdLista.SelectedItem as itf_odch;
                    grdPozycje.DataContext = rowITFodch;
                    break;
                default:
                    break;
            }
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

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";
            grdBookmark = grdLista.SelectedIndex;
            grdLista.IsEnabled = false;
            grdPozycje.IsEnabled = true;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            itf_kategorie aaa = new itf_kategorie();
            grdPozycje.DataContext = aaa;
            //listITFkategoria.Add(aaa);
        }

        private void BtnKlonuj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "K";
            grdLista.IsEnabled = false;
            grdPozycje.IsEnabled = true;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            akcja = "P";
            grdBookmark = grdLista.SelectedIndex;
            grdLista.IsEnabled = false;
            grdPozycje.IsEnabled = true;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            grdLista.IsEnabled = true;
            grdPozycje.IsEnabled = false;
            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            switch (co)
            {
                // 1 - ITF Kategorie
                case 1:
                    ZatwierdzITFkategorie();
                    listITFkategoria = frmWyroby_db.PobierzITFKategorie();
                    grdLista.ItemsSource = listITFkategoria;
                    grdLista.SelectedIndex = grdBookmark;
                    break;
                // 2 - ITF znaki / litery
                case 2:
                    ZatwierdzITFlitery();
                    break;
                // 3 - ITF Indykacja czasu chłodzenia
                case 3:
                    ZatwierdzITFicc();
                    break;
                // 4 - ITF Czas chłodzenia
                case 4:
                    ZatwierdzITFcc();
                    break;
                // 5 - ITF Średnice
                case 5:
                    ZatwierdzITFsr();
                    break;
                // 6 - ITF Typ regulacji napięcia
                case 6:
                    ZatwierdzITFtrn();
                    break;
                // 7 - ITF Odchylenia
                case 7:
                    ZatwierdzITFodch();
                    break;
                default:
                    break;
            }
            grdLista.IsEnabled = true;
            grdPozycje.IsEnabled = false;
            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;
        }

        // 1 - ITF Kategorie
        
        private void ZatwierdzITFkategorie()
        {
            switch (akcja)
            {
                case "D":
                    if (grdPozycje.DataContext is itf_kategorie)
                    {
                        var row = new itf_kategorie();
                        row = grdPozycje.DataContext as itf_kategorie;
                        row.id = PanelITF_db.IdITFKategoria();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFKategoria(row);
                        //MessageBox.Show("cieplej");
                    }
                    break;
                case "K":
                    break;
                case "P":
                    rowITFkategoria.opm = frmLogin.LoggedUser.login;
                    rowITFkategoria.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFKategoria(rowITFkategoria);
                    break;
                default:
                    break;
            }
        }
        // 2 - ITF znaki / litery
        
        private void ZatwierdzITFlitery()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
        // 3 - ITF Indykacja czasu chłodzenia
        
        private void ZatwierdzITFicc()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
        // 4 - ITF Czas chłodzenia
        
        private void ZatwierdzITFcc()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
        // 5 - ITF Średnice
        
        private void ZatwierdzITFsr()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
        // 6 - ITF Typ regulacji napięcia
       
        private void ZatwierdzITFtrn()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
        // 7 - ITF Odchylenia
        private void ZatwierdzITFodch()
        {
            switch (akcja)
            {
                case "D":
                    break;
                case "K":
                    break;
                case "P":
                    break;
                default:
                    break;
            }
        }
    }
}
