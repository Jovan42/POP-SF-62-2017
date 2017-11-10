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

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for RadSaNamestajem.xaml
    /// </summary>
    public partial class RadSaNamestajem : Window {
        bool izmena = false;
        Namestaj namestaj;
        public RadSaNamestajem() {
            InitializeComponent();
            tbNaziv.Focus();
        }

        public RadSaNamestajem(Namestaj namestaj) {
            izmena = true;
            InitializeComponent();
            tbId.Text = namestaj.ID.ToString();
            tbNaziv.Text = namestaj.Naziv;
            tbCena.Text = namestaj.Cena.ToString();
            tbTip.Text = namestaj.TipNamestajaID.ToString();
            tbKolicina.Text = namestaj.Kolicina.ToString();
            btnDodaj.Content = "Izmeni";
            this.namestaj = namestaj;
            tbNaziv.Focus();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            //TODO Zastita od pogresnog unosa
            if(izmena) {
                UtilNamestaj.ChangeById(namestaj, namestaj.ID);
                Close();
            } else {
                namestaj = new Namestaj() {
                    Naziv = tbNaziv.Text,
                    Cena = Double.Parse(tbCena.Text),
                    TipNamestajaID = Int32.Parse(tbTip.Text),
                    Kolicina = Int32.Parse(tbKolicina.Text),
                    Obrisan = false
                };
                UtilNamestaj.Add(namestaj);
                Close();
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
