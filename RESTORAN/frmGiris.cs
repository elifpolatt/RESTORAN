namespace RESTORAN
{
    public partial class frmGiris : Form
    {
        public frmGiris()
        {
            InitializeComponent();
        }


        private void frmGiris_Load(object sender, EventArgs e)
        {
            cPersoneller p = new cPersoneller();
            p.personelGetbyInformation(cbKullanici);


        }
        private void btnGiris_Click(object sender, EventArgs e)
        {


            cGenel gnl = new cGenel();
            cPersoneller p = new cPersoneller();
            bool result = p.personelEntryControl(txtSifre.Text, cGenel._personelId); 

            if (result)
            {
                cPersonelHareketleri ch = new cPersonelHareketleri();
                ch.PersonelId = cGenel._personelId;
                ch.Islem = "Giriþ Yaptý";
                ch.Tarih = DateTime.Now;
                ch.PersonelActionSave(ch);


                this.Hide();
                frmMenu menu = new frmMenu();
                menu.Show();


            }
            else
            {
                MessageBox.Show("Þifreniz eksik veya hatalý...","Uyarý!!!",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void cbKullanici_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersoneller p = (cPersoneller)cbKullanici.SelectedItem;
            cGenel._personelId = p.PersonelId;
            cGenel._gorevId = p.PersonelGorevId;

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Çýkmak istediðinize emin misiniz?", "Uyarý!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
