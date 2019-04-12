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
using ecoplastol.planowanie;

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for MaszynaZlecenie.xaml
    /// </summary>
    public partial class MaszynaZlecenie : UserControl
    {
        public static event RefreshDataDelegate RefreshData;
        public static event UstawDateZleceniaDelegate UstawDateZlecenia;

        public MaszynaZlecenie(zlecenia_produkcyjne zp)
        {
            InitializeComponent();
            grdZlecenie.DataContext = zp;

            switch (zp.wyrob_typ)
            {
                //elektrooporowa
                case 0:
                    grdZlecenie.Background = (SolidColorBrush)FindResource("panelZlecenieE");
                    break;
                //doczołowa
                case 1:
                    grdZlecenie.Background = (SolidColorBrush)FindResource("panelZlecenieD");
                    break;
                //zawór
                case 2:
                    grdZlecenie.Background = (SolidColorBrush)FindResource("panelZlecenieZ");
                    break;
                //adapter
                case 3:
                    grdZlecenie.Background = (SolidColorBrush)FindResource("panelZlecenieA");
                    break;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(label1.Content.ToString());
            
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            frmZlecenieProdukcji frmZlecenieProdukcji = new frmZlecenieProdukcji(this.grdZlecenie.DataContext as zlecenia_produkcyjne);
            frmZlecenieProdukcji.ShowDialog();
            //if (frmZlecenieProdukcji.DialogResult.HasValue && frmZlecenieProdukcji.DialogResult.Value)
            //    RefreshData?.Invoke();
            //;
            RefreshData?.Invoke();
            //this.grdZlecenie.DataContext;
        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {
            var Res = MessageBox.Show("Usunąć ?", "Usuwanie pozycji", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (Res == MessageBoxResult.Yes)
            {
                var zp = this.grdZlecenie.DataContext as zlecenia_produkcyjne;
                frmZlecenieProdukcji_db.UsunZlecenie(zp);
                RefreshData?.Invoke();
            }

            
        }

        private void BtnMeldunki_Click(object sender, RoutedEventArgs e)
        {

            UstawDateZlecenia?.Invoke();
            frmMeldunki frmMeldunki = new frmMeldunki(frmPlanowanie2.dataZlecenia, Convert.ToInt32(btnMeldunki.Tag));
            frmMeldunki.ShowDialog();
        }
    }
}
