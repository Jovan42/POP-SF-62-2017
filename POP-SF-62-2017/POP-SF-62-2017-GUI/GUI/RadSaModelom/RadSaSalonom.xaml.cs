﻿using System;
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

namespace POP_SF_62_2017_GUI.GUI.RadSaModelom {
    /// <summary>
    /// Interaction logic for RadSaSalonom.xaml
    /// </summary>
    public partial class RadSaSalonom : Window {
  
        public RadSaSalonom(Salon salon) {
            InitializeComponent();
            tbNaziv.Text = salon.Naziv;
            tbAdresa.Text = salon.Adresa;
            tbMail.Text = salon.Mail;
            tbSajt.Text = salon.Sajt;
            tbTelefon.Text = salon.Telefon;
            tbPIB.Text = salon.PIB.ToString();
            tbMatBr.Text = salon.MatBr.ToString();
            tbRacun.Text = salon.ZiroRacun.ToString();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e) {
            try {
                int a;
                if (!Int32.TryParse(tbPIB.Text, out a))
                    throw new Exception("PIB salona je pogrešno unet.");
                if (!Int32.TryParse(tbMatBr.Text, out a))
                    throw new Exception("Matični broj salona je pogrešno unet.");
                if (!Int32.TryParse(tbRacun.Text, out a))
                    throw new Exception("Žiro račun salona je pogrešno unet.");

                UtilSalon.ChangeById(getFromGUI(), 0);
                this.Close();
            } catch (Exception ex) {
                MessageBox.Show($"{ex.Message}. Pokušajte opet.", "Greška");
            }
            
        }

        private Salon getFromGUI() {
            return new Salon() {
                Naziv = tbNaziv.Text,
                Adresa = tbAdresa.Text,
                Mail = tbMail.Text,
                Sajt = tbSajt.Text,
                Telefon = tbTelefon.Text,
                PIB = Int32.Parse(tbPIB.Text),
                MatBr = Int32.Parse(tbMatBr.Text),
                ZiroRacun = Int32.Parse(tbRacun.Text),
                ID = 0
            };
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}