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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sql_baglantisi bgl = new sql_baglantisi();
        public string tc;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            //yukardakini yazıp giriş yaptığımız forma kod ekledik.

            //doktor ad soyad getirme.
            SqlCommand komut = new SqlCommand("Select Doktorad,Doktorsoyad from Tbl_Doktorlar where Doktortc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //randevu getirme
            DataTable dt = new DataTable();
            //aşağıda randevudoktor= yaparken bir tane daha çift tırnak açamazdık bu yüzden tek tırnak açtık.
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_randevular where randevudoktor='" + lbladsoyad.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            frmdoktorbilgiduzenle frd = new frmdoktorbilgiduzenle();
            frd.tcno = lblTC.Text;
            frd.Show();

        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            frmduyurular fr = new frmduyurular();
            fr.Show();

        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchsikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }
    }
}
