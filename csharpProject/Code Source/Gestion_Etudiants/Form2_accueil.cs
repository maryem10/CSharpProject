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
    public partial class Form2_accueil : Form
    {
        public Form2_accueil()
        {
            InitializeComponent();
        }
        SqlConnection cnx = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=gestion_etudiant;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader rd;//classe qui nous permet de recuperer les donnes de dbase de donnees 
        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gestion_notes ma = new Gestion_notes();
            ma.Show();
        }

        private void consultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Consultation ma = new Consultation();
            ma.Show();
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

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
            this.Close();
        }

        private void Form2_accueil_Load(object sender, EventArgs e)
        {
            chart1.Series[0].XValueMember = "gender";
            chart1.Series[0].YValueMembers = "employeeid";
            this.chart1.Series[0].Points.AddXY("C++", "13.5");
            this.chart1.Series[0].Points.AddXY("JAVA", "16");
            this.chart1.Series[0].Points.AddXY("Algorithme", "15");
            this.chart1.Series[0].Points.AddXY("Mathematique Applique", "17.5");
            
            cnx.Open();

               SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM Etudiant", cnx);
                Int32 count = (Int32)comm.ExecuteScalar();

                st_etudiant.Text = count.ToString();

                comm = new SqlCommand("SELECT count(*) FROM Module", cnx);
                count = (Int32)comm.ExecuteScalar();
                
               
    

                st_module.Text = count.ToString();
                cnx.Close();



              
	      
        }

        private void pictureBox1_Click(object sender, EventArgs e){
        

        }

        private void label1_Click()
        {
        
        }

        private void bindingNavigator1_RefreshItems()
        {
        
        }

        private void pictureBox6_Click()
        {
        
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
