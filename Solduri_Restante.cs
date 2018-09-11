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
namespace Plăți_Furnizori
{
    public partial class Solduri_Restante : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=plati_furnizori;allowuservariables=True");
        public Solduri_Restante()
        {
            InitializeComponent();
        }

        private void Solduri_Restante_Load(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from factura where CNP_client = '" + Autentificare.client.getCNP().ToString() + "'and data_scadenta='"+Form1.data+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            Selectare dzs = new Selectare();
            dzs.Show();
            this.Hide();
        }

        
    }
}
