using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Gestion_Etudiants
{
    public partial class module : Form
    {
        public module()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader rd;//classe qui nous permet de recuperer les donnes de dbase de donnees 
        
        private void button1_Click(object sender, EventArgs e)
        {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = String.Format("insert into Module values( '" + textBox2.Text + "')");
                int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
                if (r != 0)
                {
                    MessageBox.Show("Etudiant bien ajouté", "ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cnx.Close();
                    vider();
                    affiche_table();
                }

          
        }

        private void affiche_table()
        {
            cnx.Open();
            cmd = new SqlCommand("Select * from Module", cnx);
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rd);
            dataGridView1.DataSource = dt;
            rd.Close();
            cnx.Close();

        }
        private void vider()
        {

            cnx.Open();
            textBox1.Text = "";
            textBox2.Text = "";
           
            cnx.Close();
        }
        

        private void module_Load(object sender, EventArgs e)
        {
            affiche_table();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnx.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection =cnx;
            cmd.CommandText = String.Format("update  Module set   nom_module ='" + textBox2.Text + "'  where num_module=" + textBox1.Text + "");

            int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
            if (r != 0)
            {
                MessageBox.Show("modulet bien modifié", "modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
               cnx.Close();
               vider();
               affiche_table();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cnx.Open();
            string req = " Delete from Module where num_module=" + textBox1.Text + "";
            cmd = new SqlCommand(req, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Bien Supprimé");
            cnx.Close();
            vider();
            affiche_table();
        }

        private void etudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3_Etudiant ma = new Form3_Etudiant();
            ma.Show();
        }

        private void moduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            module ma = new module();
            ma.Show();
        }

        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gestion_notes ma  = new Gestion_notes();
            ma.Show();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void consultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Consultation ma = new Consultation();
            ma.Show();
        }



    }
}
