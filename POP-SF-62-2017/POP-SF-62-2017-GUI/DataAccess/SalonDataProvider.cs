using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class SalonDataProvider : DataAccess {
        public static SalonDataProvider Instance { get; } = new SalonDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Salon salon = (Salon)e;
            ObservableCollection<Salon> saloni = new ObservableCollection<Salon>();
            saloni = Projekat.Instance.Saloni;
            salon.ID = saloni.Count();
            saloni.Add(salon);
            Projekat.Instance.SetSaloni(saloni);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<Salon> saloni = new ObservableCollection<Salon>();
            saloni = Projekat.Instance.Saloni;
            foreach (Salon salon in saloni) {
                if (salon.ID == id) {
                    salon.Obrisan = true;
                    Projekat.Instance.SetSaloni(saloni);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            Salon s = (Salon)e;
            ObservableCollection<Salon> saloni = new ObservableCollection<Salon>();
            saloni = Projekat.Instance.Saloni;
            foreach (Salon salon in saloni) {
                if (salon.ID == id) {
                    if (salon.Equals(s)) return true;

                    salon.Naziv = s.Naziv;
                    salon.Mail = s.Mail;
                    salon.MatBr = s.MatBr;
                    salon.Obrisan = s.Obrisan;
                    salon.PIB = s.PIB;
                    salon.Sajt = s.Sajt;
                    salon.Telefon = s.Telefon;
                    salon.ZiroRacun = s.ZiroRacun;
                    salon.Adresa = s.Adresa;
                    Projekat.Instance.SetSaloni(saloni);
                    return true;

                }
            }
            return false;
        }

        public ObservableCollection<Salon> GetAll() {
            ObservableCollection<Salon> salon = new ObservableCollection<Salon>();
            foreach (Salon korisnk in Projekat.Instance.Saloni) {
                if (!korisnk.Obrisan)
                    salon.Add(korisnk);
            }
            return salon;
        }

        public Entitet GetByID(int id) {
            foreach (Salon korisnik in Projekat.Instance.Saloni) {
                if (korisnik.ID == id) {
                    return korisnik;
                }
            }
            return null;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
