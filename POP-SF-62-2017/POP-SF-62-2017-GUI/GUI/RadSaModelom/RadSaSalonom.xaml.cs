using System;
using System.Windows;
using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaSalonom.xaml
    /// </summary>
    public partial class RadSaSalonom : Window {
        Salon salon = new Salon();

        //Konstruktor u slučaju da argument niej prosleđem (dodavanje novog objekta)
        public RadSaSalonom(Salon salon) {
            InitializeComponent();
            this.salon = salon;
            SetDataContexts();
        }
        private void SetDataContexts() {
            tbNaziv.DataContext = this.salon;
            tbAdresa.DataContext = this.salon;
            tbMail.DataContext = this.salon;
            tbSajt.DataContext = this.salon;
            tbTelefon.DataContext = this.salon;
            tbPIB.DataContext = this.salon;
            tbMatBr.DataContext = this.salon;
            tbRacun.DataContext = this.salon;
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e) {
            tbNaziv.BorderBrush = System.Windows.Media.Brushes.Black;
            tbAdresa.BorderBrush = System.Windows.Media.Brushes.Black;
            tbMail.BorderBrush = System.Windows.Media.Brushes.Black;
            tbSajt.BorderBrush = System.Windows.Media.Brushes.Black;
            tbTelefon.BorderBrush = System.Windows.Media.Brushes.Black;
            tbPIB.BorderBrush = System.Windows.Media.Brushes.Black;
            tbMatBr.BorderBrush = System.Windows.Media.Brushes.Black;
            tbRacun.BorderBrush = System.Windows.Media.Brushes.Black;

            try {
                if (tbNaziv.Text == "") {
                    tbNaziv.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbNaziv.Focus();
                    throw new Exception("Naziv salona je pogrešno unet.");
                }
                if (tbAdresa.Text == "") {
                    tbAdresa.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbAdresa.Focus();
                    throw new Exception("Adresa salona je pogrešno unet.");
                }
                if (tbMail.Text == "") {
                    tbMail.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbMail.Focus();
                    throw new Exception("Mail salona je pogrešno unet.");
                }
                if (tbSajt.Text == "") {
                    tbSajt.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbSajt.Focus();
                    throw new Exception("Sajt salona je pogrešno unet.");
                }
                if (tbTelefon.Text == "") {
                    tbTelefon.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbTelefon.Focus();
                    throw new Exception("Telefon salona je pogrešno unet.");
                }
                if (tbPIB.Text == "") {
                    tbPIB.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbPIB.Focus();
                    new Exception("PIB salona je pogrešno unet.");
                }
                if (tbMatBr.Text == "") {
                    tbMatBr.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbMatBr.Focus();
                    throw new Exception("Matični broj salona je pogrešno unet.");
                }
                if (tbRacun.Text == "") {
                    tbRacun.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbRacun.Focus();
                    throw new Exception("Račun salona je pogrešno unet.");
                }

                SalonDataProvider.Instance.EditByID(this.salon, 0);
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
            
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
