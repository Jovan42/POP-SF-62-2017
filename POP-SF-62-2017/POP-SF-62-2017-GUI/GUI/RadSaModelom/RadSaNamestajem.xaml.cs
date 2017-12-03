using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for RadSaNamestajem.xaml
    /// </summary>
    /// 
    public partial class RadSaNamestajem : Window {
        bool izmena = false;
        Namestaj namestaj = new Namestaj();

        //Konstruktor u slučaju da argument niej prosleđem (dodavanje novog objekta)
        public RadSaNamestajem() {
            InitializeComponent();
            tbNaziv.Focus();
            populateComboBox();
            comboTip.SelectedIndex = 0;
        }

        public RadSaNamestajem(Namestaj namestaj) {
            izmena = true;            
            InitializeComponent();
            
            btnDodaj.Content = "Izmeni";

            //TODO: Rad sa kopijom mi daje gresku
            this.namestaj = namestaj;
            
            //this.tipNamestaja = TipNamestajaDataProvider.Instance.getAll
            tbId.DataContext = this.namestaj;
            tbNaziv.DataContext = this.namestaj;
            tbCena.DataContext = this.namestaj;
            tbKolicina.DataContext = this.namestaj;

            comboTip.ItemsSource = TipNamestajaDataProvider.Instance.GetAll();
            //comboTip.DataContext = this.namestaj;
            
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            int a;
            double b;
            try {
                if (!Int32.TryParse(tbKolicina.Text, out a))
                    throw new Exception("Kolicina namestaja je pogrešno uneta.");
                if (!Double.TryParse(tbCena.Text, out b))
                    throw new Exception("Cena namestaja je pogrešno uneta.");
                if (izmena) {
                    NamestajDataProvider.Instance.EditByID(getNamestajFromGUI(), Int32.Parse(tbId.Text));
                    
                    Close();
                } else {
                    NamestajDataProvider.Instance.Add(getNamestajFromGUI());
                    Close();
                }
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }

        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();

        }

        private Namestaj getNamestajFromGUI() {
            Namestaj n = new Namestaj() {
                Naziv = tbNaziv.Text,
                Cena = Double.Parse(tbCena.Text),
                Kolicina = Int32.Parse(tbKolicina.Text),
                Obrisan = false,
            };
            if (izmena)
                n.ID = Int32.Parse(tbId.Text);

            n.TipNamestajaID = ((TipNamestaja)comboTip.SelectedItem).ID;
            
            return n;
        }

        private void populateComboBox() {
            foreach (TipNamestaja tip in TipNamestajaDataProvider.Instance.GetAll()) {
                comboTip.Items.Add(tip);
            }
        }
    }
}

