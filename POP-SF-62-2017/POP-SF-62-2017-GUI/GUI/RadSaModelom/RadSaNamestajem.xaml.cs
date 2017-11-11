    using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;
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
    /// Interaction logic for RadSaNamestajem.xaml
    /// </summary>
    /// 
    public partial class RadSaNamestajem : Window {
        bool izmena = false;
        Namestaj namestaj;
        public RadSaNamestajem() {
            InitializeComponent();
            tbNaziv.Focus();
            populateComboBox();
        }

        public RadSaNamestajem(Namestaj namestaj) {
            izmena = true;
            InitializeComponent();
            tbId.Text = namestaj.ID.ToString();
            tbNaziv.Text = namestaj.Naziv;
            tbCena.Text = namestaj.Cena.ToString();
            populateComboBox();
            

            tbKolicina.Text = namestaj.Kolicina.ToString();
            btnDodaj.Content = "Izmeni";
            this.namestaj = namestaj;
            tbNaziv.Focus();
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

            string tipId = comboTip.SelectedItem.ToString().Split('.')[0];
            n.TipNamestajaID = Int32.Parse(tipId);
            return n;
        }

        private void populateComboBox() {
            foreach (TipNamestaja tip in UtilTipNamestaja.getAll()) {
                comboTip.Items.Add($"{tip.ID}. {tip.Naziv}");
            }

        }
    }
}

