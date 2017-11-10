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

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for Meni.xaml
    /// </summary>
    public partial class Meni : Window {
        private bool admin;
        public Meni(bool admin) {
            InitializeComponent();
            this.admin = admin;
            if (admin) {
                btnRadSaKorisnicima.IsEnabled = true;
            }
        }

        private void btnRadSaNamestajem_Click(object sender, RoutedEventArgs e) {
            new PregledNamestaja().Show();
            this.Close();
        }

        private void btnRadSaAkcijama_Click(object sender, RoutedEventArgs e) {
            //TODO
        }

        private void btnRadSaTipovimaNamestaja_Click(object sender, RoutedEventArgs e) {
            //TODO
        }

        private void btnRadSaProdajama_Click(object sender, RoutedEventArgs e) {
            //TODO
        }

        private void btnRadSaKorisnicima_Click(object sender, RoutedEventArgs e) {
            //TODO
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnOdjaviSe_Click(object sender, RoutedEventArgs e) {
            new Login().Show();
            this.Close();
        }
    }
}
