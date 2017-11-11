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
using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom
{
    /// <summary>
    /// Interaction logic for RadSaTipomNamestaja.xaml
    /// </summary>
    public partial class RadSaTipomNamestaja : Window
    {
        bool izmena = false;
        TipNamestaja tipNamestaja;
        public RadSaTipomNamestaja()
        {
            InitializeComponent();
            tbNaziv.Focus();
        }

        public RadSaTipomNamestaja(TipNamestaja tipNamestaja) {
            izmena = true;
            InitializeComponent();
            tbId.Text = tipNamestaja.ID.ToString();
            tbNaziv.Text = tipNamestaja.Naziv;
            btnDodaj.Content = "Izmeni";
            this.tipNamestaja = tipNamestaja;
            tbNaziv.Focus();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            if (izmena) {
                UtilTipNamestaja.ChangeById(getFromGUI(), Int32.Parse(tbId.Text));
                Close();
            } else {
                UtilTipNamestaja.Add(getFromGUI());
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
