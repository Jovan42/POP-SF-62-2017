using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.Model {
    public class DodatnaUsluga : Entitet {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        public object Clone() {
            throw new NotImplementedException();
        }
    }
}
