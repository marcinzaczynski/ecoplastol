using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ecoplastol.konfiguracja.traceability;

namespace ecoplastol.konfiguracja.produkcja
{
    /// <summary>
    /// Interaction logic for PanelProdWyroby.xaml
    /// </summary>
    /// 

    public partial class PanelProdWyroby : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private wyroby rowWyrob;

        private int typFiltrTypKsztaltki; // 0 - elektrooporowe, 1 - doczołowe, 2 - zawory, 3 - adaptory, -1 - wszystkie                       

        private bool widoczneWyroby; // True - aktywne , False - wszystkie

        private kodITF kodITF;
        private kodTrace kodTrace;

        private List<wyroby> listWyroby;
        private List<wyroby_typ> listTypyWyrobow;
        private List<wyroby_zakres_sdr> listWyrobZakresSDR;
        private List<wyroby_zast_zaworu> listWyrobZastZaworu;
        private List<wyroby_druty> listWyrobRodzajDrutu;

        private List<itf_kategorie> listITFKategorie;
        private List<itf_litery> listITFZnaki1;
        private List<itf_litery> listITFZnaki2;
        private List<itf_icc> listITFicc;
        private List<itf_cc> listITFcc1;
        private List<itf_cc> listITFcc2;
        private List<itf_sr> listITFsmin;
        private List<itf_sr> listITFsmax;
        private List<itf_trn> listITFtrn;
        private List<itf_odch> listITFodch;

        private List<trace_litery> listTraceZnaki1;
        private List<trace_litery> listTraceZnaki2;
        private List<trace_kategoria> listTraceKategorie;
        private List<trace_sr> listTraceSmin;
        private List<trace_sr> listTraceSmax;
        private List<trace_producent> listTraceProducent;
        private List<trace_sdr> listTraceSDR;
        private List<trace_pe_m> listTracePEm;
        private List<trace_material> listTraceMaterial;
        private List<trace_pe_o> listTracePEo;
        private List<trace_mfr> listTraceMFR;

