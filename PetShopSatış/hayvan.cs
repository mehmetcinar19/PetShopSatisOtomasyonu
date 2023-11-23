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
    public partial class hayvan : Form
    {
        public hayvan()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        private void hayvan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into evcil(isim,ırk,yas,renk)values(@isim,@ırk,@yas,@renk)", baglanti);
            komut.Parameters.AddWithValue("@isim", txtİsim.Text);
            komut.Parameters.AddWithValue("@ırk", txtIrk.Text);
            komut.Parameters.AddWithValue("@yas", txtYas.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Evcil Hayvan Eklendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = " ";
                }
            }
        }
    }
}
