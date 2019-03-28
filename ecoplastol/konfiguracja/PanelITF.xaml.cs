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
                    grdPozycje.DataContext = rowITFlitery;
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
            switch (co)
            {
                // 1 - ITF Kategorie
                case 1:
                    itf_kategorie pozITFkategorie = new itf_kategorie();
                    grdPozycje.DataContext = pozITFkategorie;
                    break;
                // 2 - ITF znaki / litery
                case 2:
                    itf_litery pozITFlitery = new itf_litery();
                    grdPozycje.DataContext = pozITFlitery;
                    break;
                // 3 - ITF Indykacja czasu chłodzenia
                case 3:
                    itf_icc pozITFicc = new itf_icc();
                    grdPozycje.DataContext = pozITFicc;
                    break;
                // 4 - ITF Czas chłodzenia
                case 4:
                    itf_cc pozITFcc = new itf_cc();
                    grdPozycje.DataContext = pozITFcc;
                    break;
                // 5 - ITF Średnice
                case 5:
                    itf_sr pozITFsr = new itf_sr();
                    grdPozycje.DataContext = pozITFsr;
                    break;
                // 6 - ITF Typ regulacji napięcia
                case 6:
                    itf_trn pozITFtrn = new itf_trn();
                    grdPozycje.DataContext = pozITFtrn;
                    break;
                // 7 - ITF Odchylenia
                case 7:
                    itf_odch pozITFodch = new itf_odch();
                    grdPozycje.DataContext = pozITFodch;
                    break;
                default:
                    break;
            }
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

            

            switch (co)
            {
                // 1 - ITF Kategorie
                case 1:
                    itf_kategorie pozITFkategorie = new itf_kategorie();
                    pozITFkategorie.parametr = rowITFkategoria.parametr;
                    pozITFkategorie.wartosc = rowITFkategoria.wartosc;
                    pozITFkategorie.opis = rowITFkategoria.opis;
                    grdPozycje.DataContext = pozITFkategorie;
                    break;
                // 2 - ITF znaki / litery
                case 2:
                    itf_litery pozITFlitery = new itf_litery();
                    pozITFlitery.parametr = rowITFlitery.parametr;
                    pozITFlitery.wartosc = rowITFlitery.wartosc;
                    pozITFlitery.opis = rowITFlitery.opis;
                    grdPozycje.DataContext = pozITFlitery;
                    break;
                // 3 - ITF Indykacja czasu chłodzenia
                case 3:
                    itf_icc pozITFicc = new itf_icc();
                    pozITFicc.parametr = rowITFicc.parametr;
                    pozITFicc.wartosc = rowITFicc.wartosc;
                    pozITFicc.opis = rowITFicc.opis;
                    grdPozycje.DataContext = pozITFicc;
                    break;
                // 4 - ITF Czas chłodzenia
                case 4:
                    itf_cc pozITFcc = new itf_cc();
                    pozITFcc.parametr = rowITFcc.parametr;
                    pozITFcc.wartosc = rowITFcc.wartosc;
                    pozITFcc.opis = rowITFcc.opis;
                    grdPozycje.DataContext = pozITFcc;
                    break;
                // 5 - ITF Średnice
                case 5:
                    itf_sr pozITFsr = new itf_sr();
                    pozITFsr.parametr = rowITFsr.parametr;
                    pozITFsr.wartosc = rowITFsr.wartosc;
                    pozITFsr.opis = rowITFsr.opis;
                    grdPozycje.DataContext = pozITFsr;
                    break;
                // 6 - ITF Typ regulacji napięcia
                case 6:
                    itf_trn pozITFtrn = new itf_trn();
                    pozITFtrn.parametr = rowITFtrn.parametr;
                    pozITFtrn.wartosc = rowITFtrn.wartosc;
                    pozITFtrn.opis = rowITFtrn.opis;
                    grdPozycje.DataContext = pozITFtrn;
                    break;
                // 7 - ITF Odchylenia
                case 7:
                    itf_odch pozITFodch = new itf_odch();
                    pozITFodch.parametr = rowITFodch.parametr;
                    pozITFodch.wartosc = rowITFodch.wartosc;
                    pozITFodch.opis = rowITFodch.opis;
                    grdPozycje.DataContext = pozITFodch;
                    break;
                default:
                    break;
            }
            
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

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            var Res = MessageBox.Show("Usunąć ?", "Usuwanie pozycji", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (Res == MessageBoxResult.Yes)
            {
                switch (co)
                {
                    // 1 - ITF Kategorie
                    case 1:
                        PanelITF_db.UsunITFkategoria(rowITFkategoria);
                        listITFkategoria = frmWyroby_db.PobierzITFKategorie();
                        grdLista.ItemsSource = listITFkategoria;
                        break;
                    // 2 - ITF znaki / litery
                    case 2:
                        PanelITF_db.UsunITFlitery(rowITFlitery);
                        listITFlitery = frmWyroby_db.PobierzITFZnaki();
                        grdLista.ItemsSource = listITFlitery;
                        break;
                    // 3 - ITF Indykacja czasu chłodzenia
                    case 3:
                        PanelITF_db.UsunITFicc(rowITFicc);
                        listITFicc = frmWyroby_db.PobierzITFicc();
                        grdLista.ItemsSource = listITFicc;
                        break;
                    // 4 - ITF Czas chłodzenia
                    case 4:
                        PanelITF_db.UsunITFcc(rowITFcc);
                        listITFcc = frmWyroby_db.PobierzITFcc();
                        grdLista.ItemsSource = listITFcc;
                        break;
                    // 5 - ITF Średnice
                    case 5:
                        PanelITF_db.UsunITFsr(rowITFsr);
                        listITFsr = frmWyroby_db.PobierzITFsr();
                        grdLista.ItemsSource = listITFsr;
                        break;
                    // 6 - ITF Typ regulacji napięcia
                    case 6:
                        PanelITF_db.UsunITFtrn(rowITFtrn);
                        listITFtrn = frmWyroby_db.PobierzITFtrn();
                        grdLista.ItemsSource = listITFtrn;
                        break;
                    // 7 - ITF Odchylenia
                    case 7:
                        PanelITF_db.UsunITFodch(rowITFodch);
                        listITFodch = frmWyroby_db.PobierzITFodch();
                        grdLista.ItemsSource = listITFodch;
                        break;
                    default:
                        break;
                }
                
            }
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
            grdLista.IsEnabled = true;
            grdPozycje.IsEnabled = false;
            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            switch (co)
            {
                // 1 - ITF Kategorie
                case 1:
                    ZatwierdzITFkategorie();
                    listITFkategoria = frmWyroby_db.PobierzITFKategorie();
                    grdLista.ItemsSource = listITFkategoria;
                    break;
                // 2 - ITF znaki / litery
                case 2:
                    ZatwierdzITFlitery();
                    listITFlitery = frmWyroby_db.PobierzITFZnaki();
                    grdLista.ItemsSource = listITFlitery;
                    break;
                // 3 - ITF Indykacja czasu chłodzenia
                case 3:
                    ZatwierdzITFicc();
                    listITFicc = frmWyroby_db.PobierzITFicc();
                    grdLista.ItemsSource = listITFicc;
                    break;
                // 4 - ITF Czas chłodzenia
                case 4:
                    ZatwierdzITFcc();
                    listITFcc = frmWyroby_db.PobierzITFcc();
                    grdLista.ItemsSource = listITFcc;
                    break;
                // 5 - ITF Średnice
                case 5:
                    ZatwierdzITFsr();
                    listITFsr = frmWyroby_db.PobierzITFsr();
                    grdLista.ItemsSource = listITFsr;
                    break;
                // 6 - ITF Typ regulacji napięcia
                case 6:
                    ZatwierdzITFtrn();
                    listITFtrn = frmWyroby_db.PobierzITFtrn();
                    grdLista.ItemsSource = listITFtrn;
                    break;
                // 7 - ITF Odchylenia
                case 7:
                    ZatwierdzITFodch();
                    listITFodch = frmWyroby_db.PobierzITFodch();
                    grdLista.ItemsSource = listITFodch;
                    break;
                default:
                    break;
            }

            grdLista.SelectedIndex = grdBookmark;
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
                        row.id = PanelITF_db.IdITFkategoria();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFkategoria(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_kategorie)
                    {
                        var row = new itf_kategorie();
                        row = grdPozycje.DataContext as itf_kategorie;
                        row.id = PanelITF_db.IdITFkategoria();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFkategoria(row);
                    }
                        break;
                case "P":
                    rowITFkategoria.opm = frmLogin.LoggedUser.login;
                    rowITFkategoria.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFkategoria(rowITFkategoria);
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
                    if (grdPozycje.DataContext is itf_litery)
                    {
                        var row = new itf_litery();
                        row = grdPozycje.DataContext as itf_litery;
                        row.id = PanelITF_db.IdITFlitery();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFlitery(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_litery)
                    {
                        var row = new itf_litery();
                        row = grdPozycje.DataContext as itf_litery;
                        row.id = PanelITF_db.IdITFlitery();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFlitery(row);
                    }
                    break;
                case "P":
                    rowITFlitery.opm = frmLogin.LoggedUser.login;
                    rowITFlitery.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFlitery(rowITFlitery);
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
                    if (grdPozycje.DataContext is itf_icc)
                    {
                        var row = new itf_icc();
                        row = grdPozycje.DataContext as itf_icc;
                        row.id = PanelITF_db.IdITFicc();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFicc(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_icc)
                    {
                        var row = new itf_icc();
                        row = grdPozycje.DataContext as itf_icc;
                        row.id = PanelITF_db.IdITFicc();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFicc(row);
                    }
                    break;
                case "P":
                    rowITFicc.opm = frmLogin.LoggedUser.login;
                    rowITFicc.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFicc(rowITFicc);
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
                    if (grdPozycje.DataContext is itf_cc)
                    {
                        var row = new itf_cc();
                        row = grdPozycje.DataContext as itf_cc;
                        row.id = PanelITF_db.IdITFcc();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFcc(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_cc)
                    {
                        var row = new itf_cc();
                        row = grdPozycje.DataContext as itf_cc;
                        row.id = PanelITF_db.IdITFcc();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFcc(row);
                    }
                    break;
                case "P":
                    rowITFcc.opm = frmLogin.LoggedUser.login;
                    rowITFcc.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFcc(rowITFcc);
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
                    if (grdPozycje.DataContext is itf_sr)
                    {
                        var row = new itf_sr();
                        row = grdPozycje.DataContext as itf_sr;
                        row.id = PanelITF_db.IdITFsr();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFsr(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_sr)
                    {
                        var row = new itf_sr();
                        row = grdPozycje.DataContext as itf_sr;
                        row.id = PanelITF_db.IdITFsr();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFsr(row);
                    }
                    break;
                case "P":
                    rowITFsr.opm = frmLogin.LoggedUser.login;
                    rowITFsr.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFsr(rowITFsr);
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
                    if (grdPozycje.DataContext is itf_trn)
                    {
                        var row = new itf_trn();
                        row = grdPozycje.DataContext as itf_trn;
                        row.id = PanelITF_db.IdITFtrn();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFtrn(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_trn)
                    {
                        var row = new itf_trn();
                        row = grdPozycje.DataContext as itf_trn;
                        row.id = PanelITF_db.IdITFtrn();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFtrn(row);
                    }
                    break;
                case "P":
                    rowITFtrn.opm = frmLogin.LoggedUser.login;
                    rowITFtrn.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFtrn(rowITFtrn);
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
                    if (grdPozycje.DataContext is itf_odch)
                    {
                        var row = new itf_odch();
                        row = grdPozycje.DataContext as itf_odch;
                        row.id = PanelITF_db.IdITFodch();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFodch(row);
                    }
                    break;
                case "K":
                    if (grdPozycje.DataContext is itf_odch)
                    {
                        var row = new itf_odch();
                        row = grdPozycje.DataContext as itf_odch;
                        row.id = PanelITF_db.IdITFodch();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelITF_db.DodajITFodch(row);
                    }
                    break;
                case "P":
                    rowITFodch.opm = frmLogin.LoggedUser.login;
                    rowITFodch.czasm = DateTime.Now;
                    PanelITF_db.PoprawITFodch(rowITFodch);
                    break;
                default:
                    break;
            }
        }

        
    }
}
