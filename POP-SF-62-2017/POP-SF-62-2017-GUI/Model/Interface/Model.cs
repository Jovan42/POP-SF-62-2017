using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Model.Interface {
    class Model : IntefaceModel {

        public void Add(object o) {
            Korisnik k = (Korisnik) o;
            Console.WriteLine(k.Ime);
        }

        public bool ChangeById(int id) {
            return true;
        }

        public bool DeleteById() {
            return true;
        }

        public object getById() {
            return new Object();
        }

        public void Initialize() {
            
        }
    }
}
