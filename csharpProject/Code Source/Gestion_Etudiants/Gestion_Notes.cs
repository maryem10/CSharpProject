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
    public partial class Gestion_notes : Form
    {
        public Gestion_notes()
        {
            InitializeComponent();
        }


        SqlConnection cnx = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader rd;//classe qui nous permet de recuperer les donnes de dbase de donnees 
        private void button_nouveau_Click(object sender, EventArgs e)
        {
            comboins.SelectedIndex = -1; //zone liste numéro inscription
            combomodule.SelectedIndex = -1; //zone liste numéro module
            textnom.Text = ""; // zone nom et prénom étudiant
            textnote.Text = ""; //zone note étudiant
            liste.DataSource = null; //zone liste d'affichage des notes
            
        }

        private float Rechercher_note_Etudiant(int numins,int nummod) //fonction qui permet de retourner la note d'un étudiant donné dans un module donné
        {
            float note = -1;
             cnx.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection =  cnx;
            cmd.CommandText = "select note from note where num_etu=" + numins + " and num_module=" + nummod;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                note = float.Parse(dr[0].ToString());
            }
            dr.Close();
             cnx.Close();
            return note;
        }
        private void affiche_table()
        {
            cnx.Open();
            cmd = new SqlCommand("select m.nom_module,n.note,e.nom_etu,e.prenom_etu from note n inner join MOdule m on n.num_module=m.num_module inner join Etudiant e  on n.num_etu=e.num_etu ", cnx);
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rd);
            liste.DataSource = dt;
            rd.Close();
            cnx.Close();

        }
        private int num_module(string nom) //fonction qui permet de retourner le numéro d'un module à partir de son nom
        {
            int num=-1;
             cnx.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection =  cnx;
            cmd.CommandText = "select num_module from Module where nom_module='" + nom + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                num = int.Parse(dr[0].ToString());
            }
            dr.Close();
             cnx.Close();
            return num;

        }
        private void vider()
        {

            cnx.Open();
            textnom.Text = "";
            textnote.Text = "";
            comboins.SelectedIndex = -1;
            combomodule.SelectedIndex = -1;
           
            cnx.Close();
        }

        private void Gestion_notes_Load(object sender, EventArgs e)
        {
            affiche_table();
            combomodule.AutoCompleteMode = AutoCompleteMode.SuggestAppend; //autocomplétion des listes pour faciliter la saisie
            combomodule.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboins.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboins.AutoCompleteSource = AutoCompleteSource.ListItems;
           
            
            try   // chargerment des listes étudiants et modules
            {
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = "select num_etu from Etudiant";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboins.Items.Add(dr[0]);
                }
                dr.Close();
                cmd.CommandText = "select num_module from Module";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    combomodule.Items.Add(dr[0]);
                }
                dr.Close();
                 cnx.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void comboins_SelectedIndexChanged(object sender, EventArgs e)
        {
            liste.DataSource = null;
            textnote.Text = "";
            if (comboins.SelectedIndex != -1) //chargement du nom et prénom de l'étudiant selectionné
            {
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = "select nom_etu,prenom_etu from Etudiant where num_etu=" + int.Parse(comboins.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    textnom.Text = dr[0] + " " + dr[1];
                }
                dr.Close();
                 cnx.Close();


                if (combomodule.SelectedIndex != -1)    // chargement de la note de l'étudiant selectionné dans le module selectionné
                {
                    int nummod = num_module(combomodule.SelectedItem.ToString());
                    float note = Rechercher_note_Etudiant(int.Parse(comboins.SelectedItem.ToString()), nummod);
                    if (note != -1)
                    {
                        textnote.Text = note.ToString();
                    }
                }
            }
        }

        private void combomodule_SelectedIndexChanged(object sender, EventArgs e)
        {
            textnote.Text = "";  //  chargement de la note de l'étudiant selectionné dans le module selectionné
            if (comboins.SelectedIndex != -1) //chargement du nom et prénom de l'étudiant selectionné
            {
                cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select nom_etu,prenom_etu from Etudiant where num_etu=" + int.Parse(comboins.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    textnom.Text = dr[0] + " " + dr[1];
                }
                dr.Close();
                cnx.Close();


                if (comboins.SelectedIndex != -1)
                {
                    int nummod = num_module(combomodule.SelectedItem.ToString());
                    float note = Rechercher_note_Etudiant(int.Parse(comboins.SelectedItem.ToString()), nummod);
                    if (note != -1)
                    {
                        textnote.Text = note.ToString();
                    }
                }
            }

        }

        private void button_enregistrer_Click(object sender, EventArgs e)
        {
            int note;
            int etu = int.Parse(comboins.SelectedItem.ToString());
                int nummod=num_module(combomodule.SelectedItem.ToString());


                cnx.Open();
                // string req = string.Format("insert into etudiant values('{0}','{1}','{2}')", textBox2.Text, textBox3.Text, textBox4.Text);
                string req = "insert into note values(" + comboins.SelectedItem.ToString() + "," + combomodule.SelectedItem.ToString() + "," + textnote.Text + ")";
                cmd = new SqlCommand(req, cnx);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bien Ajouter");
                cnx.Close();
                vider();
                affiche_table(); 
        }


        private void button_modifier_Click(object sender, EventArgs e)
        {
               
                int nummod = num_module(combomodule.SelectedItem.ToString());
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = String.Format("Update note set note=" + textnote.Text  + " where num_etu=" + comboins.SelectedItem.ToString() + " and num_module=" + combomodule.SelectedItem.ToString() + "");
                int r = cmd.ExecuteNonQuery(); // ou ecrire cmd.ExecuteNonQuery() sans retour
                if (r != 0)
                {
                    MessageBox.Show("note bien modifiée", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     cnx.Close();
                     vider();
                     affiche_table(); 
                }

            

        }

        private void button_apercu_Click(object sender, EventArgs e)
        {
            if (comboins.SelectedIndex != -1)   //charger la liste d'affichage des notes
            {
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = "select m.nom_module,n.note,e.nom_etu,e.prenom_etu from note n inner join MOdule m on n.num_module=m.num_module inner join Etudiant e  on n.num_etu=e.num_etu where e.num_etu=" + int.Parse(comboins.SelectedItem.ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable t = new DataTable();
                t.Load(dr);
                liste.DataSource = null;
                liste.DataSource = t;
                dr.Close();
                 cnx.Close();
            }
        }

        private void button_quitter_Click(object sender, EventArgs e)
        {
            vider();
        }

        private void textnote_TextChanged(object sender, EventArgs e)
        {

        }

        private void textnom_TextChanged(object sender, EventArgs e)
        {

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
           Gestion_notes ma = new Gestion_notes();
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

        private void liste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textnom_TextChanged_1(object sender, EventArgs e)
        {

        }

        

        
    }
}

