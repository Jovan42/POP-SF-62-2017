using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Enums;
using POP_SF_62_2017_GUI.GUI.RadSaModelom;
using System.Windows;

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
                
                window.Title += " - ADMIN";
            } else {
                window.Title += " - KORISNIK";
                btnRadSaSalonima.Visibility = Visibility.Hidden;
                btnRadSaKorisnicima.Visibility = Visibility.Hidden;
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
            new RadSaSalonom(((Salon)SalonDataProvider.Instance.GetByID(0))).ShowDialog();
        }
    }
}
