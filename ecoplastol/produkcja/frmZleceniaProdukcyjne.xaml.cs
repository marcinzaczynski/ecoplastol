using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ecoplastol.konfiguracja;
using ecoplastol.konfiguracja.produkcja;
using ecoplastol.konfiguracja.traceability;
using ecoplastol.planowanie;
using ecoplastol.produkcja;

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmZleceniaProdukcyjne.xaml
    /// </summary>
    public partial class frmZleceniaProdukcyjne : Window
    {
        private List<ZleceniaView> listaZlecen;
        private ZleceniaView rowZlecenie;
        private List<maszyny> listaMaszyn;
        private List<maszyny> listaMaszynZlecenie;
        private List<wyroby> listaWyrobow;

        private List<trace_pe_m> listTracePEm;
        private List<wyroby_druty> listWyrobRodzajDrutu;
        private List<trace_producent> listTraceProducent;
        private List<wyroby_zakres_sdr> listWyrobZakresSDR;
        private List<trace_sdr> listTraceSDR;
        private List<itf_trn> listITFtrn;
        private List<itf_cc> listITFcc1;
        private List<itf_cc> listITFcc2;

        private string akcja;
        private wyroby WyrobDoZlecenia;

        public frmZleceniaProdukcyjne()
        {
            InitializeComponent();
            dpDataZleceniaOd.SelectedDate = DateTime.Now;
            dpDataZleceniaDo.SelectedDate = DateTime.Now;

            listaMaszyn = PanelProdMaszyny_db.PobierzMaszyny();
            cbbMaszyna.ItemsSource = listaMaszyn;
            cbbMaszyna.SelectedValuePath = "id";
            cbbMaszyna.DisplayMemberPath = "nazwa";
            cbbMaszyna.SelectedIndex = 0;

            UstawKontrolki();  // wczytuje dane do comboboxów
            WyszukajZlecenia();
            UstawPrzyciski();
        }

        private void UstawPrzyciski()
        {
            grdDane.IsEnabled = false;
            if (listaZlecen.Count > 0) 
            {
                btnDodaj.IsEnabled = true;
                btnPopraw.IsEnabled = true;
                btnUsun.IsEnabled = true;
                btnAnuluj.IsEnabled = false;
                btnZatwierdz.IsEnabled = false;
            } 
            else
            {
                btnDodaj.IsEnabled = true;
                btnPopraw.IsEnabled = false;
                btnUsun.IsEnabled = false;
                btnAnuluj.IsEnabled = false;
                btnZatwierdz.IsEnabled = false;
            }
        }

        private void UstawKontrolki()
        {
            listaWyrobow = PanelProdWyroby_db.PobierzWyroby(false, -1);
            listaMaszynZlecenie = PanelProdMaszyny_db.PobierzMaszyny();

            listTracePEm = PanelTrace_db.PobierzTracePem();
            listWyrobRodzajDrutu = PanelProdWyrobyDruty_db.PobierzWyrobyDruty();
            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            listWyrobZakresSDR = PanelProdWyrobyZakresSDR_db.PobierzWyrobyZakresySDR();
            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            listITFtrn = PanelITF_db.PobierzITFtrn();
            listITFcc1 = PanelITF_db.PobierzITFcc();
            listITFcc2 = PanelITF_db.PobierzITFcc();


            cbbWyrobKod.ItemsSource = listaWyrobow;
            cbbZlecenieMaszyna.ItemsSource = listaMaszynZlecenie;

            cbbTracePEm.ItemsSource = listTracePEm;
            cbbWyrobRodzajDrutu.ItemsSource = listWyrobRodzajDrutu;
            cbbTraceProducent.ItemsSource = listTraceProducent;
            cbbWyrobZakresSDR.ItemsSource = listWyrobZakresSDR;
            cbbTraceSDR.ItemsSource = listTraceSDR;
            cbbITFtrn.ItemsSource = listITFtrn;
            cbbITFCC1.ItemsSource = listITFcc1;
            cbbITFCC2.ItemsSource = listITFcc2;
        }

        private void WyszukajZlecenia()
        {
            listaZlecen = produkcja_db.PobierzZlecenia(dpDataZleceniaOd.SelectedDate.Value, dpDataZleceniaDo.SelectedDate.Value, ((maszyny)cbbMaszyna.SelectedItem).id);
            dgrdZlecenia.ItemsSource = listaZlecen;
            dgrdZlecenia.SelectedIndex = 0;
        }

        

        private void DpDataZleceniaOd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                WyszukajZlecenia();
                UstawPrzyciski();
            }
        }

        private void DpDataZleceniaDo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                WyszukajZlecenia();
                UstawPrzyciski();
            }
        }

        private void CbbMaszyna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (cbbMaszyna.SelectedItem != null) 
                { 
                    WyszukajZlecenia();
                    UstawPrzyciski();
                }
                
            }
        }

        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {
            bool moznaZatwierdzic = true;

            if (cbbZlecenieMaszyna.SelectedIndex > 0)
            {
                brdZlecenieMaszyna.Visibility = Visibility.Hidden;
            }
            else
            {
                brdZlecenieMaszyna.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbWyrobKod.SelectedIndex >= 0)
            {
                brdWyrobKod.Visibility = Visibility.Hidden;
            }
            else
            {
                brdWyrobKod.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtZlecenieIlosc.Text.ToString() != "")
            {
                brdZlecenieIlosc.Visibility = Visibility.Hidden;
            }
            else
            {
                brdZlecenieIlosc.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (dpDataRozpoczecia.SelectedDate.HasValue)
            {
                brdDataRozpoczecia.Visibility = Visibility.Hidden;
            }
            else
            {
                brdDataRozpoczecia.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (dpDataZakonczenia.SelectedDate.HasValue)
            {
                brdDataZakonczenia.Visibility = Visibility.Hidden;
            }
            else
            {
                brdDataZakonczenia.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtZlecenieNrPartiiSurowca.Text.ToString() != "")
            {
                brdZlecenieNrPartiiSurowca.Visibility = Visibility.Hidden;
            }
            else
            {
                brdZlecenieNrPartiiSurowca.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbTracePEm.SelectedIndex > 0)
            {
                brdTracePEm.Visibility = Visibility.Hidden;
            }
            else
            {
                brdTracePEm.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtTracePartia.Text.ToString() != "")
            {
                brdTracePartia.Visibility = Visibility.Hidden;
            }
            else
            {
                brdTracePartia.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbTraceProducent.SelectedIndex > 0)
            {
                brdTraceProducent.Visibility = Visibility.Hidden;
            }
            else
            {
                brdTraceProducent.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbTraceSDR.SelectedIndex > 0)
            {
                brdTraceSDR.Visibility = Visibility.Hidden;
            }
            else
            {
                brdTraceSDR.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbWyrobZakresSDR.SelectedIndex > 0)
            {
                brdWyrobZakresSDR.Visibility = Visibility.Hidden;
            }
            else
            {
                brdWyrobZakresSDR.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtZlecenieNrPartiiDrutu.Text.ToString() != "")
            {
                brdZlecenieNrPartiiDrutu.Visibility = Visibility.Hidden;
            }
            else
            {
                brdZlecenieNrPartiiDrutu.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbWyrobRodzajDrutu.SelectedIndex >= 0)
            {
                brdWyrobRodzajDrutu.Visibility = Visibility.Hidden;
            }
            else
            {
                brdWyrobRodzajDrutu.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbITFtrn.SelectedIndex > 0)
            {
                brdITFtrn.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFtrn.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtITFrez.Text.ToString() != "")
            {
                brdITFrez.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFrez.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtITFcz1.Text.ToString() != "")
            {
                brdITFcz1.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFcz1.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtITFcz2.Text.ToString() != "")
            {
                brdITFcz2.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFcz2.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbITFCC1.SelectedIndex > 0)
            {
                brdITFcc1.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFcc1.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbITFCC2.SelectedIndex > 0)
            {
                brdITFcc2.Visibility = Visibility.Hidden;
            }
            else
            {
                brdITFcc2.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (moznaZatwierdzic) e.CanExecute = true;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";
            rowZlecenie = new ZleceniaView();
            rowZlecenie.zlecenie_data_rozp = DateTime.Now;
            rowZlecenie.zlecenie_data_zak = DateTime.Now;
            grdDane.DataContext = rowZlecenie;

            grdFiltry.IsEnabled = false;
            dgrdZlecenia.IsEnabled = false;
            grdDane.IsEnabled = true;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            grdFiltry.IsEnabled = false;
            dgrdZlecenia.IsEnabled = false;
            grdDane.IsEnabled = true;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            akcja = "U";
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "A";
            grdFiltry.IsEnabled = true;
            dgrdZlecenia.IsEnabled = true;
            grdDane.IsEnabled = false;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;
            btnZamknij.IsEnabled = true;
        }

        private void Zatwierdz(object sender, ExecutedRoutedEventArgs e)
        {
           
            switch (akcja)
            {
                case "D":
                    try
                    {
                        var zpv = new ZleceniaView();
                        zpv = grdDane.DataContext as ZleceniaView;
                        var zp = new zlecenia_produkcyjne();
                        zp.id = produkcja_db.IdZlecenieProdukcji();
                        zp.wyrob_kod_id = zpv.wyrob_kod_id;
                        zp.wyrob_kod = zpv.wyrob_kod;
                        zp.wyrob_kod_indeks = zpv.wyrob_kod_indeks;
                        zp.wyrob_kod_opis = zpv.wyrob_kod_opis;
                        zp.wyrob_typ = zpv.wyrob_typ;
                        zp.wyrob_il_w_op_zb = zpv.wyrob_il_w_op_zb;
                        zp.wyrob_waga_op = zpv.wyrob_waga_op;
                        zp.wyrob_waga_1szt = zpv.wyrob_waga_1szt;
                        zp.wyrob_zakres_sdr = zpv.wyrob_zakres_sdr;
                        zp.wyrob_zast = zpv.wyrob_zast;
                        zp.wyrob_rodzaj_drutu = zpv.wyrob_rodzaj_drutu;
                        zp.wyrob_norma = zpv.wyrob_norma;
                        zp.wyrob_il_w_partii = zpv.wyrob_il_w_partii;
                        zp.zlecenie_nr_maszyny = zpv.zlecenie_nr_maszyny;
                        zp.zlecenie_ilosc = zpv.zlecenie_ilosc;
                        zp.zlecenie_data_rozp = zpv.zlecenie_data_rozp;
                        zp.zlecenie_data_zak = zpv.zlecenie_data_zak;
                        zp.zlecenie_nr_partii_surowca = zpv.zlecenie_nr_partii_surowca;
                        zp.zlecenie_nr_partii_drutu = zpv.zlecenie_nr_partii_drutu;
                        zp.itf_kategoria = zpv.itf_kategoria;
                        zp.itf_znak1 = zpv.itf_znak1;
                        zp.itf_znak2 = zpv.itf_znak2;
                        zp.itf_icc = zpv.itf_icc;
                        zp.itf_cc1 = zpv.itf_cc1;
                        zp.itf_cc2 = zpv.itf_cc2;
                        zp.itf_smin = zpv.itf_smin;
                        zp.itf_smax = zpv.itf_smax;
                        zp.itf_trn = zpv.itf_trn;
                        zp.itf_prn = zpv.itf_prn;
                        zp.itf_rez = zpv.itf_rez;
                        zp.itf_odch = zpv.itf_odch;
                        zp.itf_cz1 = zpv.itf_cz1;
                        zp.itf_cz2 = zpv.itf_cz2;
                        zp.itf_ke = zpv.itf_ke;
                        zp.trace_znak1 = zpv.trace_znak1;
                        zp.trace_znak2 = zpv.trace_znak2;
                        zp.trace_kategoria = zpv.trace_kategoria;
                        zp.trace_smin = zpv.trace_smin;
                        zp.trace_smax = zpv.trace_smax;
                        zp.trace_partia = zpv.trace_partia;
                        zp.trace_producent = zpv.trace_producent;
                        zp.trace_sdr = zpv.trace_sdr;
                        zp.trace_pe_m = zpv.trace_pe_m;
                        zp.trace_material = zpv.trace_material;
                        zp.trace_pe_o = zpv.trace_pe_o;
                        zp.trace_mfr = zpv.trace_mfr;
                        zp.opw = frmLogin.LoggedUser.login;
                        zp.czasw = DateTime.Now;
                        zp.opm = frmLogin.LoggedUser.login;
                        zp.czasm = DateTime.Now;
                        produkcja_db.DodajZlecenie(zp);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Wypełnij formularz.");
                        MessageBox.Show(ex.ToString());
                    }
                    
                    break;
                case "P":
                    try
                    {
                        var zpv = new ZleceniaView();
                        zpv = grdDane.DataContext as ZleceniaView;
                        var zp = new zlecenia_produkcyjne();
                        zp.id = zpv.id;
                        zp.wyrob_kod_id = zpv.wyrob_kod_id;
                        zp.wyrob_kod = zpv.wyrob_kod;
                        zp.wyrob_kod_indeks = zpv.wyrob_kod_indeks;
                        zp.wyrob_kod_opis = zpv.wyrob_kod_opis;
                        zp.wyrob_typ = zpv.wyrob_typ;
                        zp.wyrob_il_w_op_zb = zpv.wyrob_il_w_op_zb;
                        zp.wyrob_waga_op = zpv.wyrob_waga_op;
                        zp.wyrob_waga_1szt = zpv.wyrob_waga_1szt;
                        zp.wyrob_zakres_sdr = zpv.wyrob_zakres_sdr;
                        zp.wyrob_zast = zpv.wyrob_zast;
                        zp.wyrob_rodzaj_drutu = zpv.wyrob_rodzaj_drutu;
                        zp.wyrob_norma = zpv.wyrob_norma;
                        zp.wyrob_il_w_partii = zpv.wyrob_il_w_partii;
                        zp.zlecenie_nr_maszyny = zpv.zlecenie_nr_maszyny;
                        zp.zlecenie_ilosc = zpv.zlecenie_ilosc;
                        zp.zlecenie_data_rozp = zpv.zlecenie_data_rozp;
                        zp.zlecenie_data_zak = zpv.zlecenie_data_zak;
                        zp.zlecenie_nr_partii_surowca = zpv.zlecenie_nr_partii_surowca;
                        zp.zlecenie_nr_partii_drutu = zpv.zlecenie_nr_partii_drutu;
                        zp.itf_kategoria = zpv.itf_kategoria;
                        zp.itf_znak1 = zpv.itf_znak1;
                        zp.itf_znak2 = zpv.itf_znak2;
                        zp.itf_icc = zpv.itf_icc;
                        zp.itf_cc1 = zpv.itf_cc1;
                        zp.itf_cc2 = zpv.itf_cc2;
                        zp.itf_smin = zpv.itf_smin;
                        zp.itf_smax = zpv.itf_smax;
                        zp.itf_trn = zpv.itf_trn;
                        zp.itf_prn = zpv.itf_prn;
                        zp.itf_rez = zpv.itf_rez;
                        zp.itf_odch = zpv.itf_odch;
                        zp.itf_cz1 = zpv.itf_cz1;
                        zp.itf_cz2 = zpv.itf_cz2;
                        zp.itf_ke = zpv.itf_ke;
                        zp.trace_znak1 = zpv.trace_znak1;
                        zp.trace_znak2 = zpv.trace_znak2;
                        zp.trace_kategoria = zpv.trace_kategoria;
                        zp.trace_smin = zpv.trace_smin;
                        zp.trace_smax = zpv.trace_smax;
                        zp.trace_partia = zpv.trace_partia;
                        zp.trace_producent = zpv.trace_producent;
                        zp.trace_sdr = zpv.trace_sdr;
                        zp.trace_pe_m = zpv.trace_pe_m;
                        zp.trace_material = zpv.trace_material;
                        zp.trace_pe_o = zpv.trace_pe_o;
                        zp.trace_mfr = zpv.trace_mfr;
                        zp.opw = zpv.opw;
                        zp.czasw = zpv.czasw;
                        zp.opm = frmLogin.LoggedUser.login;
                        zp.czasm = DateTime.Now;
                        produkcja_db.PoprawZlecenie(zp);
                        DialogResult = true;
                    }
                    catch
                    {
                        MessageBox.Show("Wypełnij formularz.");
                    }
                    break;
                default:
                    break;
            }
            akcja = "Z";
            grdFiltry.IsEnabled = true;
            dgrdZlecenia.IsEnabled = true;
            grdDane.IsEnabled = false;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;
            btnZamknij.IsEnabled = true;
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtZlecenieIlosc_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WyszukajZlecenia();
        }

        private void dgrdZlecenia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowZlecenie = dgrdZlecenia.SelectedItem as ZleceniaView;

            grdDane.DataContext = rowZlecenie;
        }

        private void cbbWyrobKod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cbbWyrobKod.SelectedItem as wyroby != null) && (akcja=="D"))
            {
                WyrobDoZlecenia = cbbWyrobKod.SelectedItem as wyroby;

                // żeby odświeżyć datacontext muszę utworzyć nową zmienną i przepisać wszystkie włąściwości, które mają pozostać niezmienne 
                // przy zmianie wyrobu.
                var rowZlecenie2 = new ZleceniaView();
                rowZlecenie2.zlecenie_nr_maszyny = (grdDane.DataContext as ZleceniaView).zlecenie_nr_maszyny;
                rowZlecenie2.zlecenie_ilosc = (grdDane.DataContext as ZleceniaView).zlecenie_ilosc;
                rowZlecenie2.zlecenie_data_rozp = (grdDane.DataContext as ZleceniaView).zlecenie_data_rozp;
                rowZlecenie2.zlecenie_data_zak = (grdDane.DataContext as ZleceniaView).zlecenie_data_zak;
                rowZlecenie2.zlecenie_nr_partii_surowca = (grdDane.DataContext as ZleceniaView).zlecenie_nr_partii_surowca;
                rowZlecenie2.zlecenie_nr_partii_drutu = (grdDane.DataContext as ZleceniaView).zlecenie_nr_partii_drutu;
           
                rowZlecenie2.wyrob_kod_id = WyrobDoZlecenia.id;
                rowZlecenie2.wyrob_kod = WyrobDoZlecenia.wyrob_kod;
                rowZlecenie2.wyrob_kod_indeks = WyrobDoZlecenia.wyrob_kod_indeks;
                rowZlecenie2.wyrob_kod_opis = WyrobDoZlecenia.wyrob_kod_opis;
                rowZlecenie2.wyrob_typ = WyrobDoZlecenia.wyrob_typ;
                rowZlecenie2.wyrob_il_w_op_zb = WyrobDoZlecenia.wyrob_il_w_op_zb;
                rowZlecenie2.wyrob_waga_op = WyrobDoZlecenia.wyrob_waga_op;
                rowZlecenie2.wyrob_waga_1szt = WyrobDoZlecenia.wyrob_waga_1szt;
                rowZlecenie2.wyrob_zakres_sdr = WyrobDoZlecenia.wyrob_zakres_sdr;
                rowZlecenie2.wyrob_zast = WyrobDoZlecenia.wyrob_zast;
                rowZlecenie2.wyrob_rodzaj_drutu = WyrobDoZlecenia.wyrob_rodzaj_drutu;
                rowZlecenie2.wyrob_norma = WyrobDoZlecenia.wyrob_norma;
                rowZlecenie2.wyrob_il_w_partii = WyrobDoZlecenia.wyrob_il_w_partii;
                rowZlecenie2.itf_kategoria = WyrobDoZlecenia.itf_kategoria;
                rowZlecenie2.itf_znak1 = WyrobDoZlecenia.itf_znak1;
                rowZlecenie2.itf_znak2 = WyrobDoZlecenia.itf_znak2;
                rowZlecenie2.itf_icc = WyrobDoZlecenia.itf_icc;
                rowZlecenie2.itf_cc1 = WyrobDoZlecenia.itf_cc1;
                rowZlecenie2.itf_cc2 = WyrobDoZlecenia.itf_cc2;
                rowZlecenie2.itf_smin = WyrobDoZlecenia.itf_smin;
                rowZlecenie2.itf_smax = WyrobDoZlecenia.itf_smax;
                rowZlecenie2.itf_trn = WyrobDoZlecenia.itf_trn;
                rowZlecenie2.itf_prn = WyrobDoZlecenia.itf_prn;
                rowZlecenie2.itf_rez = WyrobDoZlecenia.itf_rez;
                rowZlecenie2.itf_odch = WyrobDoZlecenia.itf_odch;
                rowZlecenie2.itf_cz1 = WyrobDoZlecenia.itf_cz1;
                rowZlecenie2.itf_cz2 = WyrobDoZlecenia.itf_cz2;
                rowZlecenie2.itf_ke = WyrobDoZlecenia.itf_ke;
                rowZlecenie2.trace_znak1 = WyrobDoZlecenia.trace_znak1;
                rowZlecenie2.trace_znak2 = WyrobDoZlecenia.trace_znak2;
                rowZlecenie2.trace_kategoria = WyrobDoZlecenia.trace_kategoria;
                rowZlecenie2.trace_smin = WyrobDoZlecenia.trace_smin;
                rowZlecenie2.trace_smax = WyrobDoZlecenia.trace_smax;
                rowZlecenie2.trace_partia = WyrobDoZlecenia.trace_partia;
                rowZlecenie2.trace_producent = WyrobDoZlecenia.trace_producent;
                rowZlecenie2.trace_sdr = WyrobDoZlecenia.trace_sdr;
                rowZlecenie2.trace_pe_m = WyrobDoZlecenia.trace_pe_m;
                rowZlecenie2.trace_material = WyrobDoZlecenia.trace_material;
                rowZlecenie2.trace_pe_o = WyrobDoZlecenia.trace_pe_o;
                rowZlecenie2.trace_mfr = WyrobDoZlecenia.trace_mfr;

                grdDane.DataContext = rowZlecenie2;
                
            }
        }
    }
}
