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
    public partial class FrmAdminBilgi : Form
    {
        public FrmAdminBilgi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        void listele()
        {   //select adminid as 'Admin ID',kalanid as 'Kalan ID', adminkullanici as 'Kullanıcı Adı',adminsifre as 'Şifre',adminadsoyad as 'Ad Soyad',admintelno as 'Tel No' from TBLADMIN
            SqlCommand komut = new SqlCommand("select * from View_Admin", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView1.DataSource = dt;      //tabloda gösterdik
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

       /* private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    //data griddeki tabloda herhangi bir sütün veya satıra tıkladığımızda bilgiler text boxın içine gelmesi için
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();   //ıd
            TxtKalanID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); //kalanıd
            TxtKullanici.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();   //kullanıcı adı
            TxtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();   //sifre
            TxtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();   //ad soyad
            TxtTelNo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();      //tel no
        }*/

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EXEC ADMINEKLE @a,@b,@c", baglanti);
            komut.Parameters.AddWithValue("@a", TxtKalanID.Text);
            komut.Parameters.AddWithValue("@b", TxtKullanici.Text);
            komut.Parameters.AddWithValue("@c", TxtSifre.Text);
            //komut.Parameters.AddWithValue("@d", TxtAdSoyad.Text);
            //komut.Parameters.AddWithValue("@e", TxtTelNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme İşlemi Gerçekleşti");
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLADMIN WHERE adminid=@a", baglanti);
            komut.Parameters.AddWithValue("@a", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Gerçekleşti");
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("EXEC ADMINGUNCELLE @a,@b,@e", baglanti);
            komut.Parameters.AddWithValue("@a", TxtKullanici.Text);
            komut.Parameters.AddWithValue("@b", TxtSifre.Text);
            //komut.Parameters.AddWithValue("@c", TxtAdSoyad.Text);
           // komut.Parameters.AddWithValue("@d", TxtTelNo.Text);
            komut.Parameters.AddWithValue("@e", TxtKalanID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Gerçekleşti");
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //data griddeki tabloda herhangi bir sütün veya satıra tıkladığımızda bilgiler text boxın içine gelmesi için
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();   //ıd
            TxtKalanID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(); //kalanıd
            TxtKullanici.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();   //kullanıcı adı
            TxtSifre.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();   //sifre
            //TxtAdSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();   //ad soyad
            //TxtTelNo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();      //tel no
        }
    }
}
