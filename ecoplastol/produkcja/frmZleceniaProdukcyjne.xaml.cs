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
        private List<wyroby> listaWyrobow;

        private List<trace_pe_m> listTracePEm;
        private List<wyroby_druty> listWyrobRodzajDrutu;
        private List<trace_producent> listTraceProducent;
        private List<wyroby_zakres_sdr> listWyrobZakresSDR;
        private List<trace_sdr> listTraceSDR;
        private List<itf_trn> listITFtrn;
        private List<itf_cc> listITFcc1;
        private List<itf_cc> listITFcc2;

        public frmZleceniaProdukcyjne()
        {
            InitializeComponent();
            dpDataZleceniaOd.SelectedDate = DateTime.Now;
            dpDataZleceniaDo.SelectedDate = DateTime.Now;

            listaMaszyn = konf_produkcja_db.PobierzMaszyny();
            cbbMaszyna.ItemsSource = listaMaszyn;
            cbbMaszyna.SelectedValuePath = "id";
            cbbMaszyna.DisplayMemberPath = "nazwa";
            cbbMaszyna.SelectedIndex = 0;

            UstawKontrolki();
        }

        private void UstawKontrolki()
        {
            listaWyrobow = konf_produkcja_db.PobierzWyroby(true, -1);

            listTracePEm = PanelTrace_db.PobierzTracePem();
            listWyrobRodzajDrutu = konf_produkcja_db.PobierzDruty();
            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            listWyrobZakresSDR = konf_produkcja_db.PobierzZakresSDR();
            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            listITFtrn = PanelITF_db.PobierzITFtrn();
            listITFcc1 = PanelITF_db.PobierzITFcc();
            listITFcc2 = PanelITF_db.PobierzITFcc();

            cbbWyrobKod.ItemsSource = listaWyrobow;
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
        }

        private void Zatwierdz(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void DpDataZleceniaOd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                WyszukajZlecenia();
            }
        }

        private void DpDataZleceniaDo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                WyszukajZlecenia();
            }
        }

        private void CbbMaszyna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (cbbMaszyna.SelectedItem != null) { WyszukajZlecenia(); }
                
            }
        }

        private void DgrdMeldunki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowZlecenie = dgrdZlecenia.SelectedItem as ZleceniaView;

            grdDane.DataContext = rowZlecenie;
        }

        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
