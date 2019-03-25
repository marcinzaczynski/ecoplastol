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
    /// Interaction logic for frmZlecenieProdukcji.xaml
    /// </summary>
    public partial class frmZlecenieProdukcji : Window
    {
        private List<wyroby> listaWyrobow;

        public frmZlecenieProdukcji(int numerMaszyny, string nazwaMaszyny)
        {
            InitializeComponent();

            lblNazwaMaszyny.Content = nazwaMaszyny;

            listaWyrobow = frmZlecenieProdukcji_db.PobierzWyroby();
            cbbWyrobKod.ItemsSource = listaWyrobow;
            cbbWyrobKod.SelectedValuePath = "id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
