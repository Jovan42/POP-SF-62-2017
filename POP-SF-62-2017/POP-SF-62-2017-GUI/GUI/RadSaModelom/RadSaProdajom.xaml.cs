using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaProdajom.xaml
    /// </summary>
    /// 

    public partial class RadSaProdajom : Window {
        List<TextBox> tbNamestajiCena = new List<TextBox>();
        List<TextBox> tbUslugeNaziv = new List<TextBox>();
        List<TextBox> tbUslugeCena =  new List<TextBox>();
        List<Button> btnObrisi = new List<Button>();
        ObservableCollection<ProdatNamestaj> oldProdatNamestaj = new ObservableCollection<ProdatNamestaj>();
        Prodaja prodaja = new Prodaja();
        bool izmena = false;

        //Konstruktor u slučaju da argument nije prosleđen (dodavanje novog objekta)
        public RadSaProdajom() {
            InitializeComponent();
            this.prodaja.DatumProdaje = DateTime.Now;
            SetDataContexts();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaProdajom(Prodaja prodaja) {
            InitializeComponent();
            this.prodaja = prodaja;
            window.Title = "Rad sa prodajom";
            izmena = true;
            btnDodaj.Content = "Izmeni";
            SetDataContexts();
            foreach (ProdatNamestaj prodatNamestaj in prodaja.ProdatNamestaj) {
                DodajNamestajUProdaju(prodatNamestaj);
            }
            foreach (int usluga in prodaja.DodatneUslugeID) {
                try {
                    DodajUsluguUProdaju((DodatnaUsluga)DodatnaUslugaDataProvider.Instance.GetByID(usluga));
                } catch (Exception) {
                }
            }
            oldProdatNamestaj = GetProdatNamestaj();
        }

        private void SetDataContexts() {
            tbId.DataContext = prodaja;
            tbDatum.DataContext = prodaja;
            tbKupac.DataContext = prodaja;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            tbKupac.BorderBrush = System.Windows.Media.Brushes.Black;
            try {
                if(tbKupac.Text.Trim() == "") {
                    tbKupac.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbKupac.Focus();
                    throw new Exception("Popust je pogrešno unet.");
                }

                if (GetProdatNamestaj() == null) {
                    throw new Exception("Jedana ili više cena nameštaja je unesena pogrešno.");
                }
                prodaja.ProdatNamestaj = GetProdatNamestaj();
               

                foreach (ProdatNamestaj pNamestaj in prodaja.ProdatNamestaj) {
                    foreach (ProdatNamestaj oldPNamestaj in oldProdatNamestaj) {
                        if(pNamestaj.NamestajID == oldPNamestaj.NamestajID) {
                            if(oldPNamestaj.Kolicina < pNamestaj.Kolicina) {
                                Namestaj namestaj = (Namestaj)NamestajDataProvider.Instance.GetByID(pNamestaj.NamestajID);
                                if (namestaj.Kolicina - pNamestaj.Kolicina < 0) {
                                    throw new Exception($"Ne postoji dovoljno nameštaja ({namestaj.Naziv}) u magacinu.");
                                }
                            }
                        }
                    }
                    Namestaj namestaj2 = (Namestaj)NamestajDataProvider.Instance.GetByID(pNamestaj.NamestajID);
                    if (namestaj2.Kolicina - pNamestaj.Kolicina < 0) {
                        throw new Exception($"Ne postoji dovoljno nameštaja ({namestaj2.Naziv}) u magacinu.");
                    } else {
                        namestaj2.Kolicina -= pNamestaj.Kolicina;
                        NamestajDataProvider.Instance.EditByID(namestaj2, namestaj2.ID);
                    }
                    
                }
                if (GetDodatneUsluge() == null) {
                    throw new Exception("Jedana ili više usluga su uneti dva ili više puta.");
                }
                prodaja.DodatneUslugeID = GetDodatneUsluge();

                if (izmena) {
                    ProdajaDataProvider.Instance.EditByID(this.prodaja, Int32.Parse(tbId.Text));
                } else {
                    ProdajaDataProvider.Instance.Add(this.prodaja);
                }

                foreach (ProdatNamestaj pNamestaj in prodaja.ProdatNamestaj) {
                    foreach (ProdatNamestaj oldPNamestaj in oldProdatNamestaj) {
                        if (pNamestaj.NamestajID == oldPNamestaj.NamestajID) {
                            if (oldPNamestaj.Kolicina < pNamestaj.Kolicina) {
                                Namestaj namestaj = (Namestaj)NamestajDataProvider.Instance.GetByID(pNamestaj.NamestajID);
                                namestaj.Kolicina -= pNamestaj.Kolicina - oldPNamestaj.Kolicina;
                                NamestajDataProvider.Instance.EditByID(namestaj, namestaj.ID);
                                break;
                            } else if (oldPNamestaj.Kolicina > pNamestaj.Kolicina) {
                                Namestaj namestaj = (Namestaj)NamestajDataProvider.Instance.GetByID(pNamestaj.NamestajID);
                                namestaj.Kolicina += oldPNamestaj.Kolicina - pNamestaj.Kolicina;
                                NamestajDataProvider.Instance.EditByID(namestaj, namestaj.ID);
                                break;
                            }
                        }
                    }
                   
                }

                foreach (ProdatNamestaj pNamestaj in prodaja.ProdatNamestaj) {
                    Namestaj namestaj = (Namestaj)NamestajDataProvider.Instance.GetByID(pNamestaj.NamestajID);
                   
                }
                Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodajUslugu_Click(object sender, RoutedEventArgs e) {
            DodajUsluguUProdaju(null);
        }

        private void DodajUsluguUProdaju(DodatnaUsluga usluga) {
            StackPanel stackPanel = new StackPanel();
            ComboBox comboBox = new ComboBox();
            Button button = new Button();
            button.Content = "-";
            button.Width = 20;
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF252526");
            button.Foreground = Brushes.White;

            comboBox.Width = 100;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = DodatnaUslugaDataProvider.Instance.GetAll();
            button.Click += new RoutedEventHandler(btnDeleteNamestaj);
            if (usluga != null) {
                comboBox.SelectedIndex = usluga.ID -1;
            }

            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(5);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(button);
            spUsluge.Children.Add(stackPanel);
        }
        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e) {
            DodajNamestajUProdaju(null);
        }
        private void DodajNamestajUProdaju(ProdatNamestaj namestaj) {
            StackPanel stackPanel = new StackPanel();
            ComboBox comboBox = new ComboBox();
            Button button = new Button();
            TextBox textBox = new TextBox();
            button.Content = "-";
            button.Width = 20;
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF252526");
            button.Foreground = Brushes.White;

            comboBox.Width = 200;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = NamestajDataProvider.Instance.GetAll();
            button.Click += new RoutedEventHandler(btnDeleteNamestaj);
            if (namestaj != null) {
                comboBox.SelectedIndex = namestaj.NamestajID-1;
                textBox.Text = namestaj.Kolicina.ToString();
            }

            textBox.Width = 50;

            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(5);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(button);
            spNamestaji.Children.Add(stackPanel);
        }

        private void btnDeleteNamestaj(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;
            StackPanel sp = (StackPanel)btn.Parent;
            ((StackPanel)sp.Parent).Children.Remove(sp);
        }

        private ObservableCollection<ProdatNamestaj> GetProdatNamestaj() {
            List<StackPanel> stackPanels = new List<StackPanel>();
            ObservableCollection<ProdatNamestaj> tmp = new ObservableCollection<ProdatNamestaj>();
            foreach (StackPanel stackPanel in spNamestaji.Children) {
                ComboBox cb = (ComboBox)stackPanel.Children[0];
                TextBox tb = (TextBox)stackPanel.Children[1];
                Namestaj n = (Namestaj)cb.SelectedItem;
                int a;
                if (!Int32.TryParse(tb.Text, out a)|| Int32.Parse(tb.Text) < 0)
                    return null;

                tmp.Add(new ProdatNamestaj() {
                    NamestajID = n.ID,
                    Kolicina = Int32.Parse(tb.Text)
                });
            }
            return tmp;
        }

        private List<int> GetDodatneUsluge() {
            List<StackPanel> stackPanels = new List<StackPanel>();
            List<int> tmp = new List<int>();
            foreach (StackPanel stackPanel in spUsluge.Children) {
                ComboBox cb = (ComboBox)stackPanel.Children[0];
                DodatnaUsluga n = (DodatnaUsluga)cb.SelectedItem;
                if (tmp.Contains(n.ID))
                    return null;
                tmp.Add(n.ID);
            }

            return tmp;
        }
    }
} 