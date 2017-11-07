﻿using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util {
    public class UtilSalon {
        public static Salon GetById(int id) {
            foreach (Salon salon in Projekat.Instance.Saloni) {
                if (salon.ID == id) {
                    return salon;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Salon> saloni = new List<Salon>();
            saloni = Projekat.Instance.Saloni;
            foreach (Salon salon in saloni) {
                if (salon.ID == id) {
                    salon.Obrisan = true;
                    Projekat.Instance.Saloni = saloni;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Salon a) {
            List<Salon> saloni = new List<Salon>();
            saloni = Projekat.Instance.Saloni;
            a.ID = saloni.Count() + 1;
            saloni.Add(a);
            Projekat.Instance.Saloni = saloni;
        }

        public static bool ChangeById(Salon s, int id) {
            List<Salon> saloni = new List<Salon>();
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
                    Projekat.Instance.Saloni = saloni;
                    return true;

                }
            }
            return false;
        }
    }
}
