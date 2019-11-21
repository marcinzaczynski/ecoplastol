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

namespace ecoplastol.raporty
{
    /// <summary>
    /// Interaction logic for frmWyrobKartaTechnologiczna.xaml
    /// </summary>
    public partial class frmWyrobKartaTechnologiczna : Window
    {
        public frmWyrobKartaTechnologiczna()
        {
            InitializeComponent();
            
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaportKT.Reset();
            //Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource("dsWyrobyview", listaWyrobow);
            //ReportViewerDemo.LocalReport.DataSources.Add(ds);
            RaportKT.LocalReport.ReportEmbeddedResource = "ecoplastol.raporty.wyrob_karta_technologiczna.rdlc";
            RaportKT.RefreshReport();
        }
    }
}
