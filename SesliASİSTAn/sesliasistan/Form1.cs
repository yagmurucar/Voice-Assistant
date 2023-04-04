using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeechLib;//Kütüphane 
using System.Speech.Recognition;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using SesliASİSTAn.sesliasistan;

namespace SesliAsistan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Baslangıcta herbirinin enable özelliği false
        DbProductEntities db = new DbProductEntities();//Tablolara ulaşmak için nesne turettım

        public void ProductList()//Method oluşturuldu 
        {
            var products = db.TblProduct.ToList();//Değerler eşittir tblproducttaki verileri getir
            dataGridView1.DataSource = products;//productan gelen değerler
        }
        void enabled()
        {
            txtBuyPrice.Enabled = false;
            txtname.Enabled = false; 
            txtSellPrice.Enabled=false;
            txtCategory.Enabled = false;
            txtStock.Enabled= false;
            txtBrand.Enabled=false;
            maskedTextBox1.Enabled=false;
            comboBox1.Enabled = false;

        }
        void colormethod()
        {
            txtBuyPrice.BackColor = Color.White;
            txtname.BackColor = Color.White;
            txtSellPrice.BackColor = Color.White;
            txtCategory.BackColor = Color.White;
            txtStock.BackColor = Color.White;
            txtBrand.BackColor = Color.White;
            maskedTextBox1.BackColor = Color.White;
            comboBox1.BackColor = Color.White;

        }
       
        private void BtnListen_Click(object sender, EventArgs e)
        {
            BtnListen.BackColor = Color.Red;

            SpVoice ses = new SpVoice();
            ses.Speak(richTextBox1.Text);
        }

        private void txtSpeak_Click(object sender, EventArgs e)
            //Sesi Metne Dönüştürme
        {
            btnSpeak.BackColor = Color.BlueViolet;

            SpeechRecognitionEngine sr = new SpeechRecognitionEngine();//ses tanımlama methodu  nesne türettik//KOnusma tanıma methodu spech
            Grammar g = new DictationGrammar();//gramer sınıfından nesne türetildi
            sr.LoadGrammar(g);//imla yapısı oluşturuldu
            //Yönetici izinlerinin alınması gerekiyor nasıl alınacak
            try
            {
                btnSpeak.Text = "Please Speak";
                sr.SetInputToDefaultAudioDevice();//kullanılan ses çıkışını varsayılan ses çıkışı olarak ayarlandı
                RecognitionResult res= sr.Recognize();//tanımlama
                //richTextBox1.Clear();
                richTextBox1.Text = res.Text;//tnımlanan değerleri  resden gelen değeri text olarak ver
            }
            catch(Exception)
            {
                btnSpeak.Text = "error try again";
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductList();//methodu cağırdık ekleyip eklemediğine baktık ürünleri
            //richtexbox içinde product list yazarsa çağırılacak bu method

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //PRODUCT KISMININ OLUSTURULMASI
            //Başlangıcta ifi okur Hiyarşik sıralama kullanılır o yuzden en başa yazıldı
            if (txtname.BackColor == Color.Yellow  && txtname.Enabled==true)//Richtexboxsun değeri ddeğiştiğinde arka plan rengi kırmızıysa
            {
                txtname.Text = richTextBox1.Text;//richtextboxdan gelen değeri txtname ye yazdırma
                enabled();//değeri yaz sonra tekrar pasif hale getir
                //amac o an uzerıne yazı yazılan değeri görütnülemek
                colormethod();//yazdırıldıktan sonra rengi gitsin
            }
            if(txtSellPrice.BackColor == Color.Yellow && txtSellPrice.Enabled==true)
            {
                txtSellPrice.Text = richTextBox1.Text;
                enabled();
                colormethod();
            }
            if(txtBuyPrice.BackColor == Color.Yellow && txtBuyPrice.Enabled==true)
            {
                //eğer böyle ise richtextboxdan glen değeri yazdır ve methodları devreye sok
                txtBuyPrice.Text = richTextBox1.Text;
                enabled();
                colormethod();
            }
            if(txtBrand.BackColor==Color.Yellow && txtBrand.Enabled==true)
            {
                txtBrand.Text = richTextBox1.Text;
                enabled();
                colormethod();
            }
            if(txtStock.BackColor==Color.Yellow && txtStock.Enabled==true)
            {
                txtStock.Text = richTextBox1.Text;
                enabled();
                colormethod();
            }
            if(txtCategory.BackColor==Color.Yellow && txtCategory.Enabled==true)
            {
                txtCategory.Text = richTextBox1.Text;
                enabled();
                colormethod();
            }
            if(maskedTextBox1.BackColor==Color.Yellow && maskedTextBox1.Enabled==true)
            {
                //maskedtextbox1.text = datetime.now.toshortdatestring(); //kısa tarih formatını

                enabled();
                colormethod();
            }
            if (comboBox1.BackColor == Color.Yellow && comboBox1.Enabled == true)
            {
                comboBox1.Text = "Active";//şimdilik aktif olsun
                enabled();
                colormethod();
            }

            //text değiştiği zaman
            //Sesli komut ile ürün listeleme
            //SESLİ KOMUT ALGILAMA KISMI
            if (richTextBox1.Text == "List of products"||richTextBox1.Text=="Products list"||richTextBox1.Text=="One"||richTextBox1.Text=="High")
            {
                ProductList();//Eğer rixhtextboxta yazılı olansa listeyi getir.
            }
            if(richTextBox1.Text=="Add"||richTextBox1.Text=="Add to")
            {
                //değerlerin veri tabanına kaydetme
                TblProduct t = new TblProduct();
                t.Name = txtname.Text;
                t.Brand = txtBrand.Text;
                t.SellPrice = decimal.Parse(txtSellPrice.Text);//desimal türünde olduğu için 
                t.BuyPrice = decimal.Parse(txtBuyPrice.Text);
                t.Stock= short.Parse(txtStock.Text);//convertoint16
                t.ProductCase = true;// veritabanında true ve no olarak girildiği için
                t.Date = DateTime.Parse(maskedTextBox1.Text);
                t.Category = txtCategory.Text;
                db.TblProduct.Add(t);
                db.SaveChanges();
                label10.Text = "Products Saved in Database";
            }
            //İmleci istenilen araca konumlama
            if(richTextBox1.Text == "Products name"||richTextBox1.Text=="Two")
            {
                txtname.Enabled = true;
                txtname.Focus();
                txtname.BackColor = Color.Yellow;
            }
            //textbox1.ToLower(); ile büyük küçük 
            if (richTextBox1.Text == "Brand" || richTextBox1.Text == "Six")
            {
                txtBrand.Enabled = true;
                txtBrand.Focus();
                txtBrand.BackColor = Color.Yellow;
            }
            if(richTextBox1.Text=="Stock"||richTextBox1.Text=="Nine")
            {
                txtStock.Enabled = true;
                txtStock.Focus();
                txtStock.BackColor = Color.Yellow;
                
            }
            if (richTextBox1.Text == "Sell price"||richTextBox1.Text=="Four")
            {
                txtSellPrice.Enabled = true;
                txtSellPrice.Focus();
                txtSellPrice.BackColor = Color.Yellow;

            }
            if(richTextBox1.Text=="Category"|| richTextBox1.Text=="Seven")
            {
                txtCategory.Enabled = true;
                txtCategory.BackColor = Color.Yellow;
                txtCategory.Focus();

            }
            if(richTextBox1.Text=="Date")
            {
                maskedTextBox1.Enabled = true;
                maskedTextBox1.Focus();
                maskedTextBox1.BackColor = Color.Yellow;
            }
            if(richTextBox1.Text=="Case")
            {
                comboBox1.Enabled = true;
                comboBox1.Focus();
                comboBox1.BackColor = Color.Yellow;

            }
            if (richTextBox1.Text == "Buy price"||richTextBox1.Text=="Five")
            {
                txtBuyPrice.Enabled = true;
                txtBuyPrice.Focus();
                txtBuyPrice.BackColor = Color.Yellow;
            }
            if(richTextBox1.Text=="Exit Application"||richTextBox1.Text=="Exit app"||richTextBox1.Text=="Exit"||richTextBox1.Text=="Quit")
            {
                Application.Exit();
            }
            //if(richTextBox1.Text=="Paint")
            //{
            //    //painti açtırmak için yaparız.
            //    System.Diagnostics.Process.Start("MsPaint");
            //}
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            enabled();//False enable değeri
        }

     

        private void comboBox1_BackColorChanged(object sender, EventArgs e)
        {
            if (comboBox1.BackColor == Color.Yellow)
            {
                comboBox1.Text = "Active";
            }
        }

        private void maskedTextBox1_BackColorChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.BackColor == Color.Yellow)
            {
                maskedTextBox1.Text = DateTime.Now.ToString("dd.MM.yyyy");//Gün ay Yıl olarak yazması için 
            }
        }

        private void label10_TextChanged(object sender, EventArgs e)
        {
            //eklendikten sonra sesli bildiri vermesi için kullanıldı
            SpVoice ses = new SpVoice();
            ses.Speak(label10.Text);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ////string delete = "delete from TblProduct where Id=@secili";
            ////SqlParameter dl = new SqlParameter( "secili", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

            ////SqlDataReader sqlDataReader = new SqlCommand(delete,);
        }
    }





        //Uygulamanın tamamnen sesli olması için timer kullanıp içine sesi algılama komutlarını 
        //yerleştrebiliriz.

    }

