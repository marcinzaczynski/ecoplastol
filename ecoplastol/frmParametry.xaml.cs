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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmParametry.xaml
    /// </summary>
    public partial class frmParametry : Window
    {
        private List<parametry> listP;
        private int _id_upd;
        private string _akcja;

        public frmParametry()
        {
            InitializeComponent();

        }

        private void TabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    listP = frmParametry_db.GetParameters("ADMINISTRACJA");
                    break;
                case 1:
                    listP = frmParametry_db.GetParameters("SIEC");
                    break;
                case 2:
                    listP = frmParametry_db.GetParameters("ITF");
                    break;
                case 3:
                    listP = frmParametry_db.GetParameters("TRACEABILITY");
                    break;
                case 4:
                    listP = frmParametry_db.GetParameters("WYROB");
                    break;
            }
            dgParametry.ItemsSource = listP;

            if (listP.Count == 0)
                UstawPrzyciski(0);
            else
                UstawPrzyciski(1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource parametersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("parametersViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // parametersViewSource.Source = [generic data source]
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

        private void DgParametry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var row = dgParametry.SelectedRows[0].DataBoundItem as parameters;
            try
            {
                var row = dgParametry.SelectedItem as parametry;
                _id_upd = row.id;
                txtKategoria.Text = row.kategoria;
                txtGrupa.Text = row.grupa;
                txtIndeks.Text = row.indeks.ToString();
                txtParametr.Text = row.parametr;
                txtWartosc.Text = row.wartosc;
                txtOpis.Text = row.opis;
            }
            catch
            {
                txtKategoria.Text = "";
                txtGrupa.Text = "";
                txtIndeks.Text = "";
                txtParametr.Text = "";
                txtWartosc.Text = "";
                txtOpis.Text = "";
            }
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            _akcja = "D";
            TabControl1.IsEnabled = false;
            dgParametry.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            txtGrupa.IsEnabled = true;
            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    txtKategoria.Text = "ADMINISTRACJA";
                    break;
                case 1:
                    txtKategoria.Text = "SIEC";
                    break;
                case 2:
                    txtKategoria.Text = "ITF";
                    break;
                case 3:
                    txtKategoria.Text = "TRACEABILITY";
                    break;
                case 4:
                    txtKategoria.Text = "WYROB";
                    break;
            }
            txtGrupa.Text = "";
            txtIndeks.Text = "";
            txtParametr.Text = "";
            txtWartosc.Text = "";
            txtOpis.Text = "";
        }

        private void BtnKlonuj_Click(object sender, RoutedEventArgs e)
        {
            _akcja = "K";
            TabControl1.IsEnabled = false;
            dgParametry.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            txtGrupa.IsEnabled = true;
            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            _akcja = "P";
            TabControl1.IsEnabled = false;
            dgParametry.IsEnabled = false;
            btnDodaj.IsEnabled = false;
            btnKlonuj.IsEnabled = false;
            btnPopraw.IsEnabled = false;
            btnUsun.IsEnabled = false;
            btnAnuluj.IsEnabled = true;
            btnZatwierdz.IsEnabled = true;
            btnZamknij.IsEnabled = false;

            txtGrupa.IsEnabled = true;
            txtIndeks.IsEnabled = true;
            txtParametr.IsEnabled = true;
            txtWartosc.IsEnabled = true;
            txtOpis.IsEnabled = true;
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            TabControl1.IsEnabled = true;
            dgParametry.IsEnabled = true;
            btnZamknij.IsEnabled = true;
            DgParametry_SelectionChanged(null, null);

            if (listP.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                UstawPrzyciski(1);
            }


            txtGrupa.IsEnabled = false;
            txtIndeks.IsEnabled = false;
            txtParametr.IsEnabled = false;
            txtWartosc.IsEnabled = false;
            txtOpis.IsEnabled = false;
        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            var newParameter = new parametry();
            newParameter.kategoria = txtKategoria.Text;
            newParameter.grupa = txtGrupa.Text;
            newParameter.indeks = Int32.Parse(txtIndeks.Text);
            newParameter.parametr = txtParametr.Text;
            newParameter.wartosc = txtWartosc.Text;
            newParameter.opis = txtOpis.Text;
            if (_akcja == "D")
            {

                newParameter.opw = frmLogin.LoggedUser.login;
                newParameter.czasw = DateTime.Now;
                newParameter.opm = frmLogin.LoggedUser.login;
                newParameter.czasm = DateTime.Now;
                frmParametry_db.AddParameter(newParameter);
                TabControl1_SelectionChanged(null, null);

            }
            else if (_akcja == "K")
            {
                newParameter.opw = frmLogin.LoggedUser.login;
                newParameter.czasw = DateTime.Now;
                newParameter.opm = frmLogin.LoggedUser.login;
                newParameter.czasm = DateTime.Now;
                frmParametry_db.AddParameter(newParameter);
                TabControl1_SelectionChanged(null, null);
            }
            else if (_akcja == "P")
            {
                var row = dgParametry.SelectedItem as parametry;
                newParameter.id = row.id;
                newParameter.opw = row.opw;
                newParameter.czasw = row.czasw;
                newParameter.opm = frmLogin.LoggedUser.login;
                newParameter.czasm = DateTime.Now;
                try
                {
                    frmParametry_db.UpdParameter2(newParameter);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException is SqlException sqlex)
                    {
                        if (sqlex.Number == 2601)
                        {
                            MessageBox.Show(sqlex.Message);
                            return;
                        }
                    }

                    MessageBox.Show(ex.Message);
                }

                TabControl1_SelectionChanged(null, null);
            }
            TabControl1.IsEnabled = true;
            dgParametry.IsEnabled = true;
            btnZamknij.IsEnabled = true;
            if (listP.Count == 0)
            {
                UstawPrzyciski(0);
            }
            else
            {
                UstawPrzyciski(1);
            }

            txtGrupa.IsEnabled = false;
            txtIndeks.IsEnabled = false;
            txtParametr.IsEnabled = false;
            txtWartosc.IsEnabled = false;
            txtOpis.IsEnabled = false;
        }


    }
}
