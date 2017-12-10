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
        List<Namestaj> namestaji = new List<Namestaj>();
        Akcija akcija = new Akcija();
        bool izmena = false;

        //Konstruktor u slučaju da argument niej prosleđen (dodavanje novog objekta)
        public RadSaAkcijom() {           
            InitializeComponent();
            SetDataContexts();
        }

        //Konstruktor u slučaju da je argument prosleđen (izmena postojećeg)
        public RadSaAkcijom(Akcija akcija) {
            InitializeComponent();
            btnDodaj.Content = "Izmeni";
            izmena = true;

            this.akcija = akcija;
            SetDataContexts();
            foreach (Namestaj namestaj in akcija.NamestajNaAkciji) {
                DodajNamestajUAkciju(namestaj);
            }

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
                
                if(GetNamesetajNaAkciji() == null) {
                    throw new Exception("Jedan ili više nameštaja su uneti dva ili više puta.");
                }
                akcija.NamestajNaAkcijiID = GetNamesetajNaAkciji();

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

        private List<int> GetNamesetajNaAkciji() {
            List<StackPanel> stackPanels = new List<StackPanel>();
            List<int> tmp = new List<int>();
            foreach (StackPanel stackPanel in spNamestaji.Children) {
                ComboBox cb = (ComboBox)stackPanel.Children[0];
                Namestaj n = (Namestaj)cb.SelectedItem;
                if (tmp.Contains(n.ID))
                    return null;
                tmp.Add(n.ID);
            }
            
            return tmp;
        }

        private void DodajNamestajUAkciju(Namestaj namestaj) {
            StackPanel stackPanel = new StackPanel();
            ComboBox comboBox = new ComboBox();
            Button button = new Button();
            button.Content = "-";
            button.Width = 20;
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF252526");
            button.Foreground = Brushes.White;

            comboBox.Width = 240;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = NamestajDataProvider.Instance.GetAll();
            button.Click += new RoutedEventHandler(btnDeleteNamestaj);
            if(namestaj != null) {
                comboBox.SelectedItem = namestaj;
            }

            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Thickness(5);
            stackPanel.Children.Add(comboBox);
            stackPanel.Children.Add(button);
            spNamestaji.Children.Add(stackPanel);
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e) {
            DodajNamestajUAkciju(null);
        }

        private void btnDeleteNamestaj(object sender, RoutedEventArgs e) {
            Button btn = (Button)sender;
            StackPanel sp  = (StackPanel)btn.Parent;
            ((StackPanel)sp.Parent).Children.Remove(sp);
            
        }
    }
}
