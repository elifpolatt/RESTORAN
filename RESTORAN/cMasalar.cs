using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RESTORAN
{
    internal class cMasalar
    {
        #region Fields
        private int _ID;
        private int _KAPASITE;
        private int _SERVİSTURU;
        private int _DURUM;
        private int _ONAY;
        #endregion


        #region Properties
        public int ID { get => _ID; set => _ID = value; }
        public int KAPASITE { get => _KAPASITE; set => _KAPASITE = value; }
        public int SERVİSTURU { get => _SERVİSTURU; set => _SERVİSTURU = value; }
        public int DURUM { get => _DURUM; set => _DURUM = value; }
        public int ONAY { get => _ONAY; set => _ONAY = value; }
        #endregion

        cGenel gnl = new cGenel();
        public string SessionSum(int state, string masaId)
        {
            string dt = "";
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select TARIH,MasaId From adisyonlar Right Join Masalar on adisyonlar.MasaId=Masalar.ID Where Masalar.DURUM=@durum and adisyonlar.Durum=0 and masalar.ID=@masaId", con);
            SqlDataReader dr = null;
            cmd.Parameters.Add("@durum", SqlDbType.Int).Value = state;
            cmd.Parameters.Add("@masaId", SqlDbType.Int).Value = Convert.ToInt32(masaId);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dt = Convert.ToDateTime(dr["Tarih"]).ToString();

                }

            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
            return dt;
        }


        public int TableGetbyNumber(string TableValue)
        {
            string aa = TableValue;
            int lenght = aa.Length;
            //btnMasa10 gibi karakter sayısı sekizden buyukse uzunlugun son iki kısmını al demektir.
            if (lenght > 8)
            {
                return Convert.ToInt32(aa.Substring(lenght - 2, 2));

            }
            //eğer değilse zaten karakter sayısı btnMasa3 gibi 8 den küçük oluyor. oranın da sondaki karakteri al 
            else
            {
                return Convert.ToInt32(aa.Substring(lenght - 1, 1));

            }
            
        }



        //masanın durumunu öğrenmek icin yazıyoruz

        public bool TableGetbyState(int ButtonName, int state)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select durum From masalar Where Id = @TableId and DURUM=@state", con);

            cmd.Parameters.Add("@TableId", SqlDbType.Int).Value = ButtonName;
            cmd.Parameters.Add("@state", SqlDbType.Int).Value = state;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                result = Convert.ToBoolean(cmd.ExecuteScalar());


            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                con.Close();
                con.Close();
            }
            return result;



        }

        //masanın durumunu değiştir

        public void setChangeTableState(string ButonName, int state)
        {
            cGenel gnl = new cGenel();
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update masalar Set DURUM=@Durum where ID=@MasaNo", con);

            string masaNo = "";


            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string aa = ButonName;
            int uzunluk = aa.Length;

            cmd.Parameters.Add("@Durum", SqlDbType.Int).Value = state;

            //btnMasa10 gibi karakter sayısı sekizden buyukse uzunlugun son iki kısmını al demektir.
            if(uzunluk > 8)
            {
                masaNo = aa.Substring(uzunluk - 2, 2);

            }
            //eğer değilse zaten karakter sayısı btnMasa3 gibi 8 den küçük oluyor. oranın da sondaki karakteri al 
            else
            {
                masaNo = aa.Substring(uzunluk - 1, 1);

            }


            masaNo = aa.Substring(uzunluk - 1, 1);
            cmd.Parameters.Add("@MasaNo", SqlDbType.Int).Value = masaNo;
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();

            return;
        }

    }
}