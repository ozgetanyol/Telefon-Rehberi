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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server = .; Database = NORTHWND; Trusted_Connection = True;");
        private void btnGetir_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Employees",baglanti);
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[2].ToString()+ " "+ dr[1].ToString());
            }
            baglanti.Close();   
        }

        private void btnara_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); 

            SqlCommand cmd = new SqlCommand("Select * from Employees where Firstname like '%"+ txtad.Text+"%'" , baglanti);
            if (baglanti.State==ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr[2].ToString() + " " + dr[1].ToString());
            }
            baglanti.Close();
        }
    }
}
