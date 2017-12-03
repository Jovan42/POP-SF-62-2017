using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Enums;
using POP_SF_62_2017_GUI.GUI.RadSaModelom;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for PregledNamestaja.xaml
    /// </summary>
    public partial class Pregled : Window, INotifyPropertyChanged {
        private Entitet izabrano;

        public Entitet Izabrano {
            get { return izabrano; }
            set { izabrano = value; onPropertyChanged("Izabrano"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

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
            switch (tip) {
                case TipKlase.NAMESTAJ:                  
                dgNamestaji.ItemsSource = NamestajDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                break;
                case TipKlase.AKCIJA:
                dgNamestaji.ItemsSource = AkcijaDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                break;
                case TipKlase.KORISNIK:
                dgNamestaji.ItemsSource = KorisnikDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                //window.Title += "a";
                break;
                case TipKlase.PRODAJA:
                dgNamestaji.ItemsSource = ProdajaDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                break;
                case TipKlase.SALON:
                dgNamestaji.ItemsSource = SalonDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
                //window.Title += "a";
                break;
                case TipKlase.TIP_NAMESTAJA:
                dgNamestaji.ItemsSource = TipNamestajaDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
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
                if (TipNamestajaDataProvider.Instance.GetAll().Count == 0) {
                    MessageBox.Show("Prvo morate dodati neki tip nameštaja", "Greška");
                    new RadSaTipomNamestaja().ShowDialog();

                } else
                    new RadSaNamestajem().ShowDialog();
                break;
                case TipKlase.AKCIJA:
                if (NamestajDataProvider.Instance.GetAll().Count == 0) {
                    MessageBox.Show("Prvo morate dodati nameštaj da bi ste dodali akciju", "Greška");
                    new RadSaTipomNamestaja().ShowDialog();
                } else
                    new RadSaAkcijom().ShowDialog();
                break;
                case TipKlase.KORISNIK:
                new RadSaKorisnikom().ShowDialog();
                break;
                case TipKlase.PRODAJA:
                if (NamestajDataProvider.Instance.GetAll().Count == 0) {
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
            if (izabrano == null) return;
            switch (tip) {
                case TipKlase.NAMESTAJ:
                new RadSaNamestajem((Namestaj)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.AKCIJA:
                new RadSaAkcijom((Akcija)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.KORISNIK:
                new RadSaKorisnikom((Korisnik)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.PRODAJA:
                new RadSaProdajom((Prodaja)Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                new RadSaTipomNamestaja((TipNamestaja)Izabrano.Clone()).ShowDialog();
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
                    NamestajDataProvider.Instance.DeleteByID(namestaj.ID);
                }
                break;
                case TipKlase.AKCIJA:
                Akcija akcija = (Akcija)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(akcija.ID);
                }
                break;
                case TipKlase.KORISNIK:
                Korisnik korisnik = (Korisnik)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj korisnika: {korisnik.KorIme} ", "Brisanje korisnika", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    KorisnikDataProvider.Instance.DeleteByID(korisnik.ID);
                }
                break;
                case TipKlase.PRODAJA:
                Prodaja prodaja = (Prodaja)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(prodaja.ID);
                }
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                TipNamestaja tipNamestaja = (TipNamestaja)dgNamestaji.SelectedItem;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete tip nameštaja: {tipNamestaja.Naziv}", "Brisanje tipa nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    TipNamestajaDataProvider.Instance.DeleteByID(tipNamestaja.ID);
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
            if (e.Column.Header.ToString() == "ID" || e.Column.Header.ToString() == "TipNamestajaID" 
                || e.Column.Header.ToString() == "Obrisan" || e.Column.Header.ToString() == "NamestajNaAkcijiID"
                || e.Column.Header.ToString() == "Lozinka") {
                e.Cancel = true;
            }
            
        }
    }
}
