using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;
using POP_SF_62_2017_GUI.Enums;
using POP_SF_62_2017_GUI.GUI.RadSaModelom;
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
                btnRadSaSalonima.IsEnabled = true;
                window.Title += " - ADMIN";
            } else {
                window.Title += " - KORISNIK";
            }

        }

        private void btnRadSaNamestajem_Click(object sender, RoutedEventArgs e) {
            new Pregled(TipKlase.NAMESTAJ, admin).Show();
            this.Close();
        }

        private void btnRadSaAkcijama_Click(object sender, RoutedEventArgs e) {
            new Pregled(TipKlase.AKCIJA, admin).Show();
            this.Close();
        }

        private void btnRadSaTipovimaNamestaja_Click(object sender, RoutedEventArgs e) {
            new Pregled(TipKlase.TIP_NAMESTAJA, admin).Show();
            this.Close();
        }

        private void btnRadSaProdajama_Click(object sender, RoutedEventArgs e) {
            new Pregled(TipKlase.PRODAJA, admin).Show();
            this.Close();
        }

        private void btnRadSaKorisnicima_Click(object sender, RoutedEventArgs e) {
            new Pregled(TipKlase.KORISNIK, admin).Show();
            this.Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnOdjaviSe_Click(object sender, RoutedEventArgs e) {
            new Login().Show();
            this.Close();
        }

        private void btnRadSaSalonima_Click(object sender, RoutedEventArgs e) {
            new RadSaSalonom(UtilSalon.GetById(0)).ShowDialog();
        }
    }
}
