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
    class NamestajDataProvider : DataAccess{
        public static NamestajDataProvider Instance { get; } = new NamestajDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Namestaj namestaj = (Namestaj)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Namestaj(TipNamestajaId, Naziv, Kolicina, Cena) VALUES (@TipNamestajaId, @Naziv, @Kolicina, @Cena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("TipNamestajaId", namestaj.TipNamestajaID);
                cmd.Parameters.AddWithValue("Naziv", namestaj.Naziv);
                cmd.Parameters.AddWithValue("Kolicina", namestaj.Kolicina);
                cmd.Parameters.AddWithValue("Cena", namestaj.Cena);
                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());

                namestaj.ID = newID;
                Projekat.Instance.Namestaji.Add(namestaj);
            }
        }

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (Namestaj namestaj in Projekat.Instance.Namestaji) {
                    if (namestaj.ID == id) {
                        Projekat.Instance.Namestaji.Remove(namestaj);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            Namestaj t = (Namestaj)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Namestaj SET TipNamestajaId=@TipNamestajaId, Naziv=@Naziv, Kolicina=@Kolicina, Cena=@Cena, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", t.ID);
                cmd.Parameters.AddWithValue("Naziv", t.Naziv);
                cmd.Parameters.AddWithValue("TipNamestajaId", t.TipNamestajaID);
                cmd.Parameters.AddWithValue("Kolicina", t.Kolicina);
                cmd.Parameters.AddWithValue("Cena", t.Cena);
                cmd.Parameters.AddWithValue("Obrisan", t.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (Namestaj namestaj in Projekat.Instance.Namestaji) {
                    if (namestaj.ID == t.ID) {
                        namestaj.Naziv = t.Naziv;
                        namestaj.Cena = t.Cena;
                        namestaj.TipNamestajaID = t.TipNamestajaID;
                        namestaj.TipNamestaja = t.TipNamestaja;
                        namestaj.Kolicina = t.Kolicina;
                        namestaj.Obrisan = t.Obrisan;
                        break;
                    }
                }
                return true;
            }
        }

        public ObservableCollection<Namestaj> GetAll() {
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=0";

                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Namestaj");

                foreach (DataRow row in dataSet.Tables["Namestaj"].Rows) {
                    Namestaj namestaj = new Namestaj();
                    namestaj.TipNamestajaID = int.Parse(row["TIpNamestajaId"].ToString());
                    namestaj.ID = int.Parse(row["ID"].ToString());
                    namestaj.Naziv = row["Naziv"].ToString();
                    namestaj.Cena = Double.Parse(row["Cena"].ToString());
                    namestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                    namestaj.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    namestaji.Add(namestaj);
                }
            }
            return namestaji;
        }

        public Entitet GetByID(int id) {
            
            Namestaj namestaj = new Namestaj();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Namestaj");

                foreach (DataRow row in dataSet.Tables["Namestaj"].Rows) {
                    namestaj.TipNamestajaID = int.Parse(row["TipNamestajaId"].ToString());
                    namestaj.ID = int.Parse(row["ID"].ToString());
                    namestaj.Naziv = row["Naziv"].ToString();
                    namestaj.Cena = Double.Parse(row["Cena"].ToString());
                    namestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                    namestaj.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                }
            }
            return namestaj;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        public ObservableCollection<Namestaj> GetAll(bool obrisani) {
            if (!obrisani) return GetAll();
            ObservableCollection<Namestaj> namestaji = new ObservableCollection<Namestaj>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=1";

                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Namestaj");

                foreach (DataRow row in dataSet.Tables["Namestaj"].Rows) {
                    Namestaj namestaj = new Namestaj();
                    namestaj.TipNamestajaID = int.Parse(row["TIpNamestajaId"].ToString());
                    namestaj.ID = int.Parse(row["ID"].ToString());
                    namestaj.Naziv = row["Naziv"].ToString();
                    namestaj.Cena = Double.Parse(row["Cena"].ToString());
                    namestaj.Kolicina = int.Parse(row["Kolicina"].ToString());
                    namestaj.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    namestaji.Add(namestaj);
                }
            }
            return namestaji;
        }
        #endregion
    }
}
