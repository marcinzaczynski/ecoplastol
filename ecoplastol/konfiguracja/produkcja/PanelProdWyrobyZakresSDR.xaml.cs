using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ecoplastol.konfiguracja.produkcja
{
    /// <summary>
    /// Interaction logic for PanelProdWyrobZakresSDR.xaml
    /// </summary>
    public partial class PanelProdWyrobyZakresSDR : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private wyroby_zakres_sdr rowWyrobZakresSDR;

        private List<wyroby_zakres_sdr> listWyrobyZakresySDR;
        public PanelProdWyrobyZakresSDR()
        {
            InitializeComponent();
            listWyrobyZakresySDR = PanelProdWyrobyZakresSDR_db.PobierzWyrobyZakresySDR();
            grdLista.ItemsSource = listWyrobyZakresySDR;
            grdPozycje.IsEnabled = false;

            if (listWyrobyZakresySDR.Count == 0)
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

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowWyrobZakresSDR = grdLista.SelectedItem as wyroby_zakres_sdr;
            grdPozycje.DataContext = rowWyrobZakresSDR;
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
            WyczyscKontrolkiWyrobZakresSDR();
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

            wyroby_zakres_sdr poz = new wyroby_zakres_sdr();
            poz = rowWyrobZakresSDR;
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
                PanelProdWyrobyZakresSDR_db.UsunWyrobZakresSDR(rowWyrobZakresSDR);
                listWyrobyZakresySDR = PanelProdWyrobyZakresSDR_db.PobierzWyrobyZakresySDR();
                grdLista.ItemsSource = listWyrobyZakresySDR;
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

            listWyrobyZakresySDR = PanelProdWyrobyZakresSDR_db.PobierzWyrobyZakresySDR();
            grdLista.ItemsSource = listWyrobyZakresySDR;

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
                    if (grdPozycje.DataContext is wyroby_zakres_sdr)
                    {
                        var row = new wyroby_zakres_sdr();
                        row = grdPozycje.DataContext as wyroby_zakres_sdr;
                        row.id = PanelProdWyrobyZakresSDR_db.IdWyrobyZakresSDR();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelProdWyrobyZakresSDR_db.DodajWyrobZakresSDR(row);
                    }
                    break;
                case "P":
                    rowWyrobZakresSDR.opm = frmLogin.LoggedUser.login;
                    rowWyrobZakresSDR.czasm = DateTime.Now;
                    PanelProdWyrobyZakresSDR_db.PoprawWyrobZakresSDR(rowWyrobZakresSDR);
                    break;
                default:
                    break;
            }
            listWyrobyZakresySDR = PanelProdWyrobyZakresSDR_db.PobierzWyrobyZakresySDR();
            grdLista.ItemsSource = listWyrobyZakresySDR;
            grdLista.SelectedIndex = grdBookmark;
            grdLista.Focus();
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

        private void WyczyscKontrolkiWyrobZakresSDR()
        {
            txtParametr.Text = "";
            txtWartosc.Text = "";
            txtOpis.Text = "";

            List<ComboBox> comboBoxes = GetLogicalChildCollection<ComboBox>(grdPozycje);
            foreach (var cmbBox in comboBoxes)
            {
                cmbBox.SelectedValue = -1;
                //MessageBox.Show(cmbBox.Name);
            }
        }
    }
}
