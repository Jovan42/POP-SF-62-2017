using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Model.TipNamestaja
//  - int ID
//  - int Naziv
//  - bool Obrisan

namespace POP_SF_62_2017.Model {
    public class TipNamestaja : Entitet {

        #region Fields and properties
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

        private bool obrisan;

        public bool Obrisan {
            get { return obrisan; }
            set { obrisan = value; onPropertyChanged("Obrisan"); }
        }
        public static TipNamestaja GetById(int id) {
            foreach (TipNamestaja tipNamestaja in Projekat.Instance.TipoviNamestaja) {
                if (tipNamestaja.ID == id) {
                    return tipNamestaja;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        public override string ToString() {
            return $"{Naziv}";
        }

        protected void onPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }

        }

        public object Clone() {
            return new TipNamestaja { ID = id, Naziv = naziv, Obrisan = obrisan };
        }
    }
}
