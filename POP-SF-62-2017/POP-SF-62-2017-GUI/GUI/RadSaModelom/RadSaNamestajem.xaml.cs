using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace POP_SF_62_2017_GUI.GUI {
    public partial class RadSaNamestajem : Window {
        bool izmena = false;
        Namestaj namestaj = new Namestaj();

        //Konstruktor u slučaju da argument niej prosleđem (dodavanje novog objekta)
        public RadSaNamestajem() {
            InitializeComponent();
            tbNaziv.Focus();
            comboTip.SelectedIndex = 0;
            comboTip.ItemsSource = TipNamestajaDataProvider.Instance.GetAll();
            SetDataContexts();
        }

        public RadSaNamestajem(Namestaj namestaj) {
            izmena = true;            
            InitializeComponent();
            
            btnDodaj.Content = "Izmeni";
            this.namestaj = namestaj;
            SetDataContexts();            
        }

        private void SetDataContexts() {
            tbId.DataContext = namestaj;
            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;

            comboTip.ItemsSource = TipNamestajaDataProvider.Instance.GetAll();
            comboTip.DataContext = namestaj;
            comboTip.SelectedIndex = namestaj.TipNamestajaID - 1;
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            int a;
            double b;
            tbNaziv.BorderBrush = System.Windows.Media.Brushes.Black;
            tbKolicina.BorderBrush = System.Windows.Media.Brushes.Black;
            tbCena.BorderBrush = System.Windows.Media.Brushes.Black;
            try {
                if (tbNaziv.Text.Trim() == "") {
                    tbNaziv.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbNaziv.Focus();
                    throw new Exception("Naziv je pogrešno unet.");
                }
                if (!Int32.TryParse(tbKolicina.Text, out a) || Int32.Parse(tbKolicina.Text) < 0) {
                    tbKolicina.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbKolicina.Focus();
                    throw new Exception("Kolicina namestaja je pogrešno uneta.");
                }
                if (!Double.TryParse(tbCena.Text, out b) || Int32.Parse(tbCena.Text) < 0) {
                    tbCena.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbCena.Focus();
                    throw new Exception("Cena namestaja je pogrešno uneta.");
                }
                bool tn = false;
                foreach (TipNamestaja tipNamestaj in comboTip.Items) {
                    if (tipNamestaj.Naziv.Trim() == comboTip.Text.Trim()) {
                        tn = true;
                        break;
                    }
                }
                if (!tn) {
                    comboTip.Focus();
                    throw new Exception("Tip namestaja je pogrešno unet.");
                }
                if (izmena) {
                    NamestajDataProvider.Instance.EditByID(this.namestaj, Int32.Parse(tbId.Text));

                } else {
                    NamestajDataProvider.Instance.Add(this.namestaj);
                }
                Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }

        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}

