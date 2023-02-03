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
    public partial class FrmAptBilgi : Form
    {
        public FrmAptBilgi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        void listele()
        {
            SqlCommand komut = new SqlCommand("select aptadres as 'Adres',aptkalankisi as 'Kalan Kişi',aptdairesayisi as 'Daire Sayısı',aptbloksayisi as 'Blok Sayısı' from TBLAPTBILGI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView1.DataSource = dt;      //tabloda gösterdik
        }
        private void FrmAptBilgi_Load(object sender, EventArgs e)
        {
            listele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();   //adres
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();   //kalan kişi
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();   //daire sayısı
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();   //bloksayısı
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnGucelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("exec APTGETIR @a,@b,@c,@d", baglanti);       //procedür sayesinde güncelledik
            komut.Parameters.AddWithValue("@a", textBox1.Text);
            komut.Parameters.AddWithValue("@b", textBox2.Text);
            komut.Parameters.AddWithValue("@c", textBox4.Text);
            komut.Parameters.AddWithValue("@d", textBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Gerçekleşti");
        }
    }
}
