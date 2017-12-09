using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class DodatnaUslugaDataProvider : DataAccess {
        public static DodatnaUslugaDataProvider Instance { get; } = new DodatnaUslugaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            DodatnaUsluga dodatnaUsluga = (DodatnaUsluga)e;
            ObservableCollection<DodatnaUsluga> dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            dodatneUsluge = Projekat.Instance.DodatneUsluge;
            dodatnaUsluga.ID = dodatneUsluge.Count();
            dodatneUsluge.Add(dodatnaUsluga);
            Projekat.Instance.SetDodatneUsluge(dodatneUsluge);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<DodatnaUsluga> dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            dodatneUsluge = Projekat.Instance.DodatneUsluge;
            foreach (DodatnaUsluga dodatnaUsluga in dodatneUsluge) {
                if (dodatnaUsluga.ID == id) {
                    dodatnaUsluga.Obrisan = true;
                    Projekat.Instance.SetDodatneUsluge(dodatneUsluge);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            DodatnaUsluga d = (DodatnaUsluga)e;
            ObservableCollection<DodatnaUsluga> dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            dodatneUsluge = Projekat.Instance.DodatneUsluge;
            foreach (DodatnaUsluga dodatnaUsluga in dodatneUsluge) {
                if (dodatnaUsluga.ID == id) {
                    dodatnaUsluga.Naziv = d.Naziv;
                    dodatnaUsluga.Cena = d.Cena;
                    dodatnaUsluga.Obrisan = d.Obrisan;
                    Projekat.Instance.SetDodatneUsluge(dodatneUsluge);
                    return true;

                }
            }
            return false;
        }

        public Entitet GetByID(int id) {
            foreach (DodatnaUsluga dodatnaUsluga in Projekat.Instance.DodatneUsluge) {
                if (dodatnaUsluga.ID == id) {
                    return dodatnaUsluga;
                }
            }
            return null;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        #endregion

        public ObservableCollection<DodatnaUsluga> GetAll() {
            ObservableCollection<DodatnaUsluga> dodatneUsluge = new ObservableCollection<DodatnaUsluga>();
            foreach (DodatnaUsluga dodatnaUsluga in Projekat.Instance.DodatneUsluge) {
                if (!dodatnaUsluga.Obrisan)
                    dodatneUsluge.Add(dodatnaUsluga);
            }
            return dodatneUsluge;
        }
    }
}
