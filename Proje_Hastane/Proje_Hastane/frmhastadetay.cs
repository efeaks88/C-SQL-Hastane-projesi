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
    public partial class frmhastadetay : Form
    {
        public frmhastadetay()
        {
            InitializeComponent();
        }
        //ilk olarka public değişken oluşturuyoruz.
        public string tc;

        sql_baglantisi bgl = new sql_baglantisi();

        private void frmhastadetay_Load(object sender, EventArgs e)
        {
            //yazılan tcyi text olarak alıp önceden yazdığımız tc kısmına getiriyor.
            lbltc.Text = tc;
            //adsoyad çekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            //dr.read çalıştığı süre boyunca getir
            while(dr.Read())
            {
                lbladsoyad.Text = dr[0] +" "+ dr[1];

            }
            bgl.baglanti().Close();

            //randevu geçmişi
            DataTable dt = new DataTable();
            //datatableda verileri aktarmak için sqldatadapter yazarız.
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" + tc, bgl.baglanti());
            //da adıyla oluşturduk ve onu aşağıdaki formülle doldurduk.
            da.Fill(dt);
            //dataverinin veri kaynağı = dt den gelen tablo.
            dataGridView1.DataSource = dt;

            //branşları çekelim
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                //altta sıfır yazmamızın nedeni üstteki ile aynı 0.da brans yazıyor çümnkü
                cmbbrans.Items.Add(dr2[0]);
            }
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //alttakini yazdığımızı yazmasaydık her filtreleme yaptığımızda filtre kalıp tekrar filtreleme yapıcaktık
            //ama yazdığımız için filtre sıfırlanıp tekrardan bize izin vericek.
            cmbdoktor.Items.Clear();
            //branşı seçtiğimiz zamanlar branşların görünmesini istiyoruz
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //branşı bu şu olan doktor diye gelmesini istiyoruz bir nevi 2.filtre
            //sql command ile yazamayız datagride yazdıracağımız için
            DataTable dt = new DataTable();
            //kelime bazlı arama yaparken tek tırnak içerisinde yazmamız gerektiğinden aşağıdaki gibi yazdık. Sonra ekleme yaptık nden yaptık hiçbir fikrim yok
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='" + cmbbrans.Text + "'" + "and randevudoktor='"+cmbdoktor.Text+"' and RandevuDurum=0 ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnlbilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //bilgi düzenelye bastığımız zaman bilgi düzenle formuna gidicez.
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.tcno = lbltc.Text;
            fr.Show();
            //şuan geçiş okey ama biz bilgi düzenleye tıklayınca otomatik bilgilerin dolu gelmesini istiyoruz diyelim

        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update tbl_randevular set randevudurum=1,hastatc=@p1,hastasikayet=@p2 where Randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            komut.Parameters.AddWithValue("@p2", richsikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();

        }
    }
}
