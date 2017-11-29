using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    public class UtilKorisnik {

        public static bool IsAdmin(string username) {
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                if (korisnik.KorIme == username) {
                    return korisnik.Admin;
                }
            }
            return false;
        }
        public static Korisnik GetById(int id) {
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                if (korisnik.ID == id) {
                    return korisnik;
                }
            }
            return null;
        }
        public static bool CheckPass(string user, string pass) {
            ObservableCollection<Korisnik> korisnici = Projekat.Instance.Korisnici;

            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.KorIme == user && korisnik.Lozinka == pass) {
                    return true;
                }
            }
            return false;
        } 

        public static bool DeleteById(int id) {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.ID == id) {
                    korisnik.Obrisan = true;
                    Projekat.Instance.SetKorisnici(korisnici);
                    return true;

                }
            }
            return false;
        }

        public static void Add(Korisnik a) {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            a.ID = korisnici.Count();
            korisnici.Add(a);
            Projekat.Instance.SetKorisnici(korisnici);
        }

        public static bool ChangeById(Korisnik k, int id) {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.ID == id) {
                    if (korisnik.Equals(k)) return true;

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

        public static ObservableCollection<Korisnik> getAll() {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                if (!korisnik.Obrisan)
                    korisnici.Add(korisnik);
            }
            return korisnici;
        }

        public static void Initialize() {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            korisnici.Add(new Korisnik {
                ID = 0,
                Admin = true,
                Ime = "Admin",
                KorIme = "admin",
                Lozinka = "admin",
                Obrisan = false,
                Prezime = "",
        });
            Projekat.Instance.SetKorisnici(korisnici);
        }
    }
}
