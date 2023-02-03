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
    public partial class FrmSakinGirisi : Form
    {
        public FrmSakinGirisi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        private void BtnKalanList_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sql = "SELECT kalanad as 'Ad',kalansoyad as 'Soyad',kalanblok as 'Blok',aidat as 'Aidat',sahipad as 'Sahip Ad',sahiptel as 'Sahip Tel' FROM TBLKALAN INNER JOIN TBLAİDAT ON TBLAİDAT.aidatsahibi=TBLKALAN.kalanid INNER JOIN TBLSAHIP ON TBLKALAN.kalansahipid=TBLSAHIP.sahipid where kalanid =@id";
            SqlParameter prm1 = new SqlParameter("@id", TxtKalanID.Text.Trim()); //trim boşlukları yok etmek için
            SqlCommand komut = new SqlCommand(sql, baglanti);        //komutumuzu oluşturduk
            komut.Parameters.Add(prm1);        //komutumuza parametreleri atıyoruz
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);     //datatable ın içini doldurmak için
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void BtnOdeme_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update TBLAİDAT set aidat-=@a where aidatsahibi=@b", baglanti);
            komut.Parameters.AddWithValue("@a", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@b", TxtKalanID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ödeme Yapıldı");
        }

        private void BtnYoneticiKim_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from View_AdminBilgi", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
