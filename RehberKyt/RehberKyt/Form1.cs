using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace RehberKyt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        veritabani conn = new veritabani();
        string fotografyolu;
        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * From KISILER",conn.baglanti());
            da.Fill(dt); 
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["FOTOGRAF"].Visible = false;                    }
        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            txtmail.Text = "";
            mtxttel.Text = "";
            pictureBox1.Refresh();
            txtad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            try
            {
                
                if (conn.baglanti().State != ConnectionState.Closed)
                {
                    MessageBox.Show("Veri Tabanı Bağlantısı Başarılı Bir Şekilde Gerçekleşti");
                    listele();
                }
                else
                {
                    MessageBox.Show("Maalesef Bağlantı Yapılamadı...!");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Hata! " + err.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KİŞİ SİSTEME KAYDEDİLSİN Mİ ? ", "BİLGİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
            MySqlCommand komut = new MySqlCommand("insert into KISILER(AD,SOYAD,TELEFON,MAIL,FOTOGRAF) values (@P1,@P2,@P3,@P4,@P5)",conn.baglanti());
            komut.Parameters.AddWithValue("@P1",txtad.Text);
            komut.Parameters.AddWithValue("@P2",txtsoyad.Text);
            komut.Parameters.AddWithValue("@P3",mtxttel.Text);
            komut.Parameters.AddWithValue("@P4",txtmail.Text);
            komut.Parameters.AddWithValue("@P5",fotografyolu);
            komut.ExecuteNonQuery();
            conn.baglanti().Close();
               listele();
            temizle();            }           
            else {                
                MessageBox.Show("KİŞİ SİSTEME KAYDEDİLMEDİ", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);         }
            conn.baglanti().Close();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
             conn.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mtxttel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KİŞİ SİSTEMDEN SİLİNSİN Mİ ?", "BİLGİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                MySqlCommand komut = new MySqlCommand("delete from KISILER where ID="+txtid.Text,conn.baglanti());
            komut.ExecuteNonQuery();
            conn.baglanti().Close();
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("SİLME İŞLEMİ İPTAL EDİLDİ.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            conn.baglanti().Close();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KİŞİ SİSTEMDEN GÜNCELLENSİN Mİ ? ", "BİLGİ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                MySqlCommand komut = new MySqlCommand("update KISILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4,FOTOGRAF=@P5 where ID=@P6",conn.baglanti());
                komut.Parameters.AddWithValue("@P1",txtad.Text);
                komut.Parameters.AddWithValue("@P2",txtsoyad.Text);
                komut.Parameters.AddWithValue("@P3",mtxttel.Text);
                komut.Parameters.AddWithValue("@P4",txtmail.Text);
                komut.Parameters.AddWithValue("@P5",fotografyolu);
                komut.Parameters.AddWithValue("@P6",txtid.Text);
                komut.ExecuteNonQuery();
                conn.baglanti().Close();
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("GÜNCELLEME İŞLEMİ İPTAL EDİLDİ.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            conn.baglanti().Close();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            fotografyolu = openFileDialog1.FileName;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
              this.Text = this.Text.Substring(1) + this.Text.Substring(0, 1);
            this.Text.ToUpper();
        }
    }
}
