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
using ecoplastol.konfiguracja.produkcja;

namespace ecoplastol.planowanie
{
    /// <summary>
    /// Interaction logic for frmMeldunkiWadyNN.xaml
    /// </summary>
    public partial class frmMeldunkiWadyNN : Window
    {
        private List<PrzyczynyBrakowView> listaPrzyczynBrakow;
        private List<wady_nn> listaWadNN;

        private PrzyczynyBrakowView rowPrzyczyna;

        private string akcja;
        private int idMeldunku;
        private int dgBookmark;

        public frmMeldunkiWadyNN(DateTime data, int maszyna, int zlecenie, int zmiana, int op, int idMel)
        {
            InitializeComponent();
            idMeldunku = idMel;
            lblData.Content = string.Format("{0:yyyy-MM-dd}", data);
            lblMaszyna.Content = konf_produkcja_db.PobierzNazweMaszyny(maszyna);
            lblZlecenie.Content = frmZlecenieProdukcji_db.PobierzKodZlecenia(zlecenie);
            lblZmiana.Content = konf_produkcja_db.PobierzNazweZmiany(zmiana);
            lblOperator.Content = konf_produkcja_db.PobierzImieNazwiskoOperatora(op);

            listaWadNN = frmMeldunki_db.PobierzWadyNN();
            cbbPrzyczyna.ItemsSource = listaWadNN;
            cbbPrzyczyna.SelectedValuePath = "id";
            cbbPrzyczyna.DisplayMemberPath = "wartosc";

            listaPrzyczynBrakow = frmMeldunki_db.PobierzPrzyczynyBrakow(idMeldunku);
            dgrdLista.ItemsSource = listaPrzyczynBrakow;

            if (listaPrzyczynBrakow.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                dgrdLista.Focus();
                dgrdLista.SelectedIndex = 0;

                UstawPrzyciski(1);
            }
            grdDane.IsEnabled = false;
        }

        private void UstawPrzyciski(int i)
        {
            // i == 0 - nie ma żadnego rekordu z tabeli
            // i == 1 - jest co najmniej jeden rekord z tabeli
            switch (i)
            {
                case 0:
                    btnDodaj.IsEnabled = true;
                    btnPopraw.IsEnabled = false;
                    btnUsun.IsEnabled = false;
                    btnAnuluj.IsEnabled = false;
                    btnZatwierdz.IsEnabled = false;
                    break;
                case 1:
                    btnDodaj.IsEnabled = true;
                    btnPopraw.IsEnabled = true;
                    btnUsun.IsEnabled = true;
                    btnAnuluj.IsEnabled = false;
                    btnZatwierdz.IsEnabled = false;
                    break;
            }
        }


        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DgrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowPrzyczyna = dgrdLista.SelectedItem as PrzyczynyBrakowView;

            grdDane.DataContext = rowPrzyczyna;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";

            rowPrzyczyna = new PrzyczynyBrakowView();

            rowPrzyczyna.id_meldunek = idMeldunku;
            grdDane.DataContext = rowPrzyczyna;

            dgrdLista.IsEnabled = false;
            grdDane.IsEnabled = true;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            btnZamknij.IsEnabled = false;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            akcja = "P";
            dgBookmark = dgrdLista.SelectedIndex;

            dgrdLista.IsEnabled = false;
            grdDane.IsEnabled = true;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            btnZamknij.IsEnabled = false;
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            var Res = MessageBox.Show("Usunąć ?", "Usuwanie pozycji", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (Res == MessageBoxResult.Yes)
            {
                var poz = this.grdDane.DataContext as PrzyczynyBrakowView;
                frmMeldunki_db.UsunPrzyczyneBrakow(poz);
            }

            listaPrzyczynBrakow = frmMeldunki_db.PobierzPrzyczynyBrakow(idMeldunku);
            dgrdLista.ItemsSource = listaPrzyczynBrakow;

            if (listaPrzyczynBrakow.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                dgrdLista.Focus();
                dgrdLista.SelectedIndex = 0;

                UstawPrzyciski(1);
            }

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            dgrdLista.IsEnabled = true;
            grdDane.IsEnabled = false;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            btnZamknij.IsEnabled = true;
            
        }

        private void TxtIlosc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {
            bool moznaZatwierdzic = true;

            if (cbbPrzyczyna.SelectedIndex > -1)
            {
                brdPrzyczyna.Visibility = Visibility.Hidden;
            }
            else
            {
                brdPrzyczyna.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtIlosc.Text.ToString() != "")
            {
                brdIlosc.Visibility = Visibility.Hidden;
            }
            else
            {
                brdIlosc.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (moznaZatwierdzic) e.CanExecute = true;
        }

        private void Zatwierdz(object sender, ExecutedRoutedEventArgs e)
        {
            switch (akcja)
            {
                case "D":
                    MessageBox.Show("Dodajemy");

                    var row = new meldunki_wady_nn();
                    var rowAktualny = grdDane.DataContext as PrzyczynyBrakowView;
                    row.id = frmMeldunki_db.IdPrzyczynyBraku();
                    row.id_meldunek = 0;
                    row.id_wada_nn = rowAktualny.id_wada_nn;
                    row.ilosc = rowAktualny.ilosc;

                    row.opw = frmLogin.LoggedUser.login;
                    row.czasw = DateTime.Now;
                    row.opm = frmLogin.LoggedUser.login;
                    row.czasm = DateTime.Now;
                    frmMeldunki_db.DodajPrzyczyneBraku(row);
                    break;
                case "P":
                    MessageBox.Show("Poprawiamy");

                    var row2 = new meldunki_wady_nn();
                    var rowAktualny2 = grdDane.DataContext as PrzyczynyBrakowView;
                    row2.id = rowAktualny2.id;
                    row2.id_meldunek = rowAktualny2.id_meldunek;
                    row2.id_wada_nn = rowAktualny2.id_wada_nn;
                    row2.ilosc = rowAktualny2.ilosc;
                    row2.opw = rowAktualny2.opw;
                    row2.czasw = rowAktualny2.czasw;
                    row2.opm = frmLogin.LoggedUser.login;
                    row2.czasm = DateTime.Now;
                    frmMeldunki_db.PoprawPrzyczyneBraku(row2);
                    break;
                default:
                    break;
            }

            dgrdLista.IsEnabled = true;
            grdDane.IsEnabled = false;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            btnZamknij.IsEnabled = true;

            listaPrzyczynBrakow = frmMeldunki_db.PobierzPrzyczynyBrakow(idMeldunku);
            dgrdLista.ItemsSource = listaPrzyczynBrakow;
            dgrdLista.SelectedIndex = dgBookmark;
        }
    }
}
