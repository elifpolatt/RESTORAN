using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace RESTORAN
{
    internal class cPaketler
    {
        cGenel gnl = new cGenel();
        #region fields
        private int _ID;
        private int _AdditionID;
        private int _ClientId;
        private string _Description;
        private int _State;
        private int __Paytypeid;
        #endregion

        #region propeties
        public int ID { get => _ID; set => _ID = value; }
        public int AdditionID { get => _AdditionID; set => _AdditionID = value; }
        public int ClientId { get => _ClientId; set => _ClientId = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int State { get => _State; set => _State = value; }
        public int Paytypeid { get => __Paytypeid; set => __Paytypeid = value; }
        #endregion



        //paket servis aç 
        public bool OrderServiceOpen(cPaketler order)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert input paketSiparis(ADISYONID,MUSTERIID,ODEMETURID,ACIKLAMA)values(@ADISYONID,@MUSTERIID,@ODEMETURID,@ACIKLAMA)", con);  //ekleme yapılacagı için insert komutunu kullanıyoruz 

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.Add("@ADISYONID", SqlDbType.Int).Value = order.AdditionID;
                cmd.Parameters.Add("@MUSTERIID", SqlDbType.Int).Value = order.ClientId;
                cmd.Parameters.Add("@ODEMETURID", SqlDbType.Int).Value = order.__Paytypeid;
                cmd.Parameters.Add("@ACIKLAMA", SqlDbType.VarChar).Value = order._Description;
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
            return result;
        }


        //kasaya para geldiğinde paket servis kapat
        public void OrderServiceClose(int AdditionID)
        {
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update paketSiparis set paketSiparis.durum = 1 paketSiparis where paketSiparis.ADISYONID=@AdditionID", con);  

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.Add("@AdditionID", SqlDbType.Int).Value = AdditionID;

                Convert.ToBoolean(cmd.ExecuteNonQuery());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
        }



        //acılan adisyon ve paket siparise ait on girilen odeme tur ıd
        public int OdemeTurIdGetir(int adisyonId)
        {
            int odemeTurID = 0;



            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select paketSiparis.ODEMETURID from paketSiparis Inner Join Adisyonlar on paketSiparis.ADISYONID=Adisyonlar.ID where adisyonlar.ID=@adisyonId", con);  

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = adisyonId;
                
                odemeTurID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
            return odemeTurID;

        }


        //siparis kontrol için musterıye ait acık olan en son adısyon nosunu getırme
        //bır musterıye aıt ıkı tane sıparısın acık olamaacagını belırtıyoruz

        public int musteriSonAdisyonIDGetir(int musteriID)
        {
            int no = 0;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select adisyonlar.ID from adisyonlar Inner Join paketSiparis on paketSiparis.ADISYONID=Adisyonlar.ID where (adisyonlar.DURUM=0) and (paketSiparis.DURUM=0) and (paketSiparis..MUSTERIID=@musteriID)", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.Add("@adisyonId", SqlDbType.Int).Value = musteriID;

                no = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }

            return no;
        }


        //acık adısyon var mı kontrolu yapacagız 
        //musterı arama ekranında adısyonbul butonu adsyon acık mı degıl mı kontrolu
        public bool getCheckOpenAdditionID(int additionID)
        {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * from adisyonlar where (DURUM=0) and (ID=@additionID)", con); //ekleme yapılacagı için insert komutunu kullanıyoruz 

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                cmd.Parameters.Add("@additionID", SqlDbType.Int).Value = additionID;
        
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }

            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Dispose();
                con.Close();


            }
            return result;
        }

    } 
}
