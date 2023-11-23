using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lanet
{
    public partial class frmÜrün : Form
    {
        public frmÜrün()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into urunm(barkodno,urunadi,miktar,alisfiyati,satisfiyati)values(@barkodno,@urunadi,@miktar,@alisfiyati,@satisfiyati)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
            komut.Parameters.AddWithValue("@miktar", int.Parse (txtMiktar.Text));
            komut.Parameters.AddWithValue("@alisfiyati",double.Parse (txtAlışFiyat.Text));
            komut.Parameters.AddWithValue("@satisfiyati",double.Parse (txtSatısFıyat.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün Eklendi");
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
        }

        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            if (BarkodNotxt.Text == "")
            {
                label11.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urunm where barkodno like '"+BarkodNotxt.Text+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                ÜrünAdıtxt.Text = read["barkodno"].ToString();
                Miktartxt.Text = read["miktar"].ToString();
                AlısFıyattxt.Text = read["alisfiyati"].ToString();
                SatısFıyattxt.Text = read["satisfiyati"].ToString();

            }
            baglanti.Close();
        }

        private void btnVarOlanEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urunm set miktar=miktar+'" + int.Parse(Miktartxt.Text) + "' where barkodno= '" + BarkodNotxt.Text + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("Var Olan Ürüne Ekleme Yapıldı");
        }

        private void frmÜrün_Load(object sender, EventArgs e)
        {

        }
    }
}
