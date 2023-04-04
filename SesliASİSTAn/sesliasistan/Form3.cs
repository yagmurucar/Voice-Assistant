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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
       
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-M2DJI34\MSSQLSERVER01;Initial Catalog=KullaniciGiris;Integrated Security=True");
        private void veriler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select* from parola", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Sifre"].ToString());
                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
        int id = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update parola set Ad='"+txtKullanici.Text.ToString()+"',Sifre='"+txtSifre.Text.ToString()+"'where id="+id+"",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriler();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            txtKullanici.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtSifre.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            veriler();
        }

       
    }
}
