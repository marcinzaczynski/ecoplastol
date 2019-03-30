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
    /// Interaction logic for PanelTraceProducenci.xaml
    /// </summary>
    public partial class PanelTraceProducenci : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private trace_producent rowTraceProducent;
        private List<trace_producent> listTraceProducent;

        public PanelTraceProducenci(List<trace_producent> lista)
        {
            InitializeComponent();
            listTraceProducent = lista;
            grdLista.ItemsSource = listTraceProducent;
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
            rowTraceProducent = grdLista.SelectedItem as trace_producent;
            grdPozycje.DataContext = rowTraceProducent;
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

            trace_producent poz = new trace_producent();
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

            trace_producent poz = new trace_producent();
            poz.parametr = rowTraceProducent.parametr;
            poz.wartosc = rowTraceProducent.wartosc;
            poz.opis = rowTraceProducent.opis;
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
                PanelTrace_db.UsunTraceProducent(rowTraceProducent);
                listTraceProducent = frmWyroby_db.PobierzTraceProducent();
                grdLista.ItemsSource = listTraceProducent;
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
            listTraceProducent = frmWyroby_db.PobierzTraceProducent();
            grdLista.ItemsSource = listTraceProducent;
            
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
                    if (grdPozycje.DataContext is trace_producent)
                    {
                        var row = new trace_producent();
                        row = grdPozycje.DataContext as trace_producent;
                        row.id = PanelTrace_db.IdTraceProducent();
                        row.opw = frmLogin.LoggedUser.login;
                        row.czasw = DateTime.Now;
                        row.opm = frmLogin.LoggedUser.login;
                        row.czasm = DateTime.Now;
                        PanelTrace_db.DodajTraceProducent(row);
                    }
                    break;
                case "P":
                    rowTraceProducent.opm = frmLogin.LoggedUser.login;
                    rowTraceProducent.czasm = DateTime.Now;
                    PanelTrace_db.PoprawTraceProducent(rowTraceProducent);
                    break;
                default:
                    break;
            }
            listTraceProducent = frmWyroby_db.PobierzTraceProducent();
            grdLista.ItemsSource = listTraceProducent;
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
