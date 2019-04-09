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

namespace ecoplastol
{
    /// <summary>
    /// Interaction logic for MaszynaZlecenie.xaml
    /// </summary>
    public partial class MaszynaZlecenie : UserControl
    {
        public MaszynaZlecenie(zlecenia_produkcyjne zp)
        {
            InitializeComponent();
            grdZlecenie.DataContext = zp;
            //label1.Content = labelText;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(label1.Content.ToString());
            
        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            
            //this.grdZlecenie.DataContext;
        }
    }
}
