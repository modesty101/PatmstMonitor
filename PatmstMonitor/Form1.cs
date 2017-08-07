using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace PatmstMonitor
{
    public partial class Form1 : Form
    {
  
        public Form1()
        {
            InitializeComponent();
        }

        private void CountPatient(object sender, EventArgs e)
        {
            string connInfo = "Server=192.168.119.131; Port=3306; Database=hospital;Uid=root;Pwd=123;";
            // string query = "SELECT count(patid) FROM patmst";

            MySqlConnection conn = new MySqlConnection(connInfo);
            MySqlCommand count = new MySqlCommand("SELECT COUNT(patid) FROM patmst",conn);
            conn.Open();
            int myCount = Convert.ToInt32(count.ExecuteScalar());
            //MessageBox.Show(Convert.ToString(count.ExecuteScalar()));
            label1.Text = myCount.ToString();
        }

        private void RefreshPatmst(object sender, EventArgs e)
        {
            string connInfo = "Server=192.168.119.131; Port=3306; Database=hospital;Uid=root;Pwd=123;";
            string query = "SELECT * FROM patmst";

            using (MySqlConnection conn = new MySqlConnection(connInfo))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void Refresh(object sender, EventArgs e)
        {
            RefreshPatmst(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            RefreshPatmst(sender, e);
            timer1.Interval = 30000;
            timer1.Tick += new EventHandler(RefreshPatmst);
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            CountPatient(sender, e);
        }
    }
}
