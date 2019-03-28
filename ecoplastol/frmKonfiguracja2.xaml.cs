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
    }
}
