using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel;
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
        Prodaja prodaja = new Prodaja();
        bool izmena = false;

        //Konstruktor u slučaju da argument nije prosleđen (dodavanje novog objekta)
        public RadSaProdajom() {
            InitializeComponent();
            DrawNamestaj();
            tbDatum.Text = DateTime.Now.ToString("dd. MM. yyy.");
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaProdajom(Prodaja prodaja) {
            InitializeComponent();
            this.prodaja = prodaja;
            window.Title = "Rad sa prodajom";
            izmena = true;
            btnDodaj.Content = "Izmeni";

            tbId.DataContext = prodaja;
            tbDatum.DataContext = prodaja;
            tbKupac.DataContext = prodaja;

            
            DrawNamestaj();
            DrawDodatneUsluge();
        }

        public void DrawDodatneUsluge() {
            foreach(DodatnaUsluga dodataUsluga in prodaja.DodatneUsluge) {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                TextBox tbUslugaNaziv = new TextBox();
                tbUslugaNaziv.Foreground = Brushes.Black;
                tbUslugaNaziv.Margin = new Thickness(5);
                tbUslugaNaziv.Width = 100;
                tbUslugaNaziv.Name = "tbUslugaNaziv" + prodaja.DodatneUsluge.IndexOf(dodataUsluga).ToString();
                tbUslugaNaziv.Text = dodataUsluga.Naziv;
                tbUslugaNaziv.DataContext = dodataUsluga;

                TextBox tbUslugaCena = new TextBox();
                tbUslugaCena.Foreground = Brushes.Black;
                tbUslugaCena.Margin = new Thickness(5);
                tbUslugaCena.Width = 30;
                tbUslugaCena.Name = "tbUslugaNaziv" + prodaja.DodatneUsluge.IndexOf(dodataUsluga).ToString();
                tbUslugaCena.Text = dodataUsluga.Cena.ToString();
                tbUslugaCena.DataContext = dodataUsluga;
                stackPanel.Children.Add(tbUslugaNaziv);
                stackPanel.Children.Add(tbUslugaCena);
                spDodatneUsluge.Children.Add(stackPanel);

                tbUslugeNaziv.Add(tbUslugaNaziv);
                tbUslugeCena.Add(tbUslugaCena);
            }
        }

        public void DrawNamestaj() {
            foreach (Entitet entitet in NamestajDataProvider.Instance.GetAll()) {
                StackPanel stackPanel = new StackPanel();
                Namestaj namestaj = (Namestaj)entitet;
                Label label = new Label();
                label.Foreground = Brushes.White;
                label.Margin = new Thickness(5);
                stackPanel.Orientation = Orientation.Horizontal;
                Binding binding = new Binding("Naziv");
                BindingOperations.SetBinding(label, Label.ContentProperty, binding);
                label.DataContext = namestaj;

                TextBox text = new TextBox();
                text.Height = 25;
                text.Width = 40;
                text.Name = "tb" + namestaj.ID.ToString();
                label.Margin = new Thickness(5);
                try {
                    text.Text = prodaja.getKolicinaFromNamestajID(namestaj.ID).ToString();
                } catch (Exception) {
                    text.Text = "";
                }
                
                tbNamestajiCena.Add(text);

                stackPanel.Children.Add(text);
                stackPanel.Children.Add(label);
                spNamestaji.Children.Add(stackPanel);
            }
        }

        public Prodaja getFromGUI() {
            Prodaja prodaja = new Prodaja() {
                Kupac = tbKupac.Text,
                ID = 0,
                Obrisan = false,
                DodatneUsluge = new List<DodatnaUsluga>(),
                ProdatNamestaj = new ObservableCollection<ProdatNamestaj>(),
            };

            if (izmena)
                prodaja.DatumProdaje = this.prodaja.DatumProdaje;
            else
                prodaja.DatumProdaje = DateTime.Now;
                       
            foreach (TextBox text in tbNamestajiCena) {
                if (text.Text != null && text.Text != "" && Int32.Parse(text.Text) != 0) {
                    string id = text.Name.Substring(2, text.Name.Length - 2);
                    prodaja.ProdatNamestaj.Add(new ProdatNamestaj() {
                        NamestajID = Int32.Parse(id),
                        Kolicina = Int32.Parse(text.Text)
                    });
                }
            }

            prodaja.DodatneUsluge = new List<DodatnaUsluga>();
            foreach (TextBox uslugCena in tbUslugeCena) {
                DodatnaUsluga dodatna = new DodatnaUsluga();
                try {
                    dodatna.Cena = Double.Parse(uslugCena.Text);
                    int index = tbUslugeCena.IndexOf(uslugCena);
                    dodatna.Naziv = tbUslugeNaziv[index].Text;
                } catch (Exception) {
                    MessageBox.Show("Pogrešno uneta cena", "Greška");
                }
                prodaja.DodatneUsluge.Add(dodatna);
            }
            
            return prodaja;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            if (izmena) {
                ProdajaDataProvider.Instance.EditByID(getFromGUI(), Int32.Parse(tbId.Text));
            } else {
                ProdajaDataProvider.Instance.Add(getFromGUI());
            }
            Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodajUslugu_Click(object sender, RoutedEventArgs e) {
            foreach(TextBox naziv in tbUslugeNaziv) {
                if(naziv.Text == "") {
                    MessageBox.Show("Popunete prethodna polaj da bi ste dodali novu uslugu", "Greška");
                    return;
                }
            }
            foreach (TextBox naziv in tbUslugeCena) {
                if (naziv.Text == "") {
                    MessageBox.Show("Popunete prethodna polaj da bi ste dodali novu uslugu", "Greška");
                    return;
                }
            }
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            TextBox tbUslugaNaziv = new TextBox();
            tbUslugaNaziv.Foreground = Brushes.White;
            tbUslugaNaziv.Margin = new Thickness(5);
            tbUslugaNaziv.Width = 100;
            tbUslugaNaziv.Name = "tbUslugaNaziv" + tbUslugeNaziv.Count;
            tbUslugaNaziv.Foreground = Brushes.Black;
            tbUslugeNaziv.Add(tbUslugaNaziv);
            stackPanel.Children.Add(tbUslugaNaziv);

            TextBox tbUslugaCena = new TextBox();
            tbUslugaCena.Foreground = Brushes.White;
            tbUslugaCena.Margin = new Thickness(5);
            tbUslugaCena.Width = 30;
            tbUslugaCena.Foreground = Brushes.Black;
            tbUslugaCena.Name = "tbUslugaCena" + tbUslugeCena.Count;
            tbUslugeCena.Add(tbUslugaCena);
            stackPanel.Children.Add(tbUslugaCena);

            spDodatneUsluge.Children.Add(stackPanel);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e) {
            spDodatneUsluge.Children.RemoveRange(0, spDodatneUsluge.Children.Count);
            tbUslugeNaziv = new List<TextBox>();
            tbUslugeCena = new List<TextBox>();
            btnObrisi = new List<Button>();
        }
    }
} 