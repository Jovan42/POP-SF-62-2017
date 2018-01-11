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
    class TipNamestajaDataProvider : DataAccess {
        public static TipNamestajaDataProvider Instance { get; } = new TipNamestajaDataProvider();

        #region DataAccess Implementation

        public void Add(Entitet e) {
            TipNamestaja tipNamestaja = (TipNamestaja)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO TipNamestaja(Naziv, Obrisan) VALUES (@Naziv, 0);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY(); ";
                cmd.Parameters.AddWithValue("Naziv", tipNamestaja.Naziv);
                int newID = Int32.Parse(cmd.ExecuteScalar().ToString());

                tipNamestaja.ID = newID;
                Projekat.Instance.TipoviNamestaja.Add(tipNamestaja);
            }
        }
        
        /*public void Add(Entitet e) {
            TipNamestaja tipNamestaja = (TipNamestaja)e;
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            tipNamestaja.ID = tipoviNamestaja.Count();
            tipoviNamestaja.Add(tipNamestaja);
            Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
        }*/

        /*public bool DeleteByID(int id) {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            foreach (TipNamestaja akcija in tipoviNamestaja) {
                if (akcija.ID == id) {
                    akcija.Obrisan = true;
                    Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
                    return true;

                }
            }
            return false;
        }*/

        public bool DeleteByID(int id) {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja SET Obrisan=1 WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();

                foreach (TipNamestaja tipNamestaj in Projekat.Instance.TipoviNamestaja) {
                    if (tipNamestaj.ID == id) {
                        Projekat.Instance.TipoviNamestaja.Remove(tipNamestaj);
                        break;
                    }
                }
                return true;
            }
        }

        public bool EditByID(Entitet e, int id) {
            TipNamestaja t = (TipNamestaja)e;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja SET Naziv=@Naziv, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", t.ID);
                cmd.Parameters.AddWithValue("Naziv", t.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", t.Obrisan);

                cmd.ExecuteNonQuery();

                foreach (TipNamestaja tipNamestaj in Projekat.Instance.TipoviNamestaja) {
                    if(tipNamestaj.ID == t.ID) {
                        tipNamestaj.Naziv = t.Naziv;
                        tipNamestaj.Obrisan = t.Obrisan;
                        break;
                    }
                }
                return true;
            }
        }
        /*
        public bool EditByID(Entitet e, int id) {
            TipNamestaja t = (TipNamestaja)e;
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            tipoviNamestaja = Projekat.Instance.TipoviNamestaja;
            foreach (TipNamestaja tipNamestaja in tipoviNamestaja) {
                if (tipNamestaja.ID == id) {
                    tipNamestaja.Naziv = t.Naziv;
                    tipNamestaja.Obrisan = t.Obrisan;
                    Projekat.Instance.SetTipoveNamestaja(tipoviNamestaja);
                    return true;

                }
            }
            return false;
        }*/

        public ObservableCollection<TipNamestaja> GetAll() {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();

            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan=0";
                
                DataSet dataSet = new DataSet();
                
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "TipNamestaja");

                foreach (DataRow row in dataSet.Tables["TipNamestaja"].Rows) {
                    TipNamestaja tipNamestaja = new TipNamestaja();
                    tipNamestaja.ID = int.Parse(row["ID"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    tipoviNamestaja.Add(tipNamestaja);
                }
            }
            return tipoviNamestaja;
        }

        public ObservableCollection<TipNamestaja> GetAll(bool obrisani) {
            if (!obrisani) return GetAll();
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan=1";

                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "TipNamestaja");

                foreach (DataRow row in dataSet.Tables["TipNamestaja"].Rows) {
                    TipNamestaja tipNamestaja = new TipNamestaja();
                    tipNamestaja.ID = int.Parse(row["ID"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    tipoviNamestaja.Add(tipNamestaja);
                }
            }
            return tipoviNamestaja;
        }

        /*public ObservableCollection<TipNamestaja> GetAll() {
            ObservableCollection<TipNamestaja> tipoviNamestaja = new ObservableCollection<TipNamestaja>();
            foreach (TipNamestaja korisnk in Projekat.Instance.TipoviNamestaja) {
                if (!korisnk.Obrisan)
                    tipoviNamestaja.Add(korisnk);
            }
            return tipoviNamestaja;
        }*/

        public Entitet GetByID(int id) {
            TipNamestaja tipNamestaja = new TipNamestaja();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id);
                DataSet dataSet = new DataSet();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "TipNamestaja");

                foreach (DataRow row in dataSet.Tables["TipNamestaja"].Rows) {
                    
                    tipNamestaja.ID = int.Parse(row["ID"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = Boolean.Parse(row["Obrisan"].ToString());

                    
                }
            }
            return tipNamestaja;
        }

        public void Initialize() {
            throw new NotImplementedException();
        }
        #endregion

    }
}
