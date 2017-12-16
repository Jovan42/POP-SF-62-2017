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
    class DodatnaUslugaDataProvider : DataAccess {
        public static DodatnaUslugaDataProvider Instance { get; } = new DodatnaUslugaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            DodatnaUsluga du = (DodatnaUsluga)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO DodatnaUsluga(Naziv, Cena) VALUES (@Naziv, @Cena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);

                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());

                du.ID = newID;
                Projekat.Instance.DodatneUsluge.Add(du);
            }
        }

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE DodatnaUsluga SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (DodatnaUsluga dodatnaUsluga in Projekat.Instance.DodatneUsluge) {
                    if (dodatnaUsluga.ID == id) {
                        Projekat.Instance.DodatneUsluge.Remove(dodatnaUsluga);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            DodatnaUsluga du = (DodatnaUsluga)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE DodatnaUsluga SET Naziv=@Naziv, Cena=@Cena WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", du.ID);
                cmd.Parameters.AddWithValue("Naziv", du.Naziv);
                cmd.Parameters.AddWithValue("Cena", du.Cena);

                cmd.ExecuteNonQuery();

                foreach (DodatnaUsluga dodatneUsluge in Projekat.Instance.DodatneUsluge) {
                    if (dodatneUsluge.ID == du.ID) {
                        dodatneUsluge.Naziv = du.Naziv;
                        dodatneUsluge.Cena = du.Cena;
                        break;
                    }
                }
                return true;
            }
        }

        public Entitet GetByID(int id) {
            DodatnaUsluga dodatnaUsluga = new DodatnaUsluga();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "DodatnaUsluga");

                foreach (DataRow row in dataSet.Tables["DodatnaUsluga"].Rows) {
                    dodatnaUsluga.Naziv= row["Naziv"].ToString();
                    dodatnaUsluga.Cena = Double.Parse(row["Cena"].ToString());
                }
            }
            return dodatnaUsluga;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        #endregion

        public ObservableCollection<DodatnaUsluga> GetAll() {
            ObservableCollection<DodatnaUsluga> dodatneUsluge = new ObservableCollection<DodatnaUsluga>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DodatnaUsluga WHERE Obrisan=0";
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "DodatnaUsluga");

                foreach (DataRow row in dataSet.Tables["DodatnaUsluga"].Rows) {
                    DodatnaUsluga dodatnaUsluga = new DodatnaUsluga();
                    dodatnaUsluga.Naziv = row["Naziv"].ToString();
                    dodatnaUsluga.Cena = Double.Parse(row["Cena"].ToString());


                    dodatneUsluge.Add(dodatnaUsluga);
                }
            }
            return dodatneUsluge;
        }
    }
}
