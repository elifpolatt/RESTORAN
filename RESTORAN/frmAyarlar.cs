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
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
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

        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            cPersoneller cp = new cPersoneller();
            cPersonellerGorev cpg = new cPersonellerGorev();
            string gorev = cpg.PersonelGorevTanim(cGenel._gorevId);
            if (gorev == "Müdür")
            {
                cp.personelGetbyInformation(cbPersonel);
                cpg.PersonelGorevGetir(cbGorevi);
                cp.personelBilgileriniGetirLV(lvPersoneller);
                btnYeni.Enabled = true;
                btnSil.Enabled = false;
                btnBilgiDegistir.Enabled = false;
                btnEkle.Enabled = false;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                groupBox4.Visible = true;
                txtSifre.ReadOnly = true;
                txtSifreTekrar.ReadOnly = true;
                lblBilgi.Text = "Mevki : Müdür / Yetki Sınırlı / Kullanıcı: " + cp.personelBilgiGetirIsim(cGenel._personelId);


            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                groupBox4.Visible = false;
                lblBilgi.Text = "Mevki : Müdür / Yetki Sınırlı / Kullanıcı: " + cp.personelBilgiGetirIsim(cGenel._personelId);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtYeniSifreTekrar.Text.Trim() != "" || txtYeniSifreTekrar.Text.Trim() != "")
            {
                if (txtYeniSifre.Text == txtYeniSifreTekrar.Text)
                {
                    if (txtPersonelId.Text != "")
                    {
                        cPersoneller c = new cPersoneller();
                        bool sonuc = c.personelSifreDegistir(Convert.ToInt32(txtPersonelId.Text), txtYeniSifre.Text);
                        if (sonuc)
                        {
                            MessageBox.Show("Şifre Değiştirme İşlemi Başarıyla Gerçekleşmiştir.");
                        }
                        else
                        {
                            MessageBox.Show("Personel Seçiniz!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Şifreler Aynı Değil!");
                    }
                }
                else
                {
                    MessageBox.Show("Şifre Alanını Boş Bırakmayınız.");
                }
            }
        }

        private void cbGorevi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersonellerGorev c = (cPersonellerGorev)cbGorevi.SelectedItem;
            txtGorevId2.Text = Convert.ToString(c.PersonelGorevId);


        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnYeni.Enabled = false;
            btnEkle.Enabled = true;
            btnBilgiDegistir.Enabled = false;
            btnSil.Enabled = false;
            txtSifre.ReadOnly = false;
            txtSifreTekrar.ReadOnly = false;




        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Silmek İstediğinize Emin Misiniz?","Uyarı!!!",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)== DialogResult.OK)
            {
                if (lvPersoneller.SelectedItems.Count>0)
                {
                    cPersoneller c = new cPersoneller();
                    bool sonuc = false;
                    //bool sonuc = c.personelSil(Convert.ToInt32(txtpersonelD.Text));
                    if (sonuc)
                    {
                        MessageBox.Show("Kayıt Başarıyla Silinmiştir.");
                        c.personelBilgileriniGetirLV(lvPersoneller);
                    }
                    else
                    {
                        MessageBox.Show("Kayıt Silinirken Bir Hata Oluştu.");
                    }
                }
                else
                {
                    MessageBox.Show("Kayıt Seçiniz!!!!");
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

        }
    }
}
