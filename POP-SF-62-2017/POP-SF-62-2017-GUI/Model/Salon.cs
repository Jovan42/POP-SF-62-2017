using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Salon : INotifyPropertyChanged {
        private int id;

        public int ID {
            get { return id; }
            set { id = value; onPropertyChanged("ID"); }
        }

        private string naziv;

        public string Naziv {
            get { return naziv; }
            set { naziv = value; onPropertyChanged("Naziv"); }
        }

        private string adresa;

        public string Adresa {
            get { return adresa; }
            set { adresa = value; onPropertyChanged("Adresa"); }
        }

        private string mail;

        public string Mail {
            get { return mail; }
            set { mail = value; onPropertyChanged("Mail"); }
        }

        private string sajt;

        public string Sajt {
            get { return sajt; }
            set { sajt = value; onPropertyChanged("Sajt"); }
        }

        private string telefon;

        public string Telefon {
            get { return telefon; }
            set { telefon = value; onPropertyChanged("Telefon"); }
        }

        private int pib;

        public int PIB {
            get { return pib; }
            set { pib = value; onPropertyChanged("PIB"); }
        }

        private int matBr;

        public int MatBr {
            get { return matBr; }
            set { matBr = value; onPropertyChanged("MatBr"); }
        }

        private int ziroRacun;

        public int ZiroRacun {
            get { return ziroRacun; }
            set { ziroRacun = value; onPropertyChanged("ZiroRacun"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        public Salon getCoppy() {
            return new Salon() {
                ID = id,
                Adresa = adresa,
                Naziv = naziv,
                Mail = mail,
                MatBr = matBr,
                Obrisan = obrisan,
                PIB = pib,
                Sajt = sajt,
                Telefon = telefon,
                ZiroRacun = ziroRacun,
            };
        }
    }
}
