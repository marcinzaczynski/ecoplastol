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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class frmMain : Window
    {
        frmLogin frmL;
        public frmMain(frmLogin ptrF)
        {
            InitializeComponent();
            frmL = ptrF;
        }

        private void btnWyroby_Click(object sender, RoutedEventArgs e)
        {
            frmWyroby frmWyroby = new frmWyroby();
            ////DialogResult result = new DialogResult();
            frmWyroby.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            frmL.Close();
        }

        private void btnParametry_Click(object sender, RoutedEventArgs e)
        {
            frmParametry frmParametry = new frmParametry();
            ////DialogResult result = new DialogResult();
            frmParametry.ShowDialog();
        }

        private void BtnPlanowanie_Click(object sender, RoutedEventArgs e)
        {
            frmPlanowanie frmPlanowanie = new frmPlanowanie();
            frmPlanowanie.ShowDialog();
        }
    }
}
