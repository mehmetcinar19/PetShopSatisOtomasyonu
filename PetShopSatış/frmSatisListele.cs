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
    public partial class frmSatisListele : Form
    {
        public frmSatisListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=pets;Integrated Security=True");
        DataSet daset = new DataSet();

        private void satislistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from satiss ", baglanti);
            adtr.Fill(daset, "satiss");

            dataGridView1.DataSource = daset.Tables["satiss"];
            
            baglanti.Close();
        }
        private void frmSatisListele_Load(object sender, EventArgs e)
        {
            satislistele();
        }
    }
}
