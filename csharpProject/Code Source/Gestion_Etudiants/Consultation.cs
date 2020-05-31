

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
    public partial class Consultation : Form
    {
        public Consultation()
        {
            InitializeComponent();
        }
        SqlConnection cnx= new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader rd;//classe qui nous permet de recuperer les donnes de dbase de donnees 

        private int num_module(string nom)   //fonction qui permet de retourner le numéro d'un module à partir de son nom
        {
            int num = -1;
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

        private void Consultation_Load(object sender, EventArgs e)
        {
            //chargement de la liste par les noms des modules
            combomodule.DropDownStyle = ComboBoxStyle.DropDownList; //permettre la sélection seulement
            try
            {
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = "select nom_module from Module";
                SqlDataReader dr = cmd.ExecuteReader();
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

        private void combomodule_SelectedIndexChanged(object sender, EventArgs e)
        {
            //charger la liste et afficher la moyenne


            if (combomodule.SelectedIndex != -1)
            {
                int num = num_module(combomodule.SelectedItem.ToString());
                 cnx.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection =  cnx;
                cmd.CommandText = "select e.num_etu,e.nom_etu,n.Note from note n inner join Etudiant e on n.num_etu=e.num_etu where n.num_module=" + num + " order by n.Note desc";
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable t = new DataTable();
                t.Load(dr);
                liste.DataSource = null;
                liste.DataSource = t;
                dr.Close();
                cmd.CommandText = "select avg(Note) from note where num_module=" + num;
                float moy = float.Parse(cmd.ExecuteScalar().ToString());
                textmoy.Text = moy.ToString();
                 cnx.Close();
            }
        }

        

        private void moduleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            module ma = new module();
            ma.Show();

        }

        private void noteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
         this.Hide();
         Gestion_notes ma = new Gestion_notes();
          ma.Show();
        }

        private void quitterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void etudiantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3_Etudiant ma = new Form3_Etudiant();
            ma.Show();

        }

        private void textmoy_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
