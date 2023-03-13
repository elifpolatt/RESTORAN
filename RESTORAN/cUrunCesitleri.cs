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
    internal class cUrunCesitleri
    {
        cGenel gnl = new cGenel();

        #region fields
        private int _UrunTurNo;
        private string _KategoriAd;
        private string _Aciklama;
        #endregion

        #region Propeties
        public int UrunTurNo { get => _UrunTurNo; set => _UrunTurNo = value; }
        public string KategoriAd { get => _KategoriAd; set => _KategoriAd = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; } 
        #endregion

        public void getByProductTypes(ListView Cesitler,Button btn)
        {
            Cesitler.Items.Clear(); //her cagrılmada bı temızlenmesı gerekır tablodaki ürünler üst üste yazar bunu eklemezsek
            SqlConnection conn = new SqlConnection(gnl.conString); //kategoriler tablosyla ürünler tablosunu birleştir ortak olanlar kategoriler ıd ve urunler ıd'leri al
            SqlCommand comm = new SqlCommand("Select URUNAD,FIYAT,urunler.ID From kategoriler Inner Join urunler on kategoriler.ID = urunler.KATEGORIID where urunler.KATEGORIID=@KATEGORIID", conn);

            string aa = btn.Name;
            int uzunluk = aa.Length;

            comm.Parameters.Add("@KATEGORIID", SqlDbType.Int).Value = aa.Substring(uzunluk - 1, 1);
            //burada iki tane eşittir kullanmısım ıkı gun bu hatayı bulmaya calıstım saka gibi aksljndkld
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;

            while (dr.Read())  //URUNID FIYAT URUNID sonra alt satıra gecıp bunları tekrar yazacak 

            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
            }
            dr.Close();
            conn.Dispose();
            conn.Close();

        }
        public void getByProductSearch(ListView Cesitler, int txt)
        {
            Cesitler.Items.Clear(); //her cagrılmada bı temızlenmesı gerekır tablodaki ürünler üst üste yazar bunu eklemezsek
            SqlConnection conn = new SqlConnection(gnl.conString); //kategoriler tablosyla ürünler tablosunu birleştir ortak olanlar kategoriler ıd ve urunler ıd'leri al
            SqlCommand comm = new SqlCommand("Select * from urunler where ID = @ID", conn);


            comm.Parameters.Add("@ID", SqlDbType.Int).Value = txt;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlDataReader dr = comm.ExecuteReader();
            int i = 0;

            while (dr.Read())  //URUNID FIYAT URUNID sonra alt satıra gecıp bunları tekrar yazacak 

            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
            }
            dr.Close();
            conn.Dispose();
            conn.Close();

        }
    }
}
