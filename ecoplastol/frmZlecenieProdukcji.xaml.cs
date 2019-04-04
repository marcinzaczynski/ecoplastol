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
        private List<itf_cc> listITFcc1;
        private List<itf_cc> listITFcc2;

        private wyroby Wyrob;

        public frmZlecenieProdukcji(int numerMaszyny, string nazwaMaszyny)
        {
            InitializeComponent();

            lblNazwaMaszyny.Content = nazwaMaszyny;

            listaWyrobow = frmZlecenieProdukcji_db.PobierzWyroby();
            listTracePEm = PanelTrace_db.PobierzTracePem();
            listWyrobRodzajDrutu = produkcja_db.PobierzDruty();
            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            listWyrobZakresSDR = produkcja_db.PobierzZakresSDR();
            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            listITFcc1 = PanelITF_db.PobierzITFcc();
            listITFcc2 = PanelITF_db.PobierzITFcc();

            cbbWyrobKod.ItemsSource = listaWyrobow;
            //cbbWyrobKod.SelectedValuePath = "id";
            cbbTracePEm.ItemsSource = listTracePEm;
            cbbWyrobRodzajDrutu.ItemsSource = listWyrobRodzajDrutu;
            cbbTraceProducent.ItemsSource = listTraceProducent;
            cbbWyrobZakresSDR.ItemsSource = listWyrobZakresSDR;
            cbbITFCC1.ItemsSource = listITFcc1;
            cbbITFCC2.ItemsSource = listITFcc2;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CbbWyrobKod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wyrob = cbbWyrobKod.SelectedItem as wyroby;
            grdZlecenie.DataContext = Wyrob;
        }
    }
}