        public PanelProdWyroby(bool czyAktywne)
        {
            InitializeComponent();
            widoczneWyroby = czyAktywne;
            typFiltrTypKsztaltki = -1;
            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
            grdPozycje.IsEnabled = false;

            UstawWyrob();
            UstawITF();
            UstawTrace();

            kodITF = new kodITF();
            kodTrace = new kodTrace();

            if (listWyroby.Count == 0)
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

        private void UstawWyrob()
        {
            listTypyWyrobow = konf_produkcja_db.PobierzTypy();
            cbbWyrobTyp.ItemsSource = listTypyWyrobow;

            listWyrobZakresSDR = konf_produkcja_db.PobierzZakresSDR();
            cbbWyrobZakresSDR.ItemsSource = listWyrobZakresSDR;

            listWyrobZastZaworu = konf_produkcja_db.PobierzZastZaworu();
            cbbWyrobZastZaworu.ItemsSource = listWyrobZastZaworu;

            listWyrobRodzajDrutu = konf_produkcja_db.PobierzDruty();
            cbbWyrobRodzajDrutu.ItemsSource = listWyrobRodzajDrutu;
        }

        private void UstawITF()
        {
            // wczytuje parametry do cbb dla kodu ITF
            listITFKategorie = PanelITF_db.PobierzITFKategorie();
            cbbITFKategoria.ItemsSource = listITFKategorie;

            listITFZnaki1 = PanelITF_db.PobierzITFZnaki();
            cbbITFZnak1.ItemsSource = listITFZnaki1;

            listITFZnaki2 = PanelITF_db.PobierzITFZnaki();
            cbbITFZnak2.ItemsSource = listITFZnaki2;

            listITFicc = PanelITF_db.PobierzITFicc();
            cbbITFICC.ItemsSource = listITFicc;

            listITFcc1 = PanelITF_db.PobierzITFcc();
            cbbITFCC1.ItemsSource = listITFcc1;

            listITFcc2 = PanelITF_db.PobierzITFcc();
            cbbITFCC2.ItemsSource = listITFcc2;

            listITFsmin = PanelITF_db.PobierzITFsr();
            cbbITFsmin.ItemsSource = listITFsmin;

            listITFsmax = PanelITF_db.PobierzITFsr();
            cbbITFsmax.ItemsSource = listITFsmax;

            listITFtrn = PanelITF_db.PobierzITFtrn();
            cbbITFtrn.ItemsSource = listITFtrn;

            listITFodch = PanelITF_db.PobierzITFodch();
            cbbITFodch.ItemsSource = listITFodch;
        }

        private void UstawTrace()
        {
            // wczytuje parametry do cbb dla kodu Traceability
            listTraceZnaki1 = PanelTrace_db.PobierzTraceZnak();
            cbbTraceZnak1.ItemsSource = listTraceZnaki1;

            listTraceZnaki2 = PanelTrace_db.PobierzTraceZnak();
            cbbTraceZnak2.ItemsSource = listTraceZnaki2;

            listTraceKategorie = PanelTrace_db.PobierzTraceKategorie();
            cbbTraceKategoria.ItemsSource = listTraceKategorie;

            listTraceSmin = PanelTrace_db.PobierzTraceSr();
            cbbTraceSmin.ItemsSource = listTraceSmin;

            listTraceSmax = PanelTrace_db.PobierzTraceSr();
            cbbTraceSmax.ItemsSource = listTraceSmax;

            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            cbbTraceProducent.ItemsSource = listTraceProducent;

            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            cbbTraceSDR.ItemsSource = listTraceSDR;

            listTracePEm = PanelTrace_db.PobierzTracePem();
            cbbTracePEm.ItemsSource = listTracePEm;

            listTraceMaterial = PanelTrace_db.PobierzTraceMaterial();
            cbbTraceMaterial.ItemsSource = listTraceMaterial;

            listTracePEo = PanelTrace_db.PobierzTracePeo();
            cbbTracePEo.ItemsSource = listTracePEo;

            listTraceMFR = PanelTrace_db.PobierzTraceMfr();
            cbbTraceMFR.ItemsSource = listTraceMFR;
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowWyrob = grdLista.SelectedItem as wyroby;
            grdPozycje.DataContext = rowWyrob;

            WyczyscKontrolkiKodKr();
            PrzygotujPodKodKrITF();
            UzupelnijKodITF();
            PrzygotujPodKodKrTrace();
            UzupelnijKodTrace();
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";

            grdLista.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            grdPozycje.IsEnabled = true;
            WyczyscKontrolkiWyrob();
            WyczyscKontrolkiKodKr();
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

            wyroby poz = new wyroby();
            poz = rowWyrob;
            grdPozycje.DataContext = poz;
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
                konf_produkcja_db.UsunWyrob(rowWyrob);
                listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
                grdLista.ItemsSource = listWyroby;
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

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;

            grdLista.SelectedIndex = grdBookmark;
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

            switch (akcja)
            {
                case "D":
                case "K":
                    if (grdPozycje.DataContext is wyroby)
                    {
                        var row = new wyroby();
                        row = grdPozycje.DataContext as wyroby;
                        row.id = konf_produkcja_db.IdWyroby();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        konf_produkcja_db.DodajWyrob(row);
                    }
                    break;
                case "P":
                    rowWyrob.opm = frmLogin.LoggedUser.login;
                    rowWyrob.czasm = DateTime.Now;
                    konf_produkcja_db.PoprawWyrob(rowWyrob);
                    break;
                default:
                    break;
            }
            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            grdLista.SelectedIndex = grdBookmark;
            grdLista.Focus();
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

        private void TxtWyrobIwOpZ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtWyrobWagaOp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtWyrobWaga1szt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtWyrobNorma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtWyrobIlwPartii_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtITFprn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtITFrez_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            string aa = txtITFrez.Text;
            int count = aa.Count(f => f == '.');
            //if (count == 0 )
            if (str == "." && count > 0)
            {
                e.Handled = true;
            }
            else
            {
                Regex _regex = new Regex("[^0-9.]+");
                bool result = _regex.IsMatch(str);
                e.Handled = result;
            }
        }

        private void TxtITFcz1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtITFcz2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtITFke_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtTracePartia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void CbbITFKategoria_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp1.Background = Brushes.Yellow;
            txtITFp2.Background = Brushes.Yellow;
            txtITF2p1.Background = Brushes.Yellow;
            txtITF2p2.Background = Brushes.Yellow;
        }

