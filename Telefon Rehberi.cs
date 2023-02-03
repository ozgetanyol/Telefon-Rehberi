using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ders021122
{
    public partial class Telefon_Rehberi : Form
    {
        public Telefon_Rehberi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Server = .; Database = db_TelefonRehberi; Trusted_Connection = True;");
        private void btnkaydet_Click(object sender, EventArgs e)

        {
            try
            {
                SqlCommand komut = new SqlCommand("INSERT INTO [dbo].[Kisiler] ([kisiad] ,[kisisoyad],[kisitelefonno],[kisiAktifMi] ,[kisiAciklama]) VALUES('" + txtad.Text + "', '" + txtsoyad.Text + "', '" + txttelefon.Text + "', '" + cbxaktif.Checked + "', '" + txtaciklama.Text + "')", baglanti);
                baglanti.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Kaydedildi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kaydetmede bir sıkıntı çıktı, lütfen sonra tekrar deneyiniz." + hata.Message);

            }
            finally
            {
                baglanti.Close();   
            }
            

        }

        private void Telefon_Rehberi_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from [dbo].[Kisiler] ", baglanti);
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            lblkisisayisi.Text=komut.ExecuteScalar().ToString();
            baglanti.Close();
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
           
            try
            {
                SqlCommand komut = new SqlCommand("UPDATE [dbo].[Kisiler] SET[kisiad] = '" + txtad.Text + "',[kisisoyad] = '" + txtsoyad.Text + "',[kisitelefonno] = '" + txttelefon.Text+ "',[kisiAktifMi] = '" + cbxaktif.Checked + "',[kisiAciklama] = '" + txtaciklama.Text + "' WHERE kisiID =" + txtkisiid.Text.ToString(), baglanti);

                baglanti.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncellendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Güncellerken bir sıkıntı çıktı, lütfen sonra tekrar deneyiniz." + hata.Message);

            }
            finally
            {
                baglanti.Close();
            }
        }

        private void btngetir_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlCommand komut = new SqlCommand("select * from [dbo].[Kisiler]", baglanti);
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[0].ToString()+" : "+dr[2].ToString() + " " + dr[1].ToString());
            }
            baglanti.Close();
        }

        private void btnara_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlCommand komut = new SqlCommand("select * from [dbo].[Kisiler] where kisisoyad like '%" + txtsoyad.Text + "%'", baglanti);
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[2].ToString() + " " + dr[1].ToString());
            }
            baglanti.Close();  
        }

        private void btnkisiekle_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("DELETE FROM [dbo].[Kisiler] WHERE kisiID =" + txtkisiid.Text.ToString(), baglanti);

                baglanti.Open();
                komut.ExecuteNonQuery();
                MessageBox.Show("Silindi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Silerken bir sıkıntı çıktı, lütfen sonra tekrar deneyiniz." + hata.Message);

            }
            finally
            {
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlCommand komut = new SqlCommand("select * from [dbo].[Kisiler] where kisiID="+txtkisiid.Text, baglanti);
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr["kisiad"].ToString();
                txtsoyad.Text = dr["kisisoyad"].ToString();
                txttelefon.Text = dr["kisitelefonno"].ToString();
                cbxaktif.Checked =(Convert.ToInt32(dr["kisiAktifMi"]) == 1) ? true :false;
                txtaciklama.Text = dr["kisiAciklama"].ToString();    
            }
            baglanti.Close();
        }
    }
}
