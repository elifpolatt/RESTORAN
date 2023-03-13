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
    public partial class frmMusteriAra : Form
    {
        public frmMusteriAra()
        {
            InitializeComponent();
        }

        private void frmMusteriAra_Load(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.musteriBilgileriGetir(lvMusteriler);

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

        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            MusteriEkleme m = new MusteriEkleme();
            cGenel._musteriEkleme = 1;
           // m.btn.Visible = false;
           // m.btnEkle.Visible = false;
            m.Show();
        }
        private void btnMusteriSec_Click(object sender, EventArgs e)
        {

        }
        private void btnMusteriGuncelle_Click(object sender, EventArgs e)
        {
            if (lvMusteriler.SelectedItems.Count>0)
            {
                MusteriEkleme frm = new MusteriEkleme();
                cGenel._musteriEkleme = 1;
                cGenel._musteriId = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
               // frm.btnEkle.Visible = false;
               // frm.btnGuncelle.Visible = true;
                
                
                this.Close();
                frm.Show();
            }
        
        
        }

        private void frmGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void txtMusteriAd_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.musterigetirAd(lvMusteriler, txtMusteriAd.Text);
        }

        private void frmAdisyonBul_Click(object sender, EventArgs e)
        {
            if (txtAdisyonID.Text!="")
            {
                cGenel._AdisyonId = txtAdisyonID.Text;
                cPaketler c = new cPaketler();

                bool sonuc = c.getCheckOpenAdditionID(Convert.ToInt32(txtAdisyonID.Text));  
                if (sonuc)
                {
                    frmBill frm = new frmBill();
                    cGenel._ServisturNo = 2;
                    frm.Show();
                }
                else
                {
                    MessageBox.Show(txtAdisyonID.Text + "nolu adisyon bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Aramak istediğiniz adisyonu yazınız.");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
