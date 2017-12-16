using POP_SF_62_2017.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_62_2017_GUI.DataAccess {
    class IzvrsenaDodatnaUslugaDataProvider {

        public static IzvrsenaDodatnaUslugaDataProvider Instance { get; } = new IzvrsenaDodatnaUslugaDataProvider();

        public void Add(List<int> dodatneUslugeID, int prodajaId) {
           
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                
                foreach (int  dodatnaUsluga in dodatneUslugeID) {
                    cmd.CommandText = "INSERT INTO IzvrsenaDodatnaUsluga(DodatnaUslugaId, ProdajaId) VALUES (@DodatnaUslugaId, @ProdajaId);";
                    cmd.Parameters.AddWithValue("DodatnaUslugaId", dodatnaUsluga);
                    cmd.Parameters.AddWithValue("ProdajaId", prodajaId);
                    cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
        }

        public void Edit(List<int> dodatneUslugeID, int prodajaId) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE IzvrsenaDodatnaUsluga WHERE ProdajaId=@Id";
                cmd.Parameters.AddWithValue("Id", prodajaId);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                
                Add(dodatneUslugeID, prodajaId);               
            }
        }

        public List<int> Get(int prodajaId) {
            List<int> izvrseneDodatneUsluge = new List<int>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT DodatnaUslugaId FROM IzvrsenaDodatnaUsluga WHERE ProdajaId=@ProdajaId";
                cmd.Parameters.AddWithValue("ProdajaId", prodajaId);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "IzvrsenaDodatnaUsluga");

                foreach (DataRow row in dataSet.Tables["IzvrsenaDodatnaUsluga"].Rows) {

                    izvrseneDodatneUsluge.Add(int.Parse(row["DodatnaUslugaId"].ToString()));
                }
            }
            return izvrseneDodatneUsluge;
        }
    }
}
