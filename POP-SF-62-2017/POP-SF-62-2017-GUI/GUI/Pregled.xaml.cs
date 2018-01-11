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
            
            if(!admin || tip == TipKlase.PRODAJA) {
                cbObrisani.Visibility = Visibility.Hidden;
                lblObrisani.Visibility = Visibility.Hidden;
            }
            OsveziPrikaz();
            if (tip == TipKlase.PRODAJA) {
                btnObrisi.Visibility = Visibility.Hidden;
            }

            if(tip == TipKlase.PRODAJA && admin == false) {
                btnIzmeni.Visibility = Visibility.Hidden;
            }

            if (tip == TipKlase.AKCIJA) {
                dgNamestaji.SelectionChanged += new SelectionChangedEventHandler(selectionChanged);
                //Izabrano = (Entitet)dgNamestaji.Items[0];
                DataGrid dgNaAkciji = new DataGrid();
                dgNaAkciji.Margin = new Thickness(5);
                window.Width = 1000;
                Binding binding = new Binding("NamestajNaAkciji");
                BindingOperations.SetBinding(dgNaAkciji, DataGrid.ItemsSourceProperty, binding);
                
                try {
                    dgNaAkciji.DataContext = ((Akcija)Projekat.Instance.Izabrano);
                    dgNaAkciji.IsSynchronizedWithCurrentItem = true;
                    //dgNaAkciji.ItemsSource = ((Akcija)Projekat.Instance.Izabrano).NamestajNaAkciji;
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
                Binding binding = new Binding("ProdatNamestaj");
                window.Width = 1000;
                BindingOperations.SetBinding(dgProdatNamestaj, DataGrid.ItemsSourceProperty, binding);
                try {
                    dgProdatNamestaj.DataContext = ((Prodaja)Projekat.Instance.Izabrano);
                    dgProdatNamestaj.IsSynchronizedWithCurrentItem = true;
                    //dgProdatNamestaj.ItemsSource = ((Prodaja)Projekat.Instance.Izabrano).ProdatNamestaj;
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
                BindingOperations.SetBinding(dgDodatneUsluge, DataGrid.ItemsSourceProperty, binding2);
                
                try {
                    dgDodatneUsluge.DataContext = ((Prodaja)Projekat.Instance.Izabrano);
                    dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
                    //dgDodatneUsluge.ItemsSource = ((Prodaja)Projekat.Instance.Izabrano).DodatneUslugeID;
                } catch (Exception) {
                }
                dgDodatneUsluge.IsReadOnly = true;
                dgDodatneUsluge.CanUserAddRows = false;
                dgDodatneUsluge.AutoGeneratingColumn += new EventHandler<DataGridAutoGeneratingColumnEventArgs>(dgNaAkciji_AutoGeneratingColumn);
                dgDodatneUsluge.Width = 150;
                dgDodatneUsluge.Height = 300;
                dgDodatneUsluge.MaxHeight = 300;
                dgDodatneUsluge.MaxWidth = 150;
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
                    dgNaAkciji.DataContext = ((Akcija)Projekat.Instance.Izabrano);
                } catch (Exception) {
                }
            }
            if(tip == TipKlase.PRODAJA) {
                try {
                    dgProdatNamestaj.DataContext = ((Prodaja)Projekat.Instance.Izabrano);
                    dgDodatneUsluge.DataContext = ((Prodaja)Projekat.Instance.Izabrano);
                    //dgDodatneUsluge.ItemsSource = ((Prodaja)Projekat.Instance.Izabrano).DodatnaUsluga;
                    //dgProdatNamestaj.ItemsSource = ((Prodaja)Projekat.Instance.Izabrano).ProdatNamestaj;
                    double cena = ((Prodaja)Projekat.Instance.Izabrano).getUkupnaCena();
                    lblCenaBez.Content = $"Cena bez PDV-a: {cena.ToString()}";
                    cena *= 1.2;
                    lblCenaSa.Content = $"Cena sa PDV-om:  {cena.ToString()}";
                } catch (Exception) {
                }
            }
            
        }

        //TODO Dinamicki Databinding
        private void OsveziPrikaz() {
            //dgNamestaji.ItemsSource = null;
            Binding binding = new Binding("");
            switch (tip) {
                case TipKlase.NAMESTAJ:
                binding = new Binding("Namestaji");
                break;
                case TipKlase.AKCIJA:
                binding = new Binding("Akcije");
                break;
                case TipKlase.KORISNIK:
                binding = new Binding("Korisnici");
                //window.Title += "a";
                break;
                case TipKlase.PRODAJA:
                binding = new Binding("Prodaje");
                break;
                case TipKlase.SALON:
                binding = new Binding("Saloni");
                //window.Title += "a";
                break;
                case TipKlase.TIP_NAMESTAJA:
                binding = new Binding("TipoviNamestaja");
                //window.Title = $"Pregled tipova nameštaja";
                break;
                case TipKlase.DODATNE_USLUGE:
                binding = new Binding("DodatneUsluge");
                break;
                default:
                break;
            }
            BindingOperations.SetBinding(dgNamestaji, DataGrid.ItemsSourceProperty, binding);
            dgNamestaji.DataContext = Projekat.Instance;
            dgNamestaji.IsSynchronizedWithCurrentItem = true;
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
            if (Projekat.Instance.Izabrano == null) return;
            switch (tip) {
                case TipKlase.NAMESTAJ:
                new RadSaNamestajem((Namestaj)Projekat.Instance.Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.AKCIJA:
                new RadSaAkcijom((Akcija)Projekat.Instance.Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.KORISNIK:
                new RadSaKorisnikom((Korisnik)Projekat.Instance.Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.PRODAJA:
                new RadSaProdajom((Prodaja)Projekat.Instance.Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                new RadSaTipomNamestaja((TipNamestaja)Projekat.Instance.Izabrano.Clone()).ShowDialog();
                break;
                case TipKlase.DODATNE_USLUGE:
                new RadSaDodatnomUslugom((DodatnaUsluga)Projekat.Instance.Izabrano.Clone()).ShowDialog();
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
                Namestaj namestaj = (Namestaj)Projekat.Instance.Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj: {namestaj.Naziv}", "Brisanje nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    NamestajDataProvider.Instance.DeleteByID(namestaj.ID);
                }
                break;
                case TipKlase.AKCIJA:
                Akcija akcija = (Akcija)Projekat.Instance.Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(akcija.ID);
                }
                break;
                case TipKlase.KORISNIK:
                Korisnik korisnik = (Korisnik)Projekat.Instance.Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj korisnika: {korisnik.KorIme} ", "Brisanje korisnika", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    KorisnikDataProvider.Instance.DeleteByID(korisnik.ID);
                }
                break;
                case TipKlase.PRODAJA:
                Prodaja prodaja = (Prodaja)Projekat.Instance.Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete nameštaj akciju: ", "Brisanje akcije", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    AkcijaDataProvider.Instance.DeleteByID(prodaja.ID);
                }
                break;
                case TipKlase.SALON:
                break;
                case TipKlase.TIP_NAMESTAJA:
                TipNamestaja tipNamestaja = (TipNamestaja)Projekat.Instance.Izabrano;
                dialogResult = MessageBox.Show($"Jeste li sigurni da želite da obrišete tip nameštaja: {tipNamestaja.Naziv}", "Brisanje tipa nameštaja", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes) {
                    TipNamestajaDataProvider.Instance.DeleteByID(tipNamestaja.ID);
                }
                break;
                case TipKlase.DODATNE_USLUGE:
                DodatnaUsluga dodatnaUsluga = (DodatnaUsluga)Projekat.Instance.Izabrano;
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
                || e.Column.Header.ToString() == "DodatnaUsluga" || e.Column.Header.ToString() == "DodatneUsluge") {
                e.Cancel = true;
            }
            
        }

        private void cbObrisani_Click(object sender, RoutedEventArgs e) {
            if(cbObrisani.IsChecked == true) {
                switch (tip) {
                    case TipKlase.NAMESTAJ:
                    Projekat.Instance.Namestaji = NamestajDataProvider.Instance.GetAll(true);
                    break;
                    case TipKlase.AKCIJA:
                    Projekat.Instance.Akcije = AkcijaDataProvider.Instance.GetAll(true);
                    break;
                    case TipKlase.KORISNIK:
                    Projekat.Instance.Korisnici = KorisnikDataProvider.Instance.GetAll(true);
                    break;
                    case TipKlase.PRODAJA:
                    Projekat.Instance.Prodaje = ProdajaDataProvider.Instance.GetAll(true);
                    break;
                    case TipKlase.SALON:
                    
                    break;
                    case TipKlase.TIP_NAMESTAJA:
                    Projekat.Instance.TipoviNamestaja = TipNamestajaDataProvider.Instance.GetAll(true);
                    break;
                    case TipKlase.DODATNE_USLUGE:
                    Projekat.Instance.DodatneUsluge = DodatnaUslugaDataProvider.Instance.GetAll(true);
                    break;
                    default:
                    break;
                }
                
            } else {
                switch (tip) {
                    case TipKlase.NAMESTAJ:
                    Projekat.Instance.Namestaji = NamestajDataProvider.Instance.GetAll(false);
                    break;
                    case TipKlase.AKCIJA:
                    Projekat.Instance.Akcije = AkcijaDataProvider.Instance.GetAll(false);
                    break;
                    case TipKlase.KORISNIK:
                    Projekat.Instance.Korisnici = KorisnikDataProvider.Instance.GetAll(false);
                    break;
                    case TipKlase.PRODAJA:
                    Projekat.Instance.Prodaje = ProdajaDataProvider.Instance.GetAll(false);
                    break;
                    case TipKlase.SALON:

                    break;
                    case TipKlase.TIP_NAMESTAJA:
                    Projekat.Instance.TipoviNamestaja = TipNamestajaDataProvider.Instance.GetAll(false);
                    break;
                    case TipKlase.DODATNE_USLUGE:
                    Projekat.Instance.DodatneUsluge = DodatnaUslugaDataProvider.Instance.GetAll(false);
                    break;
                    default:
                    break;
                }
            }
        }
    }
}
