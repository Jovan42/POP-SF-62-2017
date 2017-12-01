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
using POP_SF_62_2017_GUI.DataAccess;
using POP_SF_62_2017.Model;
using System.Collections.ObjectModel;
using POP_SF_62_2017_GUI.Model;

namespace POP_SF_62_2017_GUI.GUI {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window {
        public Login() {
            InitializeComponent();
            ObservableCollection<Entitet> a = AkcijaDataProvider.Instance.GetAll();
            foreach(Entitet ax in a) {
                tbUser.Text += " " + ((Akcija)ax).Popust.ToString();
            }
        }
        int brPokusaja = 3;
        private void btnLogIn_Click(object sender, RoutedEventArgs e) {
            if(UtilKorisnik.CheckPass(tbUser.Text, tbPass.Password)) {
                Meni meni = new Meni(UtilKorisnik.IsAdmin(tbUser.Text));
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
