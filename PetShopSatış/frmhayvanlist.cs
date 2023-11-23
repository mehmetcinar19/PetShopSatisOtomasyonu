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
    public partial class frmhayvanlist : Form
    {
        public frmhayvanlist()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmhayvanlist_Load(object sender, EventArgs e)
        {
            Hayvan_Göster();
        }

        private void Hayvan_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from evcil", baglanti);
            adtr.Fill(daset, "evcil");
            dataGridView1.DataSource = daset.Tables["evcil"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtİsim.Text = dataGridView1.CurrentRow.Cells["isim"].Value.ToString();
            txtIrk.Text = dataGridView1.CurrentRow.Cells["ırk"].Value.ToString();
            txtYas.Text = dataGridView1.CurrentRow.Cells["yas"].Value.ToString();
            txtRenk.Text = dataGridView1.CurrentRow.Cells["renk"].Value.ToString();
            
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update evcil set ırk=@ırk, yas=@yas,renk=@renk where isim=@isim", baglanti);
            komut.Parameters.AddWithValue("@isim", txtİsim.Text);
            komut.Parameters.AddWithValue("@ırk", txtIrk.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["evcil"].Clear();
            Hayvan_Göster();
            MessageBox.Show("Evcil Hayvan Bilgileri Güncellendi / Satın alma sahiplen");
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
            SqlCommand komut = new SqlCommand("delete from evcil where isim='" + dataGridView1.CurrentRow.Cells["isim"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["evcil"].Clear();
            Hayvan_Göster();
            MessageBox.Show("Evcil Hayvan Silindi / Satın alma sahiplen.");
        }
    }
}
