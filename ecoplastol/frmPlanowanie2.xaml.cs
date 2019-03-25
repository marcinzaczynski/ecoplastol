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
    public partial class frmPlanowanie2 : Window
    {
        private List<maszyny> listaMaszyn;
        private int _ilKolumn = 3;
        private int _ilWierszy;
        public frmPlanowanie2()
        {
            InitializeComponent();
           
            listaMaszyn = frmPlanowanie2_db.PobierzMaszyny();
            double il_w = listaMaszyn.Count / _ilKolumn;
            _ilWierszy = Convert.ToInt32(Math.Ceiling(il_w));

            for (int i = 0; i < _ilWierszy; i++)
            {
                grMaszyny.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _ilKolumn; i++)
            {
                grMaszyny.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int licznik = 0;

            for (int i = 0; i < _ilWierszy; i++)
            {
                for (int j = 0; j < _ilKolumn; j++)
                {
                    MaszynaPanel mp = new MaszynaPanel(listaMaszyn[licznik].id, listaMaszyn[licznik].numer);
                    //mp.Name = "panelM" + Convert.ToChar(listaMaszyn[licznik].id + 1);
                    
                    Grid.SetRow(mp, i);
                    Grid.SetColumn(mp, j);
                    grMaszyny.Children.Add(mp);
                    licznik++;
                }
            }
            
        }

    }
}
