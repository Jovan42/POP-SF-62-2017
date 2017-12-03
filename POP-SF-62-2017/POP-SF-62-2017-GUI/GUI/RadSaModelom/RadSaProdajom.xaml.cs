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

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaProdajom.xaml
    /// </summary>
    /// 

    public partial class RadSaProdajom : Window {
        List<TextBox> textBoxes = new List<TextBox>();
        Prodaja prodaja = new Prodaja();
        bool izmena = false;

        //Konstruktor u slučaju da argument nije prosleđen (dodavanje novog objekta)
        public RadSaProdajom() {
            InitializeComponent();
            DrawCheckBoxes();
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

            tbDodatneUsluge.DataContext = prodaja;
            DrawCheckBoxes();
        }

        public void DrawCheckBoxes() {
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
                
                textBoxes.Add(text);

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
                DodatneUsluge = new List<string>(),
                ProdatNamestaj = new List<ProdatNamestaj>(),
            };

            if (izmena)
                prodaja.DatumProdaje = this.prodaja.DatumProdaje;
            else
                prodaja.DatumProdaje = DateTime.Now;

            //TODO Dodavati dodatne usluge i njhovu cenu klikom da dugme
                       
            foreach (TextBox text in textBoxes) {
                if (text.Text != null && text.Text != "" && Int32.Parse(text.Text) != 0) {
                    string id = text.Name.Substring(2, text.Name.Length - 2);
                    prodaja.ProdatNamestaj.Add(new ProdatNamestaj() {
                        NamestajID = Int32.Parse(id),
                        Kolicina = Int32.Parse(text.Text)
                    });
                }
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
    }
} 