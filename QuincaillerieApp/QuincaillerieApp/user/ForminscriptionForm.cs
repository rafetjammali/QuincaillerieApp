using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp
{
    public partial class ForminscriptionForm : Form
    {
        public ForminscriptionForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        //methode pour vider tous les champs
        private void Vider()
        {
            iDTextBox.Text = null;
            PASSWORDTextBox.Text = null;
            NAMETextBox.Text = null;
            lastnametextbox.Text = null;
        }

        //methode prive pour ajouter les donne a la base
        private void InsertData()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=COMPAQ-PC\SQLEXPRESS;Initial Catalog=application;Integrated Security=True");
            try
            {
                SqlCommand query = new SqlCommand("Insert into utilisateur Values(@Login,@password,@nomutilisateur,@prenomutilisateur)", connection);
                query.Parameters.AddWithValue("@Login", iDTextBox.Text.Trim().ToLower());
                query.Parameters.AddWithValue("@password", PASSWORDTextBox.Text.Trim().ToLower());
                query.Parameters.AddWithValue("@nomutilisateur", NAMETextBox.Text.Trim().ToLower());
                query.Parameters.AddWithValue("@prenomutilisateur", lastnametextbox.Text.Trim().ToLower());
                connection.Open();
                query.ExecuteNonQuery();
                MessageBox.Show("Utilisateur: " + iDTextBox.Text + " est ajouté à la base de donnée tu peut login maintenent");
            }
            catch 
            {
                MessageBox.Show("Voulez vous Verifie votre Nom d'utilisateur");
            }
            finally
            {
                connection.Close();
                Vider();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //test pour verifier si un des textbox est vide
            if(iDTextBox.Text=="" || PASSWORDTextBox.Text =="" ||NAMETextBox.Text==""|| lastnametextbox.Text=="" )
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else
            {
                InsertData();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vider();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void ForminscriptionForm_Load(object sender, EventArgs e)
        {
            Vider();
        }
    }
}
