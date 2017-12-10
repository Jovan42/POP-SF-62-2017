using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Enums;
using POP_SF_62_2017_GUI.GUI.RadSaModelom;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for PregledNamestaja.xaml
    /// </summary>
    public partial class Pregled : Window, INotifyPropertyChanged {
        DataGrid dgNaAkciji = new DataGrid();
        DataGrid dgProdatNamestaj = new DataGrid();
        DataGrid dgDodatneUsluge = new DataGrid();
        Label lblCenaBez = new Label();
        Label lblCenaSa = new Label();
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
                btnObrisi.Visibility = Visibility.Hidden;
            }

            if (tip == TipKlase.AKCIJA) {
                dgNamestaji.SelectionChanged += new SelectionChangedEventHandler(selectionChanged);
                //Izabrano = (Entitet)dgNamestaji.Items[0];
                DataGrid dgNaAkciji = new DataGrid();
                dgNaAkciji.Margin = new Thickness(5);
                Binding binding = new Binding("NamestajiNaAkcijiID");
                BindingOperations.SetBinding(dgNaAkciji, DataGrid.ItemsSourceProperty, binding);
                dgNaAkciji.IsSynchronizedWithCurrentItem = true;
                try {
                    dgNaAkciji.DataContext = ((Akcija)Izabrano);
                    dgNaAkciji.ItemsSource = ((Akcija)Izabrano).NamestajNaAkciji;
                } catch (Exception) {
                }
                dgNaAkciji.IsReadOnly = true;
                dgNaAkciji.CanUserAddRows = false;
                dgNaAkciji.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(dgNaAkciji_AutoGeneratingColumn);
                dgNaAkciji.Width = 300;
                dgNaAkciji.Height = 300;
                dgNaAkciji.MaxHeight = 300;
                dgNaAkciji.MaxWidth = 300;
                Label label = new Label();
                label.Content = "Nameštaj na akciji:";
                label.Foreground = Brushes.White;
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                sp.Children.Add(label);
                sp.Children.Add(dgNaAkciji);

                spDataGrid.Children.Add(sp);
                this.dgNaAkciji = dgNaAkciji;
            }

            if(tip == TipKlase.PRODAJA) {
                dgNamestaji.SelectionChanged += new SelectionChangedEventHandler(selectionChanged);
                //Izabrano = (Entitet)dgNamestaji.Items[0];
                DataGrid dgProdatNamestaj = new DataGrid();
                dgProdatNamestaj.Margin = new Thickness(5);
                Binding binding = new Binding("NamestajiNaAkcijiID");
                BindingOperations.SetBinding(dgProdatNamestaj, DataGrid.ItemsSourceProperty, binding);
                dgProdatNamestaj.IsSynchronizedWithCurrentItem = true;
                try {
                    dgProdatNamestaj.DataContext = ((Prodaja)Izabrano);
                    dgProdatNamestaj.ItemsSource = ((Prodaja)Izabrano).ProdatNamestaj;
                } catch (Exception) {
                }
                dgProdatNamestaj.IsReadOnly = true;
                dgProdatNamestaj.CanUserAddRows = false;
                dgProdatNamestaj.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(dgNaAkciji_AutoGeneratingColumn);
                dgProdatNamestaj.Width = 200;
                dgProdatNamestaj.Height = 300;
                dgProdatNamestaj.MaxHeight = 300;
                dgProdatNamestaj.MaxWidth = 300;
                Label label = new Label();
                label.Content = "Prodat nameštaj:";
                label.Foreground = Brushes.White;
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Vertical;
                sp.Children.Add(label);
                sp.Children.Add(dgProdatNamestaj);

                spDataGrid.Children.Add(sp);
                this.dgProdatNamestaj = dgProdatNamestaj;

                
                //Izabrano = (Entitet)dgNamestaji.Items[0];
                DataGrid dgDodatneUsluge = new DataGrid();
                dgDodatneUsluge.Margin = new Thickness(5);
                Binding binding2 = new Binding("DodatneUsluge");
                BindingOperations.SetBinding(dgProdatNamestaj, DataGrid.ItemsSourceProperty, binding2);
                dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
                try {
                    dgDodatneUsluge.DataContext = ((Prodaja)Izabrano);
                    dgDodatneUsluge.ItemsSource = ((Prodaja)Izabrano).DodatneUslugeID;
                } catch (Exception) {
                }
                dgDodatneUsluge.IsReadOnly = true;
                dgDodatneUsluge.CanUserAddRows = false;
                dgDodatneUsluge.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(dgNaAkciji_AutoGeneratingColumn);
                dgDodatneUsluge.Width = 100;
                dgDodatneUsluge.Height = 300;
                dgDodatneUsluge.MaxHeight = 300;
                dgDodatneUsluge.MaxWidth = 100;
                Label label2 = new Label();
                label2.Content = "Dodatne usluge:";
                label2.Foreground = Brushes.White;
                StackPanel sp2 = new StackPanel();
                sp2.Orientation = Orientation.Vertical;
                sp2.Children.Add(label2);
                sp2.Children.Add(dgDodatneUsluge);


                Label lblCenaBez = new Label();
                lblCenaBez.Content = "Cena bez PDV-a:";
                lblCenaBez.Foreground = Brushes.White;

                Label lblCenaSa = new Label();
                lblCenaSa.Content = "Cena sa PDV-om:";
                lblCenaSa.Foreground = Brushes.White;

                sp2.Children.Add(lblCenaBez);
                sp2.Children.Add(lblCenaSa);
                this.lblCenaBez = lblCenaBez;
                this.lblCenaSa = lblCenaSa;

                spDataGrid.Children.Add(sp2);
                this.dgDodatneUsluge = dgDodatneUsluge;

            }
        }

        private void dgNaAkciji_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if(tip == TipKlase.AKCIJA && e.Column.Header.ToString() == "Kolicina") {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "ID" || e.Column.Header.ToString() == "TipNamestajaID"
                || e.Column.Header.ToString() == "Obrisan" || e.Column.Header.ToString() == "NamestajID") {
                e.Cancel = true;
            }
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e) {
            if (tip == TipKlase.AKCIJA) {
                try {
                    dgNaAkciji.ItemsSource = ((Akcija)Izabrano).NamestajNaAkciji;
                } catch (Exception) {
                }
            }
            if(tip == TipKlase.PRODAJA) {
                try {
                    dgDodatneUsluge.ItemsSource = ((Prodaja)Izabrano).DodatnaUsluga;
                    dgProdatNamestaj.ItemsSource = ((Prodaja)Izabrano).ProdatNamestaj;
                    double cena = ((Prodaja)Izabrano).getUkupnaCena();
                    lblCenaBez.Content = $"Cena bez PDV-a: {cena.ToString()}";
                    cena *= 1.2;
                    lblCenaSa.Content = $"Cena sa PDV-om:  {cena.ToString()}";
                } catch (Exception) {
                }
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
                dgNamestaji.Width = double.NaN;
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
                dgNamestaji.Width = double.NaN;
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
                case TipKlase.DODATNE_USLUGE:
                dgNamestaji.ItemsSource = DodatnaUslugaDataProvider.Instance.GetAll();
                dgNamestaji.DataContext = this;
                dgNamestaji.IsSynchronizedWithCurrentItem = true;
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
                case TipKlase.DODATNE_USLUGE:
                new RadSaDodatnomUslugom().ShowDialog();
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
                case TipKlase.DODATNE_USLUGE:
                new RadSaDodatnomUslugom((DodatnaUsluga)Izabrano.Clone()).ShowDialog();
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
                Akcija akcija = (Akcija)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(akcija.ID);
                }
                break;
                case TipKlase.KORISNIK:
                Korisnik korisnik = (Korisnik)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj korisnika: {korisnik.KorIme} ", "Brisanje korisnika", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    KorisnikDataProvider.Instance.DeleteByID(korisnik.ID);
                }
                break;
                case TipKlase.PRODAJA:
                Prodaja prodaja = (Prodaja)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(prodaja.ID);
                }
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                TipNamestaja tipNamestaja = (TipNamestaja)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete tip nameštaja: {tipNamestaja.Naziv}", "Brisanje tipa nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    TipNamestajaDataProvider.Instance.DeleteByID(tipNamestaja.ID);
                }
                break;
                case TipKlase.DODATNE_USLUGE:
                DodatnaUsluga dodatnaUsluga = (DodatnaUsluga)Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete dodatnu uslugu: {dodatnaUsluga.Naziv}", "Brisanje tipa nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    DodatnaUslugaDataProvider.Instance.DeleteByID(dodatnaUsluga.ID);
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
                || e.Column.Header.ToString() == "Lozinka" || e.Column.Header.ToString() == "NamestajNaAkciji"
                || e.Column.Header.ToString() == "ProdatNamestaj" || e.Column.Header.ToString() == "DodatneUslugeID"
                || e.Column.Header.ToString() == "DodatnaUsluga") {
                e.Cancel = true;
            }
            
        }
    }
}
