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
    /// Interaction logic for MaszynaPanel.xaml
    /// </summary>
    public partial class MaszynaPanel : UserControl
    {
        public MaszynaPanel(int _id,string _nazwa)
        {
            InitializeComponent();
            //btn.Name = "btnDodajM" + Convert.ToChar(_id);
            btn.Tag = _id;
            lblNazwaMaszyny.Content = _nazwa;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(btn.Tag.ToString());
        }
    }
}
