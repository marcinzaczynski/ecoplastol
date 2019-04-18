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
        private int nrMaszyny;
        private DateTime? dataZleceniaOd;
        private DateTime? dataZleceniaDo;
        private int idZlecenia;

        public frmMeldunki(DateTime data, int maszyna, int zlecenie)
        {
           
            InitializeComponent();

            nrMaszyny = maszyna;
            dataZleceniaOd = data;
            dataZleceniaDo = data;
            idZlecenia = zlecenie;
            dpDataZleceniaOd.SelectedDate = data;
            dpDataZleceniaDo.SelectedDate = data;

            listaMaszyn = produkcja_db.PobierzMaszyny();
            cbbMaszyna.ItemsSource = listaMaszyn;
            cbbMaszyna.SelectedValuePath = "id";
            cbbMaszyna.DisplayMemberPath = "numer";
            cbbMaszyna.SelectedValue = nrMaszyny;

            listaZmian = produkcja_db.PobierzZmiany();
            cbbZmiana.ItemsSource = listaZmian;
            cbbZmiana.SelectedValuePath = "id";
            cbbZmiana.DisplayMemberPath = "nazwa";

            cbbZlecenie.SelectedValue = idZlecenia;

        }

        private void WyszukajZlecenia()
        {
            listaZlecen = frmMeldunki_db.PobierzZleceniaDlaMaszynyOdDo(nrMaszyny, dataZleceniaOd, dataZleceniaDo);
            cbbZlecenie.ItemsSource = listaZlecen;
            cbbZlecenie.SelectedValuePath = "id";
            cbbZlecenie.SelectedIndex = 0 ;
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

    }
}
