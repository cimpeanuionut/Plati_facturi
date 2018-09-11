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
    public partial class Plata : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=plati_furnizori;allowuservariables=True");
        private string cont,nume_f;
        private double sum,coeficient_valutar;
        public Plata()
        {
            InitializeComponent();
        }

        private void Plata_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;
            label4.Visible = false;


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar !=8 && e.KeyChar != 46)
            {
                e.Handled = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from factura where cod_factura = '" + textBox3.Text + "' and CNP_client='"+Autentificare.client.getCNP().ToString()+"'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textBox1.Visible = true;
                label1.Visible = true;
                button1.Visible = true;
                textBox2.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                button2.Visible = false;
                label1.Text = reader["valuta"].ToString();
                this.cont = reader["cont_bancar"].ToString();
                this.sum = (double)reader["suma_restanta"];
                this.nume_f = reader["nume_furnizor"].ToString();
            }
            else
            {
                MessageBox.Show("Acestă factură nu există");
                textBox3.Text = "";

            }
            con.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update_Baze();
            istoric();
        }

        private double selectare_coeficient_valutar()
        {
            if (label1.Text.ToString() == "RON")
            {
                coeficient_valutar = 1;
            }
            else
            {
                if ( label1.Text.ToString() == "EUR")
                {
                    coeficient_valutar = 4.63;
                }
                else
                {
                    if (label1.Text.ToString() == "USR")
                    {
                        coeficient_valutar = 4.0051;
                    }else
                    {
                        if (label1.Text.ToString() == "GBP")
                        {
                            coeficient_valutar = 5.13;
                        }
                        else
                        {
                            coeficient_valutar = 4.11;
                        }
                    }
                }
            }

            return coeficient_valutar;
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Selectare selectare = new Selectare();
            selectare.Show();
            this.Hide();
        }

        private void update_Baze()
        {
            if (Autentificare.client.getSuma() - Double.Parse(textBox1.Text)*selectare_coeficient_valutar() >= 0)
            {
                if (textBox2.Text == cont && Double.Parse(textBox1.Text) <= sum)
                {
                    con.Open();
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update factura set suma_restanta= suma_restanta- '" + Double.Parse(textBox1.Text) + "' where cod_factura= '"+textBox3.Text.ToString()+"'";
                    cmd.ExecuteNonQuery();
                    Autentificare.client.setsuma(Double.Parse(textBox1.Text));
                    MySqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "update client set Sold= Sold- '" +Math.Round(Double.Parse(textBox1.Text)*selectare_coeficient_valutar(),2,MidpointRounding.ToEven) + "' where CNP= '"+Autentificare.client.getCNP().ToString()+"'";
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Plata s-a efectuat cu succes");
                }
                else
                {
                    textBox2.Text = "";
                    textBox1.Text = "";
                    MessageBox.Show("Cont bancar invalid sau suma este mai mare  decât valoare facturii");

                }
            }
            else
            {
                textBox2.Text = "";
                textBox1.Text = "";
                MessageBox.Show("Nu sunt suficienți bani în cont");
            }
        }

        private void istoric()
        {
            con.Open();
            var src = DateTime.Now;
            string data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            string newCon = "insert into istoric_plati(CNP_client,Nume_client,Data, suma_initiala, Tranzactie, suma_retrasa ,Suma,Valuta, Nume_furnizor) VALUES ('" + Autentificare.client.getCNP().ToString() + "','" + Autentificare.client.getNume().ToString() + "','" + data + "', '"+Math.Round(Autentificare.client.getSuma()/selectare_coeficient_valutar() + Double.Parse(textBox1.Text),2,MidpointRounding.ToEven) +"','" + "Plata factura" + "','" + Math.Round(Double.Parse(textBox1.Text),2,MidpointRounding.ToEven) + "','"+Math.Round(Autentificare.client.getSuma()/selectare_coeficient_valutar(),2,MidpointRounding.ToEven)+"','"+label1.Text.ToString()+"','"+nume_f+"')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
