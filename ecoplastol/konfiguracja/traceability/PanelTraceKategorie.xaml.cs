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

namespace ecoplastol.konfiguracja.traceability
{
    /// <summary>
    /// Interaction logic for PanelTraceKategorie.xaml
    /// </summary>
    public partial class PanelTraceKategorie : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private trace_kategoria rowTraceKategoria;
        private List<trace_kategoria> listTraceKategoria;

        public PanelTraceKategorie(List<trace_kategoria> lista)
        {
            InitializeComponent();
            listTraceKategoria = lista;
            grdLista.ItemsSource = listTraceKategoria;
            if (lista.Count == 0)
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

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rowTraceKategoria = grdLista.SelectedItem as trace_kategoria;
            grdPozycje.DataContext = rowTraceKategoria;
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

            trace_kategoria poz = new trace_kategoria();
            grdPozycje.DataContext = poz;
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

            trace_kategoria poz = new trace_kategoria();
            poz.parametr = rowTraceKategoria.parametr;
            poz.wartosc = rowTraceKategoria.wartosc;
            poz.opis = rowTraceKategoria.opis;
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
                PanelTrace_db.UsunTraceKategorie(rowTraceKategoria);
                listTraceKategoria = frmWyroby_db.PobierzTraceKategorie();
                grdLista.ItemsSource = listTraceKategoria;
            }
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            grdBookmark = grdLista.SelectedIndex;
            grdLista.IsEnabled = true;
            grdPozycje.IsEnabled = false;
            btnDodaj.IsEnabled = true;
            btnKlonuj.IsEnabled = true;
            btnPopraw.IsEnabled = true;
            btnUsun.IsEnabled = true;
            btnAnuluj.IsEnabled = false;
            btnZatwierdz.IsEnabled = false;

            listTraceKategoria = frmWyroby_db.PobierzTraceKategorie();
            grdLista.ItemsSource = listTraceKategoria;
            
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
                    if (grdPozycje.DataContext is trace_kategoria)
                    {
                        var row = new trace_kategoria();
                        row = grdPozycje.DataContext as trace_kategoria;
                        row.id = PanelTrace_db.IdTraceKategorie();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelTrace_db.DodajTraceKategorie(row);
                    }
                    break;
                case "P":
                    rowTraceKategoria.opm = frmLogin.LoggedUser.login;
                    rowTraceKategoria.czasm = DateTime.Now;
                    PanelTrace_db.PoprawTraceKategorie(rowTraceKategoria);
                    break;
                default:
                    break;
            }
            listTraceKategoria = frmWyroby_db.PobierzTraceKategorie();
            grdLista.ItemsSource = listTraceKategoria;
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
    }
}