        private void CbbITFKategoria_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp1.Background = Brushes.White;
            txtITFp2.Background = Brushes.White;
            txtITF2p1.Background = Brushes.White;
            txtITF2p2.Background = Brushes.White;
        }

        private void CbbITFZnak1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp3.Background = Brushes.Yellow;
            txtITFp4.Background = Brushes.Yellow;
            txtITF2p3.Background = Brushes.Yellow;
            txtITF2p4.Background = Brushes.Yellow;
        }

        private void CbbITFZnak1_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp3.Background = Brushes.White;
            txtITFp4.Background = Brushes.White;
            txtITF2p3.Background = Brushes.White;
            txtITF2p4.Background = Brushes.White;
        }

        private void CbbITFZnak2_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp5.Background = Brushes.Yellow;
            txtITFp6.Background = Brushes.Yellow;
            txtITF2p5.Background = Brushes.Yellow;
            txtITF2p6.Background = Brushes.Yellow;
        }

        private void CbbITFZnak2_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp5.Background = Brushes.White;
            txtITFp6.Background = Brushes.White;
            txtITF2p5.Background = Brushes.White;
            txtITF2p6.Background = Brushes.White;
        }

        private void CbbITFICC_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp7.Background = Brushes.Yellow;
            txtITF2p7.Background = Brushes.Yellow;
        }

        private void CbbITFICC_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp7.Background = Brushes.White;
            txtITF2p7.Background = Brushes.White;
        }

        private void CbbITFCC1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp8.Background = Brushes.Yellow;
        }

        private void CbbITFCC1_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp8.Background = Brushes.White;
        }

        private void CbbITFCC2_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITF2p8.Background = Brushes.Yellow;
        }

        private void CbbITFCC2_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITF2p8.Background = Brushes.White;
        }

        private void CbbITFsmin_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp9.Background = Brushes.Yellow;
            txtITFp10.Background = Brushes.Yellow;
            txtITFp11.Background = Brushes.Yellow;
            txtITF2p9.Background = Brushes.Yellow;
            txtITF2p10.Background = Brushes.Yellow;
            txtITF2p11.Background = Brushes.Yellow;
        }

        private void CbbITFsmin_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp9.Background = Brushes.White;
            txtITFp10.Background = Brushes.White;
            txtITFp11.Background = Brushes.White;
            txtITF2p9.Background = Brushes.White;
            txtITF2p10.Background = Brushes.White;
            txtITF2p11.Background = Brushes.White;
        }

        private void CbbITFsmax_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp9.Background = Brushes.Yellow;
            txtITFp10.Background = Brushes.Yellow;
            txtITFp11.Background = Brushes.Yellow;
            txtITF2p9.Background = Brushes.Yellow;
            txtITF2p10.Background = Brushes.Yellow;
            txtITF2p11.Background = Brushes.Yellow;
        }

        private void CbbITFsmax_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp9.Background = Brushes.White;
            txtITFp10.Background = Brushes.White;
            txtITFp11.Background = Brushes.White;
            txtITF2p9.Background = Brushes.White;
            txtITF2p10.Background = Brushes.White;
            txtITF2p11.Background = Brushes.White;
        }

        private void CbbITFtrn_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp12.Background = Brushes.Yellow;
            txtITF2p12.Background = Brushes.Yellow;
        }

        private void CbbITFtrn_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp12.Background = Brushes.White;
            txtITF2p12.Background = Brushes.White;
        }

        private void TxtITFprn_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp13.Background = Brushes.Yellow;
            txtITFp14.Background = Brushes.Yellow;
            txtITF2p13.Background = Brushes.Yellow;
            txtITF2p14.Background = Brushes.Yellow;
        }

        private void TxtITFprn_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp13.Background = Brushes.White;
            txtITFp14.Background = Brushes.White;
            txtITF2p13.Background = Brushes.White;
            txtITF2p14.Background = Brushes.White;
        }

        private void TxtITFrez_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp15.Background = Brushes.Yellow;
            txtITFp16.Background = Brushes.Yellow;
            txtITFp17.Background = Brushes.Yellow;
            txtITF2p15.Background = Brushes.Yellow;
            txtITF2p16.Background = Brushes.Yellow;
            txtITF2p17.Background = Brushes.Yellow;
        }

        private void TxtITFrez_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp15.Background = Brushes.White;
            txtITFp16.Background = Brushes.White;
            txtITFp17.Background = Brushes.White;
            txtITF2p15.Background = Brushes.White;
            txtITF2p16.Background = Brushes.White;
            txtITF2p17.Background = Brushes.White;
        }

        private void CbbITFodch_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp18.Background = Brushes.Yellow;
            txtITF2p18.Background = Brushes.Yellow;
        }

        private void CbbITFodch_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp18.Background = Brushes.White;
            txtITF2p18.Background = Brushes.White;
        }

        private void TxtITFcz1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp19.Background = Brushes.Yellow;
            txtITFp20.Background = Brushes.Yellow;
            txtITFp21.Background = Brushes.Yellow;
        }

        private void TxtITFcz1_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp19.Background = Brushes.White;
            txtITFp20.Background = Brushes.White;
            txtITFp21.Background = Brushes.White;

        }

        private void TxtITFcz2_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITF2p19.Background = Brushes.Yellow;
            txtITF2p20.Background = Brushes.Yellow;
            txtITF2p21.Background = Brushes.Yellow;
        }

        private void TxtITFcz2_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITF2p19.Background = Brushes.White;
            txtITF2p20.Background = Brushes.White;
            txtITF2p21.Background = Brushes.White;
        }

        private void TxtITFke_GotFocus(object sender, RoutedEventArgs e)
        {
            txtITFp22.Background = Brushes.Yellow;
            txtITFp23.Background = Brushes.Yellow;
            txtITF2p22.Background = Brushes.Yellow;
            txtITF2p23.Background = Brushes.Yellow;
        }

        private void TxtITFke_LostFocus(object sender, RoutedEventArgs e)
        {
            txtITFp22.Background = Brushes.White;
            txtITFp23.Background = Brushes.White;
            txtITF2p22.Background = Brushes.White;
            txtITF2p23.Background = Brushes.White;
        }

        private void CbbTraceZnak1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.Yellow;
            txtTracep2.Background = Brushes.Yellow;
        }

        private void CbbTraceZnak1_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.White;
            txtTracep2.Background = Brushes.White;
        }

        private void CbbTraceZnak2_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep3.Background = Brushes.Yellow;
            txtTracep4.Background = Brushes.Yellow;
        }

        private void CbbTraceZnak2_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep3.Background = Brushes.White;
            txtTracep4.Background = Brushes.White;
        }

        private void CbbTraceKategoria_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep5.Background = Brushes.Yellow;
            txtTracep6.Background = Brushes.Yellow;
        }

        private void CbbTraceKategoria_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep5.Background = Brushes.White;
            txtTracep6.Background = Brushes.White;
        }

        private void CbbTraceSmin_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.Yellow;
            txtTracep7.Background = Brushes.Yellow;
            txtTracep8.Background = Brushes.Yellow;
            txtTracep9.Background = Brushes.Yellow;
        }

        private void CbbTraceSmin_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.White;
            txtTracep7.Background = Brushes.White;
            txtTracep8.Background = Brushes.White;
            txtTracep9.Background = Brushes.White;
        }

        private void CbbTraceSmax_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.Yellow;
            txtTracep7.Background = Brushes.Yellow;
            txtTracep8.Background = Brushes.Yellow;
            txtTracep9.Background = Brushes.Yellow;
        }

        private void CbbTraceSmax_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep1.Background = Brushes.White;
            txtTracep7.Background = Brushes.White;
            txtTracep8.Background = Brushes.White;
            txtTracep9.Background = Brushes.White;
        }

        private void TxtTracePartia_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep10.Background = Brushes.Yellow;
            txtTracep11.Background = Brushes.Yellow;
            txtTracep12.Background = Brushes.Yellow;
            txtTracep13.Background = Brushes.Yellow;
            txtTracep14.Background = Brushes.Yellow;
            txtTracep15.Background = Brushes.Yellow;
        }

        private void TxtTracePartia_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep10.Background = Brushes.White;
            txtTracep11.Background = Brushes.White;
            txtTracep12.Background = Brushes.White;
            txtTracep13.Background = Brushes.White;
            txtTracep14.Background = Brushes.White;
            txtTracep15.Background = Brushes.White;
        }

        private void CbbTraceProducent_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep16.Background = Brushes.Yellow;
            txtTracep17.Background = Brushes.Yellow;
        }

        private void CbbTraceProducent_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep16.Background = Brushes.White;
            txtTracep17.Background = Brushes.White;
        }

        private void CbbTraceSDR_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep18.Background = Brushes.Yellow;
        }

        private void CbbTraceSDR_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep18.Background = Brushes.White;
        }

        private void CbbTracePEm_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep19.Background = Brushes.Yellow;
            txtTracep20.Background = Brushes.Yellow;
            txtTracep21.Background = Brushes.Yellow;
            txtTracep22.Background = Brushes.Yellow;
        }

        private void CbbTracePEm_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep19.Background = Brushes.White;
            txtTracep20.Background = Brushes.White;
            txtTracep21.Background = Brushes.White;
            txtTracep22.Background = Brushes.White;
        }

        private void CbbTraceMaterial_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep23.Background = Brushes.Yellow;
        }

        private void CbbTraceMaterial_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep23.Background = Brushes.White;
        }

        private void CbbTracePEo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep24.Background = Brushes.Yellow;
        }

        private void CbbTracePEo_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep24.Background = Brushes.White;
        }

        private void CbbTraceMFR_GotFocus(object sender, RoutedEventArgs e)
        {
            txtTracep25.Background = Brushes.Yellow;
        }

        private void CbbTraceMFR_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTracep25.Background = Brushes.White;
        }

        private void CbbITFKategoria_DropDownClosed(object sender, EventArgs e)
        {
            {
                var item = cbbITFKategoria.SelectedItem as itf_kategorie;

                //item = null - w trakcie tworzenia formy
                // a trakcie pracy parametr i wartość są puste dla id = -1
                if (item == null || item.wartosc == "")
                {
                    kodITF.kategoria = "00";
                }
                else
                {
                    kodITF.kategoria = item.wartosc;
                }
            }
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFZnak1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFZnak1.SelectedItem as itf_litery;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.znak1 = "00"; ;
            }
            else
            {
                kodITF.znak1 = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFZnak2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFZnak2.SelectedItem as itf_litery;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.znak2 = "00"; ;
            }
            else
            {
                kodITF.znak2 = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void UzupelnijKodITF()
        {
            string kodITF1;
            string kodITF2;
            try
            {
                kodITF1 = kodITF.kod1;
                kodITF2 = kodITF.kod2;
            }
            catch
            {
                MessageBox.Show("Nie wypełnione wszystkie pola.");
                kodITF1 = "0000000000000000000000000";
                kodITF2 = "0000000000000000000000000";
            }

            txtITFp1.Text = kodITF1[0].ToString();
            txtITFp2.Text = kodITF1[1].ToString();
            txtITFp3.Text = kodITF1[2].ToString();
            txtITFp4.Text = kodITF1[3].ToString();
            txtITFp5.Text = kodITF1[4].ToString();
            txtITFp6.Text = kodITF1[5].ToString();
            txtITFp7.Text = kodITF1[6].ToString();
            txtITFp8.Text = kodITF1[7].ToString();
            txtITFp9.Text = kodITF1[8].ToString();
            txtITFp10.Text = kodITF1[9].ToString();
            txtITFp11.Text = kodITF1[10].ToString();
            txtITFp12.Text = kodITF1[11].ToString();
            txtITFp13.Text = kodITF1[12].ToString();
            txtITFp14.Text = kodITF1[13].ToString();
            txtITFp15.Text = kodITF1[14].ToString();
            txtITFp16.Text = kodITF1[15].ToString();
            txtITFp17.Text = kodITF1[16].ToString();
            txtITFp18.Text = kodITF1[17].ToString();
            txtITFp19.Text = kodITF1[18].ToString();
            txtITFp20.Text = kodITF1[19].ToString();
            txtITFp21.Text = kodITF1[20].ToString();
            txtITFp22.Text = kodITF1[21].ToString();
            txtITFp23.Text = kodITF1[22].ToString();
            txtITFp24.Text = kodITF1[23].ToString();

            txtITF2p1.Text = kodITF2[0].ToString();
            txtITF2p2.Text = kodITF2[1].ToString();
            txtITF2p3.Text = kodITF2[2].ToString();
            txtITF2p4.Text = kodITF2[3].ToString();
            txtITF2p5.Text = kodITF2[4].ToString();
            txtITF2p6.Text = kodITF2[5].ToString();
            txtITF2p7.Text = kodITF2[6].ToString();
            txtITF2p8.Text = kodITF2[7].ToString();
            txtITF2p9.Text = kodITF2[8].ToString();
            txtITF2p10.Text = kodITF2[9].ToString();
            txtITF2p11.Text = kodITF2[10].ToString();
            txtITF2p12.Text = kodITF2[11].ToString();
            txtITF2p13.Text = kodITF2[12].ToString();
            txtITF2p14.Text = kodITF2[13].ToString();
            txtITF2p15.Text = kodITF2[14].ToString();
            txtITF2p16.Text = kodITF2[15].ToString();
            txtITF2p17.Text = kodITF2[16].ToString();
            txtITF2p18.Text = kodITF2[17].ToString();
            txtITF2p19.Text = kodITF2[18].ToString();
            txtITF2p20.Text = kodITF2[19].ToString();
            txtITF2p21.Text = kodITF2[20].ToString();
            txtITF2p22.Text = kodITF2[21].ToString();
            txtITF2p23.Text = kodITF2[22].ToString();
            txtITF2p24.Text = kodITF2[23].ToString();
        }

        private void UzupelnijKodTrace()
        {
            string kodTrace1;
            try
            {
                kodTrace1 = kodTrace.kod;
            }
            catch
            {
                MessageBox.Show("Nie wypełnione wszystkie pola.");
                kodTrace1 = "00000000000000000000000000";
            }

            txtTracep1.Text = kodTrace1[0].ToString();
            txtTracep2.Text = kodTrace1[1].ToString();
            txtTracep3.Text = kodTrace1[2].ToString();
            txtTracep4.Text = kodTrace1[3].ToString();
            txtTracep5.Text = kodTrace1[4].ToString();
            txtTracep6.Text = kodTrace1[5].ToString();
            txtTracep7.Text = kodTrace1[6].ToString();
            txtTracep8.Text = kodTrace1[7].ToString();
            txtTracep9.Text = kodTrace1[8].ToString();
            txtTracep10.Text = kodTrace1[9].ToString();
            txtTracep11.Text = kodTrace1[10].ToString();
            txtTracep12.Text = kodTrace1[11].ToString();
            txtTracep13.Text = kodTrace1[12].ToString();
            txtTracep14.Text = kodTrace1[13].ToString();
            txtTracep15.Text = kodTrace1[14].ToString();
            txtTracep16.Text = kodTrace1[15].ToString();
            txtTracep17.Text = kodTrace1[16].ToString();
            txtTracep18.Text = kodTrace1[17].ToString();
            txtTracep19.Text = kodTrace1[18].ToString();
            txtTracep20.Text = kodTrace1[19].ToString();
            txtTracep21.Text = kodTrace1[20].ToString();
            txtTracep22.Text = kodTrace1[21].ToString();
            txtTracep23.Text = kodTrace1[22].ToString();
            txtTracep24.Text = kodTrace1[23].ToString();
            txtTracep25.Text = kodTrace1[24].ToString();
            txtTracep26.Text = kodTrace1[25].ToString();
        }



        private void CbbITFICC_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFICC.SelectedItem as itf_icc;
            if (item == null || item.parametr == "")
            {
                kodITF.icc = "0";
            }
            else
            {
                kodITF.icc = item.parametr;
                if (item.parametr == "3")
                {

                    cbbITFCC1.IsEnabled = true;
                    cbbITFCC2.IsEnabled = true;
                }
                else
                {
                    cbbITFCC1.SelectedIndex = 0;
                    cbbITFCC1.IsEnabled = false;
                    cbbITFCC2.SelectedIndex = 0;
                    cbbITFCC2.IsEnabled = false;
                    kodITF.icc = "2";
                    kodITF.cc1 = "1";
                    kodITF.cc2 = "1";
                }
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFCC1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFCC1.SelectedItem as itf_cc;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.cc1 = "0";
            }
            else
            {
                kodITF.cc1 = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFCC2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFCC2.SelectedItem as itf_cc;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.cc2 = "0";
            }
            else
            {
                kodITF.cc2 = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFsmin_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFsmin.SelectedItem as itf_sr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.smin_p = "0";
                kodITF.smin_w = "000";
            }
            else
            {
                kodITF.smin_p = item.parametr;
                kodITF.smin_w = item.wartosc;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFsmax_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFsmax.SelectedItem as itf_sr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.smax_p = "0";
                kodITF.smax_w = "000";
            }
            else
            {
                kodITF.smax_p = item.parametr;
                kodITF.smax_w = item.wartosc;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFtrn_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFtrn.SelectedItem as itf_trn;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.trn = "0";
            }
            else
            {
                kodITF.trn = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFodch_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFodch.SelectedItem as itf_odch;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodITF.odch = "0";
            }
            else
            {
                kodITF.odch = item.parametr;
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbTraceZnak1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceZnak1.SelectedItem as trace_litery;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.znak1 = "00";
            }
            else
            {
                kodTrace.znak1 = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceZnak2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceZnak2.SelectedItem as trace_litery;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.znak2 = "00";
            }
            else
            {
                kodTrace.znak2 = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceKategoria_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceKategoria.SelectedItem as trace_kategoria;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.kategoria = "00";
            }
            else
            {
                kodTrace.kategoria = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSmin_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSmin.SelectedItem as trace_sr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.smin_p = "0";
                kodTrace.smin_w = "000";
            }
            else
            {
                kodTrace.smin_p = item.parametr;
                kodTrace.smin_w = item.wartosc;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSmax_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSmax.SelectedItem as trace_sr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.smax_p = "0";
                kodTrace.smax_w = "000";
            }
            else
            {
                kodTrace.smax_p = item.parametr;
                kodTrace.smax_w = item.wartosc;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceProducent_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceProducent.SelectedItem as trace_producent;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.producent = "00";
            }
            else
            {
                kodTrace.producent = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSDR_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSDR.SelectedItem as trace_sdr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.sdr = "0";
            }
            else
            {
                kodTrace.sdr = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTracePEm_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTracePEm.SelectedItem as trace_pe_m;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.wartosc == "")
            {
                kodTrace.pem = "0000";
            }
            else
            {
                kodTrace.pem = item.wartosc;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceMaterial_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceMaterial.SelectedItem as trace_material;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.material = "0";
            }
            else
            {
                kodTrace.material = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTracePEo_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTracePEo.SelectedItem as trace_pe_o;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.peo = "0";
            }
            else
            {
                kodTrace.peo = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceMFR_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceMFR.SelectedItem as trace_mfr;

            //item = null - w trakcie tworzenia formy
            // a trakcie pracy parametr i wartość są puste dla id = -1
            if (item == null || item.parametr == "")
            {
                kodTrace.mfr = "0";
            }
            else
            {
                kodTrace.mfr = item.parametr;
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void TxtITFprn_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Int32.TryParse(txtITFprn.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,2:00}", value);
            kodITF.prn = result;

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void TxtITFrez_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!Int32.TryParse(txtITFrez.Text, out int value))
            //{
            //    value = 0;
            //}
            var style = NumberStyles.AllowDecimalPoint;
            var culture = CultureInfo.CreateSpecificCulture("en-EN");
            if (!Decimal.TryParse(txtITFrez.Text,style, culture, out decimal value))
            {
                value = 0;
            } else
            { 
            
            }
                
             

            string result = String.Format("{0,1:000}", value);
            result = value.ToString("0.00");
            result = string.Join("", result.Where(char.IsDigit));
            kodITF.rez = result;

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void TxtITFcz1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Int32.TryParse(txtITFcz1.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,3:000}", value);
            kodITF.cz1 = result;

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void TxtITFcz2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Int32.TryParse(txtITFcz2.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,3:000}", value);
            kodITF.cz2 = result;

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void TxtITFke_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Int32.TryParse(txtITFke.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,2:00}", value);
            kodITF.ke = result;

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void TxtTracePartia_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Int32.TryParse(txtTracePartia.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,6:000000}", value);
            kodTrace.partia = result;

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }

        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        private void WyczyscKontrolkiWyrob()
        {
            txtWyrobKod.Text = "";
            txtWyrobIndeks.Text = "";
            txtWyrobOpis.Text = "";
            txtWyrobIwOpZ.Text = "";
            txtWyrobWagaOp.Text = "";
            txtWyrobWaga1szt.Text = "";

            txtITFprn.Text = "";
            txtITFrez.Text = "";
            txtITFcz1.Text = "";
            txtITFcz2.Text = "";
            txtITFke.Text = "";
            txtTracePartia.Text = "";

            List<ComboBox> comboBoxes = GetLogicalChildCollection<ComboBox>(grdPozycje);
            foreach (var cmbBox in comboBoxes)
            {
                cmbBox.SelectedValue = -1;
                //MessageBox.Show(cmbBox.Name);
            }

        }

        private void WyczyscKontrolkiKodKr()
        {
            List<TextBox> TextBoxes = GetLogicalChildCollection<TextBox>(grdKody);
            foreach (var txtBox in TextBoxes)
            {
                txtBox.Text = "";
                //MessageBox.Show(cmbBox.Name);
            }
        }

        private void PrzygotujPodKodKrITF()
        // przeleci przez wszystkie kontrolki dot. kodu ITF i ustawi właściwości klasy kod25
        {
            // kategoria
            CbbITFKategoria_DropDownClosed(null, null);

            // znak 1
            CbbITFZnak1_DropDownClosed(null, null);

            // znak 2
            CbbITFZnak2_DropDownClosed(null, null);

            // icc
            CbbITFICC_DropDownClosed(null, null);

            // cc1
            CbbITFCC1_DropDownClosed(null, null);

            // cc2
            CbbITFCC2_DropDownClosed(null, null);

            // smin
            CbbITFsmin_DropDownClosed(null, null);

            // smax
            CbbITFsmax_DropDownClosed(null, null);

            // trn
            CbbITFtrn_DropDownClosed(null, null);

            // prn
            TxtITFprn_KeyUp(null, null);

            // rez
            TxtITFrez_KeyUp(null, null);

            // odch
            CbbITFodch_DropDownClosed(null, null);

            // cz 1
            TxtITFcz1_KeyUp(null, null);
            // cz 2

            TxtITFcz2_KeyUp(null, null);

            // ke
            TxtITFke_KeyUp(null, null);
        }

        private void PrzygotujPodKodKrTrace()
        // przeleci przez wszystkie kontrolki dot. kodu Traceability i ustawi właściwości klasy kod25
        {
            // znak1
            CbbTraceZnak1_DropDownClosed(null, null);
            // znak2
            CbbTraceZnak2_DropDownClosed(null, null);
            // kategoria
            CbbTraceKategoria_DropDownClosed(null, null);
            // smin
            CbbTraceSmin_DropDownClosed(null, null);
            // smax
            CbbTraceSmax_DropDownClosed(null, null);
            // partia
            TxtTracePartia_KeyUp(null, null);
            // producent
            CbbTraceProducent_DropDownClosed(null, null);
            // sdr
            CbbTraceSDR_DropDownClosed(null, null);
            // pem
            CbbTracePEm_DropDownClosed(null, null);
            // material
            CbbTraceMaterial_DropDownClosed(null, null);
            // peo
            CbbTracePEo_DropDownClosed(null, null);
            // mfr
            CbbTraceMFR_DropDownClosed(null, null);
        }

        private void BtnPokazWszystkie_Click(object sender, RoutedEventArgs e)
        {
            btnPokazWszystkie.Foreground = Brushes.Black;
            btnPokazAktywne.Foreground = Brushes.Black;
            btnPokazWszystkie.Foreground = Brushes.Blue;
            
            
            widoczneWyroby = false;
            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void BtnPokazAktywne_Click(object sender, RoutedEventArgs e)
        {
            btnPokazWszystkie.Foreground = Brushes.Black;
            btnPokazAktywne.Foreground = Brushes.Black;
            btnPokazAktywne.Foreground = Brushes.Blue;

            widoczneWyroby = true;
            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void btnPokazElektro_Click(object sender, RoutedEventArgs e)
        {
            btnPokazElektro.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Black;
            btnPokazElektro.Foreground = Brushes.Blue;
            typFiltrTypKsztaltki = 0;

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void btnPokazDoczolowe_Click(object sender, RoutedEventArgs e)
        {
            btnPokazElektro.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Blue;
            typFiltrTypKsztaltki = 1;

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void btnPokazZawory_Click(object sender, RoutedEventArgs e)
        {
            btnPokazElektro.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Blue;
            typFiltrTypKsztaltki = 2;

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void btnPokazAdaptery_Click(object sender, RoutedEventArgs e)
        {
            btnPokazElektro.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Blue;
            typFiltrTypKsztaltki = 3;

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }

        private void btnPokazWszystkieKsztaltki_Click(object sender, RoutedEventArgs e)
        {
            btnPokazElektro.Foreground = Brushes.Black;
            btnPokazDoczolowe.Foreground = Brushes.Black;
            btnPokazZawory.Foreground = Brushes.Black;
            btnPokazAdaptery.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Black;
            btnPokazWszystkieKsztaltki.Foreground = Brushes.Blue;
            typFiltrTypKsztaltki = -1;

            listWyroby = konf_produkcja_db.PobierzWyroby(widoczneWyroby, typFiltrTypKsztaltki);
            grdLista.ItemsSource = listWyroby;
            lblIloscPozycji.Content = listWyroby.Count().ToString();
        }
    }
}
