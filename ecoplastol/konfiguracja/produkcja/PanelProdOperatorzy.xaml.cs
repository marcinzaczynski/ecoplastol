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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ecoplastol.konfiguracja.produkcja
{
    /// <summary>
    /// Interaction logic for PanelProdOperatorzy.xaml
    /// </summary>
    public partial class PanelProdOperatorzy : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private OperatorzyView rowOperatorzy;
        private List<OperatorzyView> listOperatorzy;

        public PanelProdOperatorzy()
        {
            InitializeComponent();
            listOperatorzy = PanelProdOperatorzy_db.PobierzOperatorowView();

            cbbBrygadzista.ItemsSource = PanelProdBrygadzisci_db.PobierzBrygadzistow(1);
            cbbBrygadzista.SelectedValuePath = "id";
            grdLista.ItemsSource = listOperatorzy;
            if (listOperatorzy.Count == 0)
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

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";
            grdBookmark = grdLista.SelectedIndex;
            grdLista.IsEnabled = false;
            grdPozycje.IsEnabled = true;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            grdPozycje.DataContext = new OperatorzyView();
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
                var poz = grdLista.SelectedItem as OperatorzyView;
                PanelProdOperatorzy_db.UsunOperatora(poz);
                listOperatorzy = PanelProdOperatorzy_db.PobierzOperatorowView();
                grdLista.ItemsSource = listOperatorzy;
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

            listOperatorzy = PanelProdOperatorzy_db.PobierzOperatorowView();
            grdLista.ItemsSource = listOperatorzy;

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
                        var row = new operatorzy_maszyn();
                        var rowAktualny = grdPozycje.DataContext as OperatorzyView;
                        row.id = PanelProdOperatorzy_db.IdOperatora();

                        row.imie = rowAktualny.imie;
                        row.nazwisko = rowAktualny.nazwisko;
                        row.login = rowAktualny.login;
                        row.haslo = rowAktualny.haslo;
                        row.aktywny = rowAktualny.aktywny;
                        row.brygada = rowAktualny.brygada;
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelProdOperatorzy_db.DodajOperatora(row);
                    break;
                case "P":

                    var row2 = new operatorzy_maszyn();
                    var rowAktualny2 = grdPozycje.DataContext as OperatorzyView;
                    row2.id = rowAktualny2.id;
                    row2.imie = rowAktualny2.imie;
                    row2.nazwisko = rowAktualny2.nazwisko;
                    row2.login = rowAktualny2.login;
                    row2.haslo = rowAktualny2.haslo;
                    row2.aktywny = rowAktualny2.aktywny;
                    row2.brygada = rowAktualny2.brygada;
                    row2.opw = rowAktualny2.opw;
                    row2.czasw = rowAktualny2.czasw;
                    row2.opm = frmLogin.LoggedUser.login;
                    row2.czasm = DateTime.Now;
                    PanelProdOperatorzy_db.PoprawOperatora(row2);
                    break;
                default:
                    break;
            }
            listOperatorzy = PanelProdOperatorzy_db.PobierzOperatorowView();
            grdLista.ItemsSource = listOperatorzy;
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowOperatorzy = grdLista.SelectedItem as OperatorzyView;
            grdPozycje.DataContext = rowOperatorzy;
        }
    }
}
