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
    /// Interaction logic for PanelProdWyrobyDruty.xaml
    /// </summary>
    public partial class PanelProdWyrobyDruty : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private wyroby_druty rowWyrobyDrut;

        private List<wyroby_druty> listWyrobyDruty;

        public PanelProdWyrobyDruty()
        {
            InitializeComponent();
            listWyrobyDruty = PanelProdWyrobyDruty_db.PobierzWyrobyDruty();
            grdLista.ItemsSource = listWyrobyDruty;
            grdPozycje.IsEnabled = false;

            if (listWyrobyDruty.Count == 0)
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
            rowWyrobyDrut = grdLista.SelectedItem as wyroby_druty;
            grdPozycje.DataContext = rowWyrobyDrut;
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
            WyczyscKontrolkiWyrobDrut();
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

            wyroby_druty poz = new wyroby_druty();
            poz = rowWyrobyDrut;
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
                PanelProdWyrobyDruty_db.UsunWyrobDrut(rowWyrobyDrut);
                listWyrobyDruty = PanelProdWyrobyDruty_db.PobierzWyrobyDruty();
                grdLista.ItemsSource = listWyrobyDruty;
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

            listWyrobyDruty = PanelProdWyrobyDruty_db.PobierzWyrobyDruty();
            grdLista.ItemsSource = listWyrobyDruty;

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
                    if (grdPozycje.DataContext is wyroby_druty)
                    {
                        var row = new wyroby_druty();
                        row = grdPozycje.DataContext as wyroby_druty;
                        row.id = PanelProdWyrobyDruty_db.IdWyrobyDruty();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelProdWyrobyDruty_db.DodajWyrobDrut(row);
                    }
                    break;
                case "P":
                    rowWyrobyDrut.opm = frmLogin.LoggedUser.login;
                    rowWyrobyDrut.czasm = DateTime.Now;
                    PanelProdWyrobyDruty_db.PoprawWyrobDrut(rowWyrobyDrut);
                    break;
                default:
                    break;
            }
            listWyrobyDruty = PanelProdWyrobyDruty_db.PobierzWyrobyDruty();
            grdLista.ItemsSource = listWyrobyDruty;
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

        private void WyczyscKontrolkiWyrobDrut()
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
