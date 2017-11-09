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
using POP_SF_62_2017.Util.Model;

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login() {
            InitializeComponent();
        }
        int brPokusaja = 3;
        private void btnLogIn_Click(object sender, RoutedEventArgs e) {
            if(UtilKorisnik.CheckPass(tbUser.Text, tbPass.Password)) {

            } else {
                brPokusaja--;
                MessageBox.Show($"Uneli ste pogreštno korinsičko ime i šifru, pokušajte opet... \n\nBroj preostalih pokušaja: {brPokusaja}\n", "Greška");
                if(brPokusaja == 0) {
                    this.Close();
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                if (UtilKorisnik.CheckPass(tbUser.Text, tbPass.Password)) {

                } else {
                    brPokusaja--;
                    MessageBox.Show($"Uneli ste pogreštno korinsičko ime i šifru, pokušajte opet... \n\nBroj preostalih pokušaja: {brPokusaja}\n", "Greška");
                    if (brPokusaja == 0) {
                        this.Close();
                    }
                }
            }
        }
    }
}
