using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SesliAsistan.VoiceAsistant
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-M2DJI34\MSSQLSERVER01;Initial Catalog=KullaniciGiris;Integrated Security=True");
        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();//baglanti acıldı
                string sql = "select*from parola where Ad=@adi AND Sifre=@sifresi";
                SqlParameter prm1 = new SqlParameter("adi",txtKullanici.Text.Trim());//Trim ile bosluk yok etmek için kullanıldı
                SqlParameter prm2 = new SqlParameter("sifresi",txtSifre.Text.Trim());
                SqlCommand komut = new SqlCommand(sql,baglanti);//bağlantıları birletirdik
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if(dt.Rows.Count > 0)//Satır sayısı
                {
                    Form1 fr=new Form1();
                    fr.Show();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Giriş"); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fr=new Form3();
            fr.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
