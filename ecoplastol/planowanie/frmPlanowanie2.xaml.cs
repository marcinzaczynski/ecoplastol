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

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for frmPlanowanie2.xaml
    /// </summary>
    /// 

    public partial class frmPlanowanie2 : Window
    {
        private List<maszyny> listaMaszyn;
        //private List<zlecenia_produkcyjne> listaZleceniaProdukcyjne;
        public static List<MaszynaPanel> listaPaneli;
        private int _ilKolumn = 3;
        private int _ilWierszy;

        public frmPlanowanie2()
        {
            InitializeComponent();
            dpZleceniaNaDzien.SelectedDate = DateTime.Now.Date;
            listaPaneli = new List<MaszynaPanel>();
            UstawPanele();
        }

        private void UstawPanele()
        {
            listaMaszyn = frmPlanowanie2_db.PobierzMaszyny();
            double ilMaszyn = listaMaszyn.Count;
            double il_w = ilMaszyn / _ilKolumn;
            _ilWierszy = Convert.ToInt32(Math.Ceiling(il_w));

            for (int i = 0; i < _ilWierszy; i++)
            {
                grdMaszyny.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _ilKolumn; i++)
            {
                grdMaszyny.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int licznik = 0;

            //List<MaszynaPanel> listaPaneli = new List<MaszynaPanel>();
            for (int i = 0; i < _ilWierszy; i++)
            {
                for (int j = 0; j < _ilKolumn; j++)
                {
                    if (licznik == ilMaszyn) { break; } // grid może mieć więcej komórek niż jest maszyn
                    MaszynaPanel mp = new MaszynaPanel(listaMaszyn[licznik].id, listaMaszyn[licznik].numer);
                    //mp.Name = "panelM" + Convert.ToChar(listaMaszyn[licznik].id + 1);
                    //mp.Tag = licznik + 1;
                    listaPaneli.Add(mp);
                    Grid.SetRow(mp, i);
                    Grid.SetColumn(mp, j);
                    grdMaszyny.Children.Add(mp);
                    licznik++;
                }
            }
        }

        private void UzupełnijPaneleZleceniami()
        {
            var paneleMaszyn = this.grdMaszyny.Children.OfType<MaszynaPanel>();
            foreach (var item in paneleMaszyn)
            {
                var mp = item as MaszynaPanel;
                // ... i dla każdego panela szukam w zleceń w bazie
                var listaZleceniaProdukcyjne = new List<zlecenia_produkcyjne>();
                listaZleceniaProdukcyjne = frmPlanowanie2_db.PobierzZleceniaDlaMaszyny(Convert.ToInt32(mp.Tag), dpZleceniaNaDzien.SelectedDate);
                // panelZlecen - miejsce w PanelMaszyna do którego lądują MaszynaZlecenie
                var panelZlecen = mp.spMaszynaZlecenia;
                // najpierw trzeba wyczyścić MaszynaPanel ...
                panelZlecen.Children.Clear();
                // teraz przelatuję przez zlecenia dla danej maszyny
                foreach (var zp in listaZleceniaProdukcyjne)
                {
                    // ... utworzyć nowe MaszynZlecenie ...
                    MaszynaZlecenie mz = new MaszynaZlecenie(zp);
                    // ... i dodać je do MaszynaPanel w odpowiednim miejscu - panelZlecen
                    panelZlecen.Children.Add(mz);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UzupełnijPaneleZleceniami();
            
            //MessageBox.Show(listaPaneli.Count.ToString());

            //var paneleMaszyn = this.grdMaszyny.Children.OfType<MaszynaPanel>();
            //foreach (var item in paneleMaszyn)
            //{
            //    var mp = item as MaszynaPanel;
            //    if (Convert.ToInt32(mp.Tag) == 5)
            //    {
            //        mp.lblNazwaMaszyny.Content = "toto";
            //        var aa = mp.grdMaszynaPanel;
            //        aa.RowDefinitions.Add(new RowDefinition());
            //        MaszynaZlecenie mz = new MaszynaZlecenie("aaa");
            //        Grid.SetRow(mz, 2);
            //        aa.Children.Add(mz);
            //    }
            //}
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DpZleceniaNaDzien_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UzupełnijPaneleZleceniami();
            //string msg;
            //msg = dpZleceniaNaDzien.SelectedDate.ToString();
            //MessageBox.Show(msg);

            //listaZleceniaProdukcyjne = new List<zlecenia_produkcyjne>();
            //listaZleceniaProdukcyjne = frmPlanowanie2_db.PobierzZleceniaDlaMaszyny(2, dpZleceniaNaDzien.SelectedDate);

            // przelatuję przez wszystkie panele ....

            //var paneleMaszyn = this.grdMaszyny.Children.OfType<MaszynaPanel>();
            //foreach (var item in paneleMaszyn)
            //{
            //    var mp = item as MaszynaPanel;
            //    // ... i dla każdego panela szukam w zleceń w bazie
            //    var listaZleceniaProdukcyjne = new List<zlecenia_produkcyjne>();
            //    listaZleceniaProdukcyjne = frmPlanowanie2_db.PobierzZleceniaDlaMaszyny(Convert.ToInt32(mp.Tag), dpZleceniaNaDzien.SelectedDate);

            //    // teraz przelatuję przez zlecenia dla danej maszyny

            //    foreach (var zp in listaZleceniaProdukcyjne)
            //    {
            //        // panelZleceń - miejsce w PanelMaszyna do którego lądują MaszynaZlecenie
            //        var panelZlecen = mp.spMaszynaZlecenia;
            //        MaszynaZlecenie mz = new MaszynaZlecenie(zp);
            //        panelZlecen.Children.Add(mz);
            //    }

            //}



        }
    }
}
