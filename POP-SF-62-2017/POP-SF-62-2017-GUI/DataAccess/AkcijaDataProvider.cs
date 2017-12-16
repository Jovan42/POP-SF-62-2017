using POP_SF_62_2017.Model;
using POP_SF_62_2017_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


//TODO: Akcija.NamestajNaAkcijiID popuniti iz tabele NamestjNaAkciji
namespace POP_SF_62_2017_GUI.DataAccess {
    class AkcijaDataProvider : DataAccess {
        public static AkcijaDataProvider Instance { get; } = new AkcijaDataProvider();

        #region DataAccess Implementation
        public void Add(Entitet e) {
            Akcija a = (Akcija)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Akcija(Pocetak, Kraj, Popust) VALUES (@Pocetak, @Kraj, @Popust);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("Pocetak", a.Pocetak);
                cmd.Parameters.AddWithValue("Kraj", a.Kraj);
                cmd.Parameters.AddWithValue("Popust", a.Popust);
                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());
                NamestajNaAkcijiDataProvider.Instance.Add(a.NamestajNaAkcijiID, newID);
                a.NamestajNaAkcijiID = NamestajNaAkcijiDataProvider.Instance.Get(newID);
                
                a.ID = newID;
                Projekat.Instance.Akcije.Add(a);
            }
        }

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Akcija SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (Akcija akcija in Projekat.Instance.Akcije) {
                    if (akcija.ID == id) {
                        Projekat.Instance.Akcije.Remove(akcija);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            Akcija a = (Akcija)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Akcija SET Pocetak=@Pocetak, Kraj=@Kraj, Popust=@Popust WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", a.ID);
                cmd.Parameters.AddWithValue("Pocetak", a.Pocetak);
                cmd.Parameters.AddWithValue("Kraj", a.Kraj);
                cmd.Parameters.AddWithValue("Popust", a.Popust);

                cmd.ExecuteNonQuery();

                NamestajNaAkcijiDataProvider.Instance.Edit(a.NamestajNaAkcijiID, a.ID);
                foreach (Akcija akcija in Projekat.Instance.Akcije) {
                    if (akcija.ID == a.ID) {
                        akcija.Pocetak = a.Pocetak;
                        akcija.Kraj = a.Kraj;
                        akcija.Popust = a.Popust;
                        akcija.NamestajNaAkcijiID = NamestajNaAkcijiDataProvider.Instance.Get(akcija.ID);
                        break;
                    }
                }
                return true;
            }
        }

        public Entitet GetByID(int id) {
            Akcija akcija = new Akcija();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Akcija WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Akcija");
                foreach (DataRow row in dataSet.Tables["Akcija"].Rows) {
                    akcija.ID = int.Parse(row["Id"].ToString());
                    akcija.Popust = double.Parse(row["Popust"].ToString());
                    akcija.Pocetak = DateTime.Parse(row["Pocetak"].ToString());
                    akcija.Kraj = DateTime.Parse(row["Kraj"].ToString());
                    akcija.NamestajNaAkcijiID = NamestajNaAkcijiDataProvider.Instance.Get(akcija.ID);
                }
            }
            return akcija;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }

        #endregion

        public ObservableCollection<Akcija> GetAll() {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0";
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Akcija");

                foreach (DataRow row in dataSet.Tables["Akcija"].Rows) {
                    Akcija akcija = new Akcija();
                    akcija.ID = int.Parse(row["Id"].ToString());
                    akcija.Pocetak = DateTime.Parse(row["Pocetak"].ToString());
                    akcija.Kraj = DateTime.Parse(row["Kraj"].ToString());
                    akcija.Popust = double.Parse(row["Popust"].ToString());
                    akcija.NamestajNaAkcijiID = NamestajNaAkcijiDataProvider.Instance.Get(akcija.ID);

                    akcije.Add(akcija);
                }
            }
            return akcije;
        }

        public ObservableCollection<Akcija> GetActiveAkcije() {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan=0";
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Akcija");

                foreach (DataRow row in dataSet.Tables["Akcija"].Rows) {
                    Akcija akcija = new Akcija();
                    akcija.ID = int.Parse(row["Id"].ToString());
                    akcija.Pocetak = DateTime.Parse(row["Pocetak"].ToString());
                    akcija.Kraj = DateTime.Parse(row["Kraj"].ToString());
                    akcija.Popust = double.Parse(row["Popust"].ToString());
                    akcija.NamestajNaAkcijiID = NamestajNaAkcijiDataProvider.Instance.Get(akcija.ID);

                    if (akcija.Kraj > DateTime.Now)
                        akcije.Add(akcija);
                }
            }
            return akcije;
        }
    }
}
