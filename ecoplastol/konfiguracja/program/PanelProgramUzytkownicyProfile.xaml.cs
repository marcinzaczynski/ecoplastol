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
    /// Interaction logic for PanelProgramUzytkownicyProfile.xaml
    /// </summary>
    public partial class PanelProgramUzytkownicyProfile : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private uzytkownicy_profile rowProfil;
        private List<uzytkownicy_profile> listProfile;

        public PanelProgramUzytkownicyProfile()
        {
            InitializeComponent();
            listProfile = PanelProgramUzytkownicyProfile_db.PobierzProfile();
            grdLista.ItemsSource = listProfile;

            if (listProfile.Count == 0)
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

            grdPozycje.DataContext = new uzytkownicy_profile();
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
                var poz = grdLista.SelectedItem as uzytkownicy_profile;
                PanelProgramUzytkownicyProfile_db.UsunProfil(poz);
                listProfile = PanelProgramUzytkownicyProfile_db.PobierzProfile();
                grdLista.ItemsSource = listProfile;
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

            listProfile = PanelProgramUzytkownicyProfile_db.PobierzProfile();
            grdLista.ItemsSource = listProfile;

            grdLista.SelectedIndex = grdBookmark;
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
                    var row = new uzytkownicy_profile();
                    row = grdPozycje.DataContext as uzytkownicy_profile;
                    row.id = PanelProgramUzytkownicyProfile_db.IdProfilu();
                    row.opw = frmLogin.LoggedUser.login;
                    row.czasw = DateTime.Now;
                    row.opm = frmLogin.LoggedUser.login;
                    row.czasm = DateTime.Now;
                    PanelProgramUzytkownicyProfile_db.DodajProfil(row);
                    break;
                case "P":
                    rowProfil.opm = frmLogin.LoggedUser.login;
                    rowProfil.czasm = DateTime.Now;
                    PanelProgramUzytkownicyProfile_db.PoprawProfil(rowProfil);

                    break;
                default:
                    break;
            }

            listProfile = PanelProgramUzytkownicyProfile_db.PobierzProfile();
            grdLista.ItemsSource = listProfile;
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowProfil = grdLista.SelectedItem as uzytkownicy_profile;
            grdPozycje.DataContext = rowProfil;
        }

        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {
            bool moznaZatwierdzic = true;

            if (txtProfil.Text.ToString() != "")
            {
                brdProfil.Visibility = Visibility.Hidden;
            }
            else
            {
                brdProfil.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtOpis.Text.ToString() != "")
            {
                brdOpis.Visibility = Visibility.Hidden;
            }
            else
            {
                brdOpis.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (moznaZatwierdzic) e.CanExecute = true;
        }
    }
}
