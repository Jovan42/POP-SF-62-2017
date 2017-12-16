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
            SetDataContexts();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaKorisnikom(Korisnik korisnik) {
            InitializeComponent();

            this.korisnik = korisnik;
            SetDataContexts();
            izmena = true;
        }

        private void SetDataContexts() {
            tbId.DataContext = this.korisnik;
            tbIme.DataContext = this.korisnik;
            tbKorIme.DataContext = this.korisnik;
            tbPrezime.DataContext = this.korisnik;
            cbAdmin.DataContext = this.korisnik;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            tbIme.BorderBrush = System.Windows.Media.Brushes.Black;
            tbPrezime.BorderBrush = System.Windows.Media.Brushes.Black;
            tbKorIme.BorderBrush = System.Windows.Media.Brushes.Black;
            tbLozinka.BorderBrush = System.Windows.Media.Brushes.Black;
            tbLozinka2.BorderBrush = System.Windows.Media.Brushes.Black;
            try {
                if(tbIme.Text.Trim() == "") {
                    tbIme.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbIme.Focus();
                    throw new Exception("Ime je pogrešno uneto.");
                }
                if (tbPrezime.Text.Trim() == "") {
                    tbPrezime.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbPrezime.Focus();
                    throw new Exception("Prezime je pogrešno uneto.");
                }
                if(tbKorIme.Text.Trim() == "") {
                    tbKorIme.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbKorIme.Focus();
                    throw new Exception("Korisničko ime je pogrešno uneto.");
                }
                if(tbLozinka.Password.Trim() == "" || tbLozinka2.Password.Trim() == "" || 
                    tbLozinka.Password.Trim() != tbLozinka2.Password.Trim()) {
                    tbLozinka.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbLozinka2.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbLozinka.Focus();
                    throw new Exception("Šifre su je pogrešno unete.");
                }

                this.korisnik.Lozinka = tbLozinka.Password;

                if (izmena) {
                    
                    KorisnikDataProvider.Instance.EditByID(this.korisnik, Int32.Parse(tbId.Text));
                } else {
                    KorisnikDataProvider.Instance.Add(this.korisnik);
                }
                Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
        }
    }
}
