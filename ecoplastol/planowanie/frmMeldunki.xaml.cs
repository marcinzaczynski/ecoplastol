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

namespace ecoplastol.planowanie
{
    /// <summary>
    /// Interaction logic for frmMeldunki.xaml
    /// </summary>
    public partial class frmMeldunki : Window
    {
        private List<maszyny> listaMaszyn;
        private List<zmiany> listaZmian;
        private List<zlecenia_produkcyjne> listaZlecen;
        private List<operatorzy_maszyn> listaOperatorow;
        private List<wady_nn> listaWadNN;
        private List<meldunki_wynik> listaWynikSprWtr;
        private List<meldunki_wynik> listaWygladZew;
        private List<meldunki_wynik> listaWygladGrzejnika;
        private List<meldunki_wynik_prz_maszyny> listaWynikPrzMasz;

        private List<MeldunekView> listaMeldunkow;
        private MeldunekView rowMeldunek;

        private int nrMaszyny;
        private DateTime? dataZleceniaOd;
        private DateTime? dataZleceniaDo;
        private int idZlecenia;

        private int dgBookmark;

        private string akcja;

        public frmMeldunki(object sender, DateTime data, int maszyna, int zlecenie)
        {

            InitializeComponent();

            try
            {
                if (sender is Button)
                {
                    if (((Button)sender).Name == "btnMeldunki")
                    {
                        // Otwarte przez przycisk meldunki

                        UstawPrzyciski1();
                    }
                    else
                    {
                        MessageBox.Show("Otwarte przez jakiś przycisk, ale nie meldunki");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                throw;
            }
            nrMaszyny = maszyna;
            dataZleceniaOd = data;
            dataZleceniaDo = data;
            idZlecenia = zlecenie;
            dpDataZleceniaOd.SelectedDate = data;
            dpDataZleceniaDo.SelectedDate = data;
            dpMeldunekData.SelectedDate = DateTime.Now;

            listaMaszyn = produkcja_db.PobierzMaszyny();
            cbbMaszyna.ItemsSource = listaMaszyn;
            cbbMaszyna.SelectedValuePath = "id";
            cbbMaszyna.DisplayMemberPath = "numer";
            cbbMaszyna.SelectedValue = nrMaszyny;

            listaZmian = produkcja_db.PobierzZmiany();
            cbbZmiana.ItemsSource = listaZmian;
            cbbZmiana.SelectedValuePath = "id";
            cbbZmiana.DisplayMemberPath = "nazwa";

            cbbMeldunekZmiana.ItemsSource = listaZmian;
            cbbMeldunekZmiana.SelectedValuePath = "id";
            cbbMeldunekZmiana.DisplayMemberPath = "nazwa";

            cbbZlecenie.SelectedValue = idZlecenia;

            listaOperatorow = produkcja_db.PobierzOperatorow();
            cbbMeldunekOperator.ItemsSource = listaOperatorow;
            cbbMeldunekOperator.SelectedValuePath = "id";

            listaWadNN = produkcja_db.PobierzWadyNN();

            listaWynikSprWtr = frmMeldunki_db.PobierzWynikiDlaMeldunki();
            cbbMeldunekWynikSprWtr.ItemsSource = listaWynikSprWtr;
            cbbMeldunekWynikSprWtr.SelectedValuePath = "id";
            cbbMeldunekWynikSprWtr.DisplayMemberPath = "wynik";

            listaWygladZew = frmMeldunki_db.PobierzWynikiDlaMeldunki();
            cbbMeldunekWygladZewnetrzny.ItemsSource = listaWygladZew;
            cbbMeldunekWygladZewnetrzny.SelectedValuePath = "id";
            cbbMeldunekWygladZewnetrzny.DisplayMemberPath = "wynik";

            listaWygladGrzejnika = frmMeldunki_db.PobierzWynikiDlaMeldunki();
            cbbMeldunekWygladGrzejnika.ItemsSource = listaWygladGrzejnika;
            cbbMeldunekWygladGrzejnika.SelectedValuePath = "id";
            cbbMeldunekWygladGrzejnika.DisplayMemberPath = "wynik";

            listaWynikPrzMasz = frmMeldunki_db.PobierzWynikiPrzMasz();
            cbbMeldunekPrzCodzMasz.ItemsSource = listaWynikPrzMasz;
            cbbMeldunekPrzCodzMasz.SelectedValuePath = "id";
            cbbMeldunekPrzCodzMasz.DisplayMemberPath = "wynik";

            listaMeldunkow = frmMeldunki_db.PobierzMeldunki2(((zlecenia_produkcyjne)cbbZlecenie.SelectedItem).id);
            dgrdMeldunki.ItemsSource = listaMeldunkow;

            if (listaMeldunkow.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                dgrdMeldunki.Focus();
                dgrdMeldunki.SelectedIndex = 0;

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

        private void UstawPrzyciski1()
        {
            //dpDataZleceniaOd.IsEnabled = false;
            //dpDataZleceniaDo.IsEnabled = false;
            //cbbMaszyna.IsEnabled = false;
            //cbbZlecenie.IsEnabled = false;

            //dpMeldunekData.IsEnabled = false;
            //cbbMeldunekMaszyna.IsEnabled = false;
            //cbbMeldunekZlecenie.IsEnabled = false;
            //cbbMeldunekZmiana.IsEnabled = false;
        }

        private void WyszukajZlecenia()
        {
            zlecenia_produkcyjne zp = new zlecenia_produkcyjne();
            List<zlecenia_produkcyjne> lzp = new List<zlecenia_produkcyjne>();
            lzp.Add(zp);
            listaZlecen = frmMeldunki_db.PobierzZleceniaDlaMaszynyOdDo(nrMaszyny, dataZleceniaOd, dataZleceniaDo);
            listaZlecen.InsertRange(0, lzp);
            cbbZlecenie.ItemsSource = listaZlecen;
            cbbZlecenie.SelectedValuePath = "id";
            cbbZlecenie.SelectedIndex = 0;
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DpDataZleceniaOd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dataZleceniaOd = dpDataZleceniaOd.SelectedDate;
            WyszukajZlecenia();
        }

        private void DpDataZleceniaDo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dataZleceniaDo = dpDataZleceniaDo.SelectedDate;
            WyszukajZlecenia();
        }

        private void CbbMaszyna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbbMaszyna.SelectedItem as maszyny;
            nrMaszyny = item.id;
            WyszukajZlecenia();
        }

        private void CbbZlecenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbZlecenie.SelectedItem != null)
            {
                listaMeldunkow = frmMeldunki_db.PobierzMeldunki2(((zlecenia_produkcyjne)cbbZlecenie.SelectedItem).id);
            }
            else
            {
                listaMeldunkow = frmMeldunki_db.PobierzMeldunki2(0);
            }
            dgrdMeldunki.ItemsSource = listaMeldunkow;
            dgrdMeldunki.Focus();
            dgrdMeldunki.SelectedIndex = 0;
        }

