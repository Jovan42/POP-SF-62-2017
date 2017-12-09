using System.Windows;
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017.Model;
using System.Collections.ObjectModel;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
            
            if(KorisnikDataProvider.Instance.CheckPass(tbUser.Text, tbPass.Password)) {
                Meni meni = new Meni(KorisnikDataProvider.Instance.IsAdmin(tbUser.Text));
                meni.Show();
                this.Close();
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


    }
}
