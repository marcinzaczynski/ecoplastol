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
    /// Interaction logic for frmPlanowanie.xaml
    /// </summary>
    public partial class frmPlanowanie : Window
    {
        int i = 0;

        public frmPlanowanie()
        {
            InitializeComponent();
        }

        private void BtnDodajM1_Click(object sender, RoutedEventArgs e)
        {
            //gridMaszyny.RowDefinitions[0].Height = new GridLength(200);

            MaszynaZlecenie mz = new MaszynaZlecenie($"Komponent {i}");

            ParentGrid.RowDefinitions.Add(new RowDefinition());
            ParentGrid.Children.Add(mz);
            Grid.SetRow(mz, i++);
        }

        private void BtnDodajM2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
