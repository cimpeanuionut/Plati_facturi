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
    public partial class Plati_Luna : Form
    {
        private MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database=plati_furnizori");
        public Plati_Luna()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Selectare s = new Selectare();
            s.Show();
            this.Hide();
        }

        private void Plati_Luna_Load(object sender, EventArgs e)
        {
            
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from istoric_plati where Tranzactie = '" + "Plata factura" + "'and MONTH(data)='"+DateTime.Today.Month+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
