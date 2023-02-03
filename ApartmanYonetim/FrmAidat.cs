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
    public partial class FrmAidat : Form
    {
        public FrmAidat()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        void listele()
        {
            SqlCommand komut = new SqlCommand("select aidatid as 'ID',kalanad+' '+kalansoyad as 'Ad Soyad',aidat as 'Aidat Kalan Borç' from TBLAİDAT INNER JOIN TBLKALAN ON TBLAİDAT.aidatsahibi=TBLKALAN.kalanid", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView1.DataSource = dt;      //tabloda gösterdik
        }
        private void FrmAidat_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKalanList_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKalanSil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu İşlem Kalan Kişi Silinmesi Yapıldığında Otomatik Olarak Yapılacaktır");
        }

        private void BtnKalanEkle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu İşlem Kalan Kişi Eklemesi Yapıldığında Otomatik Olarak Yapılacaktır");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAidat.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void BtnKalanGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLAİDAT SET aidat=@b where aidatid=@a", baglanti);
            komut.Parameters.AddWithValue("@a", TxtID.Text);
            komut.Parameters.AddWithValue("@b", TxtAidat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarılı");
            listele();
        }
    }
}
