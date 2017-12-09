using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.Model {
    public class DodatnaUsluga : Entitet {
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

        private double cena;

        public double Cena {
            get { return cena; }
            set { cena = value; onPropertyChanged("Cena"); }
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

        public object Clone() {
            return new DodatnaUsluga() {
                ID = id,
                Naziv = naziv,
                Cena = cena,
                Obrisan = obrisan
            };
        }
    }
}
