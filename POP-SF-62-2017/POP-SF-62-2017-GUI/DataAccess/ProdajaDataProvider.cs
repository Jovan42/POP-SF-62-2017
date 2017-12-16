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


//TODO: ProdatNamestaj i Dodatne Usluge
namespace POP_SF_62_2017_GUI.DataAccess {
    class ProdajaDataProvider {
        public static ProdajaDataProvider Instance { get; } = new ProdajaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Prodaja p = (Prodaja)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Prodaja(Kupac, DatumProdaje) VALUES (@Kupac, @DatumProdaje);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("Kupac", p.Kupac);
                cmd.Parameters.AddWithValue("DatumProdaje", p.DatumProdaje);
                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());

                p.ID = newID;
                Projekat.Instance.Prodaje.Add(p);
            }
        }

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Prodaja SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (Prodaja p in Projekat.Instance.Prodaje) {
                    if (p.ID == id) {
                        Projekat.Instance.Prodaje.Remove(p);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            Prodaja p = (Prodaja)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Prodaja SET Kupac=@Kupac, DatumProdaje=@DatumProdaj, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", p.ID);
                cmd.Parameters.AddWithValue("Kupac", p.Kupac);
                cmd.Parameters.AddWithValue("DatumProdaje", p.DatumProdaje);
                cmd.Parameters.AddWithValue("Obrisan", p.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (Prodaja prodaja in Projekat.Instance.Prodaje) {
                    if (prodaja.ID == p.ID) {
                        prodaja.DatumProdaje = p.DatumProdaje;
                        prodaja.Kupac = p.Kupac;
                        prodaja.Obrisan = p.Obrisan;
                        break;
                    }
                }
                return true;
            }
        }

        public ObservableCollection<Prodaja> GetAll() {
            ObservableCollection<Prodaja> prodaje = new ObservableCollection<Prodaja>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Prodaja WHERE Obrisan=0";

                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Prodaja");

                foreach (DataRow row in dataSet.Tables["Korisnik"].Rows) {
                    Prodaja p = new Prodaja();
                    p.ID = int.Parse(row["Id"].ToString());
                    p.Kupac = row["Kupac"].ToString();
                    p.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    p.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                    
                    prodaje.Add(p);
                }
            }
            return prodaje;
        }

        public Entitet GetByID(int id) {
            Prodaja p = new Prodaja();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Prodaja WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Prodaja");

                foreach (DataRow row in dataSet.Tables["Prodaja"].Rows) {
                    p.ID = int.Parse(row["Id"].ToString());
                    p.Kupac = row["Kupac"].ToString();
                    p.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    p.Obrisan = Boolean.Parse(row["Obrisan"].ToString());
                }
            }
            return p;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion
    }
}
