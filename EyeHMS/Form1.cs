using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Xml.Linq;
using System.Threading.Tasks.Dataflow;

namespace EyeHMS
{
    public partial class Form1 : Form
    {
        private SqlConnection Con;
        private SqlCommand cmd;
        private DataTable dt;
        private SqlDataAdapter sda;
        private string Constr;

        public Form1()
        {
            InitializeComponent();

        }
        
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=C:\\USERS\\USER\\DOCUMENTS\\EYECAREDB.MDF;Integrated Security=True");
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand komut = new SqlCommand("insert into PatientTbl (PatName, PatPhone, PatAdress, PatDOB, PatGender, PatAllergies) values  (@PatName, @PatPhone, @PatAdress, @PatDOB, @PatGender, @PatAllergies)", baglanti);

                komut.Parameters.AddWithValue("@PatName", textBox1.Text);
                komut.Parameters.AddWithValue("@PatPhone", textBox2.Text);
                komut.Parameters.AddWithValue("@PatAdress", textBox3.Text);
                komut.Parameters.AddWithValue("@PatDOB", textBox4.Text);
                komut.Parameters.AddWithValue("@PatGender", comboBox1.Text);
                komut.Parameters.AddWithValue("@PatAllergies", textBox6.Text);

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Hasta Baþarýyla Kayýt Eildi!");


                DataSet dataSet = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from PatientTbl", baglanti);
                adapter.Fill(dataSet);
                guna2DataGridView1.DataSource = dataSet.Tables[0];
                guna2DataGridView1.Columns[0].HeaderText = "PatId";
                guna2DataGridView1.Columns[1].HeaderText = "PatName";
                guna2DataGridView1.Columns[2].HeaderText = "PatPhone";
                guna2DataGridView1.Columns[3].HeaderText = "PatAdress";
                guna2DataGridView1.Columns[4].HeaderText = "PatDOB";
                guna2DataGridView1.Columns[5].HeaderText = "PatGender";
                guna2DataGridView1.Columns[6].HeaderText = "PatAllergies";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "Ýþlemde yanlýþlýk var tekrar deneyiniz!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2DataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
            textBox5.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = guna2DataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = guna2DataGridView1.CurrentRow.Cells[6].Value.ToString();

        }
      
       
       
        void MusteriGetir()
        {
            
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select *From PatientTbl", baglanti);
            DataTable tablo = new DataTable();
            adtr.Fill(tablo);
            guna2DataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("update PatientTbl set PatName=@PatName ,PatPhone=@PatPhone,PatAdress=@PatAdress,PatDOB=@PatDOB,PatGender=@PatGender,PatAllergies=@PatAllergies where PatId=@PatId", baglanti);

                komut.Parameters.AddWithValue("@PatId", textBox5.Text);
                komut.Parameters.AddWithValue("@PatName", textBox1.Text);
                komut.Parameters.AddWithValue("@PatPhone", textBox2.Text);
                komut.Parameters.AddWithValue("@PatAdress", textBox3.Text);
                komut.Parameters.AddWithValue("@PatDOB", textBox4.Text);
                komut.Parameters.AddWithValue("@PatGender", comboBox1.Text);
                komut.Parameters.AddWithValue("@PatAllergies", textBox6.Text);


                komut.ExecuteNonQuery();
                baglanti.Close();
                MusteriGetir();
                MessageBox.Show("Güncelleme Baþarýlý");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "Ýþlemde yanlýþlýk var tekrar deneyiniz!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
    }
}