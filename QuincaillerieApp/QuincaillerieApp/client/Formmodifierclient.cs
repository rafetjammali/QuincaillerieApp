using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp.client
{
    public partial class Formmodifierclient : Form
    {
        public Formmodifierclient()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }

        //update function
        private void updatedata(string text)
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                SqlCommand query = new SqlCommand(text, connection);
                connection.Open();
                query.ExecuteNonQuery();
                MessageBox.Show("Modification a ete termine");
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }
        }

        //select function
        private void getdata(string text)
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                //read from the data base
                SqlDataAdapter query = new SqlDataAdapter(text, connection);
                DataTable dataTable = new DataTable();
                query.Fill(dataTable);
                DataGridView.Visible = true;
                DataGridView.DataSource = dataTable;
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text=="")
            {
                MessageBox.Show("La boite de recherche est vide");
            }
            else
            {
                string text = "Select nomclient,prenomclient,numero,adresse,ville,gouvernement from client where idclient='" + txtSearch.Text.Trim() + "'";
                getdata(text);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            txtSearch.Text = null;
        }

        private void btnAnnulerTous_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("La boite de modification du Nom est vide");
            }
            else
            {
                string text = "Update client set nomclient = '" + textBox1.Text.Trim().ToLower() + "' where idclient='" + txtSearch.Text.Trim().ToLower() + "'";
                updatedata(text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("La boite de modification du Prenom est vide");
            }
            else
            {
                string text = "Update client set prenomclient = '" + textBox2.Text.Trim().ToLower() + "' where idclient='" + txtSearch.Text.Trim().ToLower() + "'";
                updatedata(text);
            }
        }
    }
}
