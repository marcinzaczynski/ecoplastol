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
        public static event RefreshDataDelegate RefresData;

        public int numerMaszyny { get; set; }
        public string nazwaMaszyny { get; set; }

        public MaszynaPanel(int _id,string _nazwa)
        {
            InitializeComponent();
            //btn.Name = "btnDodajM" + Convert.ToChar(_id);
            numerMaszyny = _id;
            nazwaMaszyny = _nazwa;

            this.Tag = _id;
            btn.Tag = _id;
            lblNazwaMaszyny.Content = _nazwa;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int nr_panela = Convert.ToInt32(btn.Tag);
            var mp = frmPlanowanie2.listaPaneli[nr_panela - 1];
            // -- dodanie do stackpanela
            //var mp = frmPlanowanie2.listaPaneli[nr_panela - 1];
            //var gr = mp.spMaszynaZlecenia;
            //MaszynaZlecenie mz = new MaszynaZlecenie("aaa");
            //gr.Children.Add(mz);


            // -- dodanie do datagrida
            //gr.RowDefinitions.Add(new RowDefinition());
            //var il_w = gr.RowDefinitions.Count;
            //MaszynaZlecenie mz = new MaszynaZlecenie("aaa");
            //Grid.SetRow(mz, il_w - 1);
            //gr.Children.Add(mz);

            frmZlecenieProdukcji frmZlecenieProdukcji = new frmZlecenieProdukcji(numerMaszyny, nazwaMaszyny);
            frmZlecenieProdukcji.ShowDialog();

            if (frmZlecenieProdukcji.DialogResult.HasValue && frmZlecenieProdukcji.DialogResult.Value)
                RefresData?.Invoke();
            ;

            
        }
    }
}