        private void WyszukajMeldunki()
        {
            // muszę wyszukać meldunki na podstawie parametrów :
            // data
            // maszyna
            // zlecenie
            // zmiana




        }

        private void CbbZmiana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbMeldunekZmiana.SelectedIndex = cbbZmiana.SelectedIndex;
        }

        private void BtnWadyNN_Click(object sender, RoutedEventArgs e)
        {
            int idMeld;
            switch (akcja)
            {
                case "D":
                case "K":
                    // przy dodawaniu i otwieraniu otwieram z indeksem meldunku 0, a potem przy zatwierdzaniu poprawię na poprawny
                    idMeld = 0;
                    break;
                case "P":
                    // przy poprawianiu otwieram z aktualnym numerem meldunku
                    idMeld = ((MeldunekView)grdDane.DataContext).id;
                    break;
                default:
                    idMeld = 0;
                    break;
            }

            frmMeldunkiWadyNN frmMeldunkiWadyNN = new frmMeldunkiWadyNN(dpMeldunekData.SelectedDate.Value,
                                                                        cbbMaszyna.SelectedIndex,
                                                                        ((zlecenia_produkcyjne)cbbZlecenie.SelectedItem).id,
                                                                        ((zmiany)cbbMeldunekZmiana.SelectedItem).id,
                                                                        ((operatorzy_maszyn)cbbMeldunekOperator.SelectedItem).id,
                                                                        idMeld);
            frmMeldunkiWadyNN.ShowDialog();
        }



        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            akcja = "D";

