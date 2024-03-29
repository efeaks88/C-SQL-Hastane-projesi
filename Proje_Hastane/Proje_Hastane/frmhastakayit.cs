﻿using System;
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
    public partial class frmhastakayit : Form
    {
        public frmhastakayit()
        {
            InitializeComponent();
        }
        //bu yazdığımız kod ile bağlantıya ulaşıyoruz. Sınıf oluşturmuştuyk hani
        sql_baglantisi bgl = new sql_baglantisi();

        private void btnkayityap_Click(object sender, EventArgs e)
        {
            //sql kodunu çağırıyoruz.
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet ) values (@p1,@p2,@p3,@p4,@p5,@p6) ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktc.Text);
            komut.Parameters.AddWithValue("@p4", msktel.Text);
            komut.Parameters.AddWithValue("@p5", txtsifre.Text);
            komut.Parameters.AddWithValue("@p6", cmbcinsiyet.Text);
            //çalıştırmak için
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt gerçekleşti şifreniz: " + txtsifre.Text,"bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
