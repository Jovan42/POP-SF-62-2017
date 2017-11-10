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
    /// Interaction logic for PregledNamestaja.xaml
    /// </summary>
    public partial class PregledNamestaja : Window {
        public PregledNamestaja() {
            InitializeComponent();
            OsveziPrikaz();
        }
        private void OsveziPrikaz() {
            lbNamestaji.ItemsSource = null;
            lbNamestaji.ItemsSource = UtilNamestaj.getAllNamestaj();
            lbNamestaji.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            new RadSaNamestajem().ShowDialog();
            OsveziPrikaz();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e) {
            new RadSaNamestajem((Namestaj)lbNamestaji.SelectedItem).ShowDialog();
            OsveziPrikaz();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e) {
            Namestaj namestaj = (Namestaj)lbNamestaji.SelectedItem;
            MessageBoxResult  dialogResult =  MessageBox.Show("Brisanje nameštaja", $"Jeste li sigurni da želite da obrišete nameštaj: {namestaj.Naziv}", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes) {
                UtilNamestaj.DeleteById(namestaj.ID);
            }
            OsveziPrikaz();
        }
    }
}
