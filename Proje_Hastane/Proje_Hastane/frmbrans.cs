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
    public partial class frmbrans : Form
    {
        public frmbrans()
        {
            InitializeComponent();
        }

        sql_baglantisi bgl = new sql_baglantisi();

        private void frmbrans_Load(object sender, EventArgs e)
        {
            //buraya branşları getirdik bide önceden sekreter detaya gidip buraya yönlendirmek için olan formülümüzü yazdık
            //tablo doldurmak için yine datatable yazdık ve dataadapter ı kullandık.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());
            //tüm branşları çektik ve onları getirdik.
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            //yukarda yine denklemimiz ilk önce sanal tablo oluşturup sonra ona formül girip bi sınıfa atıyıp sonra doldurup eşitledik.
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            //ekleyeceğimiz için insert
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbranss.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bi veriye çift tıklayınca texte yazı gelmesini istiyorsak datagrid in events kısmından cell click ten yapıyoruz bu olayı
            //anladığım kadarıyla datagridte seçerken int olarak alıyoruz.
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtbranss.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //silme işlemi
            SqlCommand komut = new SqlCommand("delete from tbl_branslar where bransid=@b1", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_branslar set bransad=@b1 where bransid=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtbranss.Text);
            komut.Parameters.AddWithValue("@p2", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş guncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
