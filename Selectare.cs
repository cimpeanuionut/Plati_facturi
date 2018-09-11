using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plăți_Furnizori
{
    public partial class Selectare : Form
    {
        public Selectare()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plata sr = new Plata();
            sr.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sold_Curent sc = new Sold_Curent();
            sc.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Plati_Luna pl = new Plati_Luna();
            pl.Show();
            this.Hide();
        }
    }
}
