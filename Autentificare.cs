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
    public partial class Autentificare : Form
    {

        public static Client client;
        public Autentificare()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please insert your CNP");
            }
            else
            {
                client = new Client(textBox1.Text.ToString());
                if (Client.v != 1)
                    this.Hide();                    
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar <'0' || e.KeyChar > '9')
            {
                if (e.KeyChar != (char)8)
                {
                    MessageBox.Show("You pressed "+ e.KeyChar+ "\nPlease enter number only");
                    e.KeyChar = (char)0;
                }
            }
            
        }
    }
}
