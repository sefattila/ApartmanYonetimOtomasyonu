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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ATTILA;Initial Catalog=ApartmanYonetimSistemi;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string sql = "select * from TBLADMIN where adminkullanici=@kullanici and adminsifre=@sifre";
                SqlParameter prm1 = new SqlParameter("kullanici", TxtKullaniciAdi.Text.Trim()); //trim boşlukları yok etmek için
                SqlParameter prm2 = new SqlParameter("sifre", TxtSifre.Text.Trim());
                SqlCommand komut = new SqlCommand(sql,baglanti);        //komutumuzu oluşturduk
                komut.Parameters.Add(prm1);        //komutumuza parametreleri atıyoruz
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);     //datatable ın içini doldurmak için
                da.Fill(dt);
                if(dt.Rows.Count>0)     //ilgili alanlar birbirini tutuyomu?
                {
                    FrmAdminPaneli fradmin = new FrmAdminPaneli();      //frmadmin paneline git
                    fradmin.Show();
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Giriş");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.CheckState==CheckState.Checked)
            {
                TxtSifre.UseSystemPasswordChar = true;
                checkBox1.Text = "Gizle";
            }
            else
            {
                TxtSifre.UseSystemPasswordChar = false;
                checkBox1.Text = "Göster";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmSakinGirisi frmsakingirisi = new FrmSakinGirisi();
            frmsakingirisi.Show();
        }
    }
}
