using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
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
    /// Interaction logic for RadSaDodatnomUslugom.xaml
    /// </summary>
    public partial class RadSaDodatnomUslugom : Window {
        DodatnaUsluga dodatnaUsluga = new DodatnaUsluga();
        bool izmena = false;
        public RadSaDodatnomUslugom() {
            InitializeComponent();
            SetDataContexts();
        }
        public RadSaDodatnomUslugom(DodatnaUsluga dodatnaUsluga) {
            InitializeComponent();
            izmena = true;
            this.dodatnaUsluga = dodatnaUsluga;
            btnDodaj.Content = "Izmeni";
            SetDataContexts();
        }

        private void SetDataContexts() {
            
            tbNaziv.DataContext = dodatnaUsluga;
            tbCena.DataContext = dodatnaUsluga;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            tbNaziv.BorderBrush = System.Windows.Media.Brushes.Black;
            tbCena.BorderBrush = System.Windows.Media.Brushes.Black;
            try {
                double a;
                if (tbNaziv.Text.Trim() == "") {
                    tbNaziv.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbNaziv.Focus();
                    throw new Exception("Naziv je pogrešno unet.");
                }

                if (!Double.TryParse(tbCena.Text, out a)) {
                    tbCena.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbCena.Focus();
                    throw new Exception("Cena namestaja je pogrešno uneta.");
                }

                if (izmena) {
                    DodatnaUslugaDataProvider.Instance.EditByID(this.dodatnaUsluga, dodatnaUsluga.ID);

                } else {
                    DodatnaUslugaDataProvider.Instance.Add(this.dodatnaUsluga);
                }
                Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
        }
    }
}
