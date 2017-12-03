using System;
using System.Windows;
using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaTipomNamestaja.xaml
    /// </summary>
    public partial class RadSaTipomNamestaja : Window
    {
        bool izmena = false;
        TipNamestaja tipNamestaja = new TipNamestaja();

        //Konstruktor u slučaju da argument nije prosleđen (dodavanje novog objekta)
        public RadSaTipomNamestaja()
        {
            InitializeComponent();
            tbNaziv.Focus();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaTipomNamestaja(TipNamestaja tipNamestaja) {
            izmena = true;
            InitializeComponent();

            this.tipNamestaja = tipNamestaja;
            tbId.DataContext = this.tipNamestaja;
            tbNaziv.DataContext = this.tipNamestaja;
            
            btnDodaj.Content = "Izmeni";
            this.tipNamestaja = tipNamestaja;
            tbNaziv.Focus();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            if (izmena) {
                TipNamestajaDataProvider.Instance.EditByID(getFromGUI(), Int32.Parse(tbId.Text));
                Close();
            } else {
                TipNamestajaDataProvider.Instance.Add(getFromGUI());
                Close();
            }
        }

        private TipNamestaja getFromGUI() {
            TipNamestaja n = new TipNamestaja() {
                Naziv = tbNaziv.Text,
            };
            if (izmena)
                n.ID = Int32.Parse(tbId.Text);

            return n;
        }
    }
}