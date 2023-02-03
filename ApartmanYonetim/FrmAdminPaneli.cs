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
    public partial class FrmAdminPaneli : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        public FrmAdminPaneli()
        {
            InitializeComponent();
        }
       
        private void BtnAptBilgi_Click(object sender, EventArgs e)
        {
            FrmAptBilgi frmaptbilgi = new FrmAptBilgi();
            frmaptbilgi.Show();
        }

        private void BtnAdminBilgi_Click(object sender, EventArgs e)
        {
            FrmAdminBilgi frmadminbilgi = new FrmAdminBilgi();
            frmadminbilgi.Show();
        }

        private void BtnAptKalanlar_Click(object sender, EventArgs e)
        {
            FrmAptKalanlar frmaptkalanlar = new FrmAptKalanlar();
            frmaptkalanlar.Show();
        }

        private void BtnOdemeBilgi_Click(object sender, EventArgs e)
        {
            FrmOdeme frmodeme = new FrmOdeme();
            frmodeme.Show();            
        }
        
        private void FrmAdminPaneli_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select kalanblok ,count(*) from TBLKALAN group by kalanblok", baglanti);
            SqlDataReader dr = komut.ExecuteReader();   //dr nin içine 0 ve 1 indeksleri atadık
            while(dr.Read())      //okuma işlemi
            {
                chart1.Series["Blok"].Points.AddXY(dr[0], dr[1]);       //dr nin içindki bilgileri grafiğe yazdık
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from View_Kalanlar", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);        //adapterin içini doldurduk
            dataGridView1.DataSource = dt;      //tabloda gösterdik
            baglanti.Close();
        }

        private void BtnAidat_Click(object sender, EventArgs e)
        {
            FrmAidat frmaidat = new FrmAidat();
            frmaidat.Show();
        }
    }
}
