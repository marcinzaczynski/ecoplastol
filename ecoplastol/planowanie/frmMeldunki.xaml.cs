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

namespace ecoplastol.planowanie
{
    /// <summary>
    /// Interaction logic for frmMeldunki.xaml
    /// </summary>
    public partial class frmMeldunki : Window
    {
        public frmMeldunki(DateTime data, int maszyna)
        {
            InitializeComponent();
            dpDataZleceniaOd.SelectedDate = data;
            dpDataZleceniaDo.SelectedDate = data;
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
