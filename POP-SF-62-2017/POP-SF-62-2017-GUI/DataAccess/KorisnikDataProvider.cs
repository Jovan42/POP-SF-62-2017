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
    class KorisnikDataProvider : DataAccess {
        public static KorisnikDataProvider Instance { get; } = new KorisnikDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Korisnik k = (Korisnik)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Korisnik(Ime, Prezime, KorIme, Lozinka, Admin) VALUES (@Ime, @Prezime, @KorIme, @Lozinka, @Admin);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("Ime", k.Ime);
                cmd.Parameters.AddWithValue("Prezime", k.Prezime);
                cmd.Parameters.AddWithValue("KorIme", k.KorIme);
                cmd.Parameters.AddWithValue("Lozinka", k.Lozinka);
                cmd.Parameters.AddWithValue("Admin", k.Admin);
                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());

                k.ID = newID;
                Projekat.Instance.Korisnici.Add(k);
            }
        }

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Korisnik SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (Korisnik k in Projekat.Instance.Korisnici) {
                    if (k.ID == id) {
                        Projekat.Instance.Korisnici.Remove(k);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            Korisnik k = (Korisnik)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Korisnik SET Ime=@Ime, Prezime=@Prezime, KorIme=@KorIme, Lozinka=@Lozinka, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", k.ID);
                cmd.Parameters.AddWithValue("Ime", k.Ime);
                cmd.Parameters.AddWithValue("Prezime", k.Prezime);
                cmd.Parameters.AddWithValue("KorIme", k.KorIme);
                cmd.Parameters.AddWithValue("Lozinka", k.Lozinka);
                cmd.Parameters.AddWithValue("Obrisan", k.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (Korisnik korisnik in Projekat.Instance.Korisnici) {
                    if (korisnik.ID == k.ID) {
                        korisnik.Ime = k.Ime;
                        korisnik.Prezime = k.Prezime;
                        korisnik.KorIme = k.KorIme;
                        korisnik.Lozinka = k.Lozinka;
                        korisnik.Obrisan = k.Obrisan;
                        break;
                    }
                }
                return true;
            }
        }

        public ObservableCollection<Korisnik> GetAll() {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan=0";

                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Korisnik");

                foreach (DataRow row in dataSet.Tables["Korisnik"].Rows) {
                    Korisnik k = new Korisnik();
                    k.ID = int.Parse(row["ID"].ToString());
                    k.Ime = row["Ime"].ToString();
                    k.Prezime = row["Prezime"].ToString();
                    k.KorIme = row["KorIme"].ToString();
                    k.Lozinka = row["Lozinka"].ToString();
                    k.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    k.Admin = Boolean.Parse(row["Admin"].ToString());
                    korisnici.Add(k);
                }
            }
            return korisnici;
        }

        public Entitet GetByID(int id) {
            Korisnik k = new Korisnik();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Korisnik WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Korisnik");

                foreach (DataRow row in dataSet.Tables["Korinik"].Rows) {
                    k.ID = int.Parse(row["ID"].ToString());
                    k.Ime = row["Ime"].ToString();
                    k.Prezime = row["Prezime"].ToString();
                    k.KorIme = row["KorIme"].ToString();
                    k.Lozinka = row["Lozinka"].ToString();
                    k.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    k.Admin = Boolean.Parse(row["Admin"].ToString());
                }
            }
            return k;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion

        // Proverava da li je korisnik admin
        public bool IsAdmin(string username) {
            
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Admin FROM Korisnik WHERE KorIme=@KorIme";
                cmd.Parameters.AddWithValue("KorIme", username);

                bool admin = bool.Parse(cmd.ExecuteScalar().ToString());

                return admin;
            }
        }

        // Proverava da li je username i password
        public bool CheckPass(string user, string pass) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Lozinka FROM Korisnik WHERE KorIme=@KorIme AND Obrisan=0";
                cmd.Parameters.AddWithValue("KorIme", user);

                try {
                    String p = cmd.ExecuteScalar().ToString();
                    return (p == pass);
                } catch {
                    return false;
                }
            }
        }
    }
}
