using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace RESTORAN
{
    internal class cPersonellerGorev
    {
        cGenel gnl = new cGenel();

        #region fields
        private int _personelGorevId;

        private string _tanim;
        #endregion

        #region properties
        public string Tanim { get => _tanim; set => _tanim = value; }
        public int PersonelGorevId { get => _personelGorevId; set => _personelGorevId = value; }
        #endregion

        public void PersonelGorevGetir(ComboBox cb)
        {
            
            cb.Items.Clear();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from personelGorevleri", con);
            SqlDataReader dr = null;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cPersonellerGorev c = new cPersonellerGorev();
                    c.PersonelGorevId = Convert.ToInt32(dr["ID"]);
                    c._tanim = dr["GOREV"].ToString();
                    cb.Items.Add(c);
                }
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }

            
            dr.Close();
            con.Close();
        }

        public string PersonelGorevTanim(int per)
        {
            string sonuc = "";
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select GOREV from personelGorevleri where ID=@perId", con);

            cmd.Parameters.Add("perId", SqlDbType.Int).Value = per;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sonuc = cmd.ExecuteScalar().ToString();
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            

            
            con.Close();

            return sonuc;
        }

        public override string ToString()
        {
            return _tanim;
        }
    }
}
