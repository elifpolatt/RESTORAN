﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RESTORAN
{
    public partial class MusteriEkleme : Form
    {
        public MusteriEkleme()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtTelefon.Text.Length>6)
            {
                if (txtMusteriAd.Text == "" || txtMusteriSoyad.Text == "")
                {
                    MessageBox.Show("Lütfen müşterinin ad ve soyad alanlarını doldurunuz. ");

                }
                else
                {
                    cMusteriler c = new cMusteriler();
                    bool sonuc = c.MusteriVarmi(txtTelefon.Text);
                    if (!sonuc)
                    {
                        c.Musteriad = txtMusteriAd.Text;
                        c.Musterisoyad = txtMusteriSoyad.Text;
                        c.Telefon = txtTelefon.Text;
                        c.Email = txtEmail.Text;
                        c.Adres = txtAdres.Text;
                        txtMusteriNo.Text = c.musteriEkle(c).ToString();
                        if (txtMusteriNo.Text != "")
                        {
                            MessageBox.Show("Müşteri Eklendi");
                        }
                        else
                        {
                            MessageBox.Show("Müşteri Eklenemedi");
                        }


                    }
                    else
                    {
                        MessageBox.Show("Bu isimde bir kayıt var..");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen en az yedi haneli bir telefon numarası giriniz. ");
            }
        }

        private void txtAdres_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMusteriSec_Click(object sender, EventArgs e)
        {
            if (cGenel._musteriEkleme == 0)
            {
                frmPaketSiparis frm = new frmPaketSiparis();
                cGenel._musteriEkleme = 0;
                this.Close();
                frm.Show();

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtTelefon.Text.Length > 6)
            {
                if (txtMusteriAd.Text == "" || txtMusteriSoyad.Text == "")
                {
                    MessageBox.Show("Lütfen müşterinin ad ve soyad alanlarını doldurunuz. ");

                }
                else
                {
                    cMusteriler c = new cMusteriler();

                    c.Musteriad = txtMusteriAd.Text;
                    c.Musterisoyad = txtMusteriSoyad.Text;
                    c.Telefon = txtTelefon.Text;
                    c.Email = txtEmail.Text;
                    c.Adres = txtAdres.Text;
                    c.Musteriid = Convert.ToInt32(txtMusteriNo.Text);
                    bool sonuc = c.musteriBilgileriGuncelle(c);


                    if (sonuc)
                    {
                       
                        if (txtMusteriNo.Text != "")
                        {
                            MessageBox.Show("Müşteri Güncellendi");
                        }
                        else
                        {
                            MessageBox.Show("Müşteri Güncellenemedi");
                        }


                    }
                    else
                    {
                        MessageBox.Show("Bu isimde bir kayıt var..");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen en az yedi haneli bir telefon numarası giriniz. ");
            }
        }

        private void MusteriEkleme_Load(object sender, EventArgs e)
        {
            if (cGenel._musteriId>0)
            {
                cMusteriler c = new cMusteriler();
                txtMusteriNo.Text = cGenel._musteriId.ToString();
                c.musterilerigetirID(Convert.ToInt32(txtMusteriNo.Text), txtMusteriAd, txtMusteriSoyad, txtTelefon, txtAdres, txtEmail);
            }
        }

      

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            frmMusteriAra frm = new frmMusteriAra();
            this.Close();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnCikis2_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Uyarı!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtMusteriAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMusteriSoyad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
