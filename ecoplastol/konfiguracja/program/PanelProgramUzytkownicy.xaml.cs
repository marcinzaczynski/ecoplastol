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

namespace ecoplastol.konfiguracja.program
{
    /// <summary>
    /// Interaction logic for PanelProgramUzytkownicy.xaml
    /// </summary>
    public partial class PanelProgramUzytkownicy : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private UzytkownicyView rowUzytkownik;
        private List<UzytkownicyView> listUzytkownicy;

        public PanelProgramUzytkownicy()
        {
            InitializeComponent();
            listUzytkownicy = PanelProgramUzytkownicy_db.PobierzUzytkownikowView();

            cbbProfil.ItemsSource = PanelProgramUzytkownicyProfile_db.PobierzProfile();
            cbbProfil.SelectedValuePath = "profil";
            grdLista.ItemsSource = listUzytkownicy;
            if (listUzytkownicy.Count == 0)
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

            grdPozycje.DataContext = new UzytkownicyView();
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
                var poz = grdLista.SelectedItem as UzytkownicyView;
                PanelProgramUzytkownicy_db.UsunUzytkownika(poz);
                listUzytkownicy = PanelProgramUzytkownicy_db.PobierzUzytkownikowView();
                grdLista.ItemsSource = listUzytkownicy;
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

            listUzytkownicy = PanelProgramUzytkownicy_db.PobierzUzytkownikowView();
            grdLista.ItemsSource = listUzytkownicy;

            grdLista.SelectedIndex = grdBookmark;
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowUzytkownik = grdLista.SelectedItem as UzytkownicyView;
            grdPozycje.DataContext = rowUzytkownik;
        }
        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {
            bool moznaZatwierdzic = true;

            if (txtImie.Text.ToString() != "")
            {
                brdImie.Visibility = Visibility.Hidden;
            }
            else
            {
                brdImie.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtNazwisko.Text.ToString() != "")
            {
                brdNazwisko.Visibility = Visibility.Hidden;
            }
            else
            {
                brdNazwisko.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbProfil.SelectedIndex >= 0)
            {
                brdProfil.Visibility = Visibility.Hidden;
            }
            else
            {
                brdProfil.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtLogin.Text.ToString() != "")
            {
                brdLogin.Visibility = Visibility.Hidden;
            }
            else
            {
                brdLogin.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtHaslo.Text.ToString() != "")
            {
                brdHaslo.Visibility = Visibility.Hidden;
            }
            else
            {
                brdHaslo.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (moznaZatwierdzic) e.CanExecute = true;
        }

        private void Zatwierdz(object sender, ExecutedRoutedEventArgs e)
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
                    var row = new uzytkownicy();
                    var rowAktualny = grdPozycje.DataContext as UzytkownicyView;
                    row.id = PanelProgramUzytkownicy_db.IdUzytkownika();

                    row.imie = rowAktualny.imie;
                    row.nazwisko = rowAktualny.nazwisko;
                    row.login = rowAktualny.login;
                    row.haslo = rowAktualny.haslo;
                    row.aktywny = rowAktualny.aktywny;
                    row.profil = rowAktualny.profil;
                    row.opw = frmLogin.LoggedUser.login;
                    row.czasw = DateTime.Now;
                    row.opm = frmLogin.LoggedUser.login;
                    row.czasm = DateTime.Now;
                    PanelProgramUzytkownicy_db.DodajUzytkownika(row);
                    break;
                case "P":

                    var row2 = new uzytkownicy();
                    var rowAktualny2 = grdPozycje.DataContext as UzytkownicyView;
                    row2.id = rowAktualny2.id;
                    row2.imie = rowAktualny2.imie;
                    row2.nazwisko = rowAktualny2.nazwisko;
                    row2.login = rowAktualny2.login;
                    row2.haslo = rowAktualny2.haslo;
                    row2.aktywny = rowAktualny2.aktywny;
                    row2.profil = rowAktualny2.profil;
                    row2.opw = rowAktualny2.opw;
                    row2.czasw = rowAktualny2.czasw;
                    row2.opm = frmLogin.LoggedUser.login;
                    row2.czasm = DateTime.Now;
                    PanelProgramUzytkownicy_db.PoprawUzytkownika(row2);
                    break;
                default:
                    break;
            }
            listUzytkownicy = PanelProgramUzytkownicy_db.PobierzUzytkownikowView();
            grdLista.ItemsSource = listUzytkownicy;
        }
    }
}