            rowMeldunek = new MeldunekView();

            rowMeldunek.data_meldunku = DateTime.Now;
            rowMeldunek.id_zlecenie = ((zlecenia_produkcyjne)cbbZlecenie.SelectedItem).id;
            grdDane.DataContext = rowMeldunek;

            dpDataZleceniaOd.IsEnabled = false;
            dpDataZleceniaDo.IsEnabled = false;
            cbbMaszyna.IsEnabled = false;
            cbbZlecenie.IsEnabled = false;
            cbbZmiana.IsEnabled = false;
            dgrdMeldunki.IsEnabled = false;
            btnZamknij.IsEnabled = false;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            grdDane.IsEnabled = true;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            akcja = "P";
            dgBookmark = dgrdMeldunki.SelectedIndex;
            dpDataZleceniaOd.IsEnabled = false;
            dpDataZleceniaDo.IsEnabled = false;
            cbbMaszyna.IsEnabled = false;
            cbbZlecenie.IsEnabled = false;
            cbbZmiana.IsEnabled = false;
            dgrdMeldunki.IsEnabled = false;
            btnZamknij.IsEnabled = false;

            btnDodaj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;

            grdDane.IsEnabled = true;
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            dpDataZleceniaOd.IsEnabled = true;
            dpDataZleceniaDo.IsEnabled = true;
            cbbMaszyna.IsEnabled = true;
            cbbZlecenie.IsEnabled = true;
            cbbZmiana.IsEnabled = true;
            dgrdMeldunki.IsEnabled = true;
            btnZamknij.IsEnabled = true;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            grdDane.IsEnabled = false;

