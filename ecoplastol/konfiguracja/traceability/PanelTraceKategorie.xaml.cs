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

namespace ecoplastol.konfiguracja.traceability
{
    /// <summary>
    /// Interaction logic for PanelTraceKategorie.xaml
    /// </summary>
    public partial class PanelTraceKategorie : UserControl
    {
        private int grdBookmark;
        private string akcja;
        private trace_kategoria rowTraceKategoria;
        private List<trace_kategoria> listTraceKategoria;
        public PanelTraceKategorie()
        {
            InitializeComponent();
        }

        private void GrdLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnKlonuj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPopraw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUsun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnZatwierdz_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
