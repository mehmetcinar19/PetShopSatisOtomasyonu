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
    public partial class frmÜrünList : Form
    {
        public frmÜrünList()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmÜrünList_Load(object sender, EventArgs e)
        {
            Ürün_Göster();
        }

        private void Ürün_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from urunm", baglanti);
            adtr.Fill(daset, "urunm");
            dataGridView1.DataSource = daset.Tables["urunm"];
            baglanti.Close();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urunm set urunadi=@urunadi,miktar=@miktar,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@urunadi", txtÜrünAdı.Text);
            komut.Parameters.AddWithValue("@miktar", int.Parse(txtMiktar.Text));
            komut.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlışFiyat.Text));
            komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatısFıyat.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urunm"].Clear();
            Ürün_Göster();
            MessageBox.Show("Ürün Bilgileri Güncellendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from urunm where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urunm"].Clear();
            Ürün_Göster();
            MessageBox.Show("Ürün Silindi");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            txtÜrünAdı.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtMiktar.Text = dataGridView1.CurrentRow.Cells["miktar"].Value.ToString();
            txtAlışFiyat.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            txtSatısFıyat.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }
    }
}
