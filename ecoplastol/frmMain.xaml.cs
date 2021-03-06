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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            frmL.Close();
        }

        private void btnKonfiguracja_Click(object sender, RoutedEventArgs e)
        {
            switch (frmLogin.LoggedUser.profil)
            {
                case "A":
                    frmKonfiguracja2 frmKonfiguracja2 = new frmKonfiguracja2();
                    frmKonfiguracja2.ShowDialog();
                    break;
                default:
                    break;
            }
            
        }

        private void BtnPlanowanie2_Click(object sender, RoutedEventArgs e)
        {
            switch (frmLogin.LoggedUser.profil)
            {
                case "A":
                case "B":
                case "U1":
                    frmPlanowanie2 frmPlanowanie2 = new frmPlanowanie2();
                    frmPlanowanie2.ShowDialog();
                    break;
                default:
                    break;
            }
            
        }

        private void BtnZamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnZlecenia_Click(object sender, RoutedEventArgs e)
        {
            switch (frmLogin.LoggedUser.profil)
            {
                case "A":
                case "B":
                case "U1":
                    frmZleceniaProdukcyjne frmZleceniaProdukcyjne = new frmZleceniaProdukcyjne();
                    frmZleceniaProdukcyjne.ShowDialog();
                    break;
                default:
                    break;
            }
            
        }

        private void btnTools_Click(object sender, RoutedEventArgs e)
        {
            switch (frmLogin.LoggedUser.profil)
            {
                case "A":
                    frmTools frmTools = new frmTools();
                    frmTools.ShowDialog();
                    break;
                default:
                    break;
            }
            
        }
    }
}
