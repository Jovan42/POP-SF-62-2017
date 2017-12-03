using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using System;
using System.Windows;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaKorisnikom.xaml
    /// </summary>
    public partial class RadSaKorisnikom : Window {
        Korisnik korisnik = new Korisnik();
        bool izmena = false;

        //Konstruktor u slučaju da argument nije prosleđen (dodavanje novog objekta)
        public RadSaKorisnikom() {
            InitializeComponent();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaKorisnikom(Korisnik korisnik) {
            InitializeComponent();

            this.korisnik = korisnik;
            tbId.DataContext = this.korisnik;
            tbIme.DataContext = this.korisnik;
            tbKorIme.DataContext = this.korisnik;
            tbPrezime.DataContext = this.korisnik;
            cbAdmin.DataContext = this.korisnik;
            
            izmena = true;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            if (tbLozinka.Password != tbLozinka2.Password || tbKorIme.Text == "") {
                MessageBox.Show("Lozinke koje ste uneli su različite. Pokušajte opet...", "Greška");
            } else {
                if (izmena) {
                    KorisnikDataProvider.Instance.EditByID(getFromGUI(), Int32.Parse(tbId.Text));
                } else {
                    KorisnikDataProvider.Instance.Add(getFromGUI());
                }
            }
            Close();
        }

        private Korisnik getFromGUI() {
            Korisnik korisnik = new Korisnik() {
                Admin = (bool)cbAdmin.IsChecked,
                Ime = tbIme.Text,
                Prezime = tbPrezime.Text,
                KorIme = tbKorIme.Text,
                Lozinka = tbLozinka.Password
            };
            if (izmena)
                korisnik.ID = Int32.Parse(tbId.Text);

            return korisnik;
        }
    }
}
