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

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaProdajom.xaml
    /// </summary>
    /// 
    
    public partial class RadSaProdajom : Window{
        List<TextBox> textBoxes = new List<TextBox>();
        public RadSaProdajom() {
            InitializeComponent();
            DrawCheckBoxes();
            tbDatum.Text = DateTime.Now.ToString("dd. MM. yyy.");
        }
        public RadSaProdajom(Prodaja prodaja) {
            InitializeComponent();
            DrawCheckBoxes();
            window.Title = "Rad sa prodajom";
            tbDatum.Text = prodaja.DatumProdaje.ToString("dd. MM. yyy.");
            tbId.Text = prodaja.ID.ToString();
            tbKupac.Text = prodaja.Kupac;
            tbDodatneUsluge.Text = String.Join(", ", prodaja.DodatneUsluge.ToArray());

            foreach (int namestajID in prodaja.ProdatNamestaj) {
                foreach (TextBox textBox in textBoxes) {
                    string tmp = "tb" + namestajID.ToString();
                    if (textBox.Name == tmp)
                        textBox.Text = prodaja.Kolicina[prodaja.ProdatNamestaj.IndexOf(namestajID)].ToString();
                }
            }
        }

        public void DrawCheckBoxes() {

            foreach (Namestaj namestaj in UtilNamestaj.getAll()) {
                StackPanel stackPanel = new StackPanel();

                Label label = new Label();
                label.Foreground = Brushes.White;
                label.Content = namestaj.Naziv;
                label.Margin = new Thickness(5);
                stackPanel.Orientation = Orientation.Horizontal;

                TextBox text = new TextBox();
                text.Height = 25;
                text.Width = 40;
                text.Name = "tb" + namestaj.ID.ToString();
                label.Margin = new Thickness(5);
                textBoxes.Add(text);

                stackPanel.Children.Add(text);
                stackPanel.Children.Add(label);
                
                spNamestaji.Children.Add(stackPanel);

            }
        }

        public Prodaja getFromGUI() {
            Prodaja prodaja = new Prodaja() {
                DatumProdaje = DateTime.Now,
                Kupac = tbKupac.Text,
                ID = 0,
                Obrisan = false,
                DodatneUsluge = new List<string>(),
                ProdatNamestaj = new List<int>(),
                Kolicina = new List<int>()
            };
            prodaja.DodatneUsluge = tbDodatneUsluge.Text.Split(',').ToList();
            foreach (string usluga in prodaja.DodatneUsluge) {
                usluga.Trim();
            }
            
            foreach (TextBox text in textBoxes) {
                if (text.Text != null && text.Text != "" && Int32.Parse(text.Text) != 0) {
                    string id = text.Name.Substring(2, text.Name.Length - 2);
                    prodaja.ProdatNamestaj.Add(Int32.Parse(id));
                    prodaja.Kolicina.Add(Int32.Parse(text.Text));
                }
            }
            return prodaja;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            UtilProdaja.Add(getFromGUI());
            Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
