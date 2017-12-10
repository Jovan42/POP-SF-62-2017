using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace POP_SF_62_2017_GUI.DataAccess {
    class AkcijaDataProvider : DataAccess {
        public static AkcijaDataProvider Instance { get; } = new AkcijaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Akcija akcija = (Akcija)e;
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            akcije = Projekat.Instance.Akcije;
            akcija.ID = akcije.Count();
            akcije.Add(akcija);
            Projekat.Instance.SetAkcije(akcije);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            akcije = Projekat.Instance.Akcije;
            foreach (Akcija akcija in akcije) {
                if (akcija.ID == id) {
                    akcija.Obrisan = true;
                    Projekat.Instance.SetAkcije(akcije);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            Akcija a = (Akcija)e;
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            akcije = Projekat.Instance.Akcije;
            foreach (Akcija akcija in akcije) {
                if (akcija.ID == id) {
                    akcija.Kraj = a.Kraj;
                    akcija.NamestajNaAkcijiID = a.NamestajNaAkcijiID;
                    akcija.Obrisan = a.Obrisan;
                    akcija.Pocetak = a.Pocetak;
                    akcija.Popust = a.Popust;
                    Projekat.Instance.SetAkcije(akcije);
                    return true;

                }
            }
            return false;
        }

        public Entitet GetByID(int id) {
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (akcija.ID == id) {
                    return akcija;
                }
            }
            return null;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        #endregion

        public ObservableCollection<Akcija> GetAll() {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (!akcija.Obrisan)
                    akcije.Add(akcija);
            }
            return akcije;
        }

        public ObservableCollection<Akcija> GetActiveAkcije() {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            foreach (Akcija akcija in Projekat.Instance.Akcije) {
                if (!akcija.Obrisan && akcija.Pocetak < DateTime.Now && akcija.Kraj > DateTime.Now)
                    akcije.Add(akcija);
            }
            return akcije;
        }
    }
}
