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
    public partial class frmsekretergiris : Form
    {
        public frmsekretergiris()
        {
            InitializeComponent();
        }
        //sınıfmızdan bgl diye bağlantı türetiyoruz.
        sql_baglantisi bgl = new sql_baglantisi();

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            //okutalım bakalım aynı mı
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
                //okuma işleminin doğru olup olmadığına bakalım
            {
                frmsekreterdetay frs = new frmsekreterdetay();
                frs.tcnumara = msktc.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("SEN WAIFU DEĞİLSİN HATA HATA HATA HATA");
            }
            bgl.baglanti().Close();
        }
    }
}
