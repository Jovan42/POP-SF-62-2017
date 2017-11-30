using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;
using POP_SF_62_2017_GUI.Enums;
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
    /// Interaction logic for PregledNamestaja.xaml
    /// </summary>
    public partial class Pregled : Window {
        public ICloneable Izabrano { get; set; }




        bool admin;
        private TipKlase tip;
        public Pregled(TipKlase tip, bool admin) {
            this.admin = admin;
            this.tip = tip;
            InitializeComponent();

            OsveziPrikaz();

            if (tip == TipKlase.PRODAJA) {
                btnObrisi.IsEnabled = false;
            }


        }
        private void OsveziPrikaz() {
            dgNamestaji.ItemsSource = null;
            //window.Title = $"Pregled {tip.ToString().ToLower()}";
            //TODO ICollectionView  .GetDefaultView
            switch (tip) {
                case TipKlase.NAMESTAJ:
                dgNamestaji.ItemsSource = UtilNamestaj.getAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                break;
                case TipKlase.AKCIJA:
                dgNamestaji.ItemsSource = UtilAkcija.getAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                break;
                case TipKlase.KORISNIK:
                dgNamestaji.ItemsSource = UtilKorisnik.getAll();
                //window.Title += "a";
                break;
                case TipKlase.PRODAJA:
                dgNamestaji.ItemsSource = UtilProdaja.getAll();
                break;
                case TipKlase.SALON:
                dgNamestaji.ItemsSource = UtilNamestaj.getAll();
                //window.Title += "a";
                break;
                case TipKlase.TIP_NAMESTAJA:
                dgNamestaji.ItemsSource = UtilTipNamestaja.getAll();
                //window.Title = $"Pregled tipova nameštaja";
                break;
                default:
                break;
            }
            dgNamestaji.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            switch (tip) {
                case TipKlase.NAMESTAJ:
                if (UtilTipNamestaja.getAll().Count == 0) {
                    MessageBox.Show("Prvo morate dodati neki tip nameštaja", "Greška");
                    new RadSaTipomNamestaja().ShowDialog();

                } else
                    new RadSaNamestajem().ShowDialog();
                break;
                case TipKlase.AKCIJA:
                if (UtilNamestaj.getAll().Count == 0) {
                    MessageBox.Show("Prvo morate dodati nameštaj da bi ste dodali akciju", "Greška");
                    new RadSaTipomNamestaja().ShowDialog();
                } else
                    new RadSaAkcijom().ShowDialog();
                break;
                case TipKlase.KORISNIK:
                new RadSaKorisnikom().ShowDialog();
                break;
                case TipKlase.PRODAJA:
                if (UtilNamestaj.getAll().Count == 0) {
                    MessageBox.Show("Prvo morate dodati nameštaj da bi ste dodali prodaju", "Greška");
                    new RadSaTipomNamestaja().ShowDialog();
                } else
                    new RadSaProdajom().ShowDialog();
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                new RadSaTipomNamestaja().ShowDialog();
                break;
                default:
                break;
            }

            OsveziPrikaz();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e) {
            switch (tip) {
                case TipKlase.NAMESTAJ:
                new RadSaNamestajem((Namestaj)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.AKCIJA:
                new RadSaAkcijom((Akcija)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.KORISNIK:
                new RadSaKorisnikom((Korisnik)dgNamestaji.SelectedItem).ShowDialog();
                break;
                case TipKlase.PRODAJA:
                new RadSaProdajom((Prodaja)dgNamestaji.SelectedItem).ShowDialog();
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                new RadSaTipomNamestaja((TipNamestaja)dgNamestaji.SelectedItem).ShowDialog();
                break;
                default:
                break;
            }

            OsveziPrikaz();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult dialogResult;
            switch (tip) {
                case TipKlase.NAMESTAJ:
                Namestaj namestaj = (Namestaj)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj: {namestaj.Naziv}", "Brisanje nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    UtilNamestaj.DeleteById(namestaj.ID);
                }
                break;
                case TipKlase.AKCIJA:
                Akcija akcija = (Akcija)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    UtilAkcija.DeleteById(akcija.ID);
                }
                break;
                case TipKlase.KORISNIK:
                Korisnik korisnik = (Korisnik)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj korisnika: {korisnik.KorIme} ", "Brisanje korisnika", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    UtilKorisnik.DeleteById(korisnik.ID);
                }
                break;
                case TipKlase.PRODAJA:
                Prodaja prodaja = (Prodaja)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    UtilAkcija.DeleteById(prodaja.ID);
                }
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                TipNamestaja tipNamestaja = (TipNamestaja)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete tip nameštaja: {tipNamestaja.Naziv}", "Brisanje tipa nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    UtilTipNamestaja.DeleteById(tipNamestaja.ID);
                }
                break;
                default:
                break;
            }
            OsveziPrikaz();
        }

        private void btnOdajviSe_Click(object sender, RoutedEventArgs e) {
            new Login().Show();
            this.Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e) {
            new Meni(admin).Show();
            this.Close();
        }

        private void dgNamestaji_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.Column.Header.ToString() == "ID" || e.Column.Header.ToString() == "TipNamestajaID" || e.Column.Header.ToString() == "Obrisan") {
                e.Cancel = true;
            }            
        }
    }
}
