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
            listITFkategorie = frmWyroby_db.PobierzITFKategorie();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(1, listITFkategorie);


            
            grdDane.Children.Add(panelITF);

        }

        private void BtnITFlitery_Click(object sender, RoutedEventArgs e)
        {
            listITFlitery = frmWyroby_db.PobierzITFZnaki();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(2, listITFlitery);

            grdDane.Children.Add(panelITF);
        }

        private void BtnITFicc_Click(object sender, RoutedEventArgs e)
        {
            listITFicc = frmWyroby_db.PobierzITFicc();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(3, listITFicc);

            grdDane.Children.Add(panelITF);
        }

        private void BtnITFcc_Click(object sender, RoutedEventArgs e)
        {
            listITFcc = frmWyroby_db.PobierzITFcc();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(4, listITFcc);

            grdDane.Children.Add(panelITF);
        }

        private void BtnITFsr_Click(object sender, RoutedEventArgs e)
        {
            listITFsr = frmWyroby_db.PobierzITFsr();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(5, listITFsr);

            grdDane.Children.Add(panelITF);
        }

        private void BtnITFtrn_Click(object sender, RoutedEventArgs e)
        {
            listITFtrn = frmWyroby_db.PobierzITFtrn();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(6, listITFtrn);

            grdDane.Children.Add(panelITF);
        }

        private void BtnITFodch_Click(object sender, RoutedEventArgs e)
        {
            listITFodch = frmWyroby_db.PobierzITFodch();

            grdDane.Children.Clear();
            panelITF = null;
            panelITF = new PanelITF(7, listITFodch);

            grdDane.Children.Add(panelITF);
        }
    }
}
