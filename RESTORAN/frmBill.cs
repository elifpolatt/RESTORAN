using System;
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
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void btnCikis2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Uyarı!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        cSiparis cs = new cSiparis(); int odemeTurId = 0;
        private void frmBill_Load(object sender, EventArgs e)
        {
            gbIndirim.Visible = false;
            if (cGenel._ServisturNo == 1)
            {
                lblAdisyonId.Text = cGenel._AdisyonId;
                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }

                    lblToplamTutar.Text = String.Format("{0:0.000}", toplam);
                    lblOdenecek.Text = String.Format("{0:0.000}", toplam);
                    decimal kdv = Convert.ToDecimal(lblOdenecek.Text);
                    lblKDV.Text = String.Format("{0:0.000}", kdv);

                }
                gbIndirim.Visible = true;
                txtIndirimTutari.Clear();
            }


            else if (cGenel._ServisturNo == 2)
            {
                lblAdisyonId.Text = cGenel._AdisyonId;
                cPaketler pc = new cPaketler();

                txtIndirimTutari.TextChanged += new EventHandler(txtIndirimTutari_TextChanged);
                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));

                odemeTurId = pc.OdemeTurIdGetir(Convert.ToInt32(lblAdisyonId.Text));


                if (odemeTurId == 1)
                {
                    rbKrediKarti.Checked = true;
                }
                else if (odemeTurId == 2)
                {
                    rbNakit.Checked = true;
                }
                else if (odemeTurId == 3)
                {
                    rbTicket.Checked = true;
                }



                cs.getByOrder(lvUrunler, Convert.ToInt32(lblAdisyonId.Text));
                if (lvUrunler.Items.Count > 0)
                {
                    decimal toplam = 0;
                    for (int i = 0; i < lvUrunler.Items.Count; i++)
                    {
                        toplam += Convert.ToDecimal(lvUrunler.Items[i].SubItems[3].Text);
                    }

                    lblToplamTutar.Text = String.Format("{0:0.000}", toplam);
                    lblOdenecek.Text = String.Format("{0:0.000}", toplam);
                    decimal kdv = Convert.ToDecimal(lblOdenecek.Text);
                    lblKDV.Text = String.Format("{0:0.000}", kdv);

                }
                gbIndirim.Visible = true;
                txtIndirimTutari.Clear();
            }
        }

        private void txtIndirimTutari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtIndirimTutari.Text) < Convert.ToDecimal(lblToplamTutar.Text))

                {
                    try
                    {
                        lbIndirim.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtIndirimTutari.Text));
                    }
                    catch (Exception)
                    {
                        lbIndirim.Text = String.Format("{0:0.000}", 0);
                    }

                }
                else
                {
                    MessageBox.Show("İndirim Tutarı Toplam Tutardan Fazla Olamaz");

                }
            }
            catch (Exception)
            {
                lbIndirim.Text = String.Format("{0:0.000}", 0);
            }
        }

        private void chkIndirim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndirim.Checked)
            {
                gbIndirim.Visible = true;
                txtIndirimTutari.Clear();

            }
            else
            {
                gbIndirim.Visible = false;
                txtIndirimTutari.Clear();
            }
        }



        private void lbIndirim_TextChanged(object sender, EventArgs e)
        {

            if (Convert.ToDecimal(lbIndirim.Text) > 0)
            {
                decimal odenecek = 0;
                lblOdenecek.Text = lblToplamTutar.Text;
                odenecek = Convert.ToDecimal(lblOdenecek.Text) - Convert.ToDecimal(lbIndirim.Text);
                lblOdenecek.Text = String.Format("{0:0.000}", odenecek);

            }
            decimal kdv = Convert.ToDecimal(lblOdenecek.Text) * 18 / 100;
            lblKDV.Text = String.Format("{0:0.000}", kdv);
        }

        cMasalar masalar = new cMasalar();
        cRezervasyon rezerve = new cRezervasyon();


        private void button2_Click(object sender, EventArgs e)
        {
            if (cGenel._ServisturNo == 1)
            {
                int masaid = masalar.TableGetbyNumber(cGenel._ButtonName);
                int musteriId = 0;

                if (masalar.TableGetbyState(masaid,4) == true)
                {
                    musteriId = rezerve.getByClientIdFromReservation(masaid);
                }
                else
                {
                    musteriId = 1; //standart müşteri
                }
                int odemeTurId = 0;
                if (rbNakit.Checked)
                {
                    odemeTurId = 1;

                }
                if (rbKrediKarti.Checked)
                {
                    odemeTurId = 2;
                }
                if (rbTicket.Checked)
                {
                    odemeTurId = 3;
                }
                cOdeme odeme = new cOdeme();
                //classın nesnesini burada olusturup 
                odeme.AdisyonID = Convert.ToInt32(lblAdisyonId.Text);
                odeme.OdemeTurId = odemeTurId;
                odeme.MusteriId = musteriId;
                odeme.AraToplam = Convert.ToInt32(lblOdenecek.Text);
                odeme.Kdvtutari = Convert.ToDecimal(lblKDV.Text);
                odeme.GenelToplam = Convert.ToDecimal(lblToplamTutar.Text);
                odeme.Indirim = Convert.ToDecimal(lbIndirim.Text); //label oldugu ıcın text almamız gerekıyor

                //classın ıcıne kendısını tekrar gonderıyoruz sanırım bu recursive fonksıyon 
                bool result = odeme.billClose(odeme);
                if (result)
                {
                    MessageBox.Show("Hesap Kapatıldı.");
                    masalar.setChangeTableState(Convert.ToString(masaid),1);

                    cRezervasyon c = new cRezervasyon();
                    c.reservationClose(Convert.ToInt32(lblAdisyonId.Text));

                    cAdisyon a = new cAdisyon();
                    a.additionClose(Convert.ToInt32(lblAdisyonId.Text), 0);

                    this.Close();
                    frmMasa frm = new frmMasa();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Hesap Kapatılırken Bir Hata Oluştu...");
                }
            }

            //paket sipariş
            else if(cGenel._ServisturNo == 2)
            {
                cOdeme odeme = new cOdeme();
                //classın nesnesini burada olusturup 
                odeme.AdisyonID = Convert.ToInt32(lblAdisyonId.Text);
                odeme.OdemeTurId = odemeTurId;
                odeme.MusteriId = 1; //paket sipariş ıd'si
                odeme.AraToplam = Convert.ToInt32(lblOdenecek.Text);
                odeme.Kdvtutari = Convert.ToDecimal(lblKDV.Text);
                odeme.GenelToplam = Convert.ToDecimal(lblToplamTutar.Text);
                odeme.Indirim = Convert.ToDecimal(lbIndirim.Text); //label oldugu ıcın text almamız gerekıyor
               
                
                

                bool result = odeme.billClose(odeme);
                if (result)
                {

                    
                    cAdisyon a = new cAdisyon();
                    a.additionClose(Convert.ToInt32(lblAdisyonId.Text), 0);

                    cPaketler p = new cPaketler();
                    p.OrderServiceClose(Convert.ToInt32(lblAdisyonId.Text));
                    MessageBox.Show("Hesap Kapatıldı.");



                    this.Close();
                    frmMasa frm = new frmMasa();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Hesap Kapatılırken Bir Hata Oluştu...");
                }
            }
        }

        
        private void btnHesapOzeti_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }

        Font Baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb = new SolidBrush(Color.Black);

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Restaurant", Baslik, sb, 350, 100, st);
            e.Graphics.DrawString("----------", altBaslik, sb, 350, 120, st);
            e.Graphics.DrawString("Ürün Adı                   Adet               Fiyat", altBaslik, sb, 150, 280, st);
            e.Graphics.DrawString("------------------------------------------", altBaslik, sb, 350, 100, st);
            for (int i = 0; i < lvUrunler.Items.Count; i++)
            {
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[0].Text, icerik, sb, 150, 300 + i * 30, st); 
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[1].Text, icerik, sb, 350, 300 + i * 30, st); 
                e.Graphics.DrawString(lvUrunler.Items[i].SubItems[3].Text, icerik, sb, 420, 300 + i * 30, st); 

            }
            e.Graphics.DrawString("-------------------------------------", altBaslik, sb, 150, 300+30*lvUrunler.Items.Count,st);
            e.Graphics.DrawString("İndirim Tutarı   :---------------" + lbIndirim.Text + " TL", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 1 ), st);
            e.Graphics.DrawString("KDV Tutarı       :---------------" + lblKDV.Text + " TL", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 2), st);
            e.Graphics.DrawString("Toplam Tutar     :---------------" + lblToplamTutar.Text + " TL", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 3), st);
            e.Graphics.DrawString("Ödediğiniz Tutar :---------------" + lblOdenecek.Text + " TL", altBaslik, sb, 250, 300 + 30 * (lvUrunler.Items.Count + 4), st);

        }

       

        private void frmBill_Load_1(object sender, EventArgs e)
        {

        }

       
    }
}
