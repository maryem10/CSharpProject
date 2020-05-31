using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;//biblio feha lles classes li ankhdem bihum f connection dial base de donnes

namespace Gestion_Etudiants
{
    public partial class Form3_Etudiant : Form
    {
        public Form3_Etudiant()
        {
            InitializeComponent();
        }
        //alors hna kayn instansiw classe sqlconncetion ha9itach heya lrespnsable ela connction bin bd o visual 
        //datasource: nom de serveur ; catalog: nom de bd ; lakhhera dial securite
        SqlConnection cnx = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader rd;//classe qui nous permet de recuperer les donnes de dbase de donnees 



        private void affiche_table()
            {
                    cnx.Open();
                    cmd = new SqlCommand("Select * from etudiant", cnx);
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
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            cnx.Close();
        }
        



        private void etudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3_Etudiant ma = new Form3_Etudiant();
            ma.Show();
        }

        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text == "" && dateTimePicker1.Text == "")
            {
                MessageBox.Show("veuillez remplir tous les champs");
            }


            try
            {
                cnx.Open();


                // string req = string.Format("insert into etudiant values('{0}','{1}','{2}')", textBox2.Text, textBox3.Text, textBox4.Text);
                string req = "insert into Etudiant values( '" + textBox2.Text + "' , '" + textBox3.Text + "' ,  '"+ dateTimePicker1.Text + "')";
                cmd = new SqlCommand(req, cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bien Ajouter");
                cnx.Close();
                vider();
                affiche_table();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void button2_supprimer_Click(object sender, EventArgs e)
        {
                cnx.Open();
                string req = " Delete from Etudiant where num_etu=" + textBox1.Text + "";
                cmd = new SqlCommand(req, cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bien Supprimé");
                cnx.Close();
                vider();
                affiche_table();
            
            
        }

        private void button3_modifier_Click(object sender, EventArgs e)
        {
            cnx.Open();
            string req = "update  Etudiant set   nom_etu ='" + textBox2.Text + "' ,prenom_etu='" + textBox3.Text + "' ,date_naissance='" + dateTimePicker1.Text + "'  where num_etu=" + textBox1.Text + "";
            SqlCommand cmd = new SqlCommand(req, cnx);
            cmd.ExecuteNonQuery();
            MessageBox.Show("bien modifié");
            cnx.Close();
            vider();
            affiche_table();
        }

        private void Form3_Etudiant_Load(object sender, EventArgs e)
        {
            affiche_table();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_quitter_Click(object sender, EventArgs e)
        {
            vider();
            
        }

        private void gestionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        private void etudiantToolStripMenuItem_Click_1(object sender, EventArgs e)
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
           Gestion_notes ma = new Gestion_notes();
            ma.Show();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void consultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Consultation ma = new Consultation();
            ma.Show();
        }

        

        
    }
}
