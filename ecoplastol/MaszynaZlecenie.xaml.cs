﻿using System;
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
        public MaszynaZlecenie(string labelText)
        {
            InitializeComponent();

            label1.Content = labelText;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(label1.Content.ToString());
        }
    }
}
