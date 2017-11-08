using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util.Model {
    public class UtilKorisnik {
        public static Korisnik GetById(int id) {
            foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                if (korisnik.ID == id) {
                    return korisnik;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            foreach (Korisnik korisnik in korisnici) {
                if (korisnik.ID == id) {
                    korisnik.Obrisan = true;
                    Projekat.Instance.Korisnici = korisnici;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Korisnik a) {
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici = Projekat.Instance.Korisnici;
            a.ID = korisnici.Count();
            korisnici.Add(a);
            Projekat.Instance.Korisnici = korisnici;
        }

        public static bool ChangeById(Korisnik k, int id) {
            List<Korisnik> korisnici = new List<Korisnik>();
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
                    Projekat.Instance.Korisnici = korisnici;
                    return true;
                }
            }
            return false;
        }
        public static void Initialize() {
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici.Add(new Korisnik {
                ID = 0,
                Admin = true,
                Ime = "Admin",
                KorIme = "admin",
                Lozinka = "admin",
                Obrisan = false,
                Prezime = "",
        });
            Projekat.Instance.Korisnici = korisnici;
        }
    }
}
