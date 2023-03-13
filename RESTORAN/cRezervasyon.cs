using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RESTORAN
{
    internal class cRezervasyon
    {

            cGenel gnl = new cGenel();
 
            #region fields
            private int _ID;
            private int _TableId;
            private int _ClientId;
            private DateTime _Date;
            private int _ClientCount;
            private string _Descripton;
            private int _AdditionId;
            #endregion

            #region properties
            public int ID { get => _ID; set => _ID = value; }
            public int TableId { get => _TableId; set => _TableId = value; }
            public int ClientId { get => _ClientId; set => _ClientId = value; }
            public DateTime Date { get => _Date; set => _Date = value; }
            public int ClientCount { get => _ClientCount; set => _ClientCount = value; }
            public string Descripton { get => _Descripton; set => _Descripton = value; }
            public int AdditionId { get => _AdditionId; set => _AdditionId = value; }
            #endregion


            //MusterıId masa numarasına göre
            public int getByClientIdFromReservation(int tableId)
            {
                int clientId = 0;

                SqlConnection con = new SqlConnection(gnl.conString);
                SqlCommand cmd = new SqlCommand("Select top 1 MUSTERIID from Rezervasyonlar Where MASAID=@masaid order by MUSTERIID Desc ", con);

                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Parameters.Add("masaid", SqlDbType.Int).Value = tableId;
                    clientId = Convert.ToInt32(cmd.ExecuteNonQuery());
                
                }
                catch (Exception ex)
                {
                    string hata = ex.Message;
                    throw;
                }
                finally
                {
                    con.Dispose();
                    con.Close();
                }

                return clientId;
            }


        //Hesap kapatırken rezervasyonlu masayı kapatma 
            public bool reservationClose(int adisyonID)
       {
            bool result = false;
            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Update Reservasyonlar set durum=0 Where ADISYONID=@adisyonId", con);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("adisyonId", SqlDbType.Int).Value = adisyonID;
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (Exception ex)
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

