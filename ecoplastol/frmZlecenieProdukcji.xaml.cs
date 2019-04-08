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
using ecoplastol.konfiguracja;
using ecoplastol.konfiguracja.traceability;
using ecoplastol.konfiguracja.produkcja;
using System.Text.RegularExpressions;

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmZlecenieProdukcji.xaml
    /// </summary>
    public partial class frmZlecenieProdukcji : Window
    {
        private List<wyroby> listaWyrobow;

        private List<trace_pe_m> listTracePEm;
        private List<wyroby_druty> listWyrobRodzajDrutu;
        private List<trace_producent> listTraceProducent;
        private List<wyroby_zakres_sdr> listWyrobZakresSDR;
        private List<trace_sdr> listTraceSDR;
        private List<itf_trn> listITFtrn;
        private List<itf_cc> listITFcc1;
        private List<itf_cc> listITFcc2;

        private wyroby Wyrob;
        private zlecenia_produkcyjne zlecenieProdukcyjne;

        public frmZlecenieProdukcji(int numerMaszyny, string nazwaMaszyny)
        {
            InitializeComponent();

            lblNazwaMaszyny.Content = nazwaMaszyny;

            listaWyrobow = produkcja_db.PobierzWyroby();

            listTracePEm = PanelTrace_db.PobierzTracePem();
            listWyrobRodzajDrutu = produkcja_db.PobierzDruty();
            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            listWyrobZakresSDR = produkcja_db.PobierzZakresSDR();
            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            listITFtrn = PanelITF_db.PobierzITFtrn();
            listITFcc1 = PanelITF_db.PobierzITFcc();
            listITFcc2 = PanelITF_db.PobierzITFcc();

            cbbWyrobKod.ItemsSource = listaWyrobow;
            //cbbWyrobKod.SelectedValuePath = "id";
            cbbTracePEm.ItemsSource = listTracePEm;
            cbbWyrobRodzajDrutu.ItemsSource = listWyrobRodzajDrutu;
            cbbTraceProducent.ItemsSource = listTraceProducent;
            cbbWyrobZakresSDR.ItemsSource = listWyrobZakresSDR;
            cbbTraceSDR.ItemsSource = listTraceSDR;
            cbbITFtrn.ItemsSource = listITFtrn;
            cbbITFCC1.ItemsSource = listITFcc1;
            cbbITFCC2.ItemsSource = listITFcc2;

            zlecenieProdukcyjne = new zlecenia_produkcyjne();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CbbWyrobKod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wyrob = cbbWyrobKod.SelectedItem as wyroby;
            switch (Wyrob.wyrob_typ)
            {
                // typ kształtki nieustalony - raczej nieprawdopodobne
                case -1:
                    grdOgolne.IsEnabled = false;
                    grdITF.IsEnabled = false;
                    break;
                // elektrooporowa
                case 0:
                    grdOgolne.IsEnabled = true;
                    grdITF.IsEnabled = true;
                    break;
                // 1 doczołowa, 2 zawór, 3 adapter
                case 1:
                case 2:
                case 3:
                    grdOgolne.IsEnabled = true;
                    grdITF.IsEnabled = false;
                    break;
            }

            //zlecenieProdukcyjne.id 
            zlecenieProdukcyjne.wyrob_kod = Wyrob.wyrob_kod;
            zlecenieProdukcyjne.wyrob_kod_indeks = Wyrob.wyrob_kod_indeks;
            zlecenieProdukcyjne.wyrob_kod_opis = Wyrob.wyrob_kod_opis;
            zlecenieProdukcyjne.wyrob_typ = Wyrob.wyrob_typ;
            zlecenieProdukcyjne.wyrob_il_w_op_zb = Wyrob.wyrob_il_w_op_zb;
            zlecenieProdukcyjne.wyrob_waga_op = Wyrob.wyrob_waga_op;
            zlecenieProdukcyjne.wyrob_waga_1szt = Wyrob.wyrob_waga_1szt;
            zlecenieProdukcyjne.wyrob_zakres_sdr = Wyrob.wyrob_zakres_sdr;
            zlecenieProdukcyjne.wyrob_zast_zaworu = Wyrob.wyrob_zast_zaworu;
            zlecenieProdukcyjne.wyrob_rodzaj_drutu = Wyrob.wyrob_rodzaj_drutu;
            zlecenieProdukcyjne.wyrob_norma = Wyrob.wyrob_norma;
            zlecenieProdukcyjne.wyrob_il_w_partii = Wyrob.wyrob_il_w_partii;
            zlecenieProdukcyjne.zlecenie_ilosc = 0;
            zlecenieProdukcyjne.zlecenie_data_rozp = DateTime.Now;
            zlecenieProdukcyjne.zlecenie_data_zak = DateTime.Now;
            zlecenieProdukcyjne.zlecenie_nr_partii_surowca = "0";
            zlecenieProdukcyjne.zlecenie_nr_partii_drutu = "0";
            zlecenieProdukcyjne.itf_kategoria = Wyrob.itf_kategoria;
            zlecenieProdukcyjne.itf_znak1 = Wyrob.itf_znak1;
            zlecenieProdukcyjne.itf_znak2 = Wyrob.itf_znak2;
            zlecenieProdukcyjne.itf_icc = Wyrob.itf_icc;
            zlecenieProdukcyjne.itf_cc1 = Wyrob.itf_cc1;
            zlecenieProdukcyjne.itf_cc2 = Wyrob.itf_cc2;
            zlecenieProdukcyjne.itf_smin = Wyrob.itf_smin;
            zlecenieProdukcyjne.itf_smax = Wyrob.itf_smax;
            zlecenieProdukcyjne.itf_trn = Wyrob.itf_trn;
            zlecenieProdukcyjne.itf_prn = Wyrob.itf_prn;
            zlecenieProdukcyjne.itf_rez = Wyrob.itf_rez;
            zlecenieProdukcyjne.itf_odch = Wyrob.itf_odch;
            zlecenieProdukcyjne.itf_cz1 = Wyrob.itf_cz1;
            zlecenieProdukcyjne.itf_cz2 = Wyrob.itf_cz2;
            zlecenieProdukcyjne.itf_ke = Wyrob.itf_ke;
            zlecenieProdukcyjne.trace_znak1 = Wyrob.trace_znak1;
            zlecenieProdukcyjne.trace_znak2 = Wyrob.trace_znak2;
            zlecenieProdukcyjne.trace_kategoria = Wyrob.trace_kategoria;
            zlecenieProdukcyjne.trace_smin = Wyrob.trace_smin;
            zlecenieProdukcyjne.trace_smax = Wyrob.trace_smax;
            zlecenieProdukcyjne.trace_partia = Wyrob.trace_partia;
            zlecenieProdukcyjne.trace_producent = Wyrob.trace_producent;
            zlecenieProdukcyjne.trace_sdr = Wyrob.trace_sdr;
            zlecenieProdukcyjne.trace_pe_m = Wyrob.trace_pe_m;
            zlecenieProdukcyjne.trace_material = Wyrob.trace_material;
            zlecenieProdukcyjne.trace_pe_o = Wyrob.trace_pe_o;
            zlecenieProdukcyjne.trace_mfr = Wyrob.trace_mfr;
            zlecenieProdukcyjne.opw = Wyrob.opw;
            zlecenieProdukcyjne.czasw = Wyrob.czasw;
            zlecenieProdukcyjne.opm = Wyrob.opm;
            zlecenieProdukcyjne.czasm = Wyrob.czasm;
            grdZlecenie.DataContext = zlecenieProdukcyjne;
        }

        private void TxtTracePartia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtZlecenieIlosc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // dodanie wyrobu
            var zp = new zlecenia_produkcyjne();
            zp = grdZlecenie.DataContext as zlecenia_produkcyjne;
            zp.id = frmZlecenieProdukcji_db.IdZlecenieProdukcji();
            zp.opw = frmLogin.LoggedUser.login;
            zp.czasw = DateTime.Now;
            zp.opm = frmLogin.LoggedUser.login;
            zp.czasm = DateTime.Now;
            frmZlecenieProdukcji_db.DodajZlecenie(zp);

            DialogResult = true;
        }
    }
}
