using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class SalonDataProvider : DataAccess {
        public static SalonDataProvider Instance { get; } = new SalonDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            //Not needed
        }

        public bool DeleteByID(int id) {
            return false;
           //Not needed
        }

        public bool EditByID(Entitet e, int id) {
            Salon s = (Salon)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Salon SET Naziv=@Naziv, Adresa=@Adresa, Mail=@Mail, Sajt=@Sajt, Telefon=@Telefon," +
                    "PIB=@PIB, MatBr=@MatBr, ZiroRacun=@ZiroRacun, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", s.ID);
                cmd.Parameters.AddWithValue("Naziv", s.Naziv);
                cmd.Parameters.AddWithValue("Adresa", s.Adresa);
                cmd.Parameters.AddWithValue("Mail", s.Mail);
                cmd.Parameters.AddWithValue("Sajt", s.Sajt);
                cmd.Parameters.AddWithValue("Telefon", s.Telefon);
                cmd.Parameters.AddWithValue("PIB", s.PIB);
                cmd.Parameters.AddWithValue("MatBr", s.MatBr);
                cmd.Parameters.AddWithValue("ZiroRacun", s.ZiroRacun);
                cmd.Parameters.AddWithValue("Obrisan", s.Obrisan);
                cmd.ExecuteNonQuery();

                foreach (Salon salon in Projekat.Instance.Saloni) {
                    if (salon.ID == s.ID) {
                        salon.Naziv = s.Naziv;
                        salon.Mail = s.Mail;
                        salon.MatBr = s.MatBr;
                        salon.Obrisan = s.Obrisan;
                        salon.PIB = s.PIB;
                        salon.Sajt = s.Sajt;
                        salon.Telefon = s.Telefon;
                        salon.ZiroRacun = s.ZiroRacun;
                        salon.Adresa = s.Adresa; 
                        break;
                    }
                }
                return true;
            }     
        }

        public ObservableCollection<Salon> GetAll() {
            //Not needed
            return null;
        }

        public Entitet GetByID(int id) {
            Salon s = new Salon();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Salon WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Salon");

                foreach (DataRow row in dataSet.Tables["Salon"].Rows) {
                    s.ID = int.Parse(row["ID"].ToString());
                    s.Naziv = row["Naziv"].ToString();
                    s.Mail = row["Mail"].ToString();
                    s.MatBr = row["MatBr"].ToString();
                    s.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    s.PIB = row["PIB"].ToString();
                    s.Sajt = row["Sajt"].ToString();
                    s.Telefon = row["Telefon"].ToString();
                    s.ZiroRacun = row["ZiroRacun"].ToString();
                    s.Adresa = row["Adresa"].ToString();
                }
            }
            return s;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
