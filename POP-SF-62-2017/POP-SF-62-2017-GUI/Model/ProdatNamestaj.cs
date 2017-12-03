using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.Model {
    public class ProdatNamestaj: Entitet{

        #region Fields and properties
        private int namestajID;

        public int NamestajID {
            get { return namestajID; }
            set { namestajID = value; OnPropertyChanged("Namestaj"); }
        }

        private int kolicina;

        public int Kolicina {
            get { return kolicina; }
            set { kolicina = value; OnPropertyChanged("Kolicina"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        protected void OnPropertyChanged(string properyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }

        public object Clone() {
            throw new NotImplementedException();
        }
    }
}
