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
    public partial class Sold_Curent : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database=plati_furnizori");
        private string valuta;
        private double coeficient;
        public Sold_Curent()
        {
            InitializeComponent();
        }

        private void Sold_Curent_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            button6.Visible = false;
        }

        public void design()
        {
            label2.Visible = true;
            label3.Visible = true;
            label1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            design();
            label2.Text = Autentificare.client.getSuma() +" RON";
            valuta = "RON";
            coeficient = 1;
            istoric();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            design();
            label2.Text = Math.Round(Autentificare.client.getSuma()/4.63,2,MidpointRounding.ToEven) + " EUR";
            valuta = "EUR";
            coeficient = 4.63;
            istoric();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            design();
            label2.Text = Math.Round(Autentificare.client.getSuma() / 4.0051, 2, MidpointRounding.ToEven) + " USD";
            valuta = "USR";
            coeficient = 4.0051;
            istoric();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            design();
            label2.Text = Math.Round(Autentificare.client.getSuma() / 5.13, 2, MidpointRounding.ToEven) + " GBP";
            valuta = "GBP";
            coeficient = 5.13;
            istoric();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            design();
            label2.Text = Math.Round(Autentificare.client.getSuma() / 4.11, 2, MidpointRounding.ToEven) + " CHF";
            valuta = "CHF";
            coeficient = 4.11;
            istoric();
        }

        public void istoric()
        {
            con.Open();
            var src = DateTime.Now;
            string data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            string newCon = "insert into istoric_plati(CNP_client,Nume_client,Data,Tranzactie,Suma,Valuta) VALUES ('" + Autentificare.client.getCNP().ToString() + "','" + Autentificare.client.getNume().ToString() + "','" + data + "','" + "Vizualizare Sold" + "','" + Math.Round(Autentificare.client.getSuma()/coeficient,2,MidpointRounding.ToEven) + "','"+valuta+"')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Selectare dzs = new Selectare();
            dzs.Show();
            this.Hide();
        }
    }
}
