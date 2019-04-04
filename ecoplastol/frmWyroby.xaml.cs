using System;
using System.Collections;
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
using ecoplastol.konfiguracja.produkcja;
using ecoplastol.konfiguracja.traceability;
using ecoplastol.konfiguracja;

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmWyroby.xaml
    /// </summary>
    public partial class frmWyroby : Window
    {
        private kodITF kodITF;
        private kodTrace kodTrace;

        private string _akcja;
        private int _id_upd;
        private int dgBookmark;
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

        public frmWyroby()
        {
            InitializeComponent();
            grdPozycje.IsEnabled = false;
            listWyroby = frmWyroby_db.GetProducts();
            grdWyroby.ItemsSource = listWyroby;


            // var firsItem = dgWyroby.Items[0];
            //dgWyroby.SelectedItem = firsItem;
            //dgWyroby.CurrentItem = firsItem;
            //dgWyroby.ScrollIntoView(firsItem);

            UstawWyrob();
            UstawITF();
            UstawTrace();

            kodITF = new kodITF();
            kodTrace = new kodTrace();

            if (listWyroby.Count == 0)
            {
                UstawPrzyciski(0);
            } else
            {
                grdWyroby.Focus();
                grdWyroby.SelectedIndex = 0;

                DgWyroby_SelectionChanged(null, null);
                UstawPrzyciski(1);
            }
        }

        // DESTRUKTOR
        //~frmWyroby()
        //{
        //    MessageBox.Show("Destruktor");
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource wyrobyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("wyrobyViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // wyrobyViewSource.Source = [generic data source]
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
            _akcja = "D";

            grdWyroby.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            grdPozycje.IsEnabled = true;
            WyczyscKontrolkiWyrob();
            WyczyscKontrolkiKodKr();
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

        private void UstawWyrob()
        {
            listTypyWyrobow = produkcja_db.PobierzTypy();
            cbbWyrobTyp.ItemsSource = listTypyWyrobow;
            cbbWyrobTyp.SelectedValuePath = "indeks";

            listWyrobZakresSDR = produkcja_db.PobierzZakresSDR();
            cbbWyrobZakresSDR.ItemsSource = listWyrobZakresSDR;
            cbbWyrobZakresSDR.SelectedValuePath = "indeks";

            listWyrobZastZaworu = produkcja_db.PobierzZastZaworu();
            cbbWyrobZastZaworu.ItemsSource = listWyrobZastZaworu;
            cbbWyrobZastZaworu.SelectedValuePath = "indeks";

            listWyrobRodzajDrutu = produkcja_db.PobierzDruty();
            cbbWyrobRodzajDrutu.ItemsSource = listWyrobRodzajDrutu;
            cbbWyrobRodzajDrutu.SelectedValuePath = "id";
        }

        private void UstawITF()
        {
            // wczytuje parametry do cbb dla kodu ITF
            listITFKategorie = PanelITF_db.PobierzITFKategorie();
            cbbITFKategoria.ItemsSource = listITFKategorie;
            cbbITFKategoria.SelectedValuePath = "id";

            listITFZnaki1 = PanelITF_db.PobierzITFZnaki();
            cbbITFZnak1.ItemsSource = listITFZnaki1;
            cbbITFZnak1.SelectedValuePath = "id";

            listITFZnaki2 = PanelITF_db.PobierzITFZnaki();
            cbbITFZnak2.ItemsSource = listITFZnaki2;
            cbbITFZnak2.SelectedValuePath = "id";

            listITFicc = PanelITF_db.PobierzITFicc();
            cbbITFICC.ItemsSource = listITFicc;
            cbbITFICC.SelectedValuePath = "id";

            listITFcc1 = PanelITF_db.PobierzITFcc();
            cbbITFCC1.ItemsSource = listITFcc1;
            cbbITFCC1.SelectedValuePath = "id";

            listITFcc2 = PanelITF_db.PobierzITFcc();
            cbbITFCC2.ItemsSource = listITFcc2;
            cbbITFCC2.SelectedValuePath = "id";

            listITFsmin = PanelITF_db.PobierzITFsr();
            cbbITFsmin.ItemsSource = listITFsmin;
            cbbITFsmin.SelectedValuePath = "id";

            listITFsmax = PanelITF_db.PobierzITFsr();
            cbbITFsmax.ItemsSource = listITFsmax;
            cbbITFsmax.SelectedValuePath = "id";

            listITFtrn = PanelITF_db.PobierzITFtrn();
            cbbITFtrn.ItemsSource = listITFtrn;
            cbbITFtrn.SelectedValuePath = "id";

            listITFodch = PanelITF_db.PobierzITFodch();
            cbbITFodch.ItemsSource = listITFodch;
            cbbITFodch.SelectedValuePath = "id";
        }

        private void UstawTrace()
        {
            // wczytuje parametry do cbb dla kodu Traceability
            listTraceZnaki1 = PanelTrace_db.PobierzTraceZnak();
            cbbTraceZnak1.ItemsSource = listTraceZnaki1;
            cbbTraceZnak1.SelectedValuePath = "id";

            listTraceZnaki2 = PanelTrace_db.PobierzTraceZnak();
            cbbTraceZnak2.ItemsSource = listTraceZnaki2;
            cbbTraceZnak2.SelectedValuePath = "id";

            listTraceKategorie = PanelTrace_db.PobierzTraceKategorie();
            cbbTraceKategoria.ItemsSource = listTraceKategorie;
            cbbTraceKategoria.SelectedValuePath = "id";

            listTraceSmin = PanelTrace_db.PobierzTraceSr();
            cbbTraceSmin.ItemsSource = listTraceSmin;
            cbbTraceSmin.SelectedValuePath = "id";

            listTraceSmax = PanelTrace_db.PobierzTraceSr();
            cbbTraceSmax.ItemsSource = listTraceSmax;
            cbbTraceSmax.SelectedValuePath = "id";

            listTraceProducent = PanelTrace_db.PobierzTraceProducent();
            cbbTraceProducent.ItemsSource = listTraceProducent;
            cbbTraceProducent.SelectedValuePath = "id";

            listTraceSDR = PanelTrace_db.PobierzTraceSdr();
            cbbTraceSDR.ItemsSource = listTraceSDR;
            cbbTraceSDR.SelectedValuePath = "id";

            listTracePEm = PanelTrace_db.PobierzTracePem();
            cbbTracePEm.ItemsSource = listTracePEm;
            cbbTracePEm.SelectedValuePath = "id";

            listTraceMaterial = PanelTrace_db.PobierzTraceMaterial();
            cbbTraceMaterial.ItemsSource = listTraceMaterial;
            cbbTraceMaterial.SelectedValuePath = "id";

            listTracePEo = PanelTrace_db.PobierzTracePeo();
            cbbTracePEo.ItemsSource = listTracePEo;
            cbbTracePEo.SelectedValuePath = "id";

            listTraceMFR = PanelTrace_db.PobierzTraceMfr();
            cbbTraceMFR.ItemsSource = listTraceMFR;
            cbbTraceMFR.SelectedValuePath = "id";
        }

        private void DgWyroby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var row = grdWyroby.SelectedItem as wyroby;
                //grdInne.DataContext = row;
                _id_upd = row.id;
                cbbWyrobTyp.SelectedValue = row.wyrob_typ;
                txtWyrobKod.Text = row.wyrob_kod;
                txtWyrobIndeks.Text = row.wyrob_kod_indeks;
                txtWyrobOpis.Text = row.wyrob_kod_opis;
                txtWyrobIwOpZ.Text = row.wyrob_il_w_op_zb.ToString();
                txtWyrobWagaOp.Text = row.wyrob_waga_op.ToString();
                txtWyrobWaga1szt.Text = row.wyrob_waga_1szt.ToString();
                cbbWyrobZakresSDR.SelectedValue = row.wyrob_zakres_sdr;
                txtWyrobNorma.Text = row.wyrob_norma.ToString();
                txtWyrobIlwPartii.Text = row.wyrob_il_w_partii.ToString();
                // ITF
                cbbITFKategoria.SelectedValue = row.itf_kategoria;
                cbbITFZnak1.SelectedValue = row.itf_znak1;
                cbbITFZnak2.SelectedValue = row.itf_znak2;
                cbbITFICC.SelectedValue = row.itf_icc;
                cbbITFCC1.SelectedValue = row.itf_cc1;
                cbbITFCC2.SelectedValue = row.itf_cc2;
                cbbITFsmin.SelectedValue = row.itf_smin;
                cbbITFsmax.SelectedValue = row.itf_smax;
                cbbITFtrn.SelectedValue = row.itf_trn;
                txtITFprn.Text = row.itf_prn;
                txtITFrez.Text = row.itf_rez;
                cbbITFodch.SelectedValue = row.itf_odch;
                txtITFcz1.Text = row.itf_cz1;
                txtITFcz2.Text = row.itf_cz2;
                txtITFke.Text = row.itf_ke;
                // TRACEABILITY
                cbbTraceZnak1.SelectedValue = row.trace_znak1;
                cbbTraceZnak2.SelectedValue = row.trace_znak2;
                cbbTraceKategoria.SelectedValue = row.trace_kategoria;
                cbbTraceSmin.SelectedValue = row.trace_smin;
                cbbTraceSmax.SelectedValue = row.trace_smax;
                txtTracePartia.Text = row.trace_partia;
                cbbTraceProducent.SelectedValue = row.trace_producent;
                cbbTraceSDR.SelectedValue = row.trace_sdr;
                cbbTracePEm.SelectedValue = row.trace_pe_m;
                cbbTraceMaterial.SelectedValue = row.trace_material;
                cbbTracePEo.SelectedValue = row.trace_pe_o;
                cbbTraceMFR.SelectedValue = row.trace_mfr;
                // INNE
                cbbWyrobZastZaworu.SelectedValue = row.wyrob_zast_zaworu;
                cbbWyrobRodzajDrutu.SelectedValue = row.wyrob_rodzaj_drutu;

                WyczyscKontrolkiKodKr();
                PrzygotujPodKodKrITF();
                UzupelnijKodITF();
                PrzygotujPodKodKrTrace();
                UzupelnijKodTrace();

            }
            catch
            {
                //txtWyrobKod.Text = "";
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

        private void BtnKlonuj_Click(object sender, RoutedEventArgs e)
        {
            _akcja = "K";

            grdWyroby.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            grdPozycje.IsEnabled = true;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            _akcja = "P";
            dgBookmark = grdWyroby.SelectedIndex;

            grdWyroby.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            grdPozycje.IsEnabled = true;
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            grdWyroby.IsEnabled = true;
            btnZamknij.IsEnabled = true;
            if (listWyroby.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                UstawPrzyciski(1);
            }
            grdPozycje.IsEnabled = false;
            listWyroby = frmWyroby_db.GetProducts();
            grdWyroby.ItemsSource = listWyroby;
            grdWyroby.SelectedIndex = dgBookmark;
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            var nowyWyrob = new wyroby();
            nowyWyrob.wyrob_kod = txtWyrobKod.Text;              // [wyrob_kod] [varchar] (20) NULL,
            nowyWyrob.wyrob_kod_indeks = txtWyrobIndeks.Text; //	[wyrob_kod_indeks] [varchar] (20) NULL,
            nowyWyrob.wyrob_kod_opis = txtWyrobOpis.Text;     // [wyrob_kod_opis] [varchar] (100) NULL,
            nowyWyrob.wyrob_typ = Int32.Parse(cbbWyrobTyp.SelectedValue.ToString());        // [wyrob_typ] [int] NULL,
            nowyWyrob.wyrob_il_w_op_zb = Int32.Parse(txtWyrobIwOpZ.Text);
            nowyWyrob.wyrob_waga_op = Int32.Parse(txtWyrobWagaOp.Text);
            nowyWyrob.wyrob_waga_1szt = Int32.Parse(txtWyrobWaga1szt.Text);
            nowyWyrob.wyrob_zakres_sdr = Int32.Parse(cbbWyrobZakresSDR.SelectedValue.ToString());
            nowyWyrob.wyrob_zast_zaworu = Int32.Parse(cbbWyrobZastZaworu.SelectedValue.ToString());
            nowyWyrob.wyrob_rodzaj_drutu = Int32.Parse(cbbWyrobRodzajDrutu.SelectedValue.ToString());
            if (txtWyrobNorma.Text == "") { txtWyrobNorma.Text = "0"; }
            nowyWyrob.wyrob_norma = Int32.Parse(txtWyrobNorma.Text);
            if (txtWyrobIlwPartii.Text == "") { txtWyrobIlwPartii.Text = "0"; }
            nowyWyrob.wyrob_il_w_partii = Int32.Parse(txtWyrobIlwPartii.Text);
            nowyWyrob.itf_kategoria = Int32.Parse(cbbITFKategoria.SelectedValue.ToString()); // [itf_kategoria] [int] NULL,
            nowyWyrob.itf_znak1 = Int32.Parse(cbbITFZnak1.SelectedValue.ToString());         // [itf_znak1] [int] NULL,
            nowyWyrob.itf_znak2 = Int32.Parse(cbbITFZnak2.SelectedValue.ToString());         // [itf_znak2] [int] NULL,
            nowyWyrob.itf_icc = Int32.Parse(cbbITFICC.SelectedValue.ToString());             // [itf_icc] [int] NULL,
            nowyWyrob.itf_cc1 = Int32.Parse(cbbITFCC1.SelectedValue.ToString());             // [itf_cc1] [int] NULL,
            nowyWyrob.itf_cc2 = Int32.Parse(cbbITFCC2.SelectedValue.ToString());             // [itf_cc2] [int] NULL,
            nowyWyrob.itf_smin = Int32.Parse(cbbITFsmin.SelectedValue.ToString());           // [itf_smin] [int] NULL,
            nowyWyrob.itf_smax = Int32.Parse(cbbITFsmax.SelectedValue.ToString());           // [itf_smax] [int] NULL,
            nowyWyrob.itf_trn = Int32.Parse(cbbITFtrn.SelectedValue.ToString());             // [itf_trn] [int] NULL,
            nowyWyrob.itf_prn = txtITFprn.Text;                                              // [itf_prn] [varchar] (2) NULL,
            nowyWyrob.itf_rez = txtITFrez.Text;                                              // [itf_rez] [varchar] (50) NULL,
            nowyWyrob.itf_odch = Int32.Parse(cbbITFodch.SelectedValue.ToString());           // [itf_odch] [int] NULL,
            nowyWyrob.itf_cz1 = txtITFcz1.Text;                                              // [itf_cz1] [varchar] (2) NULL,
            nowyWyrob.itf_cz2 = txtITFcz2.Text;                                              // [itf_cz2] [varchar] (2) NULL,
            nowyWyrob.itf_ke = txtITFke.Text;                                                // [itf_ke] [varchar] (2) NULL,
            nowyWyrob.trace_znak1 = Int32.Parse(cbbTraceZnak1.SelectedValue.ToString());     // [trace_znak1] [int] NULL,
            nowyWyrob.trace_znak2 = Int32.Parse(cbbTraceZnak2.SelectedValue.ToString());     // [trace_znak2] [int] NULL,
            nowyWyrob.trace_kategoria = Int32.Parse(cbbTraceKategoria.SelectedValue.ToString());     // 	[trace_kategoria] [int] NULL,
            nowyWyrob.trace_smin = Int32.Parse(cbbTraceSmin.SelectedValue.ToString());       // [trace_smin] [int] NULL,
            nowyWyrob.trace_smax = Int32.Parse(cbbTraceSmax.SelectedValue.ToString());       // [trace_smax] [int] NULL,
            nowyWyrob.trace_partia = txtTracePartia.Text;                                    // [trace_partia] [varchar] (6) NULL,
            nowyWyrob.trace_producent = Int32.Parse(cbbTraceProducent.SelectedValue.ToString());     // 	[trace_producent] [int] NULL,
            nowyWyrob.trace_sdr = Int32.Parse(cbbTraceSDR.SelectedValue.ToString());         // [trace_sdr] [int] NULL,
            nowyWyrob.trace_pe_m = Int32.Parse(cbbTracePEm.SelectedValue.ToString());        // [trace_pe_m] [int] NULL,
            nowyWyrob.trace_material = Int32.Parse(cbbTraceMaterial.SelectedValue.ToString());     //  	[trace_material] [int] NULL,
            nowyWyrob.trace_pe_o = Int32.Parse(cbbTracePEo.SelectedValue.ToString());        // [trace_pe_o] [int] NULL,
            nowyWyrob.trace_mfr = Int32.Parse(cbbTraceMFR.SelectedValue.ToString());         // [trace_mfr] [int] NULL,
            if (_akcja == "D")
            {
                // [id][int] IDENTITY(1, 1) NOT NULL,
                nowyWyrob.opw = frmLogin.LoggedUser.login;            	                     // [opw] [varchar] (20) NULL,
                nowyWyrob.czasw = DateTime.Now;                           	                     // [czasw] [datetime] NULL,
                nowyWyrob.opm = frmLogin.LoggedUser.login;                                   // [opm] [varchar] (20) NULL,
                nowyWyrob.czasm = DateTime.Now;          	                                     // [czasm] [datetime] NULL,
                frmWyroby_db.AddProduct(nowyWyrob);
            }
            else if (_akcja == "K")
            {
                nowyWyrob.opw = frmLogin.LoggedUser.login;            	                     // [opw] [varchar] (20) NULL,
                nowyWyrob.czasw = DateTime.Now;                           	                     // [czasw] [datetime] NULL,
                nowyWyrob.opm = frmLogin.LoggedUser.login;                                   // [opm] [varchar] (20) NULL,
                nowyWyrob.czasm = DateTime.Now;          	                                     // [czasm] [datetime] NULL,
                frmWyroby_db.AddProduct(nowyWyrob);
            }
            else if (_akcja == "P")
            {
                var row = grdWyroby.SelectedItem as wyroby;
                nowyWyrob.id = row.id;
                nowyWyrob.opw = row.opw;            	                     // [opw] [varchar] (20) NULL,
                nowyWyrob.czasw = row.czasw;                           	                     // [czasw] [datetime] NULL,
                nowyWyrob.opm = frmLogin.LoggedUser.login;                                   // [opm] [varchar] (20) NULL,
                nowyWyrob.czasm = DateTime.Now;          	                                     // [czasm] [datetime] NULL,
                frmWyroby_db.UpdProduct(nowyWyrob);
            };

            listWyroby = frmWyroby_db.GetProducts();
            grdWyroby.ItemsSource = listWyroby;
            grdWyroby.SelectedIndex = dgBookmark;

            grdWyroby.IsEnabled = true;
            btnZamknij.IsEnabled = true;
            if (listWyroby.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                UstawPrzyciski(1);
            }
            grdPozycje.IsEnabled = false;
        }

        private void CbbITFKategoria_DropDownClosed(object sender, EventArgs e)
        {
            //if (cbbITFKategoria.SelectedIndex > 0)
            {
                var item = cbbITFKategoria.SelectedItem as itf_kategorie;

                //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
                if (item.wartosc != "") { kodITF.kategoria = item.wartosc; } else { kodITF.kategoria = "00"; }
                
            }
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFZnak1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFZnak1.SelectedItem as itf_litery;
            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.znak1 = item.parametr; } else { kodITF.znak1 = "00"; }
            
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFZnak2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFZnak2.SelectedItem as itf_litery;
            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.znak2 = item.parametr; } else { kodITF.znak2 = "00"; }
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
            if (item.parametr == "3")
            {
                cbbITFCC1.IsEnabled = true;
                cbbITFCC2.IsEnabled = true;
                //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
                if (item.parametr != "") { kodITF.icc = item.parametr; } else { kodITF.icc = "0"; }
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
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFCC1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFCC1.SelectedItem as itf_cc;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.cc1 = item.parametr; } else { kodITF.cc1 = "0"; }
          
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFCC2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFCC2.SelectedItem as itf_cc;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.cc2 = item.parametr; } else { kodITF.cc2 = "0"; }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFsmin_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFsmin.SelectedItem as itf_sr;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "")
            {
                kodITF.smin_p = item.parametr;
                kodITF.smin_w = item.wartosc;
            } else
            {
                kodITF.smin_p = "0";
                kodITF.smin_w = "000";
            }
            
            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFsmax_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFsmax.SelectedItem as itf_sr;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "")
            {
                kodITF.smax_p = item.parametr;
                kodITF.smax_w = item.wartosc;
            }
            else
            {
                kodITF.smax_p = "0";
                kodITF.smax_w = "000";
            }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFtrn_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFtrn.SelectedItem as itf_trn;
            
            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.trn = item.parametr; } else { kodITF.trn = "0"; }

            kodITF.GenerujKody();
            UzupelnijKodITF();
        }

        private void CbbITFodch_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbITFodch.SelectedItem as itf_odch;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodITF.odch = item.parametr; } else { kodITF.odch = "0"; }

            kodITF.GenerujKody();
            UzupelnijKodITF();
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
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
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
            if (!Int32.TryParse(txtITFrez.Text, out int value))
            {
                value = 0;
            }

            string result = String.Format("{0,3:000}", value);
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

        private void Wykonaj(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Sprawdz(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CbbTraceZnak1_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceZnak1.SelectedItem as trace_litery;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.znak1 = item.parametr; } else { kodTrace.znak1 = "00"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceZnak2_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceZnak2.SelectedItem as trace_litery;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.znak2 = item.parametr; } else { kodTrace.znak2 = "00"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceKategoria_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceKategoria.SelectedItem as trace_kategoria;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.kategoria = item.parametr; } else { kodTrace.kategoria = "00"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSmin_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSmin.SelectedItem as trace_sr;
            
            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "")
            {
                kodTrace.smin_p = item.parametr;
                kodTrace.smin_w = item.wartosc;
            } else
            {
                kodTrace.smin_p = "0";
                kodTrace.smin_w = "000";
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSmax_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSmax.SelectedItem as trace_sr;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "")
            {
                kodTrace.smax_p = item.parametr;
                kodTrace.smax_w = item.wartosc;
            }
            else
            {
                kodTrace.smax_p = "0";
                kodTrace.smax_w = "000";
            }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceProducent_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceProducent.SelectedItem as trace_producent;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.producent = item.parametr; } else { kodTrace.producent = "00"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceSDR_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceSDR.SelectedItem as trace_sdr;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.sdr = item.parametr; } else { kodTrace.sdr = "0"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTracePEm_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTracePEm.SelectedItem as trace_pe_m;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.wartosc != "") { kodTrace.pem = item.wartosc; } else { kodTrace.pem = "0000"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceMaterial_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceMaterial.SelectedItem as trace_material;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.material = item.parametr; } else { kodTrace.material = "0"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTracePEo_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTracePEo.SelectedItem as trace_pe_o;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.peo = item.parametr; } else { kodTrace.peo = "0"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
        }

        private void CbbTraceMFR_DropDownClosed(object sender, EventArgs e)
        {
            var item = cbbTraceMFR.SelectedItem as trace_mfr;

            //zabezpieczenie jak wybierze się pierwszą, pustą pozycję
            if (item.parametr != "") { kodTrace.mfr = item.parametr; } else { kodTrace.mfr = "0"; }

            kodTrace.GenerujKod();
            UzupelnijKodTrace();
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
    }
}
