using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;
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

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaKorisnikom.xaml
    /// </summary>
    public partial class RadSaKorisnikom : Window {
        bool izmena = false;

        public RadSaKorisnikom() {
            InitializeComponent();
        }

        public RadSaKorisnikom(Korisnik korisnik) {
            InitializeComponent();
            tbId.Text = korisnik.ID.ToString();
            tbIme.Text = korisnik.Ime;
            tbKorIme.Text = korisnik.KorIme;
            tbPrezime.Text = korisnik.Prezime;
            cbAdmin.IsChecked = korisnik.Admin;
            izmena = true;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            if (tbLozinka.Password != tbLozinka2.Password) {
                MessageBox.Show("Lozinke koje ste uneli su različite. Pokušajte opet...", "Greška");
            } else {
                if (izmena) {
                    UtilKorisnik.ChangeById(getFromGUI(), Int32.Parse(tbId.Text));
                } else {
                    UtilKorisnik.ChangeById(getFromGUI(), 0);
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
