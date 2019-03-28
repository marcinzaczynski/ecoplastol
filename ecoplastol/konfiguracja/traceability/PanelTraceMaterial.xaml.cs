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
    /// Interaction logic for PanelTraceMaterial.xaml
    /// </summary>
    public partial class PanelTraceMaterial : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private trace_material rowTraceMaterial;
        private List<trace_material> listTraceMaterial;

        public PanelTraceMaterial(List<trace_material> lista)
        {
            InitializeComponent();
            listTraceMaterial = lista;
            grdLista.ItemsSource = listTraceMaterial;
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
            rowTraceMaterial = grdLista.SelectedItem as trace_material;
            grdPozycje.DataContext = rowTraceMaterial;
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

            trace_material poz = new trace_material();
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

            trace_material poz = new trace_material();
            poz.parametr = rowTraceMaterial.parametr;
            poz.wartosc = rowTraceMaterial.wartosc;
            poz.opis = rowTraceMaterial.opis;
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

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {

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
