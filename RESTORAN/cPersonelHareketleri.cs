﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RESTORAN
{
    internal class cPersonelHareketleri
    {
        cGenel gnl = new cGenel();

        #region Fields
        private int _ID;
        private int _PersonelId;
        private int _PersonelGorevId;
        private string _Islem;
        private DateTime _Tarih;
        private bool _Durum;
        #endregion

        #region Properties
        public int ID { get => _ID; set => _ID = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public int PersonelGorevId { get => _PersonelGorevId; set => _PersonelGorevId = value; }
        public string Islem { get => _Islem; set => _Islem = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public bool Durum { get => _Durum; set => _Durum = value; } 
        #endregion

        public bool PersonelActionSave(cPersonelHareketleri ph) //daha güvenli olması için 
        {
            bool result = false;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Insert Into personelHareketleri(PERSONELID,ISLEM,TARIH)Values(@personelId,@islem,@tarih)",con);

            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.Parameters.Add("@personelId", SqlDbType.Int).Value = ph._PersonelId;
                cmd.Parameters.Add("@islem", SqlDbType.VarChar).Value = ph._Islem;
                cmd.Parameters.Add("@tarih", SqlDbType.DateTime).Value = ph._Tarih;

                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }

            catch (Exception ex)
            {
                string hata = ex.Message;
                throw;
            }
            
            return result;
        }
    }
}
