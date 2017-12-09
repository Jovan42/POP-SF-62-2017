using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaAkcijom.xaml
    /// </summary>
    public partial class RadSaAkcijom : Window {
        List<CheckBox> checkBoxes = new List<CheckBox>();
        Akcija akcija = new Akcija();
        bool izmena = false;

        //Konstruktor u slučaju da argument niej prosleđen (dodavanje novog objekta)
        public RadSaAkcijom() {           
            InitializeComponent();
            SetDataContexts();
            DrawCheckBoxes();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaAkcijom(Akcija akcija) {
            InitializeComponent();
            btnDodaj.Content = "Izmeni";
            izmena = true;

            this.akcija = akcija;
            SetDataContexts();
            DrawCheckBoxes();
        }

        private void SetDataContexts() {
            tbId.DataContext = this.akcija;
            calKraj.DataContext = this.akcija;
            calPocetak.DataContext = this.akcija;
            tbPopust.DataContext = this.akcija;
        }
        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e) {
            tbPopust.BorderBrush = System.Windows.Media.Brushes.Black;
            calKraj.BorderBrush = System.Windows.Media.Brushes.Black;
            calPocetak.BorderBrush = System.Windows.Media.Brushes.Black;
            try {
                double a;
                if (!Double.TryParse(tbPopust.Text, out a)) {
                    tbPopust.BorderBrush = System.Windows.Media.Brushes.Red;
                    tbPopust.Focus();
                    throw new Exception("Popust je pogrešno unet.");
                } 

                if(calKraj.SelectedDate < calPocetak.SelectedDate) {
                    calKraj.BorderBrush = System.Windows.Media.Brushes.Red;
                    calPocetak.BorderBrush = System.Windows.Media.Brushes.Red;
                    calKraj.Focus();
                    throw new Exception("Datumi su pogrešno uneti.");
                }
                if (izmena) {
                    AkcijaDataProvider.Instance.EditByID(this.akcija, Int32.Parse(tbId.Text));
                } else {
                    AkcijaDataProvider.Instance.Add(this.akcija);
                }

                Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
        }

        public void DrawCheckBoxes() {
            foreach (Namestaj namestaj in NamestajDataProvider.Instance.GetAll()) {
                StackPanel stackPanel = new StackPanel();

                CheckBox check = new CheckBox();
                check.Margin = new Thickness(5);
                check.Name = "cb" + namestaj.ID.ToString();
                if (izmena) {
                    check.IsChecked = akcija.IfAkcijaByNamestajID(namestaj.ID);
                    foreach (int namestajID in akcija.NamestajNaAkcijiID) {
                        foreach (CheckBox checkBox in checkBoxes) {
                            string tmp = "cb" + namestajID.ToString();
                            if (checkBox.Name == tmp)
                                checkBox.IsChecked = true;
                        }
                    }
                }

                Label label = new Label();
                label.Foreground = Brushes.White;
                label.Margin = new Thickness(5);
                stackPanel.Orientation = Orientation.Horizontal;
                Binding binding = new Binding("Naziv");
                BindingOperations.SetBinding(label, Label.ContentProperty, binding);
                label.DataContext = namestaj;
                checkBoxes.Add(check);
                stackPanel.Children.Add(check);
                stackPanel.Children.Add(label);
                spNamestaji.Children.Add(stackPanel);
            }
        }
    }
}
