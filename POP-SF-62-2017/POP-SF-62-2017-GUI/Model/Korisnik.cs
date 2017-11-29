using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model {
    public class Korisnik : INotifyPropertyChanged {
        private int id;

        public int ID {
            get { return id; }
            set { id = value; onPropertyChanged("ID"); }
        }

        private string ime;

        public string Ime {
            get { return ime; }
            set { ime = value; onPropertyChanged("Ime"); }
        }

        private string prezime;

        public string Prezime {
            get { return prezime; }
            set { prezime = value; onPropertyChanged("Prezime"); }
        }

        private string korIme;

        public string KorIme {
            get { return korIme; }
            set { korIme = value; onPropertyChanged("KorIme"); }
        }

        private string lozinka;

        public string Lozinka {
            get { return lozinka; }
            set { lozinka = value; onPropertyChanged("Lozinka"); }
        }

        private bool admin;

        public bool Admin {
            get { return admin; }
            set { admin = value; onPropertyChanged("Admin"); }
        }

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }

        public override string ToString() {
            string tip = Admin ? "Admin" : "Korisnik";
            return $"{tip} - {KorIme}: {Ime} {Prezime}";
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }
    }
}
