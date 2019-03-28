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

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmKonfiguracja2.xaml
    /// </summary>
    public partial class frmKonfiguracja2 : Window
    {
        private List<itf_kategorie> listITFkategorie;
        private List<itf_litery> listITFlitery;
        private List<itf_icc> listITFicc;
        private List<itf_cc> listITFcc;
        private List<itf_sr> listITFsr;
        private List<itf_trn> listITFtrn;
        private List<itf_odch> listITFodch;

        private PanelITF panelITF;

        private List<trace_litery> listTraceLitery;
        private List<trace_kategoria> listTraceKategoria;
        private List<trace_sr> listTraceSr;
        private List<trace_producent> listTraceProducent;
        private List<trace_sdr> listTraceSDR;
        private List<trace_pe_m> listTracePEm;
        private List<trace_material> listTraceMaterial;
        private List<trace_pe_o> listTracePEo;
        private List<trace_mfr> listTraceMFR;

        private PanelTraceLitery panelTraceLitery;
        private PanelTraceKategorie panelTraceKategorie;
        private PanelTraceSrednice panelTraceSrednice;
        private PanelTraceProducenci panelTraceProducenci;
        private PanelTraceSDR panelTraceSDR;
        private PanelTracePEm panelTracePEm;
        private PanelTraceMaterial panelTraceMaterial;
        private PanelTracePEo panelTracePEo;
        private PanelTraceMFR panelTraceMFR;



        public frmKonfiguracja2()
        {
            InitializeComponent();
        }

        private void BtnITFkategoria_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFkategoria.Content;
            listITFkategorie = frmWyroby_db.PobierzITFKategorie();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(1, listITFkategorie);



            grdITFDane.Children.Add(panelITF);

        }

        private void BtnITFlitery_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFlitery.Content;
            listITFlitery = frmWyroby_db.PobierzITFZnaki();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(2, listITFlitery);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnITFicc_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFicc.Content;
            listITFicc = frmWyroby_db.PobierzITFicc();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(3, listITFicc);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnITFcc_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFcc.Content;
            listITFcc = frmWyroby_db.PobierzITFcc();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(4, listITFcc);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnITFsr_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFsr.Content;
            listITFsr = frmWyroby_db.PobierzITFsr();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(5, listITFsr);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnITFtrn_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFtrn.Content;
            listITFtrn = frmWyroby_db.PobierzITFtrn();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(6, listITFtrn);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnITFodch_Click(object sender, RoutedEventArgs e)
        {
            lblITFinfo.Content = btnITFodch.Content;
            listITFodch = frmWyroby_db.PobierzITFodch();

            grdITFDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(7, listITFodch);

            grdITFDane.Children.Add(panelITF);
        }

        private void BtnTraceLitery_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceLitery.Content;
            listTraceLitery = frmWyroby_db.PobierzTraceZnak();

            grdTraceDane.Children.Clear();
            panelTraceLitery = null;
            panelTraceLitery = new PanelTraceLitery(listTraceLitery);

            grdTraceDane.Children.Add(panelTraceLitery);
        }

        private void BtnTraceKategoria_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceKategoria.Content;
            listTraceKategoria = frmWyroby_db.PobierzTraceKategorie();

            grdTraceDane.Children.Clear();
            panelTraceKategorie = null;
            panelTraceKategorie = new PanelTraceKategorie(listTraceKategoria);

            grdTraceDane.Children.Add(panelTraceKategorie);
        }

        private void BtnTraceSr_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceSr.Content;
            listTraceSr = frmWyroby_db.PobierzTraceSr();

            grdTraceDane.Children.Clear();
            panelTraceSrednice = null;
            panelTraceSrednice = new PanelTraceSrednice(listTraceSr);

            grdTraceDane.Children.Add(panelTraceSrednice);
        }

        private void BtnTraceProducenci_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceProducenci.Content;
            listTraceProducent = frmWyroby_db.PobierzTraceProducent();

            grdTraceDane.Children.Clear();
            panelTraceProducenci = null;
            panelTraceProducenci = new PanelTraceProducenci(listTraceProducent);

            grdTraceDane.Children.Add(panelTraceProducenci);
        }

        private void BtnTraceSDR_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceSDR.Content;
            listTraceSDR = frmWyroby_db.PobierzTraceSdr();

            grdTraceDane.Children.Clear();
            panelTraceSDR = null;
            panelTraceSDR = new PanelTraceSDR(listTraceSDR);

            grdTraceDane.Children.Add(panelTraceSDR);
        }

        private void BtnTracePEm_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTracePEm.Content;
            listTracePEm = frmWyroby_db.PobierzTracePem();

            grdTraceDane.Children.Clear();
            panelTracePEm = null;
            panelTracePEm = new PanelTracePEm(listTracePEm);

            grdTraceDane.Children.Add(panelTracePEm);
        }

        private void BtnTraceMaterial_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceMaterial.Content;
            listTraceMaterial = frmWyroby_db.PobierzTraceMaterial();

            grdTraceDane.Children.Clear();
            panelTraceMaterial = null;
            panelTraceMaterial = new PanelTraceMaterial(listTraceMaterial);

            grdTraceDane.Children.Add(panelTraceMaterial);
        }

        private void BtnTracePEo_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTracePEo.Content;
            listTracePEo = frmWyroby_db.PobierzTracePeo();

            grdTraceDane.Children.Clear();
            panelTracePEo = null;
            panelTracePEo = new PanelTracePEo(listTracePEo);

            grdTraceDane.Children.Add(panelTracePEo);
        }

        private void BtnTraceMFR_Click(object sender, RoutedEventArgs e)
        {
            lblTraceinfo.Content = btnTraceMFR.Content;
            listTraceMFR = frmWyroby_db.PobierzTraceMfr();

            grdTraceDane.Children.Clear();
            panelTraceMFR = null;
            panelTraceMFR = new PanelTraceMFR(listTraceMFR);

            grdTraceDane.Children.Add(panelTraceMFR);
        }
    }
}
