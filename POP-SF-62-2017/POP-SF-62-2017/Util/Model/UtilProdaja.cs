﻿using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017.Util {
    class UtilProdaja {
        public static Prodaja GetById(int id) {
            foreach (Prodaja prodaja in Projekat.Instance.Prodaje) {
                if (prodaja.ID == id) {
                    return prodaja;
                }
            }
            return null;
        }

        public static bool DeleteById(int id) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    prodaja.Obrisan = true;
                    Projekat.Instance.Prodaje = prodaje;
                    return true;

                }
            }
            return false;
        }

        public static void Add(Prodaja a) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            a.ID = prodaje.Count() + 1;
            prodaje.Add(a);
            Projekat.Instance.Prodaje = prodaje;
        }

        public static bool ChangeById(Prodaja p, int id) {
            List<Prodaja> prodaje = new List<Prodaja>();
            prodaje = Projekat.Instance.Prodaje;
            foreach (Prodaja prodaja in prodaje) {
                if (prodaja.ID == id) {
                    if (prodaja.Equals(p)) return true;
                    prodaja.DatumProdaje = p.DatumProdaje;
                    prodaja.DodatneUsluge = p.DodatneUsluge;
                    prodaja.Kupac = p.Kupac;
                    prodaja.ListaNamestaja = p.ListaNamestaja;
                    prodaja.Obrisan = p.Obrisan;
                    Projekat.Instance.Prodaje = prodaje;
                    return true;

                }
            }
            return false;
        }
    }
}
