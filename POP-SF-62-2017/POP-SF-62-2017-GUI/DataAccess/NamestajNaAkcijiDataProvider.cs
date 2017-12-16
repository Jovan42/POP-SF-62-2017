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
    class NamestajNaAkcijiDataProvider {

        public static NamestajNaAkcijiDataProvider Instance { get; } = new NamestajNaAkcijiDataProvider();

        public void Add(List<int> namestajiNaAkcijiId, int prodajaId) {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                foreach (int namestajNaAkciji in namestajiNaAkcijiId) {
                    cmd.CommandText = "INSERT INTO NamestajNaAkciji (NamestajId, AkcijaId) VALUES (@NamestajId, @AkcijaId);";
                    cmd.Parameters.AddWithValue("NamestajId", namestajNaAkciji);
                    cmd.Parameters.AddWithValue("AkcijaId", prodajaId);
                    
                    cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
        }

        public void Edit(List<int> namestajiNaAkcijiId, int akcijaId) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE NamestajNaAkciji WHERE AkcijaId=@Id";
                cmd.Parameters.AddWithValue("Id", akcijaId);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                Add(namestajiNaAkcijiId, akcijaId);
            }
        }

        public List<int> Get(int akcijaId) {
            List<int> namestajiNaAkciji = new List<int>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT NamestajId FROM NamestajNaAkciji WHERE AkcijaId=@AkcijaId";
                cmd.Parameters.AddWithValue("AkcijaId", akcijaId);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "NamestajNaAkciji");

                foreach (DataRow row in dataSet.Tables["NamestajNaAkciji"].Rows) {
                    namestajiNaAkciji.Add(int.Parse(row["NamestajId"].ToString()));
                }
            }
            return namestajiNaAkciji;
        }
    }
}
