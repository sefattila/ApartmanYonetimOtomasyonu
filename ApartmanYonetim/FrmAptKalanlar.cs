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

namespace ApartmanYonetim
{
    public partial class FrmAptKalanlar : Form
    {
        public FrmAptKalanlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        void listelekalan()
        {
            SqlCommand komut = new SqlCommand("SELECT kalanid as 'ID',kalanad as 'AD',kalansoyad as 'SOYAD',kalantel as 'TEL',kalankisi as 'KİŞİ SAYISI', kalanblok as 'BLOK', kalansahipid as 'EV SAHİBİ' FROM TBLKALAN", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView2.DataSource = dt;      //tabloda gösterdik
        }

        void listelesahip()
        {
            SqlCommand komut = new SqlCommand("select sahipid as 'ID',sahipad as 'AD SOYAD', sahiptel as 'TEL' from TBLSAHIP", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView1.DataSource = dt;      //tabloda gösterdik
        }
        private void FrmAptKalanlar_Load(object sender, EventArgs e)
        {
            listelekalan();
            listelesahip();
        }

      /*  private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKalanId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKalanAd.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtKalanSoyad.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtKalanTel.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtKisiSayisi.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtKalanBlok.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtKalanSahip.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        */
       /* private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtSahipId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtSahipAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSahipTel.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }*/

        private void BtnKalanList_Click(object sender, EventArgs e)
        {
            listelekalan();
        }

        private void BtnKalanEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EXEC KALANEKLE @ad,@soyad,@tel,@kisi,@blok,@sahip",baglanti);
            komut.Parameters.AddWithValue("@ad", TxtKalanAd.Text);
            komut.Parameters.AddWithValue("@soyad", TxtKalanSoyad.Text);
            komut.Parameters.AddWithValue("@tel", TxtKalanTel.Text);
            komut.Parameters.AddWithValue("@kisi", TxtKisiSayisi.Text);
            komut.Parameters.AddWithValue("@blok", TxtKalanBlok.Text);
            komut.Parameters.AddWithValue("@sahip", TxtKalanSahip.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme Başarılı");
            listelekalan();
        }

        private void BtnKalanSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLKALAN WHERE kalanid=@a", baglanti);
            komut.Parameters.AddWithValue("@a", TxtKalanId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti");
            listelekalan();
        }

        private void BtnKalanGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EXEC KALANGUNCELLE @ad,@soyad,@tel,@kisi,@blok,@sahip,@id", baglanti);
            komut.Parameters.AddWithValue("@ad", TxtKalanAd.Text);
            komut.Parameters.AddWithValue("@soyad", TxtKalanSoyad.Text);
            komut.Parameters.AddWithValue("@tel", TxtKalanTel.Text);
            komut.Parameters.AddWithValue("@kisi", TxtKisiSayisi.Text);
            komut.Parameters.AddWithValue("@blok", TxtKalanBlok.Text);
            komut.Parameters.AddWithValue("@sahip", TxtKalanSahip.Text);
            komut.Parameters.AddWithValue("@id", TxtKalanId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarılı");
            listelekalan();
        }

        private void BtnSahipList_Click(object sender, EventArgs e)
        {
            listelesahip();
        }

        private void BtnSahipEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLSAHIP (sahipad,sahiptel) VALUES(@a,@b)", baglanti);
            komut.Parameters.AddWithValue("@a", TxtSahipAd.Text);
            komut.Parameters.AddWithValue("@b", TxtSahipTel.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme Başarılı");
            listelekalan();
        }

        private void BtnSahipSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLSAHIP WHERE sahipid=@a", baglanti);
            komut.Parameters.AddWithValue("@a", TxtSahipId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti");
            listelekalan();
        }

        private void BtnSahipGncll_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLSAHIP SET sahipad=@a,sahiptel=@b where sahipid=@c", baglanti);
            komut.Parameters.AddWithValue("@a", TxtSahipAd.Text);
            komut.Parameters.AddWithValue("@b", TxtSahipTel.Text);
            komut.Parameters.AddWithValue("@c", TxtSahipId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarılı");
            listelekalan();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtKalanId.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKalanAd.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtKalanSoyad.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtKalanTel.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtKisiSayisi.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtKalanBlok.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtKalanSahip.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtSahipId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtSahipAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSahipTel.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
