    using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;
using POP_SF_62_2017_GUI.GUI.RadSaModelom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RadSaNamestajem.xaml
    /// </summary>
    /// 
    public partial class RadSaNamestajem : Window {
        bool izmena = false;
        Namestaj namestaj, noviNamestaj = new Namestaj();
        ObservableCollection<TipNamestaja> tipNamestaja;
        public RadSaNamestajem() {
            InitializeComponent();
            tbNaziv.Focus();
            populateComboBox();
            comboTip.SelectedIndex = 0;
        }

        public RadSaNamestajem(Namestaj namestaj) {
            izmena = true;            
            InitializeComponent();
            this.namestaj = namestaj;
            noviNamestaj = namestaj;
            btnDodaj.Content = "Izmeni";
            this.namestaj = namestaj;
            this.tipNamestaja = UtilTipNamestaja.getAll();

            tbId.DataContext = noviNamestaj;
            tbNaziv.DataContext = noviNamestaj;
            tbCena.DataContext = noviNamestaj;
            tbKolicina.DataContext = noviNamestaj;

            comboTip.ItemsSource = tipNamestaja;
            comboTip.DataContext = noviNamestaj;
            
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
                    UtilNamestaj.ChangeById(getNamestajFromGUI(), Int32.Parse(tbId.Text));
                    
                    Close();
                } else {
                    UtilNamestaj.Add(getNamestajFromGUI());
                    noviNamestaj = namestaj;
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
            foreach (TipNamestaja tip in UtilTipNamestaja.getAll()) {
                comboTip.Items.Add(tip);
            }
        }
    }
}

