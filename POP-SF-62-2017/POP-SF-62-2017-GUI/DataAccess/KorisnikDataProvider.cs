using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class KorisnikDataProvider : DataAccess {
        public static KorisnikDataProvider Instance { get; } = new KorisnikDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Korisnik korisnik = (Korisnik)e;
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            korisnik.ID = korisnici.Count();
            korisnici.Add(korisnik);
            Projekat.Instance.SetKorisnici(korisnici);
        }

        public bool DeleteByID(int id) {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            foreach (Korisnik akcija in korisnici) {
                if (akcija.ID == id) {
                    akcija.Obrisan = true;
                    Projekat.Instance.SetKorisnici(korisnici);
                    return true;

                }
            }
            return false;
        }

        public bool EditByID(Entitet e, int id) {
            Korisnik k = (Korisnik)e;
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.ID == id) {
                    korisnik.Admin = k.Admin;
                    korisnik.Ime = k.Ime;
                    korisnik.KorIme = k.KorIme;
                    korisnik.Lozinka = k.Lozinka;
                    korisnik.Obrisan = k.Obrisan;
                    korisnik.Prezime = k.Prezime;
                    Projekat.Instance.SetKorisnici(korisnici);
                    return true;

                }
            }
            return false;
        }

        public ObservableCollection<Korisnik> GetAll() {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            foreach (Korisnik korisnk in Projekat.Instance.Korisnici) {
                if (!korisnk.Obrisan)
                    korisnici.Add(korisnk);
            }
            return korisnici;
        }

        public Entitet GetByID(int id) {
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
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

        // Proverava da li je korisnik admin
        public bool IsAdmin(string username) {
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                if (korisnik.KorIme == username) {
                    return korisnik.Admin;
                }
            }
            return false;
        }

        // Proverava da li je username i password
        public bool CheckPass(string user, string pass) {
            ObservableCollection<Korisnik> korisnici = Projekat.Instance.Korisnici;

            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.KorIme == user && korisnik.Lozinka == pass) {
                    return true;
                }
            }
            return false;
        }
    }
}
