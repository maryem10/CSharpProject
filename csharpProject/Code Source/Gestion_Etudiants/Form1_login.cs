using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gestion_Etudiants
{
    public partial class Form1_login : Form
    {
        public Form1_login()
        {
            InitializeComponent();
        }

        private void button_connecter_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "user" && textBox2.Text == "root")
            {
                this.Hide();
                Form2_accueil ma = new Form2_accueil();
                ma.Show();

            }
            else
            {
                MessageBox.Show("login ou mot de passe incorrect");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_fermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
