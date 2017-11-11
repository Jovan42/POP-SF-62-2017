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
using POP_SF_62_2017.Model;
using POP_SF_62_2017.Util.Model;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom
{
    /// <summary>
    /// Interaction logic for RadSaAkcijom.xaml
    /// </summary>
    public partial class RadSaAkcijom : Window
    {
        List<CheckBox> checkBoxes = new List<CheckBox>();
        bool izmena = false;

        public RadSaAkcijom()
        {           
            InitializeComponent();
            DrawCheckBoxes();
        }

        public RadSaAkcijom(Akcija akcija) {
            InitializeComponent();
            btnDodaj.Content = "Izmeni";
            DrawCheckBoxes();
            izmena = true;

            foreach (int namestajID in akcija.NamestajNaAkcijiID) {
                foreach (CheckBox checkBox in checkBoxes) {
                    string tmp = "cb" + namestajID.ToString();
                    if (checkBox.Name == tmp)
                        checkBox.IsChecked = true;
                }
            }

            calKraj.DisplayDate = akcija.Kraj;
            calPocetak.DisplayDate = akcija.Pocetak;
            tbId.Text = akcija.ID.ToString();
            tbPopust.Text = akcija.Popust.ToString();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            try {
                double a;
                if(!Double.TryParse(tbPopust.Text, out a))
                    throw new Exception("Popust je pogrešno unet.");

                if (izmena) {
                    UtilAkcija.ChangeById(getFromGUI(), Int32.Parse(tbId.Text));
                    Close();
                } else {
                    UtilAkcija.Add(getFromGUI());
                    Close();
                }
            } catch (Exception ex) {

                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
            

        }

        public Akcija getFromGUI() {
            Akcija akcija = new Akcija() {
                Kraj = calKraj.DisplayDate,
                Pocetak = calPocetak.DisplayDate,
                Popust = Double.Parse(tbPopust.Text),
                Obrisan = false,
                NamestajNaAkcijiID = new List<int>()
            };

            foreach (CheckBox checkBox in checkBoxes) {
                if (checkBox.IsChecked == true) {
                    string id = checkBox.Name.Substring(2, checkBox.Name.Length - 2);
                    akcija.NamestajNaAkcijiID.Add(Int32.Parse(id));
                }
            }

            if (izmena)
                akcija.ID = Int32.Parse(tbId.Text);

            return akcija;
        }

        public void DrawCheckBoxes() {
            foreach (Namestaj namestaj in UtilNamestaj.getAll()) {
                StackPanel stackPanel = new StackPanel();

                CheckBox check = new CheckBox();
                check.Margin = new Thickness(5);
                check.Name = "cb" + namestaj.ID.ToString();
                Label label = new Label();
                label.Foreground = Brushes.White;
                label.Content = namestaj.Naziv;
                label.Margin = new Thickness(5);
                stackPanel.Orientation = Orientation.Horizontal;
                checkBoxes.Add(check);
                stackPanel.Children.Add(check);
                stackPanel.Children.Add(label);
                spNamestaji.Children.Add(stackPanel);
            }
        }
    }
}
