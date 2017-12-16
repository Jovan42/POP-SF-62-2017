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
    class ProdatNamsetajDataProvider {

        public static ProdatNamsetajDataProvider Instance { get; } = new ProdatNamsetajDataProvider();

        public void Add(ObservableCollection<ProdatNamestaj> prodatiNamestaji, int prodajaId) {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                foreach (ProdatNamestaj prodatNamestaj in prodatiNamestaji) {
                    cmd.CommandText = "INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (@NamestajId, @ProdajaId, @Kolicina);";
                    cmd.Parameters.AddWithValue("NamestajId", prodatNamestaj.NamestajID);
                    cmd.Parameters.AddWithValue("ProdajaId", prodajaId);
                    cmd.Parameters.AddWithValue("Kolicina", prodatNamestaj.Kolicina);
                    cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
        }

        public void Edit(ObservableCollection<ProdatNamestaj> prodatiNamestaji, int prodajaId) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE ProdatNamestaj WHERE ProdajaId=@Id";
                cmd.Parameters.AddWithValue("Id", prodajaId);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                Add(prodatiNamestaji, prodajaId);
            }
        }

        public ObservableCollection<ProdatNamestaj> Get(int prodajaId) {
            ObservableCollection<ProdatNamestaj> prodatNamestaj = new ObservableCollection<ProdatNamestaj>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT NamestajId, Kolicina FROM ProdatNamestaj WHERE ProdajaId=@ProdajaId";
                cmd.Parameters.AddWithValue("ProdajaId", prodajaId);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "IzvrsenaDodatnaUsluga");

                foreach (DataRow row in dataSet.Tables["IzvrsenaDodatnaUsluga"].Rows) {
                    ProdatNamestaj pd = new ProdatNamestaj();
                    pd.Kolicina = int.Parse(row["Kolicina"].ToString());
                    pd.NamestajID = int.Parse(row["NamestajId"].ToString());
                    prodatNamestaj.Add(pd);
                }
            }
            return prodatNamestaj;
        }
    }
}