            frmMeldunki_db.UsunPrzyczynyBrakow();
        }


        private void CzyMoznaZatwierdzic(object sender, CanExecuteRoutedEventArgs e)
        {
            bool moznaZatwierdzic = true;

            if (dpMeldunekData.SelectedDate.HasValue)
            {
                brdMeldunekData.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekData.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekZmiana.SelectedIndex > 0)
            {
                brdMeldunekZmiana.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekZmiana.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekOperator.SelectedIndex > 0)
            {
                brdMeldunekOperator.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekOperator.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekPrzCodzMasz.SelectedIndex > 0)
            {
                brdMeldunekPrzCodzMasz.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekPrzCodzMasz.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtMeldunekIloscPoz.Text.ToString() != "")
            {
                brdMeldunekIloscPoz.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekIloscPoz.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (txtMeldunekIloscTechn.Text.ToString() != "")
            {
                brdMeldunekIloscTechn.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekIloscTechn.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekWynikSprWtr.SelectedIndex > 0)
            {
                brdMeldunekWynikSprWtr.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekWynikSprWtr.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekWygladZewnetrzny.SelectedIndex > 0)
            {
                brdMeldunekWygladZewnetrzny.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekWygladZewnetrzny.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (cbbMeldunekWygladGrzejnika.SelectedIndex > 0)
            {
                brdMeldunekWygladGrzejnika.Visibility = Visibility.Hidden;
            }
            else
            {
                brdMeldunekWygladGrzejnika.Visibility = Visibility.Visible;
                moznaZatwierdzic = false;
            }

            if (moznaZatwierdzic) e.CanExecute = true;
        }

        private void Zatwierdz(object sender, ExecutedRoutedEventArgs e)
        {
            switch (akcja)
            {
                case "D":
                case "K":
                    //if (dgrdMeldunki.DataContext is meldunki)
                    {
                        var row = new meldunki();
                        var rowAktualny = grdDane.DataContext as MeldunekView;
                        row.id = frmMeldunki_db.IdMeldunki();
                        row.id_zlecenie = rowAktualny.id_zlecenie;
                        row.id_operator = rowAktualny.id_operator;
                        row.zmiana = rowAktualny.zmiana;
                        row.data_meldunku = rowAktualny.data_meldunku;
                        row.ilosc = rowAktualny.ilosc;
                        row.ilosc_techn = rowAktualny.ilosc_techn;
                        row.id_wady_nn = rowAktualny.id_wady_nn;
                        row.godz_spr_wtr = rowAktualny.godz_spr_wtr;
                        row.wynik_spr_wtr = rowAktualny.wynik_spr_wtr;
                        row.wyglad_zew = rowAktualny.wyglad_zew;
                        row.wyglad_grzejnika = rowAktualny.wyglad_grzejnika;
                        row.przeglad_codz_masz = rowAktualny.przeglad_codz_masz;

                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        frmMeldunki_db.DodajMeldunek(row);
                        frmMeldunki_db.PoprawIDPrzyczynyBrakow(row.id);
                    }
                    break;
                case "P":
                    var row2 = new meldunki();
                    var rowAktualny2 = grdDane.DataContext as MeldunekView;
                    row2.id = rowAktualny2.id;
                    row2.id_zlecenie = rowAktualny2.id_zlecenie;
                    row2.id_operator = rowAktualny2.id_operator;
                    row2.zmiana = rowAktualny2.zmiana;
                    row2.data_meldunku = rowAktualny2.data_meldunku;
                    row2.ilosc = rowAktualny2.ilosc;
                    row2.ilosc_techn = rowAktualny2.ilosc_techn;
                    row2.id_wady_nn = rowAktualny2.id_wady_nn;
                    row2.godz_spr_wtr = rowAktualny2.godz_spr_wtr;
                    row2.wynik_spr_wtr = rowAktualny2.wynik_spr_wtr;
                    row2.wyglad_zew = rowAktualny2.wyglad_zew;
                    row2.wyglad_grzejnika = rowAktualny2.wyglad_grzejnika;
                    row2.przeglad_codz_masz = rowAktualny2.przeglad_codz_masz;
                    row2.opw = rowAktualny2.opw;
                    row2.czasw = rowAktualny2.czasw;
                    row2.opm = frmLogin.LoggedUser.login;
                    row2.czasm = DateTime.Now;
                    frmMeldunki_db.PoprawMeldunek(row2);
                    frmMeldunki_db.PoprawIDPrzyczynyBrakow(row2.id);
                    break;
                default:
                    break;
            }
            //listWyroby = produkcja_db.PobierzWyroby();
            //grdLista.ItemsSource = listWyroby;
            //grdLista.SelectedIndex = grdBookmark;
            //grdLista.Focus();
            dpDataZleceniaOd.IsEnabled = true;
            dpDataZleceniaDo.IsEnabled = true;
            cbbMaszyna.IsEnabled = true;
            cbbZlecenie.IsEnabled = true;
            cbbZmiana.IsEnabled = true;
            dgrdMeldunki.IsEnabled = true;
            btnZamknij.IsEnabled = true;

            btnDodaj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            grdDane.IsEnabled = false;

            listaMeldunkow = frmMeldunki_db.PobierzMeldunki2(((zlecenia_produkcyjne)cbbZlecenie.SelectedItem).id);
            dgrdMeldunki.ItemsSource = listaMeldunkow;
            dgrdMeldunki.SelectedIndex = dgBookmark;
            //MessageBox.Show("Zatwierdzamy.");
        }

        private void TxtMeldunekIloscPoz_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void TxtMeldunekIloscTechn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            Regex _regex = new Regex("[^0-9]+");
            e.Handled = _regex.IsMatch(str);
        }

        private void DgrdMeldunki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowMeldunek = dgrdMeldunki.SelectedItem as MeldunekView;

            grdDane.DataContext = rowMeldunek;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (btnAnuluj.IsEnabled)
            {
                BtnAnuluj_Click(null, null);
            }
        }
    }
}
