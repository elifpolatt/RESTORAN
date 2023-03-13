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
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }

        private void lblMasaNo_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCikis2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinize emin misiniz?", "Uyarı!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Hesap İşlemleri
        void islem(Object sender, EventArgs e)
        {
            Button btn = sender as Button;


            //butonlara tıklandıgında ekranda yazdırılması gereken numaraları girdik
            switch (btn.Name)
            {
                case "btn1":
                    txtAdet.Text += (1).ToString();
                    break;
                case "btn2":
                    txtAdet.Text += (2).ToString();
                    break;
                case "btn3":
                    txtAdet.Text += (3).ToString();
                    break;
                case "btn4":
                    txtAdet.Text += (4).ToString();
                    break;
                case "btn5":
                    txtAdet.Text += (5).ToString();
                    break;
                case "btn6":
                    txtAdet.Text += (6).ToString();
                    break;
                case "btn7":
                    txtAdet.Text += (7).ToString();
                    break;
                case "btn8":
                    txtAdet.Text += (8).ToString();
                    break;
                case "btn9":
                    txtAdet.Text += (9).ToString();
                    break;
                case "btn0":
                    txtAdet.Text += (0).ToString();
                    break;

                default:
                    MessageBox.Show("Sayı Gir");
             break;
            }
        }

        int tableId; int AdditionId;
        private void frmSiparis_Load(object sender, EventArgs e)
        {

            //tıkladnıgında o masanın degerini tut

            //masanın ıd numarası lazım bu da masalardaki
            //bir mi iki mi üç mü diye bilmemiz için cMasalar'da bir fonksiyon yazdım
            //  string aa = TableValue;
            //int lenght = aa.Length; //aa'nın uzunlugu kadarını al 
            //return Convert.ToInt32(aa.Substring(lenght - 1, 1));  //bunu toStringInt ile integer'a dönüştür
            //lenght - 1, 1  ->>> uzunlugun bır eksiğini al ve birinci karakteri al 
            //bruada masa 1 mesela bunun uzunlugu kadarını al bır eksiginde ne var 1 var buradaki 1 sayısını 

            lblMasaNo.Text = cGenel._ButtonValue;
           
            cMasalar ms = new cMasalar();
            tableId = ms.TableGetbyNumber(cGenel._ButtonValue);

            if (ms.TableGetbyState(tableId, 2) == true || ms.TableGetbyState(tableId, 4) == true)
            {
                cAdisyon Ad = new cAdisyon();
                AdditionId = Ad.getByAddition(tableId);
                //alınan ürünler siparişlere eklenecek 
                cSiparis orders = new cSiparis();
                orders.getByOrder(lvSiparisler, AdditionId);
            }





            btn1.Click += new EventHandler(islem);
            btn2.Click += new EventHandler(islem);
            btn3.Click += new EventHandler(islem);
            btn4.Click += new EventHandler(islem);
            btn5.Click += new EventHandler(islem);
            btn6.Click += new EventHandler(islem);
            btn7.Click += new EventHandler(islem);
            btn8.Click += new EventHandler(islem);
            btn9.Click += new EventHandler(islem);
            btn0.Click += new EventHandler(islem);

        }

        cUrunCesitleri Uc = new cUrunCesitleri();

        private void btnAnaYemek3_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnAnaYemek3);
        }

        private void btnIcecekler8_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnIcecekler8);

        }

        private void btnTatlilar7_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnTatlilar7);

        }

        private void btnSalata6_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnSalata6);

        }

        private void btnFastFood5_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnFastFood5);

        }

        private void btnCorba1_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnCorba1);

        }

        private void btnMakarna4_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnMakarna4);

        }

        private void btnAraSicak2_Click(object sender, EventArgs e)
        {
            Uc.getByProductTypes(lvMenu, btnAraSicak2);

        }





        int sayac = 0; int sayac2 = 0;
        //seçilmek istenen ürüne çift tıklandığında tablodaki sıraya ekle 

        private void lvMenu_DoubleClick(object sender, EventArgs e)
        {
            if (txtAdet.Text == "")
            {
                txtAdet.Text = "1"; //adet birse direkt buradan alınacak 

            }

            // ************************************************
            // ************************************************


            if (lvMenu.Items.Count > 0)  //item count lvmenu icinde var mı 
            {


                sayac = lvSiparisler.Items.Count;  // sayac 11 gösteriyor

                lvSiparisler.Items.Add(lvMenu.SelectedItems[0].Text); //sıfırıncı sayac tablonun ilk kısmı oluyoır birinciye ekledik
                lvSiparisler.Items[sayac].SubItems.Add(txtAdet.Text);
                lvSiparisler.Items[sayac].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text.ToString()); //ürün ıd'sini veriyor
                lvSiparisler.Items[sayac].SubItems.Add((Convert.ToDecimal(lvMenu.SelectedItems[0].SubItems[1].Text) * Convert.ToDecimal(txtAdet.Text)).ToString()); //kac adet eklyoruz 1 ADET ekleniyıor 1*10 = 10 adet eklendi
                //yukardaki fiyatını veriyor. indexleri bu şekilde oldugu için
                lvSiparisler.Items[sayac].SubItems.Add("0");                 
                sayac2 = lvYeniEklenenler.Items.Count;
                lvSiparisler.Items[sayac].SubItems.Add(sayac2.ToString());
                //sayacın degeri sıfır su an bu burda tutuluyor daha sonradan bize lazım olacak 



                lvYeniEklenenler.Items.Add(AdditionId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(lvMenu.SelectedItems[0].SubItems[2].Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(txtAdet.Text);
                lvYeniEklenenler.Items[sayac2].SubItems.Add(tableId.ToString());
                lvYeniEklenenler.Items[sayac2].SubItems.Add(sayac2.ToString());

                sayac2++;

                txtAdet.Text = "";


              



            }
        }

        private void btnSiparis_Click(object sender, EventArgs e)
        {
            /**
             
            1-Masa boş
            2-Masa dolu
            3-Masa rezerve 
            4-Dolu rezerve
             */


            cMasalar masa = new cMasalar();
            frmMasa ms = new frmMasa();
            cAdisyon newAddition = new cAdisyon();
            cSiparis saveOrder = new cSiparis();

            bool sonuc = false;

            if (masa.TableGetbyState(tableId, 1) == true) //masa boş ise
            {
                newAddition.ServisTurNo = 1;
                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DateTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(cGenel._ButtonName, 2);


                if (lvSiparisler.Items.Count > 0)
                {

                    //kaç kayıt varsa dön
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        //değer string geliyor bizim bunu convert ile int etmemiz gerekiyor
                        //ürün ıd'sini alacagız tablodakiş ücüncü ama  index olarak ikinci
                        saveOrder.AdisyonID = newAddition.getByAddition(tableId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        // ürün adeti alıyoruz tabloda ikinci ama index olarak birinci
                        saveOrder.setSaveOrder(saveOrder);

                    }
                    this.Close(); //burayı kapat ve frmMasa'yı aç 
                    ms.Show();
                }
            }

            //masanın durumu 2 ise
            else if (masa.TableGetbyState(tableId, 2) == true || masa.TableGetbyState(tableId, 4) == true)
            {
                if (lvYeniEklenenler.Items.Count > 0)
                {
                    for (int i = 0; i < lvYeniEklenenler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[1].Text);
                        saveOrder.AdisyonID = newAddition.getByAddition(tableId);
                        saveOrder.Adet = Convert.ToInt32(lvYeniEklenenler.Items[i].SubItems[2].Text);
                        saveOrder.setSaveOrder(saveOrder);

                    }
                    cGenel._AdisyonId = Convert.ToString(newAddition.getByAddition(tableId));

                    this.Close();
                    ms.Show();
                }

            }
            else if (masa.TableGetbyState(tableId, 3) == true)
            {
                newAddition.ServisTurNo = 1;
                newAddition.PersonelId = 1;
                newAddition.MasaId = tableId;
                newAddition.Tarih = DateTime.Now;
                sonuc = newAddition.setByAdditionNew(newAddition);
                masa.setChangeTableState(cGenel._ButtonName, 4);


                if (lvSiparisler.Items.Count > 0)
                {

                    //kaç kayıt varsa dön
                    for (int i = 0; i < lvSiparisler.Items.Count; i++)
                    {
                        saveOrder.MasaId = tableId;
                        saveOrder.UrunId = Convert.ToInt32(lvSiparisler.Items[i].SubItems[2].Text);
                        //değer string geliyor bizim bunu convert ile int etmemiz gerekiyor
                        //ürün ıd'sini alacagız tablodakiş ücüncü ama  index olarak ikinci
                        saveOrder.AdisyonID = newAddition.getByAddition(tableId);
                        saveOrder.Adet = Convert.ToInt32(lvSiparisler.Items[i].SubItems[1].Text);
                        // ürün adeti alıyoruz tabloda ikinci ama index olarak birinci
                        saveOrder.setSaveOrder(saveOrder);

                    }
                    this.Close(); //burayı kapat ve frmMasa'yı aç 
                    ms.Show();
                }
            }
        }
        private void lvSiparisler_DoubleClick(object sender, EventArgs e)
        {
            if(lvSiparisler.Items.Count > 0)
            {
                if(lvSiparisler.SelectedItems[0].SubItems[4].Text != "0")
                {
                    cSiparis saveOrder = new cSiparis();
                    saveOrder.setDeleteOrder(Convert.ToInt32(lvSiparisler.SelectedItems[0].SubItems[4].Text));

                }
                else
                {
                    for(int i =0; i < lvSiparisler.SelectedItems.Count; i++)
                    {
                        if(lvYeniEklenenler.Items[i].SubItems[4].Text == lvSiparisler.SelectedItems[0].SubItems[5].Text)
                        {
                            lvYeniEklenenler.Items.RemoveAt(i);
                        }
                    }
                }
                lvSiparisler.Items.RemoveAt(lvSiparisler.SelectedItems[0].Index);
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            if (txtAra.Text == "")
            {
                txtAra.Text = "";
            }
            else
            {
                cUrunCesitleri cu = new cUrunCesitleri();
                cu.getByProductSearch(lvMenu, Convert.ToInt32(txtAra.Text));
            }
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            cGenel._ServisturNo = 1;
            cGenel._AdisyonId = AdditionId.ToString();
            frmBill frm = new frmBill();
            this.Close();
            frm.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
