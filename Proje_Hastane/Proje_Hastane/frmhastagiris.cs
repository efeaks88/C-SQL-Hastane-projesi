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

namespace Proje_Hastane
{
    public partial class frmhastagiris : Form
    {
        public frmhastagiris()
        {
            InitializeComponent();
        }
        sql_baglantisi bgl = new sql_baglantisi();

        private void lnkuyeol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //üyeola tıklandığında hastakayıta yönlendiricek
            frmhastakayit fr = new frmhastakayit();
            fr.Show();
        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            //burada değiştireceğimzi bişi yok bu yüzden sqldatareader yazıyoruz. o okuyor sadece.
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                frmhastadetay fr = new frmhastadetay();
                //alttaki 41.yazıyı sonradan ekledik
                //bunun sebebi giriş yaptıktan sonra detay kısmına kişi bilgilerini getirmek.
                fr.tc = msktc.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı tc veya sifre");
            }
            bgl.baglanti().Close();
        }
    }
}
