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
    public delegate void RefreshDataDelegate();
    /// <summary>
    /// Interaction logic for frmPlanowanie2.xaml
    /// </summary>
    /// 

    public partial class frmPlanowanie2 : Window
    {
        private List<maszyny> listaMaszyn;
        //private List<zlecenia_produkcyjne> listaZleceniaProdukcyjne;
        public static List<MaszynaPanel> listaPaneli;
        private int _ilKolumn = 3;
        private int _ilWierszy;

        public frmPlanowanie2()
        {
            InitializeComponent();
            listaPaneli = new List<MaszynaPanel>();
            UstawPanele();
            dpZleceniaNaDzien.SelectedDate = DateTime.Now.Date;
            UzupelnijPaneleZleceniami();

            MaszynaPanel.RefresData += new RefreshDataDelegate(UzupelnijPaneleZleceniami);
            MaszynaZlecenie.RefreshData += new RefreshDataDelegate(UzupelnijPaneleZleceniami);
        }

        private void UstawPanele()
        {
            listaMaszyn = frmPlanowanie2_db.PobierzMaszyny();
            double ilMaszyn = listaMaszyn.Count;
            double il_w = ilMaszyn / _ilKolumn;
            _ilWierszy = Convert.ToInt32(Math.Ceiling(il_w));

            for (int i = 0; i < _ilWierszy; i++)
            {
                grdMaszyny.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _ilKolumn; i++)
            {
                grdMaszyny.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int licznik = 0;

            //List<MaszynaPanel> listaPaneli = new List<MaszynaPanel>();
            for (int i = 0; i < _ilWierszy; i++)
            {
                for (int j = 0; j < _ilKolumn; j++)
                {
                    if (licznik == ilMaszyn) { break; } // grid może mieć więcej komórek niż jest maszyn
                    MaszynaPanel mp = new MaszynaPanel(listaMaszyn[licznik].id, listaMaszyn[licznik].numer);
                    //mp.Name = "panelM" + Convert.ToChar(listaMaszyn[licznik].id + 1);
                    //mp.Tag = licznik + 1;
                    listaPaneli.Add(mp);
                    Grid.SetRow(mp, i);
                    Grid.SetColumn(mp, j);
                    grdMaszyny.Children.Add(mp);
                    licznik++;
                }
            }
        }

        public void UzupelnijPaneleZleceniami()
        {
            var paneleMaszyn = grdMaszyny.Children.OfType<MaszynaPanel>();
            foreach (var item in paneleMaszyn)
            {
                var mp = item as MaszynaPanel;
                // ... i dla każdego panela szukam w zleceń w bazie
                var listaZleceniaProdukcyjne = new List<zlecenia_produkcyjne>();
                listaZleceniaProdukcyjne = frmPlanowanie2_db.PobierzZleceniaDlaMaszyny(Convert.ToInt32(mp.Tag), dpZleceniaNaDzien.SelectedDate);
                // panelZlecen - miejsce w PanelMaszyna do którego lądują MaszynaZlecenie
                var panelZlecen = mp.spMaszynaZlecenia;
                // najpierw trzeba wyczyścić MaszynaPanel ...
                panelZlecen.Children.Clear();
                // teraz przelatuję przez zlecenia dla danej maszyny
                foreach (var zp in listaZleceniaProdukcyjne)
                {
                    // ... trzeba utworzyć nowe MaszynZlecenie ...
                    MaszynaZlecenie mz = new MaszynaZlecenie(zp);
                    mz.lblTracePeM.Content = konfiguracja.traceability.PanelTrace_db.PobierzTracePemOpis(zp.trace_pe_m);
                    mz.lblZlecenieNrPartiiSurowca.Content = zp.zlecenie_nr_partii_surowca;
                    mz.lblTraceProducent.Content = konfiguracja.traceability.PanelTrace_db.PobierzTraceProducentWartosc(zp.trace_producent);
                    mz.lblTracePartia.Content = zp.trace_partia;
                    mz.lblSDR.Content = String.Format("SDR : {0} ({1})",
                        konfiguracja.traceability.PanelTrace_db.PobierzTraceSdrWartosc(zp.trace_sdr),
                        konfiguracja.produkcja.produkcja_db.PobierzWyrobZakresSdrWartosc(zp.wyrob_zakres_sdr));

                    // ... i dodać je do MaszynaPanel w odpowiednim miejscu - panelZlecen
                    panelZlecen.Children.Add(mz);
                }
                if (listaZleceniaProdukcyjne.Count == 0)
                {
                    mp.border.Background = (SolidColorBrush)FindResource("panelMaszynaNieaktywny");
                } else
                {

                    mp.border.Background = (SolidColorBrush)FindResource("panelMaszynaAktywny");
                }
            }
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DpZleceniaNaDzien_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UzupelnijPaneleZleceniami();
        }

        private void BtnDataWstecz_Click(object sender, RoutedEventArgs e)
        {
            if (dpZleceniaNaDzien.SelectedDate.HasValue)
            {
                var data = new DateTime();
                data = dpZleceniaNaDzien.SelectedDate.Value;
                data = data.AddDays(-1);
                dpZleceniaNaDzien.SelectedDate = data;
            }
        }

        private void BtnDataNast_Click(object sender, RoutedEventArgs e)
        {
            if (dpZleceniaNaDzien.SelectedDate.HasValue)
            {
                var data = new DateTime();
                data = dpZleceniaNaDzien.SelectedDate.Value;
                data = data.AddDays(1);
                dpZleceniaNaDzien.SelectedDate = data;
            }
        }
    }
}
