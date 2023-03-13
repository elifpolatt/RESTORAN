using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace RESTORAN
{
    internal class cSiparis
    {
        cGenel gnl = new cGenel();
        #region fields
        private int _Id;
        private int _adisyonID;
        private int _urunId;
        private int _adet;
        private int _masaId;
        #endregion

        #region Properties

        public int Id { get => _Id; set => _Id = value; }
        public int AdisyonID { get => _adisyonID; set => _adisyonID = value; }
        public int UrunId { get => _urunId; set => _urunId = value; }
        public int Adet { get => _adet; set => _adet = value; }
        public int MasaId { get => _masaId; set => _masaId = value; } 
        #endregion


      
        public void getByOrder(ListView lv, int AdisyonId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);  //satıslardaki ID ile urunlerdeki ID'leri birleştir ve bana ADISYONID'si AdisyonId'ye eşit olanlari ver
            SqlCommand cmd = new SqlCommand("select URUNAD,FIYAT,Satislar.ID,URUNID,satislar.ADET from satislar Inner Join urunler on Satislar.URUNID= Urunler.ID Where ADISYONID = @AdisyonId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@AdisyonId", SqlDbType.Int).Value = AdisyonId;
            try
            {
                if(con.State == ConnectionState.Closed)  //bağlantı açık mı diye kontrol et 
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader(); //bilgileri oku
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(Convert.ToString(Convert.ToDecimal(dr["FIYAT"]) * Convert.ToDecimal(dr["ADET"]))); //FİYAT CARPİ ADET URUNUN FIYATIDIR 
                    lv.Items[sayac].SubItems.Add(dr["ID"].ToString());

                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
        }

        public bool setSaveOrder(cSiparis Bilgiler)
        {
            bool sonuc = false;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into satislar(ADISYONID,URUNID,ADET,MASAID) values(@AdisyonNo,@UrunId,@Adet,@masaId)", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            

            cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = Bilgiler._adisyonID;
            cmd.Parameters.Add("@UrunId", SqlDbType.Int).Value = Bilgiler._urunId;
            cmd.Parameters.Add("@Adet", SqlDbType.Int).Value = Bilgiler._adet;
            cmd.Parameters.Add("@masaId", SqlDbType.Int).Value = Bilgiler._masaId;
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
            return sonuc;
        }
        public void setDeleteOrder(int satisId)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Delete From satislar Where ID=@SatisID",con);

            cmd.Parameters.Add("@SatisID", SqlDbType.Int).Value = satisId;

            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close ();

        }

        public decimal GenelToplamBul(int musteriId)
        {
            decimal genelToplam = 0;
            /*SELECT SUM(dbo.satislar.ADET* dbo.urunler.FIYAT) AS fiyat FROM dbo.musteriler INNER JOIN dbo.paketSiparis ON dbo.musteriler.ID = paketSiparis.MUSTERIID INNER JOIN adisyonlar on adisyonlar.ID = paketSiparis.AdisyonId INNER JOIN dbo.satislar ON dbo.adisyonlar.ID = dbo.satislar.ADISYONID INNER JOIN dbo.urunler ON dbo.satislar.URUNID = dbo.urunler.ID WHERE(dbo.paketSiparis.MUSTERIID = @musteriId) AND(dbo.paketSiparis.Durum = 1)*/
            
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select sum(TOPLAMTUTAR) from hesapOdemeleri where MUSTERIID=@musteriId", con);
            cmd.Parameters.Add("musteriId",SqlDbType.Int).Value = musteriId;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                genelToplam = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
            return genelToplam;
        }

        public void adisyonPaketSiparisDetaylari(ListView lv, int adisyonID)
        {
            decimal genelToplam = 0;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select satislar.ID,urunler.URUNAD,urunler.FIYAT,satislar.ADET from satislar Inner Join adisyonlar on adisyonlar.ID=satislar.ADISYONID INNER JOIN urunler on urunler.ID=satislar.URUNID where satislar.ADISYONID=@adisyonID", con);
            cmd.Parameters.Add("adisyonID", SqlDbType.Int).Value = adisyonID;
            SqlDataReader dr = null;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                int i = 0;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lv.Items.Add(dr["satisID"].ToString());
                    lv.Items[i].SubItems.Add(dr["URUNAD"].ToString());
                    lv.Items[i].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                    i++;
                }
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
        }
    }
}
