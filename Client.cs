using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace Plăți_Furnizori
{
    public class Client
    {
        private string CNP, Nume;
        private double Suma;
        public static int v;
        private MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database=plati_furnizori");

        public Client(string verif)
        {
            con.Open();
            string query = "select * from client where CNP = '" + verif + "'";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                this.CNP = reader["CNP"].ToString();
                this.Nume = reader["Nume"].ToString();
                this.Suma = (double)reader["Sold"];
                Selectare f1 = new Selectare();
                f1.Show();
                

            }
            else
            {
                MessageBox.Show("CNP-ul este greșit");
                v = 1;
            }
            con.Close();
        }

        public double getSuma()
        {
            return Suma;
        }
        public string getCNP()
        {
            return CNP;
        }
        public string getNume()
        {
            return Nume;
        }

        public void setsuma(double val)
        {
            Suma = Suma - val;
        }  
    }
}
