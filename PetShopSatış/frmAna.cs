using System.Data;
using System.Data.SqlClient;

namespace lanet
{
    public partial class frmAna : Form
    {
        public frmAna()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from sepet ",baglanti);
            adtr.Fill(daset, "sepet");

            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["tc"].Visible = false;
            dataGridView1.Columns["adsoyad"].Visible = false;
            dataGridView1.Columns["telefon"].Visible = false;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMüsteriEkleme ekle = new frmMüsteriEkleme();
            ekle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmMüsteriList listele = new frmMüsteriList();
            listele.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hayvan evcil = new hayvan();
            evcil.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmÜrün ürün = new frmÜrün();
            ürün.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmhayvanlist hayvanlist = new frmhayvanlist();
            hayvanlist.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmÜrünList ürünlist = new frmÜrünList();
            ürünlist.ShowDialog();
        }
        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from sepet", baglanti);
                lblGenelToplam.Text = komut.ExecuteScalar() + " TL";
                baglanti.Close();
            }
            catch (Exception)
            {
                ;
            }
        }
        private void frmAna_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
           if (txtTc.Text == "")
            {
                txtAdsoyad.Text = "";
                txtTelefon.Text = "";

            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from müsteri where tc like '" + txtTc.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtAdsoyad.Text = read["adsoyad"].ToString();
                txtTelefon.Text = read["telefon"].ToString();

            }
            baglanti.Close();

        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            Temizle();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from urunm where barkodno like '" + txtBarkodNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtÜrünAdý.Text = read["urunadi"].ToString();
                txtSatýsFiyatý.Text = read["satisfiyati"].ToString();


            }
            baglanti.Close();
        }

        private void Temizle()
        {
            if (txtBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {

                    if (item is TextBox)
                    {
                        if (item != txtMiktar)
                        {
                            item.Text = "";
                        }
                    }
                }

            }
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from sepet", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtBarkodNo.Text == read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyat) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyat)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdsoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", txtÜrünAdý.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatýsFiyatý.Text));
                komut.Parameters.AddWithValue("@toplamfiyat", double.Parse(txtToplamFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet set miktari=miktari+'"+int.Parse(txtMiktar.Text)+ "' where barkodno='" + txtBarkodNo.Text + "'", baglanti);
                komut2.ExecuteNonQuery();
                SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyat=miktari*satisfiyati where barkodno='"+txtBarkodNo.Text+"'", baglanti);

                
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }
            
            txtMiktar.Text = "1";
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
            if (txtBarkodNo.Text == "")
            {


                foreach (Control item in groupBox2.Controls)
                {

                    if (item is TextBox)
                    {
                        if (item != txtMiktar)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatýsFiyatý.Text)).ToString();
            }

            catch (Exception)
            {
                ;
            }
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno='"+dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepetten çýkarýldý");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();

            
            
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSatýsFiyatý_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtToplamFiyat.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatýsFiyatý.Text)).ToString();
            }

            catch (Exception)
            {
                ;
            }
        }

        private void btnSatisÝptal_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün sepetten çýkarýldý");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmSatisListele listele = new frmSatisListele();
            listele.ShowDialog();
        }

        private void btnSatýsYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satiss(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyat) values(@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyat)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", txtAdsoyad.Text);
                komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyat", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyat"].Value.ToString()));
                komut.ExecuteNonQuery();
               
                SqlCommand komut2 = new SqlCommand("update urunm set miktar=miktar-'" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno= '" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                
                
                
            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepet", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }
    }
}